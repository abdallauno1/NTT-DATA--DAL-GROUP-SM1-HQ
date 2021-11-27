      //get final residual amount...
        private decimal CalculateFinalResidualAmount(XConnection connection, SM1Order order, SessionContext sc)
        {
            if (order == null)
                return 0m;

            if (order.MacroType != OrderMacroType.SALES)
                return 0m;

            //should be loaded by OnEvalAnomalies
            if (order.InvoiceCustomer == null)
                throw new XtelException(XApp.Messages().TryTranslateWithPar(sc.CodLng, MESSAGES.ERROR_CUSTOMER_NOT_FOUND));

            CustomerDiv cDiv = order.InvoiceCustomer.CustomerDivDetails.FirstOrDefault(cd => cd.CODDIV == order.CODDIV);
            CustomerAmount cAmount = (cDiv == null || !cDiv.CustomerAmountDetails.Any()) ? null : cDiv.CustomerAmountDetails.First();

            if (cAmount == null)
                return 0m;

            DecodeTable cptrm = XApp.Decodes().DecodeTableTrap(order.CODDIV, "CPTRM");

            DecodeEntry de = (!string.IsNullOrEmpty(order.CODPAYTRM) && cptrm != null) ?
                cptrm.DecodeRowTrap(order.CODPAYTRM) : null;

            if (de != null)
            {
                string numDays = de.GetStringValue(QTABS.VALUES.REFDAT_CPTRM.NUMDAYS);
                if (!string.IsNullOrEmpty(numDays))
                {
                    int days;
                    if (Int32.TryParse(numDays, out days) && days == 0)
                        return 0m;
                }
            }

            decimal residualAmount = cAmount == null ? 0 : cAmount.VALCREDIT - cAmount.VALEXPOSED;
            decimal paidAmount = new DepositEngine().CalculatePaidAmount(connection, order.CODCUSTINV, order.CODDIV, sc);
            decimal orderedAmount = CalculateOrderedAmount_cust(connection, order, sc);

            if (
                (order.CODSTATUS != SM1OrderStatus.VALIDO
                 && order.CODSTATUS != SM1OrderStatus.BLOCCATO
                 && order.CODSTATUS != SM1OrderStatus.CLOSED
                 && order.CODSTATUS != SM1OrderStatus.INVOICED
                  //CR04 aggiungo il order type 80 _CR04 MA_20181029
                  && order.CODTYPORD != "80"
                )
                || !DateHelper.IsEmptyDate(order.DTETOHOST)
                )
            {
                orderedAmount -= order.TOTALPAY;
            }



            //Aggiungo il typo ordine 80 con lo stato draft nel credit limite -->  CR04 CREDITE LIMITE CALCULATION  MADY 
            if (order.CODSTATUS == SM1OrderStatus.SOSPESO && order.CODTYPORD == "80")
            {
                orderedAmount += order.TOTALPAY;
            }




            return residualAmount + paidAmount - orderedAmount;
        }
        //DAL CUSTOMIZATION CR-4 - 20180305 - MA: AGGIUNTI ANCHE I CODTYPORD  DELIVERD E BOZZA(SOSPESO) , ESCLUSI LE FATTURE CON PAYMENTMODE = CASH..
        //calculate the order amount dynamnic calc & effect the residual amount credit...
        private decimal CalculateOrderedAmount_cust(XConnection connection, SM1Order order, SessionContext sc)
        {
            List<string> statuses = new List<string>() { SM1OrderStatus.VALIDO, SM1OrderStatus.BLOCCATO, SM1OrderStatus.CLOSED, SM1OrderStatus.INVOICED };
            List<string> darftStatus = new List<string>() { SM1OrderStatus.SOSPESO };
            List<string> statuses_2 = new List<string>() { SM1OrderStatus.VALIDO, SM1OrderStatus.BLOCCATO, SM1OrderStatus.CLOSED, SM1OrderStatus.INVOICED, SM1OrderStatus.DELIVERED };
            List<string> codTypeOrders = new List<string>() { "0", "70", "Z75" };

            //determine sales order types
            List<string> orderTypes = new List<string>();
            DecodeTable ctord = XApp.Decodes().DecodeTableTrap(order.CODDIV, QTABS.TABLES.CTORD);

            if (ctord != null)
            {
                orderTypes.AddRange(ctord
                    .Select(de => de.CodTabRow)
                    .Where(codTypOrd => SM1OrderHelper.GetOrderMacroType(order.CODDIV, codTypOrd) == OrderMacroType.SALES));
            }

            SqlSelect zPresalesOrdersDraft = new SqlSelect() { UseModel = true };

            zPresalesOrdersDraft.SelectFields.AddFunction("SUM", "DRAFT", "SM1Order", "Z_PRESALES_ORD_AMOUNT");
            zPresalesOrdersDraft.Constraints.AddConstraint("SM1Order", "CODDIV", SqlRelationalOperator.Equal, order.CODDIV);
            zPresalesOrdersDraft.Constraints.AddConstraint("SM1Order", "CODCUSTINV", SqlRelationalOperator.Equal, order.CODCUSTINV);
            zPresalesOrdersDraft.Constraints.AddConstraint("SM1Order", "CODPAYMOD", SqlRelationalOperator.NotEqual, "CS");
            zPresalesOrdersDraft.Constraints.AddConstraint("SM1Order", "CODSTATUS", SqlRelationalOperator.In, darftStatus);
            zPresalesOrdersDraft.Constraints.AddConstraint("SM1Order", "CODTYPORD", SqlRelationalOperator.In, "80");
            //the order is not yet sent to ERP (DTETOHOST is empty)
            zPresalesOrdersDraft.Constraints.AddConstraint(new SqlConstraints(SqlLogicalOperator.Or)
                .AddConstraint("SM1Order", "DTETOHOST", SqlRelationalOperator.IsNull, null)
                .AddConstraint("SM1Order", "DTETOHOST", SqlRelationalOperator.Equal, DateHelper.MinSM1Date));
            //exclude current order while reading from db, its amount is calculated from in memory obj
            zPresalesOrdersDraft.Constraints.AddConstraint("SM1Order", "DOCUMENTKEY", SqlRelationalOperator.NotEqual, order.DOCUMENTKEY);


            SqlSelect zPresalesOrders = new SqlSelect() { UseModel = true };

            zPresalesOrders.SelectFields.AddFunction("SUM", "CONFIRMED", "SM1Order", "NETAMOUNT+TAXAMOUNT+VATAMOUNT");
            zPresalesOrders.Constraints.AddConstraint("SM1Order", "CODDIV", SqlRelationalOperator.Equal, order.CODDIV);
            zPresalesOrders.Constraints.AddConstraint("SM1Order", "CODCUSTINV", SqlRelationalOperator.Equal, order.CODCUSTINV);
            zPresalesOrders.Constraints.AddConstraint("SM1Order", "CODPAYMOD", SqlRelationalOperator.NotEqual, "CS");
            zPresalesOrders.Constraints.AddConstraint("SM1Order", "CODSTATUS", SqlRelationalOperator.In, statuses_2);
            zPresalesOrders.Constraints.AddConstraint("SM1Order", "CODTYPORD", SqlRelationalOperator.In, "80");
            //the order is not yet sent to ERP (DTETOHOST is empty)
            zPresalesOrders.Constraints.AddConstraint(new SqlConstraints(SqlLogicalOperator.Or)
                .AddConstraint("SM1Order", "DTETOHOST", SqlRelationalOperator.IsNull, null)
                .AddConstraint("SM1Order", "DTETOHOST", SqlRelationalOperator.Equal, DateHelper.MinSM1Date));
            //exclude current order while reading from db, its amount is calculated from in memory obj
            zPresalesOrders.Constraints.AddConstraint("SM1Order", "DOCUMENTKEY", SqlRelationalOperator.NotEqual, order.DOCUMENTKEY);


            SqlSelect select = new SqlSelect() { UseModel = true };

            select.SelectFields.AddFunction("SUM", "TOTALPAY", "SM1Order", "NETAMOUNT+TAXAMOUNT+VATAMOUNT");
            select.Constraints.AddConstraint("SM1Order", "CODDIV", SqlRelationalOperator.Equal, order.CODDIV);
            select.Constraints.AddConstraint("SM1Order", "CODCUSTINV", SqlRelationalOperator.Equal, order.CODCUSTINV);
            select.Constraints.AddConstraint("SM1Order", "CODSTATUS", SqlRelationalOperator.In, statuses);
            select.Constraints.AddConstraint("SM1Order", "CODTYPORD", SqlRelationalOperator.In, codTypeOrders);
            select.Constraints.AddConstraint("SM1Order", "CODPAYMOD", SqlRelationalOperator.NotEqual, "CS");
            //the order is not yet sent to ERP (DTETOHOST is empty)
            select.Constraints.AddConstraint(new SqlConstraints(SqlLogicalOperator.Or)
                .AddConstraint("SM1Order", "DTETOHOST", SqlRelationalOperator.IsNull, null)
                .AddConstraint("SM1Order", "DTETOHOST", SqlRelationalOperator.Equal, DateHelper.MinSM1Date));
            //exclude current order while reading from db, its amount is calculated from in memory obj
            select.Constraints.AddConstraint("SM1Order", "DOCUMENTKEY", SqlRelationalOperator.NotEqual, order.DOCUMENTKEY);


            var resultPreselesDraft = zPresalesOrdersDraft.ExecuteScalar(connection);
            var a = (resultPreselesDraft == DBNull.Value ? 0 : Convert.ToDecimal(resultPreselesDraft));

            var resultPresalesOrders = zPresalesOrders.ExecuteScalar(connection);
            var b = (resultPresalesOrders == DBNull.Value ? 0 : Convert.ToDecimal(resultPresalesOrders));


            var resultSelect = select.ExecuteScalar(connection);
            var c = (resultSelect == DBNull.Value ? 0 : Convert.ToDecimal(resultSelect));


            var result = Convert.ToDecimal(a) + Convert.ToDecimal(b) + Convert.ToDecimal(c);

            //make sure that all current amounts are calculated correctly
            order.CalculateBenefits();
            decimal ordAmount;
            //controllo se l'ordine tipo 80 and draft status // MA 20181028 CR04
            if (order.CODTYPORD == "80" && order.CODSTATUS == SM1OrderStatus.SOSPESO)
            {

                ordAmount = Convert.ToDecimal(result) - order.TOTALPAY;
            }
            else
            {

                ordAmount = order.TOTALPAY + Convert.ToDecimal(result);

            }

            return ordAmount;
        }

