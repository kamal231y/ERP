using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using sms.DAL;
using System.Data.SqlClient;
using System.Threading;
using System.IO;
namespace sms
{
    public partial class Form1 : Form
    {
        private int xPos = 0;
        public static string GlobalLoginUserName = "";
        public static string GlobalLoginUserType = "";
        public static string GlobalFAYear = "";
        public static byte[] cancel;
        public static string schname = "";
        public static bool sale_reciept = true;
        public static bool fee_reciept = true;
        public static bool report_reciept = true;
        public static bool SMS = true;
        public static bool board = true;
        public static bool feesms = true;
        public static bool fee_reminder = true;
        public static string session = "";
        public static string SMSKEY = "";
        public static int TC_FORMAT = 0;
       List<CONN_STRING> listdatabase = new List<CONN_STRING>(); 

       // dataclass dc = new dataclass();
        SqlConnection sc;
        public Form1()
        {
            binddatabase();

       
            Login login = new Login();
            login.ShowDialog();
         
            InitializeComponent();
        
            BindFormTile();
            dataclass dc = new dataclass();

            sc = dc.con();
            if (GlobalLoginUserType.ToLower() != "superadmin")
            {
                //backupToolStripMenuItem.Visible = false;
                //utilityToolStripMenuItem.Visible = false;
            }
            txtUser.Text = GlobalLoginUserName.ToString();
            txtDate.Text = DateTime.Now.ToString();
            txtTerminal.Text = sc.WorkstationId.ToString();
        }


       void binddatabase()
        {
           
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con1st();
                SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM  SOF_CONNECTION", sc);
                sc.Open();
                DataTable dt = new DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    listdatabase.Clear();
                    for (int i = 0; i < dt.Rows.Count ; i++)
                    {
                        listdatabase.Add(new CONN_STRING());
                        listdatabase[i].CON_ID = dt.Rows[i]["CON_ID"].ToString();
                        listdatabase[i].CON_NAME = dt.Rows[i]["CON_NAME"].ToString();
                        listdatabase[i].SERVER_NAME = dt.Rows[i]["SERVER_NAME"].ToString();
                        listdatabase[i].DATABASE_NAME = dt.Rows[i]["DATABASE_NAME"].ToString();
                        listdatabase[i].USER_ID = dt.Rows[i]["USERID"].ToString();
                        listdatabase[i].PWD = dt.Rows[i]["PASWD"].ToString();
                        listdatabase[i].ACCDM_YEAR = dt.Rows[i]["acad_year"].ToString();
                        listdatabase[i].ISDEFAULT = dt.Rows[i]["IS_DEFAULT"].ToString();
                    }

                }

               
           
        }


