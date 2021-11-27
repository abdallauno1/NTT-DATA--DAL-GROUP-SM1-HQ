using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Web;
using System.Globalization;
using Xtel.SM1.Core.Data;
using Xtel.SM1.Core;
using Xtel.SM1.Core.Data.QueryObj;
using Xtel.SM1.SalesForce;
using Xtel.SM1.Common;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Web;







/// <summary>
/// Data model for order reports
/// if you want to add a new report, create a new namespace like Order_macrotype_x or Order_type_x and inside it create all the classes that you need
/// in each namespace, the main class should extend OrderReportArgs
/// the order report template (.rpt) has to have the same name as the namespace. Or otherwise, you have to override the method GetTemplateName
/// </summary>
namespace Xtel.SM1.SalesForce.OrderReportDataSource
{
    /// <summary>
    /// Data model for the orders with macrotype == 0
    /// </summary>
    namespace OrderReport_macrotype_0_Cust
    {
        public class OrderBenefitInfo
        {


            public decimal QtyBen { get; private set; }

            public string CodSrc { get; private set; }
            public string DesSrc { get; private set; }

            public string CodTypBen { get; private set; }
            public string DesTypBen { get; private set; }

            public string CodBenCause { get; private set; }
            public string DesBenCause { get; private set; }

            public OrderBenefitInfo(string codDiv, decimal qtyBen, string codSrc, string codTypBen, string codBenCause)
            {
                QtyBen = qtyBen;

                CodSrc = codSrc;
                DesSrc = OrderReportEngine.DecodeQtab(codDiv, "BESRC", codSrc);

                CodTypBen = codTypBen;
                DesTypBen = OrderReportEngine.DecodeQtab(codDiv, "BETYP", codTypBen);

                CodBenCause = codBenCause;
                DesBenCause = OrderReportEngine.DecodeQtab(codDiv, "CCAU1", codBenCause);
            }
        }

        public class OrderRowInfo
        {
            public int NumRow { get; private set; }
            public string CodTypRow { get; private set; }
            public string CodArt { get; private set; }
            public string CodArtCust { get; private set; }
            public string DesArt { get; private set; }
            public string NumInv { get; private set; }
            public DateTime DteInv { get; private set; }
            public string QtyOrd { get; private set; }
            public string UmOrd { get; private set; }
            public string QtyInv { get; private set; }
            public string UmInv { get; private set; }
            public string QtyAnn { get; private set; }
            public string GrossArtAmount { get; private set; }
            public string GrossArtAmountUmOrd { get; private set; }
            public string NetArtAmount { get; private set; }
            public string NetArtAmountUmOrd { get; private set; }
            public string NetAmount { get; private set; }

            //benefit 1
            public string ValBen1 { get; private set; }
            public string CodTypBen1 { get; private set; }
            public string CodSrc1 { get; private set; }
            public string CodSrcRef1 { get; private set; }
            //benefit 2
            public string ValBen2 { get; private set; }
            public string CodTypBen2 { get; private set; }
            public string CodSrc2 { get; private set; }
            public string CodSrcRef2 { get; private set; }
            //benefit 3
            public string ValBen3 { get; private set; }
            public string CodTypBen3 { get; private set; }
            public string CodSrc3 { get; private set; }
            public string CodSrcRef3 { get; private set; }
            //benefit 4
            public string ValBen4 { get; private set; }
            public string CodTypBen4 { get; private set; }
            public string CodSrc4 { get; private set; }
            public string CodSrcRef4 { get; private set; }
            //benefit 5
            public string ValBen5 { get; private set; }
            public string CodTypBen5 { get; private set; }
            public string CodSrc5 { get; private set; }
            public string CodSrcRef5 { get; private set; }
            //benefit 6
            public string ValBen6 { get; private set; }
            public string CodTypBen6 { get; private set; }
            public string CodSrc6 { get; private set; }
            public string CodSrcRef6 { get; private set; }

            public OrderRowInfo(string codDiv, int numRow, string codTypRow, string codArt, string codArtCust, string desArt, string numInv, DateTime dteInv, decimal qtyOrd,
                string umOrd, decimal qtyInv, string umInv, decimal qtyAnn, decimal grossArtAmount, decimal grossArtAmountUmOrd, decimal netArtAmount, decimal netArtAmountUmOrd, decimal netAmount,
                decimal valBen1, string codTypBen1, string codSrc1, string codSrcRef1)
            {
                NumRow = numRow;
                CodTypRow = OrderReportEngine.DecodeQtab(codDiv, "TYROW", codTypRow);
                CodArt = codArt;
                CodArtCust = codArtCust;
                DesArt = desArt;
                NumInv = numInv;
                DteInv = dteInv;
                QtyOrd = qtyOrd.ToString();
                UmOrd = umOrd;
                QtyInv = qtyInv.ToString();
                UmInv = umInv;
                QtyAnn = qtyAnn.ToString();
                GrossArtAmount = grossArtAmount.ToString();
                GrossArtAmountUmOrd = grossArtAmountUmOrd.ToString();
                NetArtAmount = netArtAmount.ToString("N");
                NetArtAmountUmOrd = netArtAmountUmOrd.ToString("N");
                NetAmount = netAmount.ToString("N");
                //benefit 1
                ValBen1 = valBen1.ToString();
                CodTypBen1 = OrderReportEngine.DecodeQtab(codDiv, "BETYP", codTypBen1);
                CodSrc1 = OrderReportEngine.DecodeQtab(codDiv, "BESRC", codSrc1);
                CodSrcRef1 = codSrcRef1;
                //init benefit fields with default values
                //benefit 2
                CodTypBen2 = CodSrc2 = " - ";
                //benefit 3
                CodTypBen3 = CodSrc3 = " - ";
                //benefit 4
                CodTypBen4 = CodSrc4 = " - ";
                //benefit 5
                CodTypBen5 = CodSrc5 = " - ";
                //benefit 6
                CodTypBen6 = CodSrc6 = " - ";
            }

            public void AddBenefit(int numBenefit, string codDiv, decimal valBen, string codTypBen, string codSrc, string codSrcRef)
            {
                codTypBen = OrderReportEngine.DecodeQtab(codDiv, "BETYP", codTypBen);
                codSrc = OrderReportEngine.DecodeQtab(codDiv, "BESRC", codSrc);
                switch (numBenefit)
                {
                    case 2:
                        ValBen2 = valBen.ToString();
                        CodTypBen2 = codTypBen;
                        CodSrc2 = codSrc;
                        CodSrcRef2 = codSrcRef;
                        break;
                    case 3:
                        ValBen3 = valBen.ToString();
                        CodTypBen3 = codTypBen;
                        CodSrc3 = codSrc;
                        CodSrcRef3 = codSrcRef;
                        break;
                    case 4:
                        ValBen4 = valBen.ToString();
                        CodTypBen4 = codTypBen;
                        CodSrc4 = codSrc;
                        CodSrcRef4 = codSrcRef;
                        break;
                    case 5:
                        ValBen5 = valBen.ToString();
                        CodTypBen5 = codTypBen;
                        CodSrc5 = codSrc;
                        CodSrcRef5 = codSrcRef;
                        break;
                    case 6:
                        ValBen6 = valBen.ToString();
                        CodTypBen6 = codTypBen;
                        CodSrc6 = codSrc;
                        CodSrcRef6 = codSrcRef;
                        break;
                }
            }
        }

        public class OrderNoteInfo
        {
            public string NoteType { get; private set; }
            public string Note { get; private set; }

            public OrderNoteInfo(string codDiv, string noteType, string note)
            {
                Note = note;
                NoteType = OrderReportEngine.DecodeQtab(codDiv, "NOTES|CNOTO", noteType);
            }
        }

        public class OrderInfo : OrderReportArgs
        {
            private Collection<OrderBenefitInfo> m_orderBenefits = new Collection<OrderBenefitInfo>();
            private Collection<OrderRowInfo> m_orderRows = new Collection<OrderRowInfo>();
            private Collection<OrderNoteInfo> m_orderNotes = new Collection<OrderNoteInfo>();

            public string DocumentKey { get; protected set; }
            public string NumOrdCust { get; protected set; }
            public DateTime DteOrdCust { get; protected set; }
            public string DesDiv { get; protected set; }

            public string CodeUsr { get; protected set; }
            public string CodeUsrDes { get; protected set; }

            public string CodCustDeliv { get; protected set; }
            public string CodCustInv { get; protected set; }
            public string DesParty1 { get; protected set; }

            public string DesAddr1 { get; protected set; }
            public string CodZip { get; protected set; }
            public string DesLoc1 { get; protected set; }
            public string CodPrv { get; protected set; }

            public DateTime DteOrd { get; protected set; }

            public string CodNode2 { get; protected set; }
            public string CodNode2Des { get; protected set; }
            public string CodNode3 { get; protected set; }
            public string CodNode3Des { get; protected set; }

            public string CodModDeliv { get; protected set; }
            public string CodModDelivDes { get; protected set; }

            public string CodShipper { get; protected set; }
            public string CodShipperDes { get; protected set; }

            public decimal QtyTot { get; protected set; }
            public string UmQtyTot { get; protected set; }

            public string CodStatus { get; protected set; }
            public string CodStatusDes { get; protected set; }

            public string CodCatDiv2 { get; protected set; }
            public string CodCatDiv2Des { get; protected set; }

            public string CodVat { get; protected set; }
            public string CodIban { get; protected set; }

            public string CodPayTrm { get; protected set; }
            public string CodPayTrmDes { get; protected set; }

            public string CodCur { get; protected set; }
            public string CodCurDes { get; protected set; }

            public decimal NetAmount { get; protected set; }
            public decimal TaxAmount { get; protected set; }
            public decimal VatAmount { get; protected set; }