        private int xpos = 0, ypos = 0;
        private void bind_session()
        {
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM tblFinancialYearControl where fayear='" + sms.Form1.GlobalFAYear.ToString() + "'", sc);
                    sc.Open();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        Form1.session = dt.Rows[0]["session"].ToString();

                    }
                }
                catch (Exception EX) { }

            }
        }

        private void bind_data()
        {
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM SETTING_T ", sc);
                    sc.Open();
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    Form1.fee_reciept = Convert.ToBoolean(dt.Rows[0][0].ToString());
                    Form1.sale_reciept = Convert.ToBoolean(dt.Rows[0][1].ToString());
                    Form1.report_reciept = Convert.ToBoolean(dt.Rows[0][2].ToString());
                    Form1.SMS = Convert.ToBoolean(dt.Rows[0][3].ToString());
                    Form1.feesms = Convert.ToBoolean(dt.Rows[0][5].ToString());
                    Form1.fee_reminder = Convert.ToBoolean(dt.Rows[0][6].ToString());
                    Form1.SMSKEY = Convert.ToString(dt.Rows[0][7].ToString());
                    Form1.TC_FORMAT =Convert.ToInt32( dt.Rows[0]["TC_FORMAT"].ToString());
                }
                catch (Exception EX) { }

            }
        }

        private void ExitFromSystem()
        {
            process p = new process();
            Thread oThread = new Thread(new ThreadStart(p.processShow));
            oThread.Start();
            int i = CreateBackup.AutoBackup();
            if (i > 0)
                MessageBox.Show("No Backup Folder");
            oThread.Abort();
            Application.Exit();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            //this.BackColor = Color.Green;
            // InitializeComponent();
            var timer = new System.Windows.Forms.Timer();
            timer.Interval = 100;
            timer.Tick += new EventHandler(timer1_Tick);
            timer.Start();  
  
            
            
            //txtDate.Text = DateTime.Now.ToString();
            //txtUser.Text = GlobalLoginUserName.ToString();
            //txtTerminal.Text = sc.WorkstationId.ToString();
            txtDate.Text = DateTime.Now.ToString();
            txtUser.Text = GlobalLoginUserName.ToString();
            txtTerminal.Text = sc.WorkstationId.ToString();
            IsMdiContainer = true;
            if (GlobalLoginUserType.ToLower() == "superadmin")
            {

            }
            else
            {
                MenuList();
                 events();
                 subdetails();
            }
            bind_data();
            bind_session();
            System.DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            System.DateTime otherDate = new DateTime(2023, 04, 15);
            System.DateTime otherDate2 = new DateTime(2023, 04, 01);
            if (dt1 > otherDate)
            {
                MessageBox.Show("Please Renew Your Software. Contact us at : 9560522802", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
            else if (otherDate2 < dt1)
            {
                MessageBox.Show("Please renew Your Software in 10 days", "Confirm", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            else
            {
                
            }
        }
        private void BindFormTile()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from COMPANY_T    ", sc);
            //where com_name=where com_name='S.K.SEMINARY(RESIDENTIAL HIGH SCHOOL)'    where com_name='SUCCESS MISSION SCHOOL'
            DataTable dt = new DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                this.Text = dt.Rows[0]["COM_NAME"].ToString() + "( " + dt.Rows[0]["session"].ToString() + " )";
                byte[] p = (byte[])dt.Rows[0]["logo"];
                MemoryStream ms = new MemoryStream(p);
                this.BackgroundImage = Image.FromStream(ms);
                this.BackgroundImageLayout = ImageLayout.Stretch;
                this.BackgroundImageLayout = ImageLayout.Tile;
                this.BackgroundImageLayout = ImageLayout.Center;
                schname = dt.Rows[0]["COM_NAME"].ToString();
            }
            else
            {
                this.Text = "";
                Environment.Exit(0);
            }
        }
        private void events()
        {
            DataTable ds = new DataTable();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("Find_Events", sc);
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@selected", 0);
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                frmEventSMS obj = new frmEventSMS();
                obj.ShowDialog();
            }
        }
        private void subdetails()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            
           
            SqlDataAdapter da = new SqlDataAdapter("subdetails", sc);
            da.SelectCommand.CommandTimeout = 5000;
            da.SelectCommand.CommandType = CommandType.StoredProcedure;
            da.SelectCommand.Parameters.AddWithValue("@student", "%");
            DataTable ds = new DataTable();
            da.Fill(ds);
            if (ds.Rows.Count > 0)
            {
                btnstudent.Text = ds.Rows[0][1].ToString();
                btnactive.Text = ds.Rows[0][2].ToString();
                btninactive.Text = ds.Rows[0][3].ToString();
                btnbirthday.Text = ds.Rows[0][4].ToString();
                btnpresent.Text = ds.Rows[0][5].ToString();
                btnabsent.Text = ds.Rows[0][6].ToString();
                btnholiday.Text = ds.Rows[0][7].ToString();
                btnhdate.Text = ds.Rows[0][8].ToString();
            }
        }
        private void MenuList()
        {
            dataclass dc = new dataclass();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblRights where UserName='" + GlobalLoginUserName + "'", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            sc = dc.con();
            SqlCommand cmd = new SqlCommand("select * from tblRights where UserName='" + GlobalLoginUserName + "'", sc);
            sc.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.HasRows)
            {
                dr.Read();

                locationToolStripMenuItem1.Visible = Convert.ToBoolean(dr["M1"]);
                studentMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["M2"]);
                //departmentToolStripMenuItem.Visible = Convert.ToBoolean(dr["M3"]);
                //designationToolStripMenuItem.Visible = Convert.ToBoolean(dr["M4"]);
                vehicleMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["M5"]);
                inveToolStripMenuItem.Visible = Convert.ToBoolean(dr["M6"]);
                commentMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["M7"]);
                bloodGroupToolStripMenuItem1.Visible = Convert.ToBoolean(dr["M8"]);
                categoryToolStripMenuItem.Visible = Convert.ToBoolean(dr["M9"]);
                casteToolStripMenuItem.Visible = Convert.ToBoolean(dr["M10"]);
                documentToolStripMenuItem.Visible = Convert.ToBoolean(dr["M11"]);
                //departmentToolStripMenuItem.Visible = Convert.ToBoolean(dr["M12"]);
                //designationToolStripMenuItem.Visible = Convert.ToBoolean(dr["M13"]);
                vehicleTypeToolStripMenuItem.Visible = Convert.ToBoolean(dr["M14"]);
                vehicleModelToolStripMenuItem.Visible = Convert.ToBoolean(dr["M15"]);
                vehicleMakeToolStripMenuItem.Visible = Convert.ToBoolean(dr["M16"]);
                publisherToolStripMenuItem.Visible = Convert.ToBoolean(dr["M17"]);

                formSaleToolStripMenuItem.Visible = Convert.ToBoolean(dr["REG1"]);
                siddstudentRegistrationToolStripMenuItem1.Visible = Convert.ToBoolean(dr["REG2"]);
                admissionTestSchedueToolStripMenuItem.Visible = Convert.ToBoolean(dr["REG3"]);
                admissionTestResultToolStripMenuItem.Visible = Convert.ToBoolean(dr["REG4"]);
                toolStripMenuItem17.Visible = Convert.ToBoolean(dr["REG5"]);
                registrationSaleDetailToolStripMenuItem.Visible = Convert.ToBoolean(dr["REG6"]);
                registrationReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["REG7"]);
                formMasterToolStripMenuItem1.Visible = Convert.ToBoolean(dr["REG8"]);

                toolStripMenuItem1.Visible = Convert.ToBoolean(dr["STU1"]);
                toolStripMenuItem4.Visible = Convert.ToBoolean(dr["STU2"]);
                admissionToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU3"]);
                toolStripMenuItem3.Visible = Convert.ToBoolean(dr["STU3"]);
                admissionDetailsToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU4"]);
                toolStripMenuItem33.Visible = Convert.ToBoolean(dr["STU5"]);
                subjectUploadToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU6"]);
                studentSubjectToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU7"]);
                stuToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU8"]);
                toolStripMenuItem16.Visible = Convert.ToBoolean(dr["STU9"]);
                toolStripMenuItem70.Visible = Convert.ToBoolean(dr["STU10"]);
                studentDataUploadByExcelToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU11"]);
                studentDataUpdateByExcelToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU11"]);

              // totalAttendanceToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU13"]);
                toolStripMenuItem22.Visible = Convert.ToBoolean(dr["STU14"]);
                //toolStripMenuItem70.Visible = Convert.ToBoolean(dr["STU15"]);
                toolStripMenuItem77.Visible = Convert.ToBoolean(dr["STU16"]);
                //tCToolStripMenuItem1.Visible = Convert.ToBoolean(dr["STU17"]);
                //studentDataUploadByExcelToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU18"]);
                //subjectUploadToolStripMenuItem.Visible = Convert.ToBoolean(dr["STU19"]);

                feeHeadToolStripMenuItem.Visible = Convert.ToBoolean(dr["FEE1"]);
                classFeeToolStripMenuItem.Visible = Convert.ToBoolean(dr["FEE2"]);
                toolStripMenuItem23.Visible = Convert.ToBoolean(dr["FEE3"]);
                //toolStripMenuItem24.Visible = Convert.ToBoolean(dr["FEE4"]);
                feeCollectionToolStripMenuItem.Visible = Convert.ToBoolean(dr["FEE5"]);
                toolStripMenuItem27.Visible = Convert.ToBoolean(dr["FEE6"]);

                toolStripMenuItem26.Visible = Convert.ToBoolean(dr["FEE7"]);
                toolStripMenuItem28.Visible = Convert.ToBoolean(dr["FEE8"]);
                toolStripMenuItem29.Visible = Convert.ToBoolean(dr["FEE9"]);
                toolStripMenuItem30.Visible = Convert.ToBoolean(dr["FEE10"]);
                feeRecieptReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["FEE11"]);
                receiptSummaryReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["FEE12"]);
                feeReceiptReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["FEE13"]);
                feeExemtedReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["FEE14"]);
                feeRecieptReportToolStripMenuItem1.Visible = Convert.ToBoolean(dr["FEE15"]);
                //sub.Visible = Convert.ToBoolean(dr["FEE16"]);
                toolStripMenuItem18.Visible = Convert.ToBoolean(dr["T1"]);
                toolStripMenuItem19.Visible = Convert.ToBoolean(dr["T2"]);
                busStopToolStripMenuItem.Visible = Convert.ToBoolean(dr["T3"]);
                toolStripMenuItem20.Visible = Convert.ToBoolean(dr["T4"]);
                studentBusStopToolStripMenuItem.Visible = Convert.ToBoolean(dr["T5"]);
                busStopReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["T6"]);

                busRouteReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["T7"]);
                vehicleReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["T8"]);

                accountHeadsToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc1"]);
                ledgerToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc2"]);
                cashVoucherToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc3"]);
                bankVoucherToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc4"]);
                jToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc5"]);
                //cashBookToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc6"]);

                dayBookToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc7"]);
                //journalBookToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc8"]);
                //toolStripMenuItem63.Visible = Convert.ToBoolean(dr["Acc9"]);
                //trialBalanceToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc10"]);
                ledgerReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["Acc11"]);

                normalSmsForStudentToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms1"]);
                normalSmsForSingleStudentToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms2"]);
                normalSmsForAllStaffToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms3"]);
                normalSmsForSingleStaffToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms4"]);
                smsBalanceToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms5"]);
                toolStripMenuItem55.Visible = Convert.ToBoolean(dr["sms6"]);
                toolStripMenuItem75.Visible = Convert.ToBoolean(dr["sms7"]);

                smsReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms9"]);
                smsCategoryWiseToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms11"]);
                sToolStripMenuItem1.Visible = Convert.ToBoolean(dr["sms12"]);

                employeeToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payroll1"]);
                employeeAttendanceToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payroll2"]);
                salaryToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payroll3"]);
                toolStripEmpDetail.Visible = Convert.ToBoolean(dr["Payroll4"]);
                employeeSalaryReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payroll5"]);
                monthlySalaryReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payroll6"]);
                allowenceTaxesToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payroll7"]);
                employeeTypeToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payroll8"]);
                attedenceToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payr10"]);
                dailyTimingToolStripMenuItem.Visible = Convert.ToBoolean(dr["Payr11"]);

                toolStripMenuItem56.Visible = Convert.ToBoolean(dr["Inv1"]);
                toolStripMenuItem73.Visible = Convert.ToBoolean(dr["Inv2"]);
                bookItemToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv3"]);
                purchaseToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv4"]);
                issueSaleToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv5"]);
                inToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv6"]);

                purchaseReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv7"]);
                stockReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv8"]);
                purchaseReportToolStripMenuItem1.Visible = Convert.ToBoolean(dr["Inv8"]);
                sToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv9"]);
                bookGroupToolStripMenuItem.Visible = Convert.ToBoolean(dr["Inv10"]);
                changePasswordToolStripMenuItem.Visible = Convert.ToBoolean(dr["SEQ1"]);
                backupToolStripMenuItem.Visible = Convert.ToBoolean(dr["SEQ2"]);
                restoreToolStripMenuItem.Visible = Convert.ToBoolean(dr["SEQ3"]);
                userToolStripMenuItem.Visible = Convert.ToBoolean(dr["SEQ4"]);
                userPermissionToolStripMenuItem.Visible = Convert.ToBoolean(dr["SEQ5"]);
                toolStripMenuItem72.Visible = Convert.ToBoolean(dr["SEQ6"]);
                settingToolStripMenuItem.Visible = Convert.ToBoolean(dr["SEQ7"]);

                headToolStripMenuItem.Visible = Convert.ToBoolean(dr["Exam1"]);
                gradingSystemToolStripMenuItem.Visible = Convert.ToBoolean(dr["Exam2"]);
                toolStripMenuItem5.Visible = Convert.ToBoolean(dr["Exam3"]);
                toolStripMenuItem12.Visible = Convert.ToBoolean(dr["Exam4"]);
                toolStripMenuItem14.Visible = Convert.ToBoolean(dr["Exam5"]);
                subjectMarksToolStripMenuItem.Visible = Convert.ToBoolean(dr["Exam6"]);
                toolStripMenuItem10.Visible = Convert.ToBoolean(dr["Exam7"]);
                admitCardToolStripMenuItem.Visible = Convert.ToBoolean(dr["Exam8"]);
                finalReportCardToolStripMenuItem.Visible = Convert.ToBoolean(dr["Exam9"]);

                periodToolStripMenuItem.Visible = Convert.ToBoolean(dr["seh1"]);
                teacherSubjectToolStripMenuItem.Visible = Convert.ToBoolean(dr["seh2"]);
                toolStripMenuItem74.Visible = Convert.ToBoolean(dr["seh3"]);
                schedulerToolStripMenuItem.Visible = Convert.ToBoolean(dr["seh4"]);

                subjectMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["Lib1"]);
                bookNameMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["Lib2"]);
                toolStripMenuItem69.Visible = Convert.ToBoolean(dr["Lib3"]);
                rackMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["Lib4"]);
                binMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["Lib5"]);
                issueBooksToolStripMenuItem.Visible = Convert.ToBoolean(dr["Lib6"]);
                bToolStripMenuItem.Visible = Convert.ToBoolean(dr["Lib7"]);
                bookReturnToolStripMenuItem.Visible = Convert.ToBoolean(dr["Lib8"]);

                leagueToolStripMenuItem.Visible = Convert.ToBoolean(dr["lib9"]);
                bookCategoryToolStripMenuItem.Visible = Convert.ToBoolean(dr["lib10"]);
                fineMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["lib11"]);
                arrangeBookToolStripMenuItem.Visible = Convert.ToBoolean(dr["lib12"]);
                bookPurchaseToolStripMenuItem.Visible = Convert.ToBoolean(dr["lib13"]);
                bookStatusToolStripMenuItem.Visible = Convert.ToBoolean(dr["lib14"]);
                bookReturnStatusToolStripMenuItem.Visible = Convert.ToBoolean(dr["lib15"]);



                toolStripMenuItem36.Visible = Convert.ToBoolean(dr["cer1"]);
                toolStripMenuItem76.Visible = Convert.ToBoolean(dr["cer2"]);
                cCToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer3"]);
                iCardToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer4"]);
                gatePassToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer5"]);
                employeeExpToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer6"]);

                holidaysToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer7"]);
                achevementToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer8"]);
                classSToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer9"]);
                studyToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer10"]);
                classSToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer11"]);
              //  lToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer12"]);
                feeCertificateToolStripMenuItem.Visible = Convert.ToBoolean(dr["cer13"]);



                cashPaymentToolStripMenuItem.Visible = Convert.ToBoolean(dr["cash_rec"]);
                bankPaymentToolStripMenuItem.Visible = Convert.ToBoolean(dr["bank_payment"]);
                expenseDetailToolStripMenuItem.Visible = Convert.ToBoolean(dr["exp_detail"]);
                saleDetailReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["int_sale"]);
                balancePaidToolStripMenuItem.Visible = Convert.ToBoolean(dr["bal_paid"]);
                absentTeacherToolStripMenuItem.Visible = Convert.ToBoolean(dr["schedule"]);

                stockItemToolStripMenuItem.Visible = Convert.ToBoolean(dr["st1"]);
                stockPurchaseToolStripMenuItem.Visible = Convert.ToBoolean(dr["st2"]);
                stockIssueToolStripMenuItem.Visible = Convert.ToBoolean(dr["st3"]);
                stockDeportToolStripMenuItem.Visible = Convert.ToBoolean(dr["st4"]);
                stockDetailReportToolStripMenuItem1.Visible = Convert.ToBoolean(dr["st5"]);
                toolStripMenuItem2.Visible = Convert.ToBoolean(dr["stu_sub"]);
                //studentAbsentAttendanceToolStripMenuItem.Visible = Convert.ToBoolean(dr["saa"]);
                bankMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["bank"]);
                smsMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["sms_master"]);
                toolStripMenuItem47.Visible = Convert.ToBoolean(dr["stu_reportexam"]);
                examScheduleToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_exam"]);
                stockDetailReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_stock_re"]);
                stockDetailReportToolStripMenuItem2.Visible = Convert.ToBoolean(dr["stu_stock_re"]);
                provisionalMarksheetToolStripMenuItem.Visible = Convert.ToBoolean(dr["p_markssheet"]);
                gatePassDetailToolStripMenuItem.Visible = Convert.ToBoolean(dr["gatepass"]);
                visitorToolStripMenuItem.Visible = Convert.ToBoolean(dr["visitor"]);


                //studentFeeHeadToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_feehead"]);
                cheToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_chkbounce"]);
                chequeBounceDetailToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_chkbt"]);
                feeHeadwiseReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_feehwr"]);
                feeDefaulterToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_feedef"]);
                monthlyFeeReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["stu_mfreport"]);


                ////////////////////////************************************/////////////////////////////////////
                studentConcessionMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["FSCM"]);
                studentYearlyBalanceToolStripMenuItem.Visible = Convert.ToBoolean(dr["FSYB"]);
                uploadCoScholastics10thToolStripMenuItem.Visible = Convert.ToBoolean(dr["EUCo"]);
                //duesTransferToolStripMenuItem.Visible = Convert.ToBoolean(dr["EPCo"]);
                examwiseReportCardToolStripMenuItem.Visible = Convert.ToBoolean(dr["EERCard"]);
                deskSlipToolStripMenuItem.Visible = Convert.ToBoolean(dr["Edesk"]);
                subjectToolStripMenuItem.Visible = Convert.ToBoolean(dr["Esub"]);
                stockToolStripMenuItem.Visible = Convert.ToBoolean(dr["ISItem"]);
                stockPurchaseToolStripMenuItem1.Visible = Convert.ToBoolean(dr["ISPurchase"]);
                stockIssueToolStripMenuItem1.Visible = Convert.ToBoolean(dr["ISIssue"]);
                toolStripMenuItem24.Visible = Convert.ToBoolean(dr["PayEIcard"]);
                signatureUploadToolStripMenuItem.Visible = Convert.ToBoolean(dr["SSU"]);
                updateSessionToolStripMenuItem.Visible = Convert.ToBoolean(dr["SUSess"]);
                createInquiryToolStripMenuItem.Visible = Convert.ToBoolean(dr["ENQ11"]);
                inquiryDetailToolStripMenuItem.Visible = Convert.ToBoolean(dr["ENQ12"]);
                studentAbsentAttendanceToolStripMenuItem1.Visible = Convert.ToBoolean(dr["Atten1"]);
                studentAttendanceToolStripMenuItem.Visible = Convert.ToBoolean(dr["Atten2"]);
                attendanceReportToolStripMenuItem.Visible = Convert.ToBoolean(dr["Atten3"]);
                leaveMasterToolStripMenuItem.Visible = Convert.ToBoolean(dr["Atten4"]);
                holidaysToolStripMenuItem1.Visible = Convert.ToBoolean(dr["Atten5"]);
                fEEUploadToolStripMenuItem.Visible = Convert.ToBoolean(dr["Web1"]);
                feeToolStripMenuItem.Visible = Convert.ToBoolean(dr["Web2"]);
                feeReceiptToolStripMenuItem.Visible = Convert.ToBoolean(dr["Web3"]);
                downloadToolStripMenuItem.Visible = Convert.ToBoolean(dr["Web4"]);
                feeReceiptToolStripMenuItem2.Visible = Convert.ToBoolean(dr["Web3"]);


            }
            else
            {
                Application.Exit();
            }
            sc.Close();
        }

        private void areaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ////frmAreaMaster obj = new frmAreaMaster();
            ////obj.ShowDialog();
        }

        private void religionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Religionmaster obj = new Religionmaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void castToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //castemaster obj = new castemaster();
            //obj.ShowDialog();
        }

        private void visionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //visionmaster obj = new visionmaster();
            //obj.ShowDialog();
        }

        private void teethToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //teethmaster obj = new teethmaster();
            //obj.ShowDialog();

        }

        private void stateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStateMaster obj = new frmStateMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExitFromSystem();
        }
        private void stuToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStudentRoll obj = new FrmStudentRoll();
            // FrmStudentrollno obj = new FrmStudentrollno();
            obj.ShowDialog();
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            frmSectionMaster obj = new frmSectionMaster();
            obj.MdiParent = this;
            obj.Show(); ;
        }

        //private void toolStripMenuItem2_Click(object sender, EventArgs e)
        //{
        //    frmTblSubjectMaster obj = new frmTblSubjectMaster();
        //    obj.ShowDialog();
        //}

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            frmSubjectClsWise obj = new frmSubjectClsWise();
            obj.MdiParent = this;
            obj.Show();
        }

        //private void toolStripMenuItem4_Click(object sender, EventArgs e)
        //{
        //    frmClasssubjectMaster obj = new frmClasssubjectMaster();
        //    obj.ShowDialog();
        //}


        private void exitToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void feeHeadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFeeHead obj = new frmFeeHead();
            obj.MdiParent = this;
            obj.Show();
        }

        private void accountHeadsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGroupMaster obj = new frmGroupMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void ledgerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLedgerName obj = new frmLedgerName();
            obj.MdiParent = this;
            obj.Show();
        }

        private void feeCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Form1.board)
            {
                FrmFeeRecipt obj = new FrmFeeRecipt();
              //  obj.MdiParent = this;
                obj.ShowDialog();
            }
            else
            {
                FrmFeeReciptUP obj = new FrmFeeReciptUP();
                obj.MdiParent = this;
                obj.ShowDialog();
            }

        }

        private void classFeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClassFeeSingle obj = new frmClassFeeSingle();
            obj.MdiParent = this;
            obj.Show();
        }

        private void classFeeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClassFee obj = new frmClassFee();
            obj.MdiParent = this;
            obj.Show();

        }

        private void toolStripFeeExampted_Click(object sender, EventArgs e)
        {
            FeeExampted obj = new FeeExampted();
            obj.MdiParent = this;
            obj.Show();
        }

        private void feeRecieptReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReceiptDetailReport obj = new ReceiptDetailReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void receiptSummaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Receiptsummaryreport obj = new Receiptsummaryreport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void busStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frmbusstop obj = new Frmbusstop();
            obj.MdiParent = this;
            obj.Show();

        }

        private void headToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EHead obj = new EHead();
            obj.MdiParent = this;
            obj.Show();

        }

        private void studentBusStopToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStuBusstop obj = new FrmStuBusstop();
            obj.MdiParent = this;
            obj.Show();
        }



        private void employeeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmployeeRegistration obj = new EmployeeRegistration();
            obj.MdiParent = this;
            obj.Show();
        }

        private void employeeAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmEmployeeAttendance obj = new frmEmployeeAttendance();
            ////FrmStudentAttandence obj = new FrmStudentAttandence();
            //obj.ShowDialog();

        }

        private void toolStripEmpDetail_Click(object sender, EventArgs e)
        {
            EmployeeRegistrationDetail1 obj = new EmployeeRegistrationDetail1();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            frmGrDetail obj = new frmGrDetail();
            obj.MdiParent = this;
            obj.Show();

        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAdmission obj = new frmAdmission();
            obj.MdiParent = this;
            obj.Show();
        }

        private void scholasticAreasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGrading1 obj = new frmGrading1();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem5_Click_1(object sender, EventArgs e)
        {
            frmSubjectCategory obj = new frmSubjectCategory();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            frmSubjectSubCategory obj = new frmSubjectSubCategory();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem14_Click(object sender, EventArgs e)
        {
            frmDescriptiveIndicators obj = new frmDescriptiveIndicators();
            obj.MdiParent = this;
            obj.Show();

        }

        private void coToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGradCoScholasticArea obj = new frmGradCoScholasticArea();
            obj.MdiParent = this;
            obj.Show();
        }

        private void subjectMarksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SubjectMarks obj = new SubjectMarks();
            obj.MdiParent = this;
            obj.Show();

        }

        private void formSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistrationFormSale obj = new frmRegistrationFormSale();
            obj.MdiParent = this;
            obj.Show();
        }

        private void siddstudentRegistrationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmStudentRegistration obj = new frmStudentRegistration();
            obj.MdiParent = this;
            obj.Show();

        }

        private void admissionTestResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmissionTestResult obj = new frmAdmissionTestResult();
            obj.MdiParent = this;
            obj.Show();
        }

        private void admissionTestSchedueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmissinTestSchedule obj = new frmAdmissinTestSchedule();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem17_Click(object sender, EventArgs e)
        {
            frmConfirmAdmission obj = new frmConfirmAdmission();
            obj.MdiParent = this;
            obj.Show();

        }

        private void registrationSaleDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegFrmSaleDetail obj = new frmRegFrmSaleDetail();
            obj.MdiParent = this;
            obj.Show();
        }

        private void registrationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegistrationReport a = new frmRegistrationReport();
            a.ShowDialog();
        }

        private void subjectWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentMarksEntrySubjectWise obj = new frmStudentMarksEntrySubjectWise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void coScholasticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentGrade obj = new frmStudentGrade();
            obj.MdiParent = this;
            obj.Show();
        }

        private void admitCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AdmitCardDate obj = new AdmitCardDate();
            obj.MdiParent = this;
            obj.Show();
        }

        private void reportCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCard a = new frmReportCard();
            // obj.MdiParent = this;
            a.ShowDialog();
        }

        private void toolStripMenuItem18_Click(object sender, EventArgs e)
        {
            frmroute obj = new frmroute();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem19_Click(object sender, EventArgs e)
        {
            vehiclemaster obj = new vehiclemaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem20_Click(object sender, EventArgs e)
        {
            FrmSetBusRoute obj = new FrmSetBusRoute();
            obj.MdiParent = this;
            obj.Show();
        }



        private void cityToolStripMenuItem1_Click(object sender, EventArgs e)
        {

            frmCityMaster obj = new frmCityMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void occupationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            occupation obj = new occupation();
            obj.MdiParent = this;
            obj.Show();
        }

        private void qualificationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Qualification obj = new Qualification();
            obj.MdiParent = this;
            obj.Show();

        }

        private void religionToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Religionmaster obj = new Religionmaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void heightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            heightmaster obj = new heightmaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void weightToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            weightmaster obj = new weightmaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void bloodGroupToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            bloodgroupmaster obj = new bloodgroupmaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void departmentToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void designationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void admissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClass obj = new frmClass();
            obj.MdiParent = this;
            obj.Show();

        }

        private void admissionDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSectionMaster obj = new frmSectionMaster();
            obj.MdiParent = this;
            obj.Show();

        }

        private void stateMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStateMaster obj = new frmStateMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem16_Click(object sender, EventArgs e)
        {
            FrmHosteler obj = new FrmHosteler();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem24_Click(object sender, EventArgs e)
        {
          
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            frmStudentFee obj = new frmStudentFee();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            frmStudentReport obj = new frmStudentReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            frmAdmission obj = new frmAdmission();
            obj.MdiParent = this;
            obj.Show();
        }
        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            frmDuesbalance obj = new frmDuesbalance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentSubjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentSubject obj = new frmStudentSubject();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            frmStudentOldDuesClassWise obj = new frmStudentOldDuesClassWise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem29_Click(object sender, EventArgs e)
        {
            frmDemandRegisterReport obj = new frmDemandRegisterReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            frmFeeReminder obj = new frmFeeReminder();
            obj.MdiParent = this;
            obj.Show();
        }

        private void feeExemtedReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeeExemtedReport obj = new FeeExemtedReport(); obj.MdiParent = this; obj.Show();
        }

        private void feeReceiptReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFeeDetailsReport obj = new frmFeeDetailsReport(); obj.MdiParent = this; obj.Show();
        }

        private void busStopReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBusStopReport obj = new frmBusStopReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void busRouteReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBusRouteReport obj = new frmBusRouteReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void vehicleReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVehicleReport obj = new frmVehicleReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentMarksStudentWise obj = new frmStudentMarksStudentWise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void rollNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentMarksExamwise obj = new frmStudentMarksExamwise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void salaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSalary obj = new frmSalary();
            obj.MdiParent = this;
            obj.Show();
        }

        private void employeeSalaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpSalaryReport obj = new frmEmpSalaryReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void employeeReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpDetailReport obj = new frmEmpDetailReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void reportCardNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCardNew obj = new frmReportCardNew();
            obj.MdiParent = this;
            obj.Show();
        }

        private void reportCardAnnualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCardAnnualNew obj = new frmReportCardAnnualNew();
            obj.MdiParent = this;
            obj.Show();
        }

        private void butformsale_Click(object sender, EventArgs e)
        {
            frmRegistrationFormSale obj = new frmRegistrationFormSale();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            frmRegistrationFormSale obj = new frmRegistrationFormSale();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            frmStudentallsms obj = new frmStudentallsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            frmStudentindividualsms obj = new frmStudentindividualsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button17_Click(object sender, EventArgs e)
        {
            frmAdmission obj = new frmAdmission();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            frmStudentResultSMS obj = new frmStudentResultSMS();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button19_Click(object sender, EventArgs e)
        {
            frmClass obj = new frmClass();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            frmStudentReport obj = new frmStudentReport(); obj.MdiParent = this; obj.Show();

        }

        private void button16_Click(object sender, EventArgs e)
        {
            frmStudentReport obj = new frmStudentReport(); obj.MdiParent = this; obj.Show();

        }

        private void button3_Click(object sender, EventArgs e)
        {
            frmSectionMaster obj = new frmSectionMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            frmSectionMaster obj = new frmSectionMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void attToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void button13_Click(object sender, EventArgs e)
        {
            frmFeeReminder obj = new frmFeeReminder();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button26_Click(object sender, EventArgs e)
        {
            frmFeeReminder obj = new frmFeeReminder();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            frmDemandRegisterReport obj = new frmDemandRegisterReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button27_Click(object sender, EventArgs e)
        {
            frmDemandRegisterReport obj = new frmDemandRegisterReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem28_Click(object sender, EventArgs e)
        {
            frmDailyScrollReport obj = new frmDailyScrollReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button54_Click(object sender, EventArgs e)
        {
            AdmitCardDate obj = new AdmitCardDate();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button41_Click(object sender, EventArgs e)
        {
            AdmitCardDate obj = new AdmitCardDate();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button56_Click(object sender, EventArgs e)
        {
            FrmStuBusstop obj = new FrmStuBusstop();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button38_Click(object sender, EventArgs e)
        {
            FrmStuBusstop obj = new FrmStuBusstop();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button51_Click(object sender, EventArgs e)
        {
            Frmbusstop obj = new Frmbusstop();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button39_Click(object sender, EventArgs e)
        {
            Frmbusstop obj = new Frmbusstop();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button57_Click(object sender, EventArgs e)
        {
            frmEmpSalaryReport obj = new frmEmpSalaryReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button44_Click(object sender, EventArgs e)
        {
            frmEmpSalaryReport obj = new frmEmpSalaryReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button58_Click(object sender, EventArgs e)
        {
            EmployeeRegistration obj = new EmployeeRegistration();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button45_Click(object sender, EventArgs e)
        {
            EmployeeRegistration obj = new EmployeeRegistration();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button59_Click(object sender, EventArgs e)
        {
            frmEmpSalaryReport obj = new frmEmpSalaryReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button43_Click(object sender, EventArgs e)
        {
            frmEmpSalaryReport obj = new frmEmpSalaryReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void normalSmsForSingleStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentindividualsms obj = new frmStudentindividualsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void normalSmsForStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentallsms obj = new frmStudentallsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void normalSmsForAllStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeallsms obj = new frmEmployeeallsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void normalSmsForSingleStaffToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmemployeeindividualsms obj = new frmemployeeindividualsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            frmstudentAttendance obj = new frmstudentAttendance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button25_Click(object sender, EventArgs e)
        {
            frmstudentAttendance obj = new frmstudentAttendance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void finalReportCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCardAnnualwise obj = new frmReportCardAnnualwise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void resultReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmResultReport obj = new FrmResultReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void annualCrossListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAnnuallist obj = new frmAnnuallist();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            FrmFeeRecipt obj = new FrmFeeRecipt();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem66_Click(object sender, EventArgs e)
        {
            RegistrationFormClass obj = new RegistrationFormClass();
            obj.MdiParent = this;
            obj.Show();
        }

        private void classXLOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrinLOCForm obj = new frmPrinLOCForm();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem64_Click(object sender, EventArgs e)
        {
            frmPrinRegForm a = new frmPrinRegForm();
            //obj.MdiParent = this;
            a.ShowDialog();
        }

        private void feeRecieptReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmFeeRecieptReport obj = new frmFeeRecieptReport(); obj.MdiParent = this; obj.Show();
        }

        private void classXIILOCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPrinXIILOCForm obj = new frmPrinXIILOCForm(); obj.MdiParent = this; obj.Show();
        }
        private void userPermissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPermission obj = new frmPermission(); obj.MdiParent = this; obj.Show();
        }

        private void groupMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGroupMaster obj = new frmGroupMaster(); obj.MdiParent = this; obj.Show();
        }

        private void studentCardMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentCardNo obj = new frmStudentCardNo(); obj.MdiParent = this; obj.Show();
        }

        private void vehicleTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVehicleType obj = new frmVehicleType(); obj.MdiParent = this; obj.Show();
        }

        private void vehicleModelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            VehicleModel obj = new VehicleModel(); obj.MdiParent = this; obj.Show();
        }

        private void vehicleMakeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmVehicleMake obj = new frmVehicleMake(); obj.MdiParent = this; obj.Show();
        }

        private void changePasswordToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmChangePassword obj = new frmChangePassword(); obj.MdiParent = this; obj.Show();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDbBackup obj = new frmDbBackup(); obj.MdiParent = this; obj.Show();
        }

        private void userToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCreateUserName obj = new frmCreateUserName(); obj.MdiParent = this; obj.Show();
        }

        private void restoreToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRestoreDatabase obj = new frmRestoreDatabase(); obj.MdiParent = this; obj.Show();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            frmDailyScrollReport obj = new frmDailyScrollReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button53_Click(object sender, EventArgs e)
        {
            frmDbBackup obj = new frmDbBackup(); obj.MdiParent = this; obj.Show();
        }

        private void button47_Click(object sender, EventArgs e)
        {
            frmRestoreDatabase obj = new frmRestoreDatabase(); obj.MdiParent = this; obj.Show();
        }

        private void button46_Click(object sender, EventArgs e)
        {
            frmChangePassword obj = new frmChangePassword(); obj.MdiParent = this; obj.Show();
        }

        private void button31_Click(object sender, EventArgs e)
        {
            frmPermission obj = new frmPermission(); obj.MdiParent = this; obj.Show();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FrmFeeRecipt obj = new FrmFeeRecipt();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button52_Click(object sender, EventArgs e)
        {
            frmReportCard a = new frmReportCard();
            //obj.MdiParent = this;
            a.ShowDialog();
        }


        private void bookItemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBookItemMaster obj = new frmBookItemMaster(); obj.MdiParent = this; obj.Show();
        }

        private void publisherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDepartmentMaster obj = new frmDepartmentMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void grToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDesignationMaster obj = new frmDesignationMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void purchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchase obj = new frmPurchase(); obj.MdiParent = this; obj.Show();
        }

        private void bookGroupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmbookgroup obj = new frmbookgroup(); obj.MdiParent = this; obj.Show();
        }

        private void issueSaleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueSale obj = new frmIssueSale();
            obj.MdiParent = this;
            obj.Show();
        }

        private void publisherWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockReport obj = new frmStockReport();
            obj.MdiParent = this;
            obj.Show();

        }

        private void purchaseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPurchaseReport obj = new frmPurchaseReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaleReport obj = new frmSaleReport(); obj.MdiParent = this; obj.Show();
        }

        private void inToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssueSaleInd obj = new frmIssueSaleInd(); obj.MdiParent = this; obj.Show();
        }


        private void bookNameMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBookNameMaster obj = new frmBookNameMaster(); obj.MdiParent = this; obj.Show();
        }

        private void toolStripMenuItem69_Click(object sender, EventArgs e)
        {
            frmPublicationMaster obj = new frmPublicationMaster(); obj.MdiParent = this; obj.Show();
        }

        private void rackMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmrackMaster obj = new frmrackMaster(); obj.MdiParent = this; obj.Show();

        }

        private void binMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRackBinMaster obj = new frmRackBinMaster(); obj.MdiParent = this; obj.Show();

        }

        private void subjectMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSubjectMaster obj = new frmSubjectMaster(); obj.MdiParent = this; obj.Show();

        }

        private void cashVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCashReceive obj = new frmCashReceive(); obj.MdiParent = this; obj.Show();

        }

        private void bankVoucherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBankReceive obj = new frmBankReceive(); obj.MdiParent = this; obj.Show();

        }

        private void journalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJournalVoucher obj = new frmJournalVoucher(); obj.MdiParent = this; obj.Show();

        }

        private void ledgerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLedgerDetail obj = new frmLedgerDetail(); obj.MdiParent = this; obj.Show();

        }

        private void issueBooksToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmIssueBook obj = new frmIssueBook(); obj.ShowDialog();
        }

        private void bToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBookDetailreport obj = new frmBookDetailreport(); obj.MdiParent = this; obj.Show();

        }

        private void bookReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssuebookreturn obj = new frmIssuebookreturn(); obj.MdiParent = this; obj.Show();

        }

        private void categoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Categorymaster obj = new Categorymaster(); obj.MdiParent = this; obj.Show();

        }

        private void casteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            castemaster obj = new castemaster(); obj.MdiParent = this; obj.Show();

        }

        private void documentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDocumentMaster obj = new frmDocumentMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void cCEReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAnnuallist obj = new frmAnnuallist();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentDataUploadByExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGrEntryByExcelsheet obj = new frmGrEntryByExcelsheet();
            obj.MdiParent = this;
            obj.Show();
        }

        private void departmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDepartmentMaster obj = new frmDepartmentMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void designationToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmDesignationMaster obj = new frmDesignationMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem70_Click(object sender, EventArgs e)
        {
            frmAWRDetail obj = new frmAWRDetail();
            obj.MdiParent = this;
            obj.Show();
        }

        private void subjectUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SUBJECTExcelsheet obj = new SUBJECTExcelsheet(); obj.MdiParent = this; obj.Show();

        }

        private void toolStripMenuItem71_Click(object sender, EventArgs e)
        {
            frmCBSESUBNROLL obj = new frmCBSESUBNROLL();
            obj.MdiParent = this;
            obj.Show();
        }

        private void exportRegistrationReportInExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentReportInExcel obj = new frmStudentReportInExcel();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem72_Click(object sender, EventArgs e)
        {
            frmSetLogo obj = new frmSetLogo();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem73_Click(object sender, EventArgs e)
        {
            frmVendor obj = new frmVendor();
            obj.MdiParent = this;
            obj.Show();
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e)
        {

            frmEmpAttendance obj = new frmEmpAttendance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void allowenceTaxesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAllowance obj = new frmAllowance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void subjectWiseScholosticBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentMarksEntrySubjectWiseScholosticB obj = new frmStudentMarksEntrySubjectWiseScholosticB();
            obj.MdiParent = this;
            obj.Show();
        }

        private void subjectWiseCoScholosticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentMarksEntrySubjectWiseCoscholostic obj = new frmStudentMarksEntrySubjectWiseCoscholostic();
            obj.MdiParent = this;
            obj.Show();
        }

        private void scholosticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCardAnnualwise obj = new frmReportCardAnnualwise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void coScholosticToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCardAnnualwise obj = new frmReportCardAnnualwise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void periodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frmperiod obj = new Frmperiod();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem56_Click(object sender, EventArgs e)
        {

        }

        private void teacherSubjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmteacherSubject obj = new frmteacherSubject();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem74_Click(object sender, EventArgs e)
        {
            frmScheduling obj = new frmScheduling();
            obj.MdiParent = this;
            obj.Show();
        }

        private void schedulerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClassScheduling obj = new frmClassScheduling();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem56_Click_1(object sender, EventArgs e)
        {
            frmPublisherMaster obj = new frmPublisherMaster(); obj.MdiParent = this; obj.Show();

        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetting obj = new frmSetting(); obj.MdiParent = this; obj.Show();

        }

        private void smsBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSmsBalance obj = new frmSmsBalance(); obj.MdiParent = this; obj.Show();

        }

        private void saleDetailReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSaleDetailReport obj = new frmSaleDetailReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {

        }

        private void commentMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCommentMaster obj = new frmCommentMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void masterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void uToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //frmPermissionNew obj = new frmPermissionNew();
            // obj.ShowDialog();
        }

        private void certificatesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCC obj = new frmCC();
            obj.MdiParent = this;
            obj.Show();
        }

        private void iCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmICard obj = new frmICard();
            obj.MdiParent = this;
            obj.Show();
        }

        private void gatePassToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGatePass obj = new frmGatePass();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem75_Click(object sender, EventArgs e)
        {
            frmEventSMS obj = new frmEventSMS(); obj.MdiParent = this; obj.Show();

        }

        private void dayBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDayBook obj = new frmDayBook(); obj.MdiParent = this; obj.Show();

        }

        private void toolStripMenuItem63_Click(object sender, EventArgs e)
        {
            frmProfitAndLoss obj = new frmProfitAndLoss(); obj.MdiParent = this; obj.Show();


        }

        private void formMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmMaster obj = new frmMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void registrationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRegMaster obj = new frmRegMaster(); obj.MdiParent = this; obj.Show();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {

        }

        private void cashBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmcashBook obj = new frmcashBook();
            obj.MdiParent = this;
            obj.Show();
        }

        private void inventoryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void totalAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstuAttupload obj = new frmstuAttupload();
            obj.MdiParent = this;
            obj.Show();
        }

        private void oUTToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeAttendanceOUT obj = new frmEmployeeAttendanceOUT();
            obj.MdiParent = this;
            obj.Show();
        }

        private void iNToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmEmployeeAttendanceIN obj = new frmEmployeeAttendanceIN();
            obj.MdiParent = this;
            obj.Show();

        }

        private void absentLeaveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmployeeAttendance obj = new frmEmployeeAttendance();
            obj.MdiParent = this;
            obj.Show();

        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e)
        {
            frmTC obj = new frmTC(); obj.MdiParent = this; obj.Show();

        }

        private void toolStripMenuItem76_Click(object sender, EventArgs e)
        {
            frmTcDetail obj = new frmTcDetail(); obj.MdiParent = this; obj.Show();

        }

        private void inventryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem77_Click(object sender, EventArgs e)
        {
            frmSiblingDetail obj = new frmSiblingDetail(); obj.MdiParent = this; obj.Show();

        }

        private void concessionMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmConcessionMaster obj = new frmConcessionMaster();
            obj.MdiParent = this;
            obj.Show();

        }

        private void toolStripMenuItem55_Click(object sender, EventArgs e)
        {
            frmStudentResultSMS obj = new frmStudentResultSMS();
            obj.MdiParent = this; obj.Show();

        }

        private void employeeExpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EmpExp obj = new EmpExp();
            obj.MdiParent = this;
            obj.Show();


        }

        private void holidaysToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void achevementToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAchevement obj = new frmAchevement();
            obj.MdiParent = this;
            obj.Show();
        }

        private void classSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmClasssyllabusSubjectWise obj = new frmClasssyllabusSubjectWise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStudySchedule obj = new FrmStudySchedule();
            obj.MdiParent = this; obj.Show();
        }

        private void adminPermissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmadminPermission obj = new frmadminPermission();
            obj.MdiParent = this; obj.Show();
        }

        private void uploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadData obj = new UploadData();
            obj.MdiParent = this; obj.Show();
        }

        private void attedenceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAttupload obj = new frmAttupload();
            obj.MdiParent = this;
            obj.Show();
        }

        //private void smsSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmsmsReport obj = new frmsmsReport();
        //    obj.MdiParent = this;
        //    obj.Show();

        //}

        private void smsReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmsmssummary obj = new frmsmssummary();
            obj.MdiParent = this;
            obj.Show();

        }

        private void monthlySalaryReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEmpMonthlySalaryReport obj = new frmEmpMonthlySalaryReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button48_Click(object sender, EventArgs e)
        {
            frmStudentindividualsms obj = new frmStudentindividualsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button30_Click(object sender, EventArgs e)
        {

        }

        private void button33_Click(object sender, EventArgs e)
        {

        }

        private void button36_Click(object sender, EventArgs e)
        {
            ExitFromSystem();
        }

        private void trialBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void journalBookToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dailyTimingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmdailytym obj = new frmdailytym(); obj.MdiParent = this; obj.Show();
        }

        private void examScheduleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmexamschedule obj = new frmexamschedule(); obj.MdiParent = this; obj.Show();

        }

        private void button49_Click(object sender, EventArgs e)
        {

        }

        private void balancePaidToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmExpense obj = new frmExpense(); obj.MdiParent = this; obj.Show();
        }

        private void absentTeacherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSchedulingforabsentteacher obj = new frmSchedulingforabsentteacher(); obj.MdiParent = this; obj.Show();

        }

        private void cheToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmchequebonus obj = new frmchequebonus();
            obj.MdiParent = this;
            obj.Show();
        }

        private void chequeBounceDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Chequesummaryreport obj = new Chequesummaryreport(); obj.MdiParent = this; obj.Show();
        }

        private void cashPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCashVoucher obj = new frmCashVoucher(); obj.MdiParent = this; obj.Show();
        }

        private void bankPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBankVoucher obj = new frmBankVoucher(); obj.MdiParent = this; obj.Show();
        }

        private void expenseDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDayBookdetail obj = new frmDayBookdetail(); obj.MdiParent = this; obj.Show();
        }

        private void feeHeadwiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            feeDetailReport obj = new feeDetailReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void pUBLISHERMASTERToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void leagueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmLanguageMaster obj = new frmLanguageMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void bookCategoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBOOKCATEGORY obj = new frmBOOKCATEGORY();
            obj.MdiParent = this;
            obj.Show();
        }

        private void fineMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBOOKFINEMaster obj = new frmBOOKFINEMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void arrangeBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmArrangebook obj = new frmArrangebook();
            obj.MdiParent = this;
            obj.Show();
        }

        private void bookPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void forStudentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIssuebookemployee obj = new frmIssuebookemployee();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentAbsentAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void smsNameWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStudentsmsnamewise obj = new FrmStudentsmsnamewise(); obj.MdiParent = this; obj.Show();
        }

        private void smsCategoryWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSMSFORCUSTOMISE obj = new frmSMSFORCUSTOMISE(); obj.MdiParent = this; obj.Show();
        }

        private void sToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmStudenttransportsms obj = new frmStudenttransportsms(); obj.MdiParent = this; obj.Show();
        }

        private void lToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockItemToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void feeDefaulterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmFeeDefaulter obj = new frmFeeDefaulter(); obj.MdiParent = this; obj.Show();

        }

        private void bookStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmbookstatus obj = new frmbookstatus(); obj.MdiParent = this; obj.Show();
        }

        private void stockPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void stockDeportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            uploadData1 obj = new uploadData1();
            obj.MdiParent = this;
            obj.Show();

        }

        //private void stockReportDetailToolStripMenuItem_Click(object sender, EventArgs e)
        //{
        //    frmstockDetailReport obj = new frmstockDetailReport(); obj.ShowDialog();
        //}

        private void studentFeeHeadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripMenuItem47_Click(object sender, EventArgs e)
        {
            frmStudentMarksExamwise obj = new frmStudentMarksExamwise(); obj.MdiParent = this; obj.Show();
        }

        private void monthlyFeeReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmnthlyfeereport obj = new frmmnthlyfeereport(); obj.MdiParent = this; obj.Show();
        }

        private void bankMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBankmaster obj = new frmBankmaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {

        }

        private void smsMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmmsgtypemaster obj = new frmmsgtypemaster();
            obj.MdiParent = this; obj.Show();
        }

        private void toolStripMenuItem15_Click(object sender, EventArgs e)
        {

        }

        private void feeCertificateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeeStructure obj = new FeeStructure();
            obj.MdiParent = this;
            obj.Show();
        }

        private void bookReturnStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmbookreturnstatus obj = new frmbookreturnstatus();
            obj.MdiParent = this;
            obj.Show();
        }

        private void provisionalMarksheetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Feeprovisional obj = new Feeprovisional();
            obj.MdiParent = this;
            obj.Show();

        }

        private void toolStripMenuItem62_Click(object sender, EventArgs e)
        {
            frmvoucherDetail obj = new frmvoucherDetail();
            obj.MdiParent = this;
            obj.Show();

        }

        private void incomeAndExpenditureToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmincomeexpense obj = new frmincomeexpense();
            obj.MdiParent = this;
            obj.Show();
        }

        private void stockDetailReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStockdetailReport obj = new frmStockdetailReport(); obj.MdiParent = this; obj.Show();
        }

        private void stockDetailReportToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            frmStockdetailReport obj = new frmStockdetailReport(); obj.MdiParent = this; obj.Show();
        }

        private void gatePassDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Visitordetailreport obj = new Visitordetailreport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void visitorToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void iNToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmVisitor obj = new frmVisitor();
            obj.MdiParent = this;
            obj.Show();
        }

        private void oUTToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmVisitorout obj = new frmVisitorout();
            obj.MdiParent = this;
            obj.Show();
        }



        private void stockDetailReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            uploadbusstop obj = new uploadbusstop();
            obj.MdiParent = this;
            obj.Show();
        }

        private void examwiseReportCardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCardAnnualwise1 obj = new frmReportCardAnnualwise1();
            obj.MdiParent = this;
            obj.Show();
        }

        private void signatureUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmSetLogo1 obj = new frmSetLogo1();
            obj.MdiParent = this;
            obj.Show();
        }

        private void schoolDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCompanyName obj = new frmCompanyName();
            obj.MdiParent = this;
            obj.Show();
        }

        private void classWiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void studentWiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void birthCertificatesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AgeCertificate obj = new AgeCertificate();
            obj.MdiParent = this;
            obj.Show();
        }

        private void deskSlipToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAdmitCardDateslip obj = new frmAdmitCardDateslip();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentConcessionMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void studentBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 obj = new Form3();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentYearlyBalanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmYearlyBalance obj = new frmYearlyBalance();
            obj.MdiParent = this;
            obj.Show();

        }

        private void stateMasterToolStripMenuItem_TextChanged(object sender, EventArgs e)
        {

        }

        private void heightToolStripMenuItem1_TextChanged(object sender, EventArgs e)
        {

        }

        private void inactiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Inactive obj = new Inactive();
            obj.MdiParent = this;
            obj.Show();
        }

        private void leaveMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmleave obj = new frmleave(); obj.Show();
            obj.MdiParent = this;
        }

        private void holidaysToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmHolidays obj = new frmHolidays();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstudentAttendance obj = new frmstudentAttendance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void classWiseReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAttendanceDetail obj = new frmAttendanceDetail();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentWiseReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmAttendanceDetailreport obj = new frmAttendanceDetailreport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void studentAbsentAttendanceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmStudentabsentAttandence obj = new FrmStudentabsentAttandence();
            obj.MdiParent = this; obj.Show();
        }

        private void subjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTblSubjectMaster obj = new frmTblSubjectMaster();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmSubjectSubCategory obj = new frmSubjectSubCategory();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem65_Click(object sender, EventArgs e)
        {

        }

        private void classXIRegistrationToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void studentAttendenceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void employeeTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void holidayToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void forEmployeeToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {

        }

        private void studentDataUpdateByExcelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmGrEntryByExcelsheetupdate obj = new frmGrEntryByExcelsheetupdate();
            obj.MdiParent = this;
            obj.Show();
        }

        private void stockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstockItemMaster obj = new frmstockItemMaster(); obj.MdiParent = this; obj.Show();
        }

        private void stockPurchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmstockPurchase obj = new frmstockPurchase(); obj.MdiParent = this; obj.Show();
        }

        private void stockIssueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmstockIssueSaleInd obj = new frmstockIssueSaleInd(); obj.MdiParent = this; obj.Show();
        }

        private void purchaseReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmstockPurchaseReport obj = new frmstockPurchaseReport(); obj.MdiParent = this; obj.Show();
        }

        private void stockDetailReportToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            frmStockdetailReport obj = new frmStockdetailReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void fEEUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Uploadstudentfee obj = new Uploadstudentfee();
            obj.MdiParent = this; obj.Show();

        }

        private void feeReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadData obj = new UploadData();
            obj.MdiParent = this; obj.Show();
        }

        private void feeReceiptToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Download obj = new Download();
            obj.MdiParent = this; obj.Show();
        }

        private void feeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UploadstudentfeeConsession obj = new UploadstudentfeeConsession();
            obj.MdiParent = this; obj.Show();
        }

        private void uploadCoScholastics10thToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmcoscholistics obj = new frmcoscholistics();
            obj.MdiParent = this;
            obj.Show();
        }

        private void duesTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmReportCardcoscholostic10 obj = new frmReportCardcoscholostic10();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem79_Click(object sender, EventArgs e)
        {

        }

        private void printFeeBookToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeeStructure obj = new FeeStructure();
            obj.MdiParent = this;
            obj.Show();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //var colors = new[] { Color.CornflowerBlue, Color.Green, Color.Aqua, Color.Azure, Color.CadetBlue };
            //var index = DateTime.Now.Second % colors.Length;
            //this.BackColor = colors[index];
          
        }

        private void securityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void downloadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
        }

        private void button40_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void signatureUploadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (panel1.Visible)
            panel1.Visible = false;
            else
                panel1.Visible = true;
        }

        private void showHideDetailPanelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (panel2.Visible)
                panel2.Visible = false;
            else
                panel2.Visible = true;
        }

        private void button23_Click(object sender, EventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {
            FrmFeeRecipt obj = new FrmFeeRecipt();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            frmAttendanceDetail obj = new frmAttendanceDetail();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button12_Click_1(object sender, EventArgs e)
        {
            frmDailyScrollReport obj = new frmDailyScrollReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button54_Click_1(object sender, EventArgs e)
        {
            AdmitCardDate obj = new AdmitCardDate();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button55_Click(object sender, EventArgs e)
        {
            frmStudentMarksEntrySubjectWise obj = new frmStudentMarksEntrySubjectWise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button52_Click_1(object sender, EventArgs e)
        {
            frmReportCardAnnualwise obj = new frmReportCardAnnualwise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button40_Click_1(object sender, EventArgs e)
        {
            frmReportCardAnnualwise obj = new frmReportCardAnnualwise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button42_Click(object sender, EventArgs e)
        {
            frmStudentMarksEntrySubjectWise obj = new frmStudentMarksEntrySubjectWise();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button41_Click_1(object sender, EventArgs e)
        {
            AdmitCardDate obj = new AdmitCardDate();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button22_Click(object sender, EventArgs e)
        {

            frmDailyScrollReport obj = new frmDailyScrollReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button25_Click_1(object sender, EventArgs e)
        {
            frmAttendanceDetail obj = new frmAttendanceDetail();
            obj.MdiParent = this;
            obj.Show();
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void button29_Click(object sender, EventArgs e)
        {
            frmStudentallsms obj = new frmStudentallsms();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button60_Click(object sender, EventArgs e)
        {
            events();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
        }

        private void btnstudent_Click(object sender, EventArgs e)
        {
            frmAdmission obj = new frmAdmission();
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnactive_Click(object sender, EventArgs e)
        {
            frmAdmission obj = new frmAdmission();
            obj.MdiParent = this;
            obj.Show();
        }

        private void btninactive_Click(object sender, EventArgs e)
        {
            frmAdmission obj = new frmAdmission();
            obj.MdiParent = this;
            obj.Show();
        }

        private void label10_Click(object sender, EventArgs e)
        {

        }

        private void btnpresent_Click(object sender, EventArgs e)
        {
            frmstudentAttendance obj = new frmstudentAttendance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void btnabsent_Click(object sender, EventArgs e)
        {
            frmstudentAttendance obj = new frmstudentAttendance();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {

        }

        private void amountConcessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeeconcessionExampted obj = new FeeconcessionExampted();
            obj.MdiParent = this;
            obj.Show();
        }

        private void headWiseConcessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FeeExampted obj = new FeeExampted();
            obj.MdiParent = this;
            obj.Show();
        }

        private void monthWiseConcessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstuFeestart obj = new frmstuFeestart(); obj.MdiParent = this; obj.Show();
        }

        private void toolStripMenuItem24_Click_1(object sender, EventArgs e)
        {
            frmEmpICard obj = new frmEmpICard();
            obj.MdiParent = this;
            obj.Show();
        }

        private void toolStripMenuItem63_Click_1(object sender, EventArgs e)
        {

        }

        private void createInquiryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmStudentInquiry obj = new frmStudentInquiry();
            obj.MdiParent = this;
            obj.Show();
        }

        private void inquiryDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmInquiryReport obj = new frmInquiryReport();
            obj.MdiParent = this;
            obj.Show();
        }

        private void button60_Click_1(object sender, EventArgs e)
        {
            Application.Exit();
            Login obj = new Login();
           obj.Show();
           
        }

        private void button61_Click(object sender, EventArgs e)
        {

        }

        private void feeReceiptToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            Download obj = new Download();
            obj.MdiParent = this; obj.Show();
        }

        private void attendanceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void updateSessionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmUpdateSession obj = new frmUpdateSession();
            obj.MdiParent = this;
            obj.Show();
        }

        private void jToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmJournalVoucher obj = new frmJournalVoucher();
            obj.MdiParent = this;
            obj.Show();
        }

        private void locationToolStripMenuItem1_Click(object sender, EventArgs e)
        {

        }

        private void vehicleMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void cashBookToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmCashBook1 obj = new frmCashBook1();
            obj.MdiParent = this;
            obj.Show();

        }

        private void trailBalanaceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmtrailbalance obj = new frmtrailbalance();
            obj.MdiParent = this;
            obj.Show();

        }

        private void profitAndLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProfitAndLoss obj = new frmProfitAndLoss();
            obj.MdiParent = this;
            obj.Show();

        }

        private void feeSummeryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmDailyDynamic obj = new frmDailyDynamic();
            obj.MdiParent = this;
            obj.Show();
        }

        private void attendaneUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstuAttupload ashu = new frmstuAttupload();
            ashu.MdiParent = this;
            ashu.Show();
        }

        private void button62_Click(object sender, EventArgs e)
        {
            frmHolidays obj = new frmHolidays();
            obj.MdiParent = this;
            obj.Show();
        }

        private void rTECertificatesCCEEToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RTECertificate obj = new RTECertificate();
            obj.MdiParent = this;
            obj.Show();

        }

        private void uploadAttendanceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstuAttupload AKKY = new frmstuAttupload();
            AKKY.MdiParent = this;
            AKKY.Show();
        }

        private void uploadAttendanceYearlyMonthlyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmstuAttuploadanual obj = new frmstuAttuploadanual();
            obj.MdiParent = this;
            obj.Show();
        }

        private void entryVoucherDayWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEntryVoucher obj = new frmEntryVoucher();
            obj.MdiParent = this;
            obj.Show();
        }

        private void statementDailyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmBalanceDetail obj = new frmBalanceDetail();
            obj.MdiParent = this;
            obj.Show();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        } 





    }
}