            public OrderInfo(OrderReportArgs order, SessionContext sc)
                : base(order)
            {
                if (!OrderReportEngine.ExistsOrderTemplate(this))
                    return;

                using (XConnection connection = XApp.CreateDefaultXConnection())
                {
                    connection.Open();

                    try
                    {
                        InitOrderData(connection);
                        InitHierData(connection);
                        InitOrderBenefits(connection);
                        InitOrderRows(connection);
                        InitOrderNotes(connection);
                    }
                    catch { }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            private void InitOrderData(XConnection connection)
            {
                //query the database for the order info, customer info, etc
                SqlSelect mainSelect = new SqlSelect();
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "DOCUMENTKEY");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "NUMORDCUST");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "DTEORDCUST");
                mainSelect.SelectFields.AddTableField("T032DIVISION", "DESDIV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODEUSR");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODCUSTDELIV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODCUSTINV");
                mainSelect.SelectFields.AddTableField("T040PARTY", "DESPARTY1");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "DESADDR1");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "CODZIP");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "DESLOC1");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "CODPRV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "DTEORD");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODMODDELIV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODSHIPPER");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "QTYTOT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "UMQTYTOT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODSTATUS");
                mainSelect.SelectFields.AddTableField("T041PARTYDIV", "CODCATDIV2");
                mainSelect.SelectFields.AddTableField("T040PARTY", "CODVAT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODIBAN");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODPAYTRM");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODCUR");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "NETAMOUNT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "TAXAMOUNT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "VATAMOUNT");
                //constraints
                mainSelect.Constraints.AddConstraint("T100ORDHEAD", "NUMORD", SqlRelationalOperator.Equal, NumOrd);
                mainSelect.Constraints.AddConstraint("T100ORDHEAD", "CODUSR", SqlRelationalOperator.Equal, CodUsr);
                mainSelect.Constraints.AddConstraint("T100ORDHEAD", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODDIV" }, JoinOperator.Inner, "T032DIVISION", new[] { "CODDIV" });
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODCUSTDELIV" }, JoinOperator.Inner, "T040PARTY", new[] { "CODPARTY" });
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODCUSTDELIV" }, JoinOperator.Inner, "T042PARTYADDR", new[] { "CODPARTY" });
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODCUSTDELIV", "CODDIV" }, JoinOperator.Inner, "T041PARTYDIV", new[] { "CODPARTY", "CODDIV" });
                //init reader
                DbDataReader reader = mainSelect.ExecuteReader(connection);
                reader.Read();
                //read data
                if (reader.HasRows)
                {
                    DocumentKey = SqlHelper.NvlString(reader["DOCUMENTKEY"], string.Empty);
                    NumOrdCust = SqlHelper.NvlString(reader["NUMORDCUST"], string.Empty);
                    DteOrdCust = SqlHelper.NvlDateTime(reader["DTEORDCUST"], DateTime.Now);
                    DesDiv = SqlHelper.NvlString(reader["DESDIV"], string.Empty);

                    CodeUsr = SqlHelper.NvlString(reader["CODEUSR"], string.Empty);
                    CodeUsrDes = OrderReportEngine.DecodeQtab(CodDiv, "USR1", CodeUsr);

                    CodCustDeliv = SqlHelper.NvlString(reader["CODCUSTDELIV"], string.Empty);
                    CodCustInv = SqlHelper.NvlString(reader["CODCUSTINV"], string.Empty);
                    DesParty1 = SqlHelper.NvlString(reader["DESPARTY1"], string.Empty);
                    DesAddr1 = SqlHelper.NvlString(reader["DESADDR1"], string.Empty);
                    CodZip = SqlHelper.NvlString(reader["CODZIP"], string.Empty);
                    DesLoc1 = SqlHelper.NvlString(reader["DESLOC1"], string.Empty);
                    CodPrv = SqlHelper.NvlString(reader["CODPRV"], string.Empty);
                    DteOrd = SqlHelper.NvlDateTime(reader["DTEORD"], DateTime.Now);

                    CodModDeliv = SqlHelper.NvlString(reader["CODMODDELIV"], string.Empty);
                    CodModDelivDes = OrderReportEngine.DecodeQtab(CodDiv, "CDEL", CodModDeliv);

                    CodShipper = SqlHelper.NvlString(reader["CODSHIPPER"], string.Empty);
                    CodShipperDes = OrderReportEngine.DecodeQtab(CodDiv, "SHIPP", CodShipper);

                    QtyTot = SqlHelper.NvlDecimal(reader["QTYTOT"], 0);
                    UmQtyTot = SqlHelper.NvlString(reader["UMQTYTOT"], string.Empty);
                    UmQtyTot = OrderReportEngine.DecodeQtab(CodDiv, "UMART", UmQtyTot);

                    CodStatus = SqlHelper.NvlString(reader["CODSTATUS"], string.Empty);
                    CodStatusDes = OrderReportEngine.DecodeQtab(CodDiv, "ORDST", CodStatus);

                    CodCatDiv2 = SqlHelper.NvlString(reader["CODCATDIV2"], string.Empty);
                    CodCatDiv2Des = OrderReportEngine.DecodeQtab(CodDiv, "CTP2D", CodCatDiv2);

                    CodVat = SqlHelper.NvlString(reader["CODVAT"], string.Empty);
                    CodIban = SqlHelper.NvlString(reader["CODIBAN"], string.Empty);

                    CodPayTrm = SqlHelper.NvlString(reader["CODPAYTRM"], string.Empty);
                    CodPayTrmDes = OrderReportEngine.DecodeQtab(CodDiv, "CPTRM", CodPayTrm);

                    CodCur = SqlHelper.NvlString(reader["CODCUR"], string.Empty);
                    CodCurDes = OrderReportEngine.DecodeQtab(CodDiv, "CUR", CodCur);

                    NetAmount = SqlHelper.NvlDecimal(reader["NETAMOUNT"], 0);
                    TaxAmount = SqlHelper.NvlDecimal(reader["TAXAMOUNT"], 0);
                    VatAmount = SqlHelper.NvlDecimal(reader["VATAMOUNT"], 0);
                }
                reader.Close();
            }

            private void InitHierData(XConnection connection)
            {
                //query the database for the customer hier info
                SqlSelect hierSelect = new SqlSelect();
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "CODNODE3");
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "DESNODE3");
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "CODNODE2");
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "DESNODE2");

                //constraints
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "IDLEVEL", SqlRelationalOperator.Equal, -1);
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "CODHIER", SqlRelationalOperator.Equal, "COMM");
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "CODNODE", SqlRelationalOperator.Equal, CodCustDeliv);
                hierSelect.Constraints.AddConstraint(new SqlConstraint(new SqlOperandValue(DteOrd), SqlRelationalOperator.Between,
                    new SqlOperandField("TB0032HIERFLATDES_CUST", "DTESTART"), new SqlOperandField("TB0032HIERFLATDES_CUST", "DTEEND")));
                //init reader
                DbDataReader reader = hierSelect.ExecuteReader(connection);
                reader.Read();
                //read data
                if (reader.HasRows)
                {
                    CodNode2 = SqlHelper.NvlString(reader["CODNODE2"], string.Empty);
                    CodNode2Des = SqlHelper.NvlString(reader["DESNODE2"], string.Empty);
                    CodNode3 = SqlHelper.NvlString(reader["CODNODE3"], string.Empty);
                    CodNode3Des = SqlHelper.NvlString(reader["DESNODE3"], string.Empty);
                }
                reader.Close();
            }

            private void InitOrderBenefits(XConnection connection)
            {
                //build select
                SqlSelect benefitSelect = new SqlSelect();
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "QTYBEN");
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "CODSRC");
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "CODTYPBEN");
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "CODBENCAUSE");
                //constraints
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "NUMORD", SqlRelationalOperator.Equal, NumOrd);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "CODUSR", SqlRelationalOperator.Equal, CodUsr);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "NUMROW", SqlRelationalOperator.Equal, 0);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "CODTYPBEN", SqlRelationalOperator.NotIn, new string[] { "98", "99" });
                //order by
                benefitSelect.OrderBy.AddTableField("T101BENEFIT", "PRGAPPLY", Xtel.SM1.Core.Data.QueryObj.SortDirection.Asc);
                //init reader
                DbDataReader reader = benefitSelect.ExecuteReader(connection);
                //read data
                while (reader.Read())
                    m_orderBenefits.Add(new OrderBenefitInfo(CodDiv, SqlHelper.NvlDecimal(reader["QTYBEN"], 0), SqlHelper.NvlString(reader["CODSRC"], string.Empty),
                        SqlHelper.NvlString(reader["CODTYPBEN"], string.Empty), SqlHelper.NvlString(reader["CODBENCAUSE"], string.Empty)));

                reader.Close();
            }

            private void InitOrderRows(XConnection connection)
            {
                //query database for order benefits
                SqlSelect select = new SqlSelect();
                select.SelectFields.AddTableField("T106ORDROW", "NUMROW");
                select.SelectFields.AddTableField("T106ORDROW", "CODTYPROW");
                select.SelectFields.AddTableField("T106ORDROW", "CODART");
                select.SelectFields.AddTableField("T106ORDROW", "CODARTCUST");
                select.SelectFields.AddTableField("T106ORDROW", "NUMINV");
                select.SelectFields.AddTableField("T106ORDROW", "DTEINV");
                select.SelectFields.AddTableField("T106ORDROW", "QTYORD");
                select.SelectFields.AddTableField("T106ORDROW", "UMORD");
                select.SelectFields.AddTableField("T106ORDROW", "QTYINV");
                select.SelectFields.AddTableField("T106ORDROW", "UMINV");
                select.SelectFields.AddTableField("T106ORDROW", "QTYANN");
                select.SelectFields.AddTableField("T106ORDROW", "GROSSARTAMOUNT");
                select.SelectFields.AddTableField("T106ORDROW", "GROSSARTAMOUNTUMORD");
                select.SelectFields.AddTableField("T106ORDROW", "NETARTAMOUNT");
                select.SelectFields.AddTableField("T106ORDROW", "NETARTAMOUNTUMORD");
                select.SelectFields.AddTableField("T106ORDROW", "NETAMOUNT");
                select.SelectFields.AddTableField("T101BENEFIT", "QTYBEN");
                select.SelectFields.AddTableField("T101BENEFIT", "CODTYPBEN");
                select.SelectFields.AddTableField("T101BENEFIT", "CODSRC");
                select.SelectFields.AddTableField("T101BENEFIT", "CODSRCREF");
                select.SelectFields.AddTableField("T060ARTICLE", "DESART");
                //constraints
                SqlConstraints constraints = new SqlConstraints();
                select.Constraints.AddConstraint("T106ORDROW", "CODUSR", SqlRelationalOperator.Equal, CodUsr);
                select.Constraints.AddConstraint("T106ORDROW", "NUMORD", SqlRelationalOperator.Equal, NumOrd);
                select.Constraints.AddConstraint("T106ORDROW", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                //join with T101
                SqlConstraints constraints1 = new SqlConstraints();
                constraints1.AddConstraint("T106ORDROW", "NUMORD", SqlRelationalOperator.Equal, "T101BENEFIT", "NUMORD");
                constraints1.AddConstraint("T106ORDROW", "CODUSR", SqlRelationalOperator.Equal, "T101BENEFIT", "CODUSR");
                constraints1.AddConstraint("T106ORDROW", "NUMROW", SqlRelationalOperator.Equal, "T101BENEFIT", "NUMROW");
                constraints1.AddConstraint("T101BENEFIT", "CODTYPBEN", SqlRelationalOperator.NotIn, new string[] { "98", "99" });
                constraints1.AddConstraint("T101BENEFIT", "CODTEOBEN", SqlRelationalOperator.IsNull, null);
                select.Constraints.AddJoinConstraint("T106ORDROW", JoinOperator.Left, "T101BENEFIT", constraints1);
                //join with T060
                SqlConstraints constraints2 = new SqlConstraints();
                constraints2.AddConstraint("T106ORDROW", "CODART", SqlRelationalOperator.Equal, "T060ARTICLE", "CODART");
                constraints2.AddConstraint("T106ORDROW", "CODDIV", SqlRelationalOperator.Equal, "T060ARTICLE", "CODDIV");
                select.Constraints.AddJoinConstraint("T106ORDROW", JoinOperator.Left, "T060ARTICLE", constraints2);
                //order
                select.OrderBy.AddTableField("T106ORDROW", "NUMROW", Core.Data.QueryObj.SortDirection.Asc);
                select.OrderBy.AddTableField("T101BENEFIT", "PRGAPPLY", Core.Data.QueryObj.SortDirection.Asc);

                DbDataReader reader = select.ExecuteReader(connection);
                int currentNumRow = -1;
                int numBenefit = 0;

                while (reader.Read())
                {
                    int numrow = SqlHelper.NvlInt(reader["NUMROW"], 0);
                    decimal valBen = SqlHelper.NvlDecimal(reader["QTYBEN"], 0);
                    string codTypBen = SqlHelper.NvlString(reader["CODTYPBEN"], string.Empty);
                    string codSrc = SqlHelper.NvlString(reader["CODSRC"], string.Empty);
                    string codSrcRef = SqlHelper.NvlString(reader["CODSRCREF"], string.Empty);

                    if (currentNumRow != numrow)
                    {
                        currentNumRow = numrow;
                        numBenefit = 1;
                        decimal qtyOrd = SqlHelper.NvlDecimal(reader["QTYORD"], 0);
                        //add the new row to the collection and add the benefit read before
                        if (qtyOrd != 0)
                            m_orderRows.Add(new OrderRowInfo(CodDiv, currentNumRow, SqlHelper.NvlString(reader["CODTYPROW"], string.Empty), SqlHelper.NvlString(reader["CODART"], string.Empty),
                                SqlHelper.NvlString(reader["CODARTCUST"], string.Empty), SqlHelper.NvlString(reader["DESART"], string.Empty), SqlHelper.NvlString(reader["NUMINV"], string.Empty),
                                SqlHelper.NvlDateTime(reader["DTEINV"], DateTime.Now), qtyOrd, SqlHelper.NvlString(reader["UMORD"], string.Empty),
                                SqlHelper.NvlDecimal(reader["QTYINV"], 0), SqlHelper.NvlString(reader["UMINV"], string.Empty), SqlHelper.NvlDecimal(reader["QTYANN"], 0),
                                SqlHelper.NvlDecimal(reader["GROSSARTAMOUNT"], 0), SqlHelper.NvlDecimal(reader["GROSSARTAMOUNTUMORD"], 0),
                                SqlHelper.NvlDecimal(reader["NETARTAMOUNT"], 0), SqlHelper.NvlDecimal(reader["NETARTAMOUNTUMORD"], 0),
                                SqlHelper.NvlDecimal(reader["NETAMOUNT"], 0), valBen, codTypBen, codSrc, codSrcRef));
                    }
                    else
                    {
                        OrderRowInfo row = m_orderRows.Where(c => c.NumRow == currentNumRow).SingleOrDefault();
                        if (row != null)
                        {
                            numBenefit = numBenefit + 1;
                            //add the benefit to the current row
                            row.AddBenefit(numBenefit, CodDiv, valBen, codTypBen, codSrc, codSrcRef);
                        }
                    }
                }

                reader.Close();
            }

            private void InitOrderNotes(XConnection connection)
            {
                //build select
                SqlSelect noteSelect = new SqlSelect();
                noteSelect.SelectFields.AddTableField("TA4410NOTES", "NOTETYPE");
                noteSelect.SelectFields.AddTableField("TA4410NOTES", "NOTE");
                //constraints
                noteSelect.Constraints.AddConstraint("TA4410NOTES", "PARENTKEY", SqlRelationalOperator.Equal, DocumentKey);
                //init reader
                DbDataReader reader = noteSelect.ExecuteReader(connection);
                //read data
                while (reader.Read())
                    m_orderNotes.Add(new OrderNoteInfo(CodDiv, SqlHelper.NvlString(reader["NOTETYPE"], string.Empty), SqlHelper.NvlString(reader["NOTE"], string.Empty)));

                reader.Close();
            }

            public override object GenerateReport(SessionContext sc)
            {
                //load template
                string reportTemplate = OrderReportEngine.GetTemplatesPath() + GetTemplateName() + ".rpt";
                ReportDocument rd = new ReportDocument();
                rd.Load(reportTemplate);

                //initialize and set the data source for the main report
                OrderReportEngine.InitializeReport(rd, sc);
                Collection<OrderInfo> source = new Collection<OrderInfo>();
                source.Add(this);
                rd.SetDataSource(source);

                //initialize and set the data source for the subReports
                foreach (ReportDocument subDocument in rd.Subreports)
                {
                    switch (subDocument.Name)
                    {
                        case "SCONTI_SUBREPORT":
                            OrderReportEngine.InitializeReport(subDocument, sc);
                            subDocument.SetDataSource(m_orderBenefits);
                            break;
                        case "OrderSubreport.rpt":
                            OrderReportEngine.InitializeReport(subDocument, sc);
                            subDocument.SetDataSource(m_orderRows);
                            break;
                        case "ORDER_NOTES":
                            OrderReportEngine.InitializeReport(subDocument, sc);
                            subDocument.SetDataSource(m_orderNotes);
                            break;
                    }
                }

                return rd;
            }
        }
    }

    /// TODO - implement by DCODE

    /// <summary>
    /// Data model for the orders with type== 53
    /// </summary>
    namespace OrderReport_type_53_Cust
    {


        public class OrderInfo : OrderReportArgs
        {



            protected SM1Order m_order;
            protected SessionContext m_sc;

            protected Collection<OrderRowInfo> m_orderRows = new Collection<OrderRowInfo>();

            public string DocNumber { get; protected set; }
            public string DesTypOrd { get; protected set; }
            public string DesRoute { get; protected set; }
            public string DesPlate { get; protected set; }
            public string DesWhs { get; protected set; }
            public string MyField { get; protected set; }
            public string ShipDocNo { get; protected set; }
            public string SalesmanCode { get; protected set; }
            public string Desusr { get; protected set; }
            public string DocNumordRef { get; protected set; }

            //added by mady 07092021
            public string PrintedBy { get; protected set; }
            public string DocStatus { get; protected set; }
            public string printed { get; protected set; }
            // ---- COD added By Ganesh B Das Dcode

            public string VanPleate { get; protected set; }
            public string ERPsalesmanid { get; protected set; }
            //public string Test { get; protected set; }
            //TODO - Add other properties that you want to use in the Chrystal Report
            //.....

            public OrderInfo(OrderReportArgs order, SessionContext sc)
                : base(order)
            {
                if (!OrderReportEngine.ExistsOrderTemplate(this))
                    return;

                m_sc = sc;

                using (XConnection connection = XApp.CreateDefaultXConnection())
                {
                    connection.Open();
                    try
                    {
                        InitOrderData(connection);
                        InitOrderRows(connection);
                    }
                    catch { }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            private void InitOrderData(XConnection connection)
            {



                //load order
                SM1OrderEngine engine = new SM1OrderEngine();
                m_order = engine.LoadSM1Order(connection, CodUsr, NumOrd, false, m_sc);

                DesTypOrd = OrderReportEngine.DecodeQtab(CodDiv, QTABS.TABLES.CTORD, m_order.CODTYPORD);

                //ROUTE
                SqlSelect selectRoute = new SqlSelect();
                selectRoute.SelectFields.AddTableField("t030user", "desusr");
                selectRoute.Constraints.AddConstraint("t030user", "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSR);
                //selectRoute.Constraints.AddConstraint("t030user", "IDROUTE", SqlRelationalOperator.Equal, m_order.IDROUTE);
                selectRoute.Top = 1;
                //TODO - Add other fields that you want to use in the Chrystal Report
                //.....

                DesRoute = SqlHelper.NvlString(selectRoute.ExecuteScalar(connection), string.Empty);
               // Test = DesRoute;


                //SELLING DAY
                const string seelingDayTable = "ta0300sellingday";
                SqlSelect selectSeelingDay = new SqlSelect();
                selectSeelingDay.SelectFields.AddTableField(seelingDayTable, "CODVEHICLE");
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODSALESMAN", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectSeelingDay.OrderBy.AddTableField(seelingDayTable, "DTESTART", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                selectSeelingDay.Top = 1;

                string codVehicle = SqlHelper.NvlString(selectSeelingDay.ExecuteScalar(connection), string.Empty);

                DesPlate = string.Empty;
                if (!string.IsNullOrWhiteSpace((codVehicle)))
                    DesPlate = XApp.Decodes().GetOptInfoStringValue(CodDiv, "VEHICLE", codVehicle, QTABS.VALUES.REFDAT_VEHICLE.DESPLATE, string.Empty);
                //-- code added by ganesh Dcode







                const string T031ErpDessalesman = "t031userdiv";
                SqlSelect SelectT031ErpDessalesman = new SqlSelect();
                SelectT031ErpDessalesman.SelectFields.AddTableField(T031ErpDessalesman, "CODPARTY");
                SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);
                SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODDIV", SqlRelationalOperator.Equal, m_order.CODDIV);
                SelectT031ErpDessalesman.Top = 1;
                ERPsalesmanid = SqlHelper.NvlString(SelectT031ErpDessalesman.ExecuteScalar(connection), string.Empty);

                const string T030Dessalesman = "t030user";
                SqlSelect SelectT030Dessalesman = new SqlSelect();
                SelectT030Dessalesman.SelectFields.AddTableField(T030Dessalesman, "desusr");
                SelectT030Dessalesman.Constraints.AddConstraint(T030Dessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);
                SelectT030Dessalesman.Top = 1;

                  Desusr = SqlHelper.NvlString(SelectT030Dessalesman.ExecuteScalar(connection), string.Empty);
                  Desusr = m_order.CODUSRMOD.ToString() + "&" + Desusr;
               //----------------------

                //WAREHOUSE
                const string userDivTable = "t031userdiv";
                SqlSelect selectWarehouse = new SqlSelect();
                selectWarehouse.SelectFields.AddTableField(userDivTable, "CODWHSDELIV");
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectWarehouse.Top = 1;

                DesWhs = string.Empty;
                string codWhsDeliv = SqlHelper.NvlString(selectWarehouse.ExecuteScalar(connection), string.Empty);
                if (!string.IsNullOrWhiteSpace((codWhsDeliv)))
                    DesWhs = string.Format("{0} - {1}", XApp.Decodes().DecodeTrap(CodDiv, "WHS", codWhsDeliv, string.Empty),
                        XApp.Decodes().GetOptInfoStringValue(CodDiv, "WHS", codWhsDeliv, QTABS.VALUES.REFDAT_WHS.WHS_ADDRESS, string.Empty));



                // GET LOAD COUNTER GANESH DCODE..
                //--12
                const string OrderHeadTableCount = "t100ordhead";
                SqlSelect SelectOrderHeadTableCount = new SqlSelect();
                SelectOrderHeadTableCount.SelectFields.AddTableField(OrderHeadTableCount, "dteord");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "codtypord", SqlRelationalOperator.Equal, "50");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                SelectOrderHeadTableCount.OrderBy.AddTableField(OrderHeadTableCount, "dteord", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                SelectOrderHeadTableCount.Top = 1;

                string dteord = SqlHelper.NvlString(SelectOrderHeadTableCount.ExecuteScalar(connection), string.Empty);
                //String  DocNumordRe2;
                if (!string.IsNullOrWhiteSpace((dteord)))
                {

                    const string OrderHeadTableCountLoad = "t100ordhead";
                    SqlSelect SelectOrderHeadTableCountLoad = new SqlSelect();
                    //SelectOrderHeadTableCountLoad.SelectFields.AddFunction("COUNT", "COUNTLoad", OrderHeadTableCountLoad, "dteord");
                    SelectOrderHeadTableCountLoad.SelectFields.AddTableField(OrderHeadTableCountLoad, "numord");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "codtypord", SqlRelationalOperator.Equal, "50");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "dteord", SqlRelationalOperator.GreaterOrEqual, DateTime.Now);
                    SelectOrderHeadTableCountLoad.Top = 1;
                    // DocNumordRe2 = SelectOrderHeadTableCountLoad.ExecuteScalar(connection);

                }
                else
                {
                    DocNumordRef = "";
                }


                //DocNumber
                String DocNumberOriginal = string.IsNullOrWhiteSpace(m_order.NUMDOC)
                    ? m_order.NUMORD.ToString(CultureInfo.InvariantCulture)
                    : m_order.NUMDOC;
                //dECODE GANESH
                DocNumber = DesPlate + "INC" + DocNumberOriginal.Trim();

                //TODO - init other properties
                MyField = "Something";

                //TODO - init other properties
                ShipDocNo = DocNumber;
               // DesPlate = Desusr;
            }

            protected virtual void InitOrderRows(XConnection connection)
            {
                foreach (var orderRow in m_order.OrderRowDetails)
                {
                    m_orderRows.Add(new OrderRowInfo(CodDiv, orderRow.CODART, orderRow.CODTYPROW, orderRow.DESART, orderRow.UMINV, orderRow.QTYINV));

                    foreach (var orderRowBatch in orderRow.OrderRowBatchDetails)
                        m_orderRows.Add(new OrderRowInfo(CodDiv, string.Empty, string.Empty, orderRowBatch.IDBATCH + " - " + orderRowBatch.DTEEXPIRE.ToString("dd/MM/yyyy"),
                            string.Empty, orderRowBatch.QTYINV, true));

                    m_orderRows.Last().LastInGroup = true;
                }
            }

            protected virtual Collection<OrderRowInfo> GetOrderRows()
            {
                return m_orderRows;
            }

            public override object GenerateReport(SessionContext sc)
            {
                //load template
                string reportTemplate = OrderReportEngine.GetTemplatesPath() + "\\" + GetTemplateName() + ".rpt";
                ReportDocument rd = new ReportDocument();
                rd.Load(reportTemplate);

                //initialize and set the data source for the main report
                OrderReportEngine.InitializeReport(rd, sc);
                Collection<OrderInfo> source = new Collection<OrderInfo>();
                source.Add(this);
                rd.SetDataSource(source);

                //initialize and set the data source for the subReports
                foreach (ReportDocument subDocument in rd.Subreports)
                {
                    switch (subDocument.Name)
                    {
                        case "OrderSubreportVan.rpt":
                            {
                                OrderReportEngine.InitializeReport(subDocument, sc);
                                subDocument.SetDataSource(GetOrderRows());
                                break;
                            }
                    }
                }

                return rd;
            }
        }

        public class OrderRowInfo
        {
            public string CodArt { get; private set; }
            public string DesTypRow { get; private set; }
            public string DesArt { get; private set; }
            public string Um { get; private set; }
            public string Qty { get; private set; }
            public bool IsBatch { get; private set; }
            public bool LastInGroup { get; set; }
            public string MyNewProp { get; set; }

            //TODO - Add other properties that you want to use in the Chrystal Report
            //.....

            public OrderRowInfo(string codDiv, string codArt, string codTypRow, string desArt, string um, decimal qty, bool isBatch = false)
            {
                CodArt = codArt;
                DesTypRow = OrderReportEngine.DecodeQtab(codDiv, QTABS.TABLES.TYROW, codTypRow);
                DesArt = desArt;
                Um = um;
                Qty = qty.ToString();
                IsBatch = isBatch;
                MyNewProp = "ABC";
            }
        }
    }

    /// TODO - implement by DCODE

    /// <summary>
    /// Data model for the orders with type== 51
    /// </summary>

    namespace OrderReport_type_51_Cust
    {


        public class OrderInfo : OrderReportArgs
        {
            protected SM1Order m_order;
            protected SessionContext m_sc;

            protected Collection<OrderRowInfo> m_orderRows = new Collection<OrderRowInfo>();


            public string DocNumber { get; protected set; }
            public string DesTypOrd { get; protected set; }
            public string DesRoute { get; protected set; }
            public string DesPlate { get; protected set; }
            public string DesWhs { get; protected set; }
            public string MyField { get; protected set; }
            public string ShipDocNo { get; protected set; }
            public string SalesmanCode { get; protected set; }
            public string Desusr { get; protected set; }
            public string DocNumordRef { get; protected set; }
            // ---- COD added By Ganesh B Das Dcode
            //added by mady 07092021
            public string PrintedBy { get; protected set; }
            public string DocStatus { get; protected set; }
            public string printed { get; protected set; }
            public string VanPleate { get; protected set; }
            public string ERPsalesmanid { get; protected set; }

           public new string MacroType { get; protected set; }

           public string PrintedBy51 { get; protected set; }
           public string DocStatus51 { get; protected set; }
           public string printed51 { get; protected set; }


            //TODO - Add other properties that you want to use in the Chrystal Report
            //.....

            public OrderInfo(OrderReportArgs order, SessionContext sc)
                : base(order)
            {
                if (!OrderReportEngine.ExistsOrderTemplate(this))
                    return;

                m_sc = sc;

                using (XConnection connection = XApp.CreateDefaultXConnection())
                {
                    connection.Open();
                    try
                    {
                        InitOrderData(connection);
                        InitOrderRows(connection);
                    }
                    catch { }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            private void InitOrderData(XConnection connection)
            {



                //load order
                SM1OrderEngine engine = new SM1OrderEngine();
                m_order = engine.LoadSM1Order(connection, CodUsr, NumOrd, false, m_sc);

                DesTypOrd = OrderReportEngine.DecodeQtab(CodDiv, QTABS.TABLES.CTORD, m_order.CODTYPORD);

                //ROUTE
                SqlSelect selectRoute = new SqlSelect();
                selectRoute.SelectFields.AddTableField("t030user", "desusr");
                selectRoute.Constraints.AddConstraint("t030user", "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSR);
                //selectRoute.Constraints.AddConstraint("t030user", "IDROUTE", SqlRelationalOperator.Equal, m_order.IDROUTE);
                selectRoute.Top = 1;
                //TODO - Add other fields that you want to use in the Chrystal Report
                //.....

                DesRoute = SqlHelper.NvlString(selectRoute.ExecuteScalar(connection), string.Empty);
                // Test = DesRoute;


                //SELLING DAY
                const string seelingDayTable = "ta0300sellingday";
                SqlSelect selectSeelingDay = new SqlSelect();
                selectSeelingDay.SelectFields.AddTableField(seelingDayTable, "CODVEHICLE");
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODSALESMAN", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectSeelingDay.OrderBy.AddTableField(seelingDayTable, "DTESTART", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                selectSeelingDay.Top = 1;

                string codVehicle = SqlHelper.NvlString(selectSeelingDay.ExecuteScalar(connection), string.Empty);

                VanPleate = string.Empty;
                if (!string.IsNullOrWhiteSpace((codVehicle)))
                    VanPleate = XApp.Decodes().GetOptInfoStringValue(CodDiv, "VEHICLE", codVehicle, QTABS.VALUES.REFDAT_VEHICLE.DESPLATE, string.Empty);
                //-- code added by ganesh Dcode

                //string getUserGroup = "";
                // get status document mady 07/09/2021



                DocStatus = XApp.Decodes().Decode(CodDiv, "ORDST", m_order.CODSTATUS);


                const string T031ErpDessalesman = "t031userdiv";
                SqlSelect SelectT031ErpDessalesman = new SqlSelect();
                SelectT031ErpDessalesman.SelectFields.AddTableField(T031ErpDessalesman, "CODPARTY");
                if(m_order.CODUSRCREREAL == null || m_order.CODUSRCREREAL == "")
                    SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSR);
                else
                    SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);

                SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODDIV", SqlRelationalOperator.Equal, m_order.CODDIV);
                SelectT031ErpDessalesman.Top = 1;
                ERPsalesmanid = SqlHelper.NvlString(SelectT031ErpDessalesman.ExecuteScalar(connection), string.Empty);

                const string T030Dessalesman = "t030user";
                SqlSelect SelectT030Dessalesman = new SqlSelect();
                SelectT030Dessalesman.SelectFields.AddTableField(T030Dessalesman, "desusr");
                SelectT030Dessalesman.Constraints.AddConstraint(T030Dessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);
                SelectT030Dessalesman.Top = 1;

                Desusr = SqlHelper.NvlString(SelectT030Dessalesman.ExecuteScalar(connection), string.Empty);
                Desusr = m_order.CODUSRMOD.ToString() + "&" + Desusr;
                //----------------------

                const string valuesRPT = "TZ_REPORT";
                SqlSelect getRPT = new SqlSelect();
                getRPT.SelectFields.AddTableField(valuesRPT, "NUMORD");
                getRPT.Constraints.AddConstraint(valuesRPT, "FLG_RPT", SqlRelationalOperator.Equal, "-1");
               // getRPT.Top = 1;
                string gerRecord = SqlHelper.NvlString(getRPT.ExecuteScalar(connection), string.Empty);
                //----------------MADY------------------
                const string reportInfo = "TZ_REPORT";
                SqlInsertValues insertDocs = new SqlInsertValues();
                insertDocs.TableName = reportInfo;
                insertDocs.Values.Add("NUMORD", m_order.NUMORD);
                insertDocs.Values.Add("CODUSR", m_order.CODUSR);
                insertDocs.Values.Add("CODDIV", m_order.CODDIV);
                insertDocs.Values.Add("CODSTATUS", m_order.CODSTATUS);
                insertDocs.Values.Add("DESSTATUS", DocStatus);
                insertDocs.Values.Add("USRLOGIN", m_sc.CodUser);
                insertDocs.Values.Add("DESUSRLOGIN", m_sc.DesUsr);
                insertDocs.Values.Add("DTEPRINT", DateTime.Now);
                if (!string.IsNullOrWhiteSpace((gerRecord)))
                {
                    insertDocs.Values.Add("FLG_RPT", "-1");
                    insertDocs.Values.Add("RPT_STATUS", "ORIGINAL");

                }
                else
                {
                    insertDocs.Values.Add("FLG_RPT", "0");
                    insertDocs.Values.Add("RPT_STATUS", "RIPRENTED");
                }
                insertDocs.Execute(connection);


                //WAREHOUSE
                const string userDivTable = "t031userdiv";
                SqlSelect selectWarehouse = new SqlSelect();
                selectWarehouse.SelectFields.AddTableField(userDivTable, "CODWHSDELIV");
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectWarehouse.Top = 1;

                DesWhs = string.Empty;
                string codWhsDeliv = SqlHelper.NvlString(selectWarehouse.ExecuteScalar(connection), string.Empty);
                if (!string.IsNullOrWhiteSpace((codWhsDeliv)))
                    DesWhs = string.Format("{0} - {1}", XApp.Decodes().DecodeTrap(CodDiv, "WHS", codWhsDeliv, string.Empty),
                        XApp.Decodes().GetOptInfoStringValue(CodDiv, "WHS", codWhsDeliv, QTABS.VALUES.REFDAT_WHS.WHS_ADDRESS, string.Empty));


                //Delivery Date in report Mady 01/09/2020
                MacroType = m_order.DTEDELIV.ToString();


                // GET LOAD COUNTER GANESH DCODE..
                //--12
                const string OrderHeadTableCount = "t100ordhead";
                SqlSelect SelectOrderHeadTableCount = new SqlSelect();
                SelectOrderHeadTableCount.SelectFields.AddTableField(OrderHeadTableCount, "dteord");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "codtypord", SqlRelationalOperator.Equal, "51");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                SelectOrderHeadTableCount.OrderBy.AddTableField(OrderHeadTableCount, "dteord", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                SelectOrderHeadTableCount.Top = 1;

                string dteord = SqlHelper.NvlString(SelectOrderHeadTableCount.ExecuteScalar(connection), string.Empty);
                //String  DocNumordRe2;
                if (!string.IsNullOrWhiteSpace((dteord)))
                {

                    const string OrderHeadTableCountLoad = "t100ordhead";
                    SqlSelect SelectOrderHeadTableCountLoad = new SqlSelect();
                    //SelectOrderHeadTableCountLoad.SelectFields.AddFunction("COUNT", "COUNTLoad", OrderHeadTableCountLoad, "dteord");
                    SelectOrderHeadTableCountLoad.SelectFields.AddTableField(OrderHeadTableCountLoad, "numord");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "codtypord", SqlRelationalOperator.Equal, "51");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "dteord", SqlRelationalOperator.GreaterOrEqual, DateTime.Now);
                    SelectOrderHeadTableCountLoad.Top = 1;
                    // DocNumordRe2 = SelectOrderHeadTableCountLoad.ExecuteScalar(connection);

                }
                else
                {
                    DocNumordRef = "";
                }


                //DocNumber
                String DocNumberOriginal = string.IsNullOrWhiteSpace(m_order.NUMDOC)
                    ? m_order.NUMORD.ToString(CultureInfo.InvariantCulture)
                    : m_order.NUMDOC;
                //dECODE GANESH
                DocNumber = DesPlate + "INC" + DocNumberOriginal.Trim();

                //TODO - init other properties
                MyField = "Something";

                //TODO - init other properties
                ShipDocNo = DocNumber;
                // DesPlate = Desusr;
            }

            protected virtual void InitOrderRows(XConnection connection)
            {
                foreach (var orderRow in m_order.OrderRowDetails)
                {
                    m_orderRows.Add(new OrderRowInfo(CodDiv, orderRow.CODART, orderRow.CODTYPROW, orderRow.DESART, orderRow.UMINV, orderRow.QTYINV));

                    foreach (var orderRowBatch in orderRow.OrderRowBatchDetails)
                        m_orderRows.Add(new OrderRowInfo(CodDiv, string.Empty, string.Empty, orderRowBatch.IDBATCH + " - " + orderRowBatch.DTEEXPIRE.ToString("dd/MM/yyyy"),
                            string.Empty, orderRowBatch.QTYINV, true));

                    m_orderRows.Last().LastInGroup = true;
                }
            }

            protected virtual Collection<OrderRowInfo> GetOrderRows()
            {
                return m_orderRows;
            }

            public override object GenerateReport(SessionContext sc)
            {
                //load template
                string reportTemplate = OrderReportEngine.GetTemplatesPath() + "\\" + GetTemplateName() + ".rpt";
                ReportDocument rd = new ReportDocument();
                rd.Load(reportTemplate);

                //initialize and set the data source for the main report
                OrderReportEngine.InitializeReport(rd, sc);
                Collection<OrderInfo> source = new Collection<OrderInfo>();
                source.Add(this);
                rd.SetDataSource(source);

                //initialize and set the data source for the subReports
                foreach (ReportDocument subDocument in rd.Subreports)
                {
                    switch (subDocument.Name)
                    {
                        case "OrderSubreportVan.rpt":
                            {
                                OrderReportEngine.InitializeReport(subDocument, sc);
                                subDocument.SetDataSource(GetOrderRows());
                                break;
                            }
                    }
                }

                return rd;
            }
        }

        public class OrderRowInfo
        {
            public string CodArt { get; private set; }
            public string DesTypRow { get; private set; }
            public string DesArt { get; private set; }
            public string Um { get; private set; }
            public string Qty { get; private set; }
            public bool IsBatch { get; private set; }
            public bool LastInGroup { get; set; }
            public string MyNewProp { get; set; }

            //TODO - Add other properties that you want to use in the Chrystal Report
            //.....

            public OrderRowInfo(string codDiv, string codArt, string codTypRow, string desArt, string um, decimal qty, bool isBatch = false)
            {
                CodArt = codArt;
                DesTypRow = OrderReportEngine.DecodeQtab(codDiv, QTABS.TABLES.TYROW, codTypRow);
                DesArt = desArt;
                Um = um;
                Qty = qty.ToString();
                IsBatch = isBatch;
                MyNewProp = "ABC";
            }
        }
    }


    /// <summary>
    /// Data model for the orders with type=80
    /// </summary>
    namespace OrderReport_type_80_Cust
    {
        public class OrderBenefitInfo
        {
            public decimal QtyBen { get; private set; }

            public string CodSrc { get; private set; }
            public string DesSrc { get; private set; }

            public string CodTypBen { get; private set; }
            public string DesTypBen { get; private set; }

            public string CodBenCause { get; private set; }
            public string DesBenCause { get; private set; }

            public OrderBenefitInfo(string codDiv, decimal qtyBen, string codSrc, string codTypBen, string codBenCause)
            {
                QtyBen = qtyBen;

                CodSrc = codSrc;
                DesSrc = OrderReportEngine.DecodeQtab(codDiv, "BESRC", codSrc);

                CodTypBen = codTypBen;
                DesTypBen = OrderReportEngine.DecodeQtab(codDiv, "BETYP", codTypBen);

                CodBenCause = codBenCause;
                DesBenCause = OrderReportEngine.DecodeQtab(codDiv, "CCAU1", codBenCause);
            }
        }

        public class OrderRowInfo
        {
            public int NumRow { get; private set; }
            public string CodTypRow { get; private set; }
            public string CodArt { get; private set; }
            public string CodArtCust { get; private set; }
            public string DesArt { get; private set; }
            public string NumInv { get; private set; }
            public DateTime DteInv { get; private set; }
            public string QtyOrd { get; private set; }
            public string UmOrd { get; private set; }
            public string QtyInv { get; private set; }
            public string UmInv { get; private set; }
            public string QtyAnn { get; private set; }
            public string GrossArtAmount { get; private set; }
            public string GrossArtAmountUmOrd { get; private set; }
            public string NetArtAmount { get; private set; }
            public string NetArtAmountUmOrd { get; private set; }
            public string NetAmount { get; private set; }

            //benefit 1
            public string ValBen1 { get; private set; }
            public string CodTypBen1 { get; private set; }
            public string CodSrc1 { get; private set; }
            public string CodSrcRef1 { get; private set; }
            //benefit 2
            public string ValBen2 { get; private set; }
            public string CodTypBen2 { get; private set; }
            public string CodSrc2 { get; private set; }
            public string CodSrcRef2 { get; private set; }
            //benefit 3
            public string ValBen3 { get; private set; }
            public string CodTypBen3 { get; private set; }
            public string CodSrc3 { get; private set; }
            public string CodSrcRef3 { get; private set; }
            //benefit 4
            public string ValBen4 { get; private set; }
            public string CodTypBen4 { get; private set; }
            public string CodSrc4 { get; private set; }
            public string CodSrcRef4 { get; private set; }
            //benefit 5
            public string ValBen5 { get; private set; }
            public string CodTypBen5 { get; private set; }
            public string CodSrc5 { get; private set; }
            public string CodSrcRef5 { get; private set; }
            //benefit 6
            public string ValBen6 { get; private set; }
            public string CodTypBen6 { get; private set; }
            public string CodSrc6 { get; private set; }
            public string CodSrcRef6 { get; private set; }

            public OrderRowInfo(string codDiv, int numRow, string codTypRow, string codArt, string codArtCust, string desArt, string numInv, DateTime dteInv, decimal qtyOrd,
                string umOrd, decimal qtyInv, string umInv, decimal qtyAnn, decimal grossArtAmount, decimal grossArtAmountUmOrd, decimal netArtAmount, decimal netArtAmountUmOrd, decimal netAmount,
                decimal valBen1, string codTypBen1, string codSrc1, string codSrcRef1)
            {
                NumRow = numRow;
                CodTypRow = OrderReportEngine.DecodeQtab(codDiv, "TYROW", codTypRow);
                CodArt = codArt;
                CodArtCust = codArtCust;
                DesArt = desArt;
                NumInv = numInv;
                DteInv = dteInv;
                QtyOrd = qtyOrd.ToString();
                UmOrd = umOrd;
                QtyInv = qtyInv.ToString();
                UmInv = umInv;
                QtyAnn = qtyAnn.ToString();
                GrossArtAmount = grossArtAmount.ToString();
                GrossArtAmountUmOrd = grossArtAmountUmOrd.ToString();
                NetArtAmount = netArtAmount.ToString("N");
                NetArtAmountUmOrd = netArtAmountUmOrd.ToString("N");
                NetAmount = netAmount.ToString("N");
                //benefit 1
                ValBen1 = valBen1.ToString();
                CodTypBen1 = OrderReportEngine.DecodeQtab(codDiv, "BETYP", codTypBen1);
                CodSrc1 = OrderReportEngine.DecodeQtab(codDiv, "BESRC", codSrc1);
                CodSrcRef1 = codSrcRef1;
                //init benefit fields with default values
                //benefit 2
                CodTypBen2 = CodSrc2 = " - ";
                //benefit 3
                CodTypBen3 = CodSrc3 = " - ";
                //benefit 4
                CodTypBen4 = CodSrc4 = " - ";
                //benefit 5
                CodTypBen5 = CodSrc5 = " - ";
                //benefit 6
                CodTypBen6 = CodSrc6 = " - ";
            }

            public void AddBenefit(int numBenefit, string codDiv, decimal valBen, string codTypBen, string codSrc, string codSrcRef)
            {
                codTypBen = OrderReportEngine.DecodeQtab(codDiv, "BETYP", codTypBen);
                codSrc = OrderReportEngine.DecodeQtab(codDiv, "BESRC", codSrc);
                switch (numBenefit)
                {
                    case 2:
                        ValBen2 = valBen.ToString();
                        CodTypBen2 = codTypBen;
                        CodSrc2 = codSrc;
                        CodSrcRef2 = codSrcRef;
                        break;
                    case 3:
                        ValBen3 = valBen.ToString();
                        CodTypBen3 = codTypBen;
                        CodSrc3 = codSrc;
                        CodSrcRef3 = codSrcRef;
                        break;
                    case 4:
                        ValBen4 = valBen.ToString();
                        CodTypBen4 = codTypBen;
                        CodSrc4 = codSrc;
                        CodSrcRef4 = codSrcRef;
                        break;
                    case 5:
                        ValBen5 = valBen.ToString();
                        CodTypBen5 = codTypBen;
                        CodSrc5 = codSrc;
                        CodSrcRef5 = codSrcRef;
                        break;
                    case 6:
                        ValBen6 = valBen.ToString();
                        CodTypBen6 = codTypBen;
                        CodSrc6 = codSrc;
                        CodSrcRef6 = codSrcRef;
                        break;
                }
            }
        }

        public class OrderNoteInfo
        {
            public string NoteType { get; private set; }
            public string Note { get; private set; }

            public OrderNoteInfo(string codDiv, string noteType, string note)
            {
                Note = note;
                NoteType = OrderReportEngine.DecodeQtab(codDiv, "NOTES|CNOTO", noteType);
            }
        }

        public class OrderInfo : OrderReportArgs
        {
            private Collection<OrderBenefitInfo> m_orderBenefits = new Collection<OrderBenefitInfo>();
            private Collection<OrderRowInfo> m_orderRows = new Collection<OrderRowInfo>();
            private Collection<OrderNoteInfo> m_orderNotes = new Collection<OrderNoteInfo>();

            public string DocumentKey { get; protected set; }
            public string NumOrdCust { get; protected set; }
            public DateTime DteOrdCust { get; protected set; }
            public string DesDiv { get; protected set; }

            public string CodeUsr { get; protected set; }
            public string CodeUsrDes { get; protected set; }

            public string CodCustDeliv { get; protected set; }
            public string CodCustInv { get; protected set; }
            public string DesParty1 { get; protected set; }

            public string DesAddr1 { get; protected set; }
            public string CodZip { get; protected set; }
            public string DesLoc1 { get; protected set; }
            public string CodPrv { get; protected set; }

            public DateTime DteOrd { get; protected set; }

            public string CodNode2 { get; protected set; }
            public string CodNode2Des { get; protected set; }
            public string CodNode3 { get; protected set; }
            public string CodNode3Des { get; protected set; }

            public string CodModDeliv { get; protected set; }
            public string CodModDelivDes { get; protected set; }

            public string CodShipper { get; protected set; }
            public string CodShipperDes { get; protected set; }

            public decimal QtyTot { get; protected set; }
            public string UmQtyTot { get; protected set; }

            public string CodStatus { get; protected set; }
            public string CodStatusDes { get; protected set; }

            public string CodCatDiv2 { get; protected set; }
            public string CodCatDiv2Des { get; protected set; }

            public string CodVat { get; protected set; }
            public string CodIban { get; protected set; }

            public string CodPayTrm { get; protected set; }
            public string CodPayTrmDes { get; protected set; }

            public string CodCur { get; protected set; }
            public string CodCurDes { get; protected set; }

            public decimal NetAmount { get; protected set; }
            public decimal TaxAmount { get; protected set; }
            public decimal VatAmount { get; protected set; }

            public OrderInfo(OrderReportArgs order, SessionContext sc)
                : base(order)
            {
                if (!OrderReportEngine.ExistsOrderTemplate(this))
                    return;

                using (XConnection connection = XApp.CreateDefaultXConnection())
                {
                    connection.Open();

                    try
                    {
                        InitOrderData(connection);
                        InitHierData(connection);
                        InitOrderBenefits(connection);
                        InitOrderRows(connection);
                        InitOrderNotes(connection);
                    }
                    catch { }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            private void InitOrderData(XConnection connection)
            {
                //query the database for the order info, customer info, etc
                SqlSelect mainSelect = new SqlSelect();
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "DOCUMENTKEY");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "NUMORDCUST");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "DTEORDCUST");
                mainSelect.SelectFields.AddTableField("T032DIVISION", "DESDIV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODEUSR");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODCUSTDELIV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODCUSTINV");
                mainSelect.SelectFields.AddTableField("T040PARTY", "DESPARTY1");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "DESADDR1");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "CODZIP");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "DESLOC1");
                mainSelect.SelectFields.AddTableField("T042PARTYADDR", "CODPRV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "DTEORD");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODMODDELIV");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODSHIPPER");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "QTYTOT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "UMQTYTOT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODSTATUS");
                mainSelect.SelectFields.AddTableField("T041PARTYDIV", "CODCATDIV2");
                mainSelect.SelectFields.AddTableField("T040PARTY", "CODVAT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODIBAN");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODPAYTRM");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "CODCUR");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "NETAMOUNT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "TAXAMOUNT");
                mainSelect.SelectFields.AddTableField("T100ORDHEAD", "VATAMOUNT");
                //constraints
                mainSelect.Constraints.AddConstraint("T100ORDHEAD", "NUMORD", SqlRelationalOperator.Equal, NumOrd);
                mainSelect.Constraints.AddConstraint("T100ORDHEAD", "CODUSR", SqlRelationalOperator.Equal, CodUsr);
                mainSelect.Constraints.AddConstraint("T100ORDHEAD", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODDIV" }, JoinOperator.Inner, "T032DIVISION", new[] { "CODDIV" });
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODCUSTDELIV" }, JoinOperator.Inner, "T040PARTY", new[] { "CODPARTY" });
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODCUSTDELIV" }, JoinOperator.Inner, "T042PARTYADDR", new[] { "CODPARTY" });
                mainSelect.Constraints.AddJoinConstraint("T100ORDHEAD", new[] { "CODCUSTDELIV", "CODDIV" }, JoinOperator.Inner, "T041PARTYDIV", new[] { "CODPARTY", "CODDIV" });
                //init reader
                DbDataReader reader = mainSelect.ExecuteReader(connection);
                reader.Read();
                //read data
                if (reader.HasRows)
                {
                    DocumentKey = SqlHelper.NvlString(reader["DOCUMENTKEY"], string.Empty);
                    NumOrdCust = SqlHelper.NvlString(reader["NUMORDCUST"], string.Empty);
                    DteOrdCust = SqlHelper.NvlDateTime(reader["DTEORDCUST"], DateTime.Now);
                    DesDiv = SqlHelper.NvlString(reader["DESDIV"], string.Empty);

                    CodeUsr = SqlHelper.NvlString(reader["CODEUSR"], string.Empty);
                    CodeUsrDes = OrderReportEngine.DecodeQtab(CodDiv, "USR1", CodeUsr);

                    CodCustDeliv = SqlHelper.NvlString(reader["CODCUSTDELIV"], string.Empty);
                    CodCustInv = SqlHelper.NvlString(reader["CODCUSTINV"], string.Empty);
                    DesParty1 = SqlHelper.NvlString(reader["DESPARTY1"], string.Empty);
                    DesAddr1 = SqlHelper.NvlString(reader["DESADDR1"], string.Empty);
                    CodZip = SqlHelper.NvlString(reader["CODZIP"], string.Empty);
                    DesLoc1 = SqlHelper.NvlString(reader["DESLOC1"], string.Empty);
                    CodPrv = SqlHelper.NvlString(reader["CODPRV"], string.Empty);
                    DteOrd = SqlHelper.NvlDateTime(reader["DTEORD"], DateTime.Now);

                    CodModDeliv = SqlHelper.NvlString(reader["CODMODDELIV"], string.Empty);
                    CodModDelivDes = OrderReportEngine.DecodeQtab(CodDiv, "CDEL", CodModDeliv);

                    CodShipper = SqlHelper.NvlString(reader["CODSHIPPER"], string.Empty);
                    CodShipperDes = OrderReportEngine.DecodeQtab(CodDiv, "SHIPP", CodShipper);

                    QtyTot = SqlHelper.NvlDecimal(reader["QTYTOT"], 0);
                    UmQtyTot = SqlHelper.NvlString(reader["UMQTYTOT"], string.Empty);
                    UmQtyTot = OrderReportEngine.DecodeQtab(CodDiv, "UMART", UmQtyTot);

                    CodStatus = SqlHelper.NvlString(reader["CODSTATUS"], string.Empty);
                    CodStatusDes = OrderReportEngine.DecodeQtab(CodDiv, "ORDST", CodStatus);

                    CodCatDiv2 = SqlHelper.NvlString(reader["CODCATDIV2"], string.Empty);
                    CodCatDiv2Des = OrderReportEngine.DecodeQtab(CodDiv, "CTP2D", CodCatDiv2);

                    CodVat = SqlHelper.NvlString(reader["CODVAT"], string.Empty);
                    CodIban = SqlHelper.NvlString(reader["CODIBAN"], string.Empty);

                    CodPayTrm = SqlHelper.NvlString(reader["CODPAYTRM"], string.Empty);
                    CodPayTrmDes = OrderReportEngine.DecodeQtab(CodDiv, "CPTRM", CodPayTrm);

                    CodCur = SqlHelper.NvlString(reader["CODCUR"], string.Empty);
                    CodCurDes = OrderReportEngine.DecodeQtab(CodDiv, "CUR", CodCur);

                    NetAmount = SqlHelper.NvlDecimal(reader["NETAMOUNT"], 0);
                    TaxAmount = SqlHelper.NvlDecimal(reader["TAXAMOUNT"], 0);
                    VatAmount = SqlHelper.NvlDecimal(reader["VATAMOUNT"], 0);
                }
                reader.Close();
            }

            private void InitHierData(XConnection connection)
            {
                //query the database for the customer hier info
                SqlSelect hierSelect = new SqlSelect();
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "CODNODE3");
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "DESNODE3");
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "CODNODE2");
                hierSelect.SelectFields.AddTableField("TB0032HIERFLATDES_CUST", "DESNODE2");

                //constraints
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "IDLEVEL", SqlRelationalOperator.Equal, -1);
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "CODHIER", SqlRelationalOperator.Equal, "COMM");
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                hierSelect.Constraints.AddConstraint("TB0032HIERFLATDES_CUST", "CODNODE", SqlRelationalOperator.Equal, CodCustDeliv);
                hierSelect.Constraints.AddConstraint(new SqlConstraint(new SqlOperandValue(DteOrd), SqlRelationalOperator.Between,
                    new SqlOperandField("TB0032HIERFLATDES_CUST", "DTESTART"), new SqlOperandField("TB0032HIERFLATDES_CUST", "DTEEND")));
                //init reader
                DbDataReader reader = hierSelect.ExecuteReader(connection);
                reader.Read();
                //read data
                if (reader.HasRows)
                {
                    CodNode2 = SqlHelper.NvlString(reader["CODNODE2"], string.Empty);
                    CodNode2Des = SqlHelper.NvlString(reader["DESNODE2"], string.Empty);
                    CodNode3 = SqlHelper.NvlString(reader["CODNODE3"], string.Empty);
                    CodNode3Des = SqlHelper.NvlString(reader["DESNODE3"], string.Empty);
                }
                reader.Close();
            }

            private void InitOrderBenefits(XConnection connection)
            {
                //build select
                SqlSelect benefitSelect = new SqlSelect();
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "QTYBEN");
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "CODSRC");
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "CODTYPBEN");
                benefitSelect.SelectFields.AddTableField("T101BENEFIT", "CODBENCAUSE");
                //constraints
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "NUMORD", SqlRelationalOperator.Equal, NumOrd);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "CODUSR", SqlRelationalOperator.Equal, CodUsr);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "NUMROW", SqlRelationalOperator.Equal, 0);
                benefitSelect.Constraints.AddConstraint("T101BENEFIT", "CODTYPBEN", SqlRelationalOperator.NotIn, new string[] { "98", "99" });
                //order by
                benefitSelect.OrderBy.AddTableField("T101BENEFIT", "PRGAPPLY", Xtel.SM1.Core.Data.QueryObj.SortDirection.Asc);
                //init reader
                DbDataReader reader = benefitSelect.ExecuteReader(connection);
                //read data
                while (reader.Read())
                    m_orderBenefits.Add(new OrderBenefitInfo(CodDiv, SqlHelper.NvlDecimal(reader["QTYBEN"], 0), SqlHelper.NvlString(reader["CODSRC"], string.Empty),
                        SqlHelper.NvlString(reader["CODTYPBEN"], string.Empty), SqlHelper.NvlString(reader["CODBENCAUSE"], string.Empty)));

                reader.Close();
            }

            private void InitOrderRows(XConnection connection)
            {
                //query database for order benefits
                SqlSelect select = new SqlSelect();
                select.SelectFields.AddTableField("T106ORDROW", "NUMROW");
                select.SelectFields.AddTableField("T106ORDROW", "CODTYPROW");
                select.SelectFields.AddTableField("T106ORDROW", "CODART");
                select.SelectFields.AddTableField("T106ORDROW", "CODARTCUST");
                select.SelectFields.AddTableField("T106ORDROW", "NUMINV");
                select.SelectFields.AddTableField("T106ORDROW", "DTEINV");
                select.SelectFields.AddTableField("T106ORDROW", "QTYORD");
                select.SelectFields.AddTableField("T106ORDROW", "UMORD");
                select.SelectFields.AddTableField("T106ORDROW", "QTYINV");
                select.SelectFields.AddTableField("T106ORDROW", "UMINV");
                select.SelectFields.AddTableField("T106ORDROW", "QTYANN");
                select.SelectFields.AddTableField("T106ORDROW", "GROSSARTAMOUNT");
                select.SelectFields.AddTableField("T106ORDROW", "GROSSARTAMOUNTUMORD");
                select.SelectFields.AddTableField("T106ORDROW", "NETARTAMOUNT");
                select.SelectFields.AddTableField("T106ORDROW", "NETARTAMOUNTUMORD");
                select.SelectFields.AddTableField("T106ORDROW", "NETAMOUNT");
                select.SelectFields.AddTableField("T101BENEFIT", "QTYBEN");
                select.SelectFields.AddTableField("T101BENEFIT", "CODTYPBEN");
                select.SelectFields.AddTableField("T101BENEFIT", "CODSRC");
                select.SelectFields.AddTableField("T101BENEFIT", "CODSRCREF");
                select.SelectFields.AddTableField("T060ARTICLE", "DESART");
                //constraints
                SqlConstraints constraints = new SqlConstraints();
                select.Constraints.AddConstraint("T106ORDROW", "CODUSR", SqlRelationalOperator.Equal, CodUsr);
                select.Constraints.AddConstraint("T106ORDROW", "NUMORD", SqlRelationalOperator.Equal, NumOrd);
                select.Constraints.AddConstraint("T106ORDROW", "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                //join with T101
                SqlConstraints constraints1 = new SqlConstraints();
                constraints1.AddConstraint("T106ORDROW", "NUMORD", SqlRelationalOperator.Equal, "T101BENEFIT", "NUMORD");
                constraints1.AddConstraint("T106ORDROW", "CODUSR", SqlRelationalOperator.Equal, "T101BENEFIT", "CODUSR");
                constraints1.AddConstraint("T106ORDROW", "NUMROW", SqlRelationalOperator.Equal, "T101BENEFIT", "NUMROW");
                constraints1.AddConstraint("T101BENEFIT", "CODTYPBEN", SqlRelationalOperator.NotIn, new string[] { "98", "99" });
                constraints1.AddConstraint("T101BENEFIT", "CODTEOBEN", SqlRelationalOperator.IsNull, null);
                select.Constraints.AddJoinConstraint("T106ORDROW", JoinOperator.Left, "T101BENEFIT", constraints1);
                //join with T060
                SqlConstraints constraints2 = new SqlConstraints();
                constraints2.AddConstraint("T106ORDROW", "CODART", SqlRelationalOperator.Equal, "T060ARTICLE", "CODART");
                constraints2.AddConstraint("T106ORDROW", "CODDIV", SqlRelationalOperator.Equal, "T060ARTICLE", "CODDIV");
                select.Constraints.AddJoinConstraint("T106ORDROW", JoinOperator.Left, "T060ARTICLE", constraints2);
                //order
                select.OrderBy.AddTableField("T106ORDROW", "NUMROW", Core.Data.QueryObj.SortDirection.Asc);
                select.OrderBy.AddTableField("T101BENEFIT", "PRGAPPLY", Core.Data.QueryObj.SortDirection.Asc);

                DbDataReader reader = select.ExecuteReader(connection);
                int currentNumRow = -1;
                int numBenefit = 0;

                while (reader.Read())
                {
                    int numrow = SqlHelper.NvlInt(reader["NUMROW"], 0);
                    decimal valBen = SqlHelper.NvlDecimal(reader["QTYBEN"], 0);
                    string codTypBen = SqlHelper.NvlString(reader["CODTYPBEN"], string.Empty);
                    string codSrc = SqlHelper.NvlString(reader["CODSRC"], string.Empty);
                    string codSrcRef = SqlHelper.NvlString(reader["CODSRCREF"], string.Empty);

                    if (currentNumRow != numrow)
                    {
                        currentNumRow = numrow;
                        numBenefit = 1;
                        decimal qtyOrd = SqlHelper.NvlDecimal(reader["QTYORD"], 0);
                        //add the new row to the collection and add the benefit read before
                        if (qtyOrd != 0)
                            m_orderRows.Add(new OrderRowInfo(CodDiv, currentNumRow, SqlHelper.NvlString(reader["CODTYPROW"], string.Empty), SqlHelper.NvlString(reader["CODART"], string.Empty),
                                SqlHelper.NvlString(reader["CODARTCUST"], string.Empty), SqlHelper.NvlString(reader["DESART"], string.Empty), SqlHelper.NvlString(reader["NUMINV"], string.Empty),
                                SqlHelper.NvlDateTime(reader["DTEINV"], DateTime.Now), qtyOrd, SqlHelper.NvlString(reader["UMORD"], string.Empty),
                                SqlHelper.NvlDecimal(reader["QTYINV"], 0), SqlHelper.NvlString(reader["UMINV"], string.Empty), SqlHelper.NvlDecimal(reader["QTYANN"], 0),
                                SqlHelper.NvlDecimal(reader["GROSSARTAMOUNT"], 0), SqlHelper.NvlDecimal(reader["GROSSARTAMOUNTUMORD"], 0),
                                SqlHelper.NvlDecimal(reader["NETARTAMOUNT"], 0), SqlHelper.NvlDecimal(reader["NETARTAMOUNTUMORD"], 0),
                                SqlHelper.NvlDecimal(reader["NETAMOUNT"], 0), valBen, codTypBen, codSrc, codSrcRef));
                    }
                    else
                    {
                        OrderRowInfo row = m_orderRows.Where(c => c.NumRow == currentNumRow).SingleOrDefault();
                        if (row != null)
                        {
                            numBenefit = numBenefit + 1;
                            //add the benefit to the current row
                            row.AddBenefit(numBenefit, CodDiv, valBen, codTypBen, codSrc, codSrcRef);
                        }
                    }
                }

                reader.Close();
            }

            private void InitOrderNotes(XConnection connection)
            {
                //build select
                SqlSelect noteSelect = new SqlSelect();
                noteSelect.SelectFields.AddTableField("TA4410NOTES", "NOTETYPE");
                noteSelect.SelectFields.AddTableField("TA4410NOTES", "NOTE");
                //constraints
                noteSelect.Constraints.AddConstraint("TA4410NOTES", "PARENTKEY", SqlRelationalOperator.Equal, DocumentKey);
                //init reader
                DbDataReader reader = noteSelect.ExecuteReader(connection);
                //read data
                while (reader.Read())
                    m_orderNotes.Add(new OrderNoteInfo(CodDiv, SqlHelper.NvlString(reader["NOTETYPE"], string.Empty), SqlHelper.NvlString(reader["NOTE"], string.Empty)));

                reader.Close();
            }

            public override object GenerateReport(SessionContext sc)
            {
                //load template
                string reportTemplate = OrderReportEngine.GetTemplatesPath() + GetTemplateName() + ".rpt";
                ReportDocument rd = new ReportDocument();
                rd.Load(reportTemplate);

                //initialize and set the data source for the main report
                OrderReportEngine.InitializeReport(rd, sc);
                Collection<OrderInfo> source = new Collection<OrderInfo>();
                source.Add(this);
                rd.SetDataSource(source);

                //initialize and set the data source for the subReports
                foreach (ReportDocument subDocument in rd.Subreports)
                {
                    switch (subDocument.Name)
                    {
                        case "SCONTI_SUBREPORT":
                            OrderReportEngine.InitializeReport(subDocument, sc);
                            subDocument.SetDataSource(m_orderBenefits);
                            break;
                        case "OrderSubreport.rpt":
                            OrderReportEngine.InitializeReport(subDocument, sc);
                            subDocument.SetDataSource(m_orderRows);
                            break;
                        case "ORDER_NOTES":
                            OrderReportEngine.InitializeReport(subDocument, sc);
                            subDocument.SetDataSource(m_orderNotes);
                            break;
                    }
                }

                return rd;
            }
        }
    }


    /// <summary>
    /// Data model for the orders with macrotype == 5
    /// </summary>
    namespace OrderReport_macrotype_5_Cust
    {
        public class OrderRowInfo
        {
            public string CodArt { get; private set; }
            public string DesTypRow { get; private set; }
            public string DesArt { get; private set; }
            public string Um { get; private set; }
            public string Qty { get; private set; }
            public bool IsBatch { get; private set; }
            public bool LastInGroup { get; set; }

            public OrderRowInfo(string codDiv, string codArt, string codTypRow, string desArt, string um, decimal qty, bool isBatch = false)
            {
                CodArt = codArt;
                DesTypRow = OrderReportEngine.DecodeQtab(codDiv, QTABS.TABLES.TYROW, codTypRow);
                DesArt = desArt;
                Um = um;
                Qty = qty.ToString();
                IsBatch = isBatch;
            }
        }

        public class OrderInfo : OrderReportArgs
        {
            protected SM1Order m_order;
            protected SessionContext m_sc;

            protected Collection<OrderRowInfo> m_orderRows = new Collection<OrderRowInfo>();

            public string DocNumber { get; protected set; }
            public string DesTypOrd { get; protected set; }
            public string DesRoute { get; protected set; }
            public string DesPlate { get; protected set; }
            public string DesWhs { get; protected set; }
            public string Desusr { get; protected set; }
            public string LoadCount { get; protected set; }

            public string ERPsalesmanid { get; protected set; }
            public string VanPleate { get; protected set; }
            //public string Desusr { get; protected set; }

            //added by mady 07092021
            public string PrintedBy { get; protected set; }
            public string DocStatus { get; protected set; }
            public string printed { get; protected set; }
            public string test { get; protected set; }
            //public string Desusr { get; protected set; }
            public OrderInfo(OrderReportArgs order, SessionContext sc)
                : base(order)
            {
                if (!OrderReportEngine.ExistsOrderTemplate(this))
                    return;

                m_sc = sc;

                using (XConnection connection = XApp.CreateDefaultXConnection())
                {
                    connection.Open();
                    try
                    {
                        InitOrderData(connection);
                        InitOrderRows(connection);
                    }
                    catch { }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            private void InitOrderData(XConnection connection)
            {
                //load order
                SM1OrderEngine engine = new SM1OrderEngine();
                m_order = engine.LoadSM1Order(connection, CodUsr, NumOrd, false, m_sc);

                DesTypOrd = OrderReportEngine.DecodeQtab(CodDiv, QTABS.TABLES.CTORD, m_order.CODTYPORD);

                //ROUTE
                SqlSelect selectRoute = new SqlSelect();
                selectRoute.SelectFields.AddTableField("TA0210ROUTE", "DESROUTE");
                selectRoute.Constraints.AddConstraint("TA0210ROUTE", "CODUSR", SqlRelationalOperator.Equal, CodUsr);
                selectRoute.Constraints.AddConstraint("TA0210ROUTE", "IDROUTE", SqlRelationalOperator.Equal, m_order.IDROUTE);
                string DesRouteID = SqlHelper.NvlString(selectRoute.ExecuteScalar(connection), string.Empty);
                test = "abc";
                //SELLING DAY
                const string seelingDayTable = "ta0300sellingday";
                SqlSelect selectSeelingDay = new SqlSelect();
                selectSeelingDay.SelectFields.AddTableField(seelingDayTable, "CODVEHICLE");
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODSALESMAN", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectSeelingDay.OrderBy.AddTableField(seelingDayTable, "DTESTART", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                selectSeelingDay.Top = 1;


                string codVehicle = SqlHelper.NvlString(selectSeelingDay.ExecuteScalar(connection), string.Empty);
                DesPlate = string.Empty;
                if (!string.IsNullOrWhiteSpace((codVehicle)))
                    DesPlate = XApp.Decodes().GetOptInfoStringValue(CodDiv, "VEHICLE", codVehicle, QTABS.VALUES.REFDAT_VEHICLE.DESPLATE, string.Empty);

                //ERP Sales MAN

                const string T031ErpDessalesman = "t031userdiv";
                SqlSelect SelectT031ErpDessalesman = new SqlSelect();
                SelectT031ErpDessalesman.SelectFields.AddTableField(T031ErpDessalesman, "CODPARTY");
                SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);
                SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODDIV", SqlRelationalOperator.Equal, m_order.CODDIV);
                SelectT031ErpDessalesman.Top = 1;
                ERPsalesmanid = SqlHelper.NvlString(SelectT031ErpDessalesman.ExecuteScalar(connection), string.Empty);

                //-- decode ganesh salesman name

                const string T030Dessalesman = "t030user";
                SqlSelect SelectT030Dessalesman = new SqlSelect();
                SelectT030Dessalesman.SelectFields.AddTableField(T030Dessalesman, "desusr");
                SelectT030Dessalesman.Constraints.AddConstraint(T030Dessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);
                SelectT030Dessalesman.Top = 1;
                Desusr = SqlHelper.NvlString(SelectT030Dessalesman.ExecuteScalar(connection), string.Empty);
                Desusr = m_order.CODUSRMOD.ToString() + "&" + Desusr;

                //WAREHOUSE
                const string userDivTable = "t031userdiv";
                SqlSelect selectWarehouse = new SqlSelect();
                selectWarehouse.SelectFields.AddTableField(userDivTable, "CODWHSDELIV");
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectWarehouse.Top = 1;



                DesWhs = string.Empty;
                string codWhsDeliv = SqlHelper.NvlString(selectWarehouse.ExecuteScalar(connection), string.Empty);
                if (!string.IsNullOrWhiteSpace((codWhsDeliv)))
                    DesWhs = string.Format("{0} - {1}", XApp.Decodes().DecodeTrap(CodDiv, "WHS", codWhsDeliv, string.Empty),
                        XApp.Decodes().GetOptInfoStringValue(CodDiv, "WHS", codWhsDeliv, QTABS.VALUES.REFDAT_WHS.WHS_ADDRESS, string.Empty));


                // GET LOAD COUNTER GANESH DCODE..
                //--12
                const string OrderHeadTableCount = "t100ordhead";
                SqlSelect SelectOrderHeadTableCount = new SqlSelect();
                SelectOrderHeadTableCount.SelectFields.AddTableField(OrderHeadTableCount, "dteord");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "codtypord", SqlRelationalOperator.Equal, "50");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                SelectOrderHeadTableCount.OrderBy.AddTableField(OrderHeadTableCount, "dteord", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                SelectOrderHeadTableCount.Top = 1;

                string dteord = SqlHelper.NvlString(SelectOrderHeadTableCount.ExecuteScalar(connection), string.Empty);

                if (string.IsNullOrWhiteSpace((dteord)))
                {

                    LoadCount = "2";
                }
                else
                {
                    const string OrderHeadTableCountLoad = "t100ordhead";
                    SqlSelect SelectOrderHeadTableCountLoad = new SqlSelect();
                    SelectOrderHeadTableCountLoad.SelectFields.AddFunction("COUNT", "COUNTLoad", OrderHeadTableCountLoad, "dteord");
                    // SelectOrderHeadTableCountLoad.SelectFields.AddTableField(OrderHeadTableCountLoad, "dteord");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "codtypord", SqlRelationalOperator.Equal, "50");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "dteord", SqlRelationalOperator.GreaterOrEqual, DateTime.Now);
                    SelectOrderHeadTableCountLoad.Top = 1;
                    var Count = SelectOrderHeadTableCountLoad.ExecuteScalar(connection);
                    if (Convert.ToInt16(Count) > 0)
                    {
                        LoadCount = (1 + Convert.ToInt16(Count)).ToString();
                    }
                    LoadCount = "2";

                }



                String DocNumberVal = string.IsNullOrWhiteSpace(m_order.NUMDOC)
                    ? m_order.NUMORD.ToString(CultureInfo.InvariantCulture)
                    : m_order.NUMDOC;
                DocNumber = DesRouteID + "-INC-" + DocNumberVal;
            }

            protected virtual void InitOrderRows(XConnection connection)
            {
                foreach (var orderRow in m_order.OrderRowDetails)
                {
                    m_orderRows.Add(new OrderRowInfo(CodDiv, orderRow.CODART, orderRow.CODTYPROW, orderRow.DESART, orderRow.UMINV, orderRow.QTYINV));

                    foreach (var orderRowBatch in orderRow.OrderRowBatchDetails)
                        m_orderRows.Add(new OrderRowInfo(CodDiv, string.Empty, string.Empty, orderRowBatch.IDBATCH + " - " + orderRowBatch.DTEEXPIRE.ToString("dd/MM/yyyy"),
                            string.Empty, orderRowBatch.QTYINV, true));

                    m_orderRows.Last().LastInGroup = true;
                }
            }

            protected virtual Collection<OrderRowInfo> GetOrderRows()
            {
                return m_orderRows;
            }

            public override object GenerateReport(SessionContext sc)
            {
                //load template
                string reportTemplate = OrderReportEngine.GetTemplatesPath() + "\\" + GetTemplateName() + ".rpt";
                ReportDocument rd = new ReportDocument();
                rd.Load(reportTemplate);

                //initialize and set the data source for the main report
                OrderReportEngine.InitializeReport(rd, sc);
                Collection<OrderInfo> source = new Collection<OrderInfo>();
                source.Add(this);
                rd.SetDataSource(source);

                //initialize and set the data source for the subReports
                foreach (ReportDocument subDocument in rd.Subreports)
                {
                    switch (subDocument.Name)
                    {
                        case "OrderSubreportVan.rpt":
                            {
                                OrderReportEngine.InitializeReport(subDocument, sc);
                                subDocument.SetDataSource(GetOrderRows());
                                break;
                            }
                    }
                }

                return rd;
            }
        }
    }




    /// <summary>
    /// Data model for the orders with type == 50
    /// </summary>
    namespace OrderReport_type_50_Cust_old
    {
        public class OrderRowInfo : OrderReport_macrotype_5.OrderRowInfo
        {
            public string UmRemainder { get; private set; }
            public string LoadInQtyInteger { get; private set; }
            public string LoadInQtyRemainder { get; private set; }
            public string TotalQtyInteger { get; private set; }
            public string TotalQtyRemainder { get; private set; }

            public OrderRowInfo(string codDiv, string codArt, string codTypRow, string desArt, string umWhs, decimal qtyOrd,
                string umRemainder, decimal loadInQtyInteger, decimal loadInQtyRemainder, decimal totalQtyInteger, decimal totalQtyRemainder) :
                base(codDiv, codArt, codTypRow, desArt, umWhs, qtyOrd, false)
            {
                LastInGroup = true;
                UmRemainder = umRemainder;
                LoadInQtyInteger = loadInQtyInteger.ToString();
                LoadInQtyRemainder = loadInQtyRemainder.ToString();
                TotalQtyInteger = totalQtyInteger.ToString();
                TotalQtyRemainder = totalQtyRemainder.ToString();
            }
        }

        public class OrderInfo : OrderReport_macrotype_5.OrderInfo
        {
            private new Collection<OrderReport_macrotype_5.OrderRowInfo> m_orderRows = new Collection<OrderReport_macrotype_5.OrderRowInfo>();

            public OrderInfo(OrderReportArgs order, SessionContext sc)
                : base(order, sc)
            {
            }

            protected override void InitOrderRows(XConnection connection)
            {
                //init order helper
                SM1OrderHelper helper = new SM1OrderHelper(m_order);
                helper.WhsBalances = new Dictionary<string, OrdWhsBalance>();
                OrdWhsBalance whsBalance = new SM1OrderEngine().CalculateWarehouseBalance(m_order.CODWHS, m_order.DOCUMENTKEY, m_sc);
                helper.WhsBalances.Add(whsBalance.CODWHS, new SM1OrderEngine().CalculateWarehouseBalance(m_order.CODWHS, m_order.DOCUMENTKEY, m_sc));
                //process each row
                foreach (var orderRow in m_order.OrderRowDetails)
                {
                    OrdProdWhsBalance prodBal = helper.GetWhsBalance(orderRow.CODART, orderRow.CODTYPROW);
                    string umWhs = orderRow.Product.UMWHS;
                    decimal qtyWhs = orderRow.Product.ConvertQuantity(orderRow.QTYORD, orderRow.UMORD, umWhs);
                    string umRemainder = SM1OrderHelper.GetRemainderUM(orderRow);
                    decimal loadInQtyInteger = 0;
                    decimal loadInQtyRemainder = 0;
                    decimal totalQtyInteger = 0;
                    decimal totalQtyRemainder = 0;
                    if (prodBal == null)
                        prodBal = new OrdProdWhsBalance(orderRow.CODART, orderRow.CODDIV, orderRow.CODTYPROW, 0, orderRow.UMORD, 0);
                    if (umRemainder != null)
                    {
                        orderRow.Product.SplitQuantity(prodBal.QTYORD, prodBal.UMORD, umWhs, umRemainder, out loadInQtyInteger, out loadInQtyRemainder);
                        totalQtyInteger = loadInQtyInteger + qtyWhs;
                        totalQtyRemainder = loadInQtyRemainder;
                    }
                    else
                    {
                        loadInQtyInteger = prodBal.QTYORD;
                        totalQtyInteger = qtyWhs + loadInQtyInteger;
                    }
                    m_orderRows.Add(new OrderRowInfo(CodDiv, orderRow.CODART, orderRow.CODTYPROW, orderRow.DESART, umWhs, qtyWhs,
                        umRemainder, loadInQtyInteger, loadInQtyRemainder, totalQtyInteger, totalQtyRemainder));
                }
            }

            protected override Collection<OrderReport_macrotype_5.OrderRowInfo> GetOrderRows()
            {
                return m_orderRows;
            }

            public override object GenerateReport(SessionContext sc)
            {
                return base.GenerateReport(sc);
            }
        }
    }

    namespace OrderReport_type_50_Cust
    {



        public class OrderInfo : OrderReportArgs
        {
            protected SM1Order m_order;
            protected SessionContext m_sc;

            protected Collection<OrderRowInfo> m_orderRows = new Collection<OrderRowInfo>();


            public string DocNumber { get; protected set; }
            public string DesTypOrd { get; protected set; }
            public string DesRoute { get; protected set; }
            public string DesPlate { get; protected set; }
            public string DesWhs { get; protected set; }
            public string MyField { get; protected set; }
            public string ShipDocNo { get; protected set; }
            public string SalesmanCode { get; protected set; }
            public string Desusr { get; protected set; }
            public string DocNumordRef { get; protected set; }
            // ---- COD added By Ganesh B Das Dcode
            public string PrintedBy { get; protected set; }
            public string DocStatus { get; protected set; }
            public string printed { get; protected set; }
            public string VanPleate { get; protected set; }
            public string ERPsalesmanid { get; protected set; }

            public new string MacroType { get; protected set; }

            public string PrintedBy51 { get; protected set; }
            public string DocStatus51 { get; protected set; }
            public string printed51 { get; protected set; }
            //TODO - Add other properties that you want to use in the Chrystal Report
            //.....

            public OrderInfo(OrderReportArgs order, SessionContext sc)
                : base(order)
            {
                if (!OrderReportEngine.ExistsOrderTemplate(this))
                    return;

                m_sc = sc;

                using (XConnection connection = XApp.CreateDefaultXConnection())
                {
                    connection.Open();
                    try
                    {
                        InitOrderData(connection);
                        InitOrderRows(connection);
                    }
                    catch { }
                    finally
                    {
                        connection.Close();
                    }
                }
            }

            private void InitOrderData(XConnection connection)
            {


                //load order
                SM1OrderEngine engine = new SM1OrderEngine();
                m_order = engine.LoadSM1Order(connection, CodUsr, NumOrd, false, m_sc);

                DesTypOrd = OrderReportEngine.DecodeQtab(CodDiv, QTABS.TABLES.CTORD, m_order.CODTYPORD);

                //ROUTE
                SqlSelect selectRoute = new SqlSelect();
                selectRoute.SelectFields.AddTableField("t030user", "desusr");
                selectRoute.Constraints.AddConstraint("t030user", "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSR);
                //selectRoute.Constraints.AddConstraint("t030user", "IDROUTE", SqlRelationalOperator.Equal, m_order.IDROUTE);
                selectRoute.Top = 1;
                //TODO - Add other fields that you want to use in the Chrystal Report
                //.....

                DesRoute = SqlHelper.NvlString(selectRoute.ExecuteScalar(connection), string.Empty);
                // Test = DesRoute;



                //SELLING DAY
                const string seelingDayTable = "ta0300sellingday";
                SqlSelect selectSeelingDay = new SqlSelect();
                selectSeelingDay.SelectFields.AddTableField(seelingDayTable, "CODVEHICLE");
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODSALESMAN", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectSeelingDay.Constraints.AddConstraint(seelingDayTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectSeelingDay.OrderBy.AddTableField(seelingDayTable, "DTESTART", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                selectSeelingDay.Top = 1;

                string codVehicle = SqlHelper.NvlString(selectSeelingDay.ExecuteScalar(connection), string.Empty);

                VanPleate = string.Empty;
                if (!string.IsNullOrWhiteSpace((codVehicle)))
                    VanPleate = XApp.Decodes().GetOptInfoStringValue(CodDiv, "VEHICLE", codVehicle, QTABS.VALUES.REFDAT_VEHICLE.DESPLATE, string.Empty);
                //-- code added by ganesh Dcode

                //mady 07/09/2021



                DocStatus = XApp.Decodes().Decode(CodDiv, "ORDST", m_order.CODSTATUS);



                const string T031ErpDessalesman = "t031userdiv";
                SqlSelect SelectT031ErpDessalesman = new SqlSelect();
                SelectT031ErpDessalesman.SelectFields.AddTableField(T031ErpDessalesman, "CODPARTY");
                SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);
                SelectT031ErpDessalesman.Constraints.AddConstraint(T031ErpDessalesman, "CODDIV", SqlRelationalOperator.Equal, m_order.CODDIV);
                SelectT031ErpDessalesman.Top = 1;
                ERPsalesmanid = SqlHelper.NvlString(SelectT031ErpDessalesman.ExecuteScalar(connection), string.Empty);

                const string T030Dessalesman = "t030user";
                SqlSelect SelectT030Dessalesman = new SqlSelect();
                SelectT030Dessalesman.SelectFields.AddTableField(T030Dessalesman, "desusr");
                SelectT030Dessalesman.Constraints.AddConstraint(T030Dessalesman, "CODUSR", SqlRelationalOperator.Equal, m_order.CODUSRCREREAL);
                SelectT030Dessalesman.Top = 1;

                Desusr = SqlHelper.NvlString(SelectT030Dessalesman.ExecuteScalar(connection), string.Empty);
                Desusr = m_order.CODUSRMOD.ToString() + "&" + Desusr;
                //----------------------


                const string valuesRPT = "TZ_REPORT";
                SqlSelect getRPT = new SqlSelect();
                getRPT.SelectFields.AddTableField(valuesRPT, "NUMORD");
                getRPT.Constraints.AddConstraint(valuesRPT, "FLG_RPT", SqlRelationalOperator.Equal, "-1");
                getRPT.Constraints.AddConstraint(valuesRPT, "NUMORD", SqlRelationalOperator.Equal, m_order.NUMORD);
                getRPT.Top = 1;
                var gerRecord = getRPT.Execute(connection);

                SqlUpdate upd = new SqlUpdate("TZ_REPORT", "T");
                upd.Constraints.AddConstraint("T", "NUMORD", SqlRelationalOperator.Equal, m_order.NUMORD);
                upd.Constraints.AddConstraint("T", "FLG_RPT", SqlRelationalOperator.Equal, "-1");
                upd.SetFields.Add("RPT_STATUS", "RIPRENTED");
                upd.SetFields.Add("USRLOGIN", m_sc.CodUser);
                upd.SetFields.Add("DESUSRLOGIN", m_sc.DesUsr);
                upd.SetFields.Add("DESSTATUS", DocStatus);
                //----------------MADY------------------

                const string reportInfo = "TZ_REPORT";
                SqlInsertValues insertDocs = new SqlInsertValues();
                insertDocs.TableName = reportInfo;

                if (gerRecord.Rows.Count > 0)
                {
                    // insertDocs.Values.Add("FLG_RPT", "0");
                    upd.Execute(connection);

                }
                else
                {
                    insertDocs.Values.Add("NUMORD", m_order.NUMORD);
                    insertDocs.Values.Add("CODUSR", m_order.CODUSR);
                    insertDocs.Values.Add("CODDIV", m_order.CODDIV);
                    insertDocs.Values.Add("CODSTATUS", m_order.CODSTATUS);
                    insertDocs.Values.Add("DESSTATUS", DocStatus);
                    insertDocs.Values.Add("USRLOGIN", m_sc.CodUser);
                    insertDocs.Values.Add("DESUSRLOGIN", m_sc.DesUsr);
                    insertDocs.Values.Add("DTEPRINT", DateTime.Now);
                    insertDocs.Values.Add("FLG_RPT", "-1");
                    insertDocs.Values.Add("RPT_STATUS", "ORIGINAL");

                    insertDocs.Execute(connection);
                }







                //WAREHOUSE
                const string userDivTable = "t031userdiv";
                SqlSelect selectWarehouse = new SqlSelect();
                selectWarehouse.SelectFields.AddTableField(userDivTable, "CODWHSDELIV");
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODDIV", SqlRelationalOperator.Equal, CodDiv);
                selectWarehouse.Constraints.AddConstraint(userDivTable, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                selectWarehouse.Top = 1;

                DesWhs = string.Empty;
                string codWhsDeliv = SqlHelper.NvlString(selectWarehouse.ExecuteScalar(connection), string.Empty);
                if (!string.IsNullOrWhiteSpace((codWhsDeliv)))
                    DesWhs = string.Format("{0} - {1}", XApp.Decodes().DecodeTrap(CodDiv, "WHS", codWhsDeliv, string.Empty),
                        XApp.Decodes().GetOptInfoStringValue(CodDiv, "WHS", codWhsDeliv, QTABS.VALUES.REFDAT_WHS.WHS_ADDRESS, string.Empty));

                //delivery date in report Mady 01/09/2020
                MacroType = m_order.DTEDELIV.ToString();

                // GET LOAD COUNTER GANESH DCODE..
                //--12
                const string OrderHeadTableCount = "t100ordhead";
                SqlSelect SelectOrderHeadTableCount = new SqlSelect();
                SelectOrderHeadTableCount.SelectFields.AddTableField(OrderHeadTableCount, "dteord");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "codtypord", SqlRelationalOperator.Equal, "50");
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                SelectOrderHeadTableCount.Constraints.AddConstraint(OrderHeadTableCount, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                SelectOrderHeadTableCount.OrderBy.AddTableField(OrderHeadTableCount, "dteord", Xtel.SM1.Core.Data.QueryObj.SortDirection.Desc);
                SelectOrderHeadTableCount.Top = 1;

                string dteord = SqlHelper.NvlString(SelectOrderHeadTableCount.ExecuteScalar(connection), string.Empty);
                //String  DocNumordRe2;
                if (!string.IsNullOrWhiteSpace((dteord)))
                {

                    const string OrderHeadTableCountLoad = "t100ordhead";
                    SqlSelect SelectOrderHeadTableCountLoad = new SqlSelect();
                    //SelectOrderHeadTableCountLoad.SelectFields.AddFunction("COUNT", "COUNTLoad", OrderHeadTableCountLoad, "dteord");
                    SelectOrderHeadTableCountLoad.SelectFields.AddTableField(OrderHeadTableCountLoad, "numord");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "codtypord", SqlRelationalOperator.Equal, "50");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODWHS", SqlRelationalOperator.Equal, codWhsDeliv);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODUSR", SqlRelationalOperator.Equal, m_order.CODEUSR);
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "CODSTATUS", SqlRelationalOperator.Equal, "12");
                    SelectOrderHeadTableCountLoad.Constraints.AddConstraint(OrderHeadTableCountLoad, "dteord", SqlRelationalOperator.GreaterOrEqual, DateTime.Now);
                    SelectOrderHeadTableCountLoad.Top = 1;
                    // DocNumordRe2 = SelectOrderHeadTableCountLoad.ExecuteScalar(connection);

                }
                else
                {
                    DocNumordRef = "";
                }


                //DocNumber
                String DocNumberOriginal = string.IsNullOrWhiteSpace(m_order.NUMDOC)
                    ? m_order.NUMORD.ToString(CultureInfo.InvariantCulture)
                    : m_order.NUMDOC;
                //dECODE GANESH
                DocNumber = DesPlate + "INC" + DocNumberOriginal.Trim();

                //TODO - init other properties
                MyField = "Something";

                //TODO - init other properties
                ShipDocNo = DocNumber;
                // DesPlate = Desusr;
            }

            protected virtual void InitOrderRows(XConnection connection)
            {
                foreach (var orderRow in m_order.OrderRowDetails)
                {
                    m_orderRows.Add(new OrderRowInfo(CodDiv, orderRow.CODART, orderRow.CODTYPROW, orderRow.DESART, orderRow.UMINV, orderRow.QTYINV));

                    foreach (var orderRowBatch in orderRow.OrderRowBatchDetails)
                        m_orderRows.Add(new OrderRowInfo(CodDiv, string.Empty, string.Empty, orderRowBatch.IDBATCH + " - " + orderRowBatch.DTEEXPIRE.ToString("dd/MM/yyyy"),
                            string.Empty, orderRowBatch.QTYINV, true));

                    m_orderRows.Last().LastInGroup = true;
                }
            }

            protected virtual Collection<OrderRowInfo> GetOrderRows()
            {
                return m_orderRows;
            }

            public override object GenerateReport(SessionContext sc)
            {
                //load template
                string reportTemplate = OrderReportEngine.GetTemplatesPath() + "\\" + GetTemplateName() + ".rpt";
                ReportDocument rd = new ReportDocument();
                rd.Load(reportTemplate);

                //initialize and set the data source for the main report
                OrderReportEngine.InitializeReport(rd, sc);
                Collection<OrderInfo> source = new Collection<OrderInfo>();
                source.Add(this);
                rd.SetDataSource(source);

                //initialize and set the data source for the subReports
                foreach (ReportDocument subDocument in rd.Subreports)
                {
                    switch (subDocument.Name)
                    {
                        case "OrderSubreportVan.rpt":
                            {
                                OrderReportEngine.InitializeReport(subDocument, sc);
                                subDocument.SetDataSource(GetOrderRows());
                                break;
                            }
                    }
                }

                return rd;
            }
        }

        public class OrderRowInfo
        {
            public string CodArt { get; private set; }
            public string DesTypRow { get; private set; }
            public string DesArt { get; private set; }
            public string Um { get; private set; }
            public string Qty { get; private set; }
            public bool IsBatch { get; private set; }
            public bool LastInGroup { get; set; }
            public string MyNewProp { get; set; }

            //TODO - Add other properties that you want to use in the Chrystal Report
            //.....

            public OrderRowInfo(string codDiv, string codArt, string codTypRow, string desArt, string um, decimal qty, bool isBatch = false)
            {
                CodArt = codArt;
                DesTypRow = OrderReportEngine.DecodeQtab(codDiv, QTABS.TABLES.TYROW, codTypRow);
                DesArt = desArt;
                Um = um;
                Qty = qty.ToString();
                IsBatch = isBatch;
                MyNewProp = "ABC";
            }
        }
    }
}