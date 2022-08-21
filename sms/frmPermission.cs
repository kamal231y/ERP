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
using MsgBoxRet;
using System.Diagnostics;

namespace sms
{
    public partial class frmPermission : Form
    {
        public frmPermission()
        {
           MsgBox.backColour = Color.WhiteSmoke;
            MsgBox.buttonBackColour = Color.Red;
            MsgBox.buttonForeColour = Color.WhiteSmoke;
            MsgBox.buttonStyle = FlatStyle.Flat;
            MsgBox.titleBackColour = Color.Red;
            MsgBox.titleForeColour = Color.WhiteSmoke;
            MsgBox.messageBoxStyle = FlatStyle.Flat;
            MsgBox.foreColour = Color.Red;

            MsgBoxRetType.backColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonBackColour = Color.Red;
            MsgBoxRetType.buttonForeColour = Color.WhiteSmoke;
            MsgBoxRetType.buttonStyle = FlatStyle.Flat;
            MsgBoxRetType.titleBackColour = Color.Red;
            MsgBoxRetType.titleForeColour = Color.WhiteSmoke;
            MsgBoxRetType.messageBoxStyle = FlatStyle.Flat;
            MsgBoxRetType.foreColour = Color.Red;
            InitializeComponent();
        }

        private void BindGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from tblRights where userType!='SUPERADMIN' order by id", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            if (ds.Tables[0].Rows.Count > 0)
            {
                dataGridView1.DataSource = ds.Tables[0];
            }
            else
            {
                dataGridView1.DataSource = null;
            }
        }

        private void frmPermissionlatest_Load(object sender, EventArgs e)
        {
            BindGrid();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                sc.Open();
                SqlCommand com = new SqlCommand("select * from tblRights where id='" + dataGridView1.CurrentRow.Cells["id"].Value.ToString() + "'", sc);
                SqlDataReader dr = com.ExecuteReader();
                if (dr.HasRows)
                {
                    dr.Read();
                    txtId.Text = dr["id"].ToString();
                    //lblMsg.Text = "User Type : " + dr["UserType"].ToString() + " User Name : " + dr["UserName"].ToString();
                    chkM1.Checked = Convert.ToBoolean(dr["M1"]);
                    chkM2.Checked = Convert.ToBoolean(dr["M2"]);
                    chkM3.Checked = Convert.ToBoolean(dr["M3"]);
                    chkM4.Checked = Convert.ToBoolean(dr["M4"]);
                    chkM5.Checked = Convert.ToBoolean(dr["M5"]);
                    chkM6.Checked = Convert.ToBoolean(dr["M6"]);
                    chkM7.Checked = Convert.ToBoolean(dr["M7"]);
                    chkM9.Checked = Convert.ToBoolean(dr["M8"]);
                    chkM8.Checked = Convert.ToBoolean(dr["M9"]);
                    chkM10.Checked = Convert.ToBoolean(dr["M10"]);

                    chkReg1.Checked = Convert.ToBoolean(dr["REG1"]);
                    chkReg2.Checked = Convert.ToBoolean(dr["REG2"]);
                    chkReg3.Checked = Convert.ToBoolean(dr["REG3"]);
                    chkReg4.Checked = Convert.ToBoolean(dr["REG4"]);
                    chkReg5.Checked = Convert.ToBoolean(dr["REG5"]);
                    chkReg6.Checked = Convert.ToBoolean(dr["REG6"]);
                    chkReg7.Checked = Convert.ToBoolean(dr["REG7"]);
                    chkReg8.Checked = Convert.ToBoolean(dr["REG8"]);


                    chkStu3.Checked = Convert.ToBoolean(dr["STU1"]);
                    chkStu4.Checked = Convert.ToBoolean(dr["STU2"]);
                    chkStu1.Checked = Convert.ToBoolean(dr["STU3"]);
                    chkStu2.Checked = Convert.ToBoolean(dr["STU4"]);
                    chkStu11.Checked = Convert.ToBoolean(dr["STU5"]);
                    chkStu19.Checked = Convert.ToBoolean(dr["STU6"]);
                    chkStu7.Checked = Convert.ToBoolean(dr["STU7"]);
                    chkStu9.Checked = Convert.ToBoolean(dr["STU8"]);
                    chkStu10.Checked = Convert.ToBoolean(dr["STU9"]);
                    chkStu15.Checked = Convert.ToBoolean(dr["STU10"]);
                    chkStu18.Checked = Convert.ToBoolean(dr["STU11"]);
                    chkStu12.Checked = Convert.ToBoolean(dr["STU12"]);
                    chkStu13.Checked = Convert.ToBoolean(dr["STU13"]);
                    chkStu14.Checked = Convert.ToBoolean(dr["STU14"]);
                    chkStu16.Checked = Convert.ToBoolean(dr["STU16"]);

                    chkF1.Checked = Convert.ToBoolean(dr["FEE1"]);
                    chkF2.Checked = Convert.ToBoolean(dr["FEE2"]);
                    chkF3.Checked = Convert.ToBoolean(dr["FEE3"]);
                    chkF4.Checked = Convert.ToBoolean(dr["FEE4"]);
                    chkF5.Checked = Convert.ToBoolean(dr["FEE5"]);
                    chkF6.Checked = Convert.ToBoolean(dr["FEE6"]);
                    chkF7.Checked = Convert.ToBoolean(dr["FEE7"]);
                    chkF8.Checked = Convert.ToBoolean(dr["FEE8"]);
                    chkF9.Checked = Convert.ToBoolean(dr["FEE9"]);
                    chkF10.Checked = Convert.ToBoolean(dr["FEE10"]);
                    chkF11.Checked = Convert.ToBoolean(dr["FEE11"]);
                    chkF12.Checked = Convert.ToBoolean(dr["FEE12"]);
                    chkF13.Checked = Convert.ToBoolean(dr["FEE13"]);
                    chkF14.Checked = Convert.ToBoolean(dr["FEE14"]);
                    chkF15.Checked = Convert.ToBoolean(dr["FEE15"]);

                    chkExam1.Checked = Convert.ToBoolean(dr["EXAM1"]);
                    chkExam2.Checked = Convert.ToBoolean(dr["EXAM2"]);
                    chkExam3.Checked = Convert.ToBoolean(dr["EXAM3"]);
                    chkExam4.Checked = Convert.ToBoolean(dr["EXAM4"]);
                    chkExam5.Checked = Convert.ToBoolean(dr["EXAM5"]);
                    chkExam6.Checked = Convert.ToBoolean(dr["EXAM6"]);
                    chkExam7.Checked = Convert.ToBoolean(dr["EXAM7"]);
                    chkExam8.Checked = Convert.ToBoolean(dr["EXAM8"]);
                    chkExam9.Checked = Convert.ToBoolean(dr["EXAM9"]);
                    chkExam10.Checked = Convert.ToBoolean(dr["EXAM10"]);
                    chkExam11.Checked = Convert.ToBoolean(dr["EXAM11"]);
                    chkExam12.Checked = Convert.ToBoolean(dr["EXAM12"]);
                    chkExam13.Checked = Convert.ToBoolean(dr["EXAM13"]);
                    chkExam14.Checked = Convert.ToBoolean(dr["EXAM14"]);

                    chkT1.Checked = Convert.ToBoolean(dr["T1"]);
                    chkT2.Checked = Convert.ToBoolean(dr["T2"]);
                    chkT3.Checked = Convert.ToBoolean(dr["T3"]);
                    chkT4.Checked = Convert.ToBoolean(dr["T4"]);
                    chkT5.Checked = Convert.ToBoolean(dr["T5"]);

                    ckhAcc1.Checked = Convert.ToBoolean(dr["Acc1"]);
                    ckhAcc2.Checked = Convert.ToBoolean(dr["Acc2"]);
                    ckhAcc3.Checked = Convert.ToBoolean(dr["Acc3"]);
                    ckhAcc4.Checked = Convert.ToBoolean(dr["Acc4"]);
                    ckhAcc5.Checked = Convert.ToBoolean(dr["Acc5"]);
                    ckhAcc6.Checked = Convert.ToBoolean(dr["Acc6"]);
                    ckhAcc7.Checked = Convert.ToBoolean(dr["Acc7"]);
                    ckhAcc8.Checked = Convert.ToBoolean(dr["Acc8"]);
                    ckhAcc9.Checked = Convert.ToBoolean(dr["Acc9"]);
                    ckhAcc10.Checked = Convert.ToBoolean(dr["Acc10"]);
                    ckhAcc11.Checked = Convert.ToBoolean(dr["Acc11"]);

                    ckhInv1.Checked = Convert.ToBoolean(dr["Inv1"]);
                    ckhInv2.Checked = Convert.ToBoolean(dr["Inv2"]);
                    ckhInv3.Checked = Convert.ToBoolean(dr["Inv3"]);
                    ckhInv4.Checked = Convert.ToBoolean(dr["Inv4"]);
                    ckhInv5.Checked = Convert.ToBoolean(dr["Inv5"]);
                    ckhInv6.Checked = Convert.ToBoolean(dr["Inv6"]);
                    ckhInv7.Checked = Convert.ToBoolean(dr["Inv7"]);
                    ckhInv8.Checked = Convert.ToBoolean(dr["Inv8"]);
                    ckhInv9.Checked = Convert.ToBoolean(dr["Inv9"]);
                    ckhInv10.Checked = Convert.ToBoolean(dr["Inv10"]);

                    ckhPr1.Checked = Convert.ToBoolean(dr["PayRoll1"]);
                    ckhPr2.Checked = Convert.ToBoolean(dr["PayRoll2"]);
                    ckhPr3.Checked = Convert.ToBoolean(dr["PayRoll3"]);
                    ckhPr4.Checked = Convert.ToBoolean(dr["PayRoll4"]);
                    ckhPr5.Checked = Convert.ToBoolean(dr["PayRoll5"]);
                    ckhPr6.Checked = Convert.ToBoolean(dr["PayRoll6"]);
                    ckhPr7.Checked = Convert.ToBoolean(dr["PayRoll7"]);
                    ckhPr8.Checked = Convert.ToBoolean(dr["PayRoll8"]);

                    chkLib1.Checked = Convert.ToBoolean(dr["Lib1"]);
                    chkLib2.Checked = Convert.ToBoolean(dr["Lib2"]);
                    chkLib3.Checked = Convert.ToBoolean(dr["Lib3"]);
                    chkLib4.Checked = Convert.ToBoolean(dr["Lib4"]);
                    chkLib5.Checked = Convert.ToBoolean(dr["Lib5"]);
                    chkLib6.Checked = Convert.ToBoolean(dr["Lib6"]);
                    chkLib7.Checked = Convert.ToBoolean(dr["Lib7"]);
                    chkLib8.Checked = Convert.ToBoolean(dr["Lib8"]);

                    chkSms1.Checked = Convert.ToBoolean(dr["sms1"]);
                    chkSms2.Checked = Convert.ToBoolean(dr["sms2"]);
                    chkSms3.Checked = Convert.ToBoolean(dr["sms3"]);
                    chkSms4.Checked = Convert.ToBoolean(dr["sms4"]);
                    chkSms5.Checked = Convert.ToBoolean(dr["sms5"]);
                    chkSms6.Checked = Convert.ToBoolean(dr["sms6"]);
                    chkSms7.Checked = Convert.ToBoolean(dr["sms7"]);
                    checkBox10.Checked = Convert.ToBoolean(dr["sms8"]);
                    checkBox11.Checked = Convert.ToBoolean(dr["sms9"]);
                    checkBox12.Checked = Convert.ToBoolean(dr["sms10"]);
                    checkBox13.Checked = Convert.ToBoolean(dr["sms11"]);
                    checkBox14.Checked = Convert.ToBoolean(dr["sms12"]);

                    chkChangePassword.Checked = Convert.ToBoolean(dr["SEQ1"]);
                    chkBackUp.Checked = Convert.ToBoolean(dr["SEQ2"]);
                    chkRestore.Checked = Convert.ToBoolean(dr["SEQ3"]);
                    chkCreateUser.Checked = Convert.ToBoolean(dr["SEQ4"]);
                    chkPermission.Checked = Convert.ToBoolean(dr["SEQ5"]);
                    checkBox2.Checked = Convert.ToBoolean(dr["SEQ6"]);
                    checkBox1.Checked = Convert.ToBoolean(dr["SEQ7"]);

                    chksch1.Checked = Convert.ToBoolean(dr["seh1"]);
                    chksch2.Checked = Convert.ToBoolean(dr["seh2"]);
                    chksch3.Checked = Convert.ToBoolean(dr["seh3"]);
                    chksch4.Checked = Convert.ToBoolean(dr["seh4"]);

                    ckhcer1.Checked = Convert.ToBoolean(dr["cer1"]);
                    ckhcer2.Checked = Convert.ToBoolean(dr["cer2"]);
                    ckhcer3.Checked = Convert.ToBoolean(dr["cer3"]);
                    ckhcer4.Checked = Convert.ToBoolean(dr["cer4"]);
                    ckhcer5.Checked = Convert.ToBoolean(dr["cer5"]);
                    ckhcer6.Checked = Convert.ToBoolean(dr["cer6"]);
                    ckhcer7.Checked = Convert.ToBoolean(dr["cer7"]);
                    ckhcer8.Checked = Convert.ToBoolean(dr["cer8"]);
                    ckhcer9.Checked = Convert.ToBoolean(dr["cer9"]);
                    ckhcer10.Checked = Convert.ToBoolean(dr["cer10"]);
                    checkBox21.Checked = Convert.ToBoolean(dr["cer11"]);
                    checkBox22.Checked = Convert.ToBoolean(dr["cer12"]);
                    checkBox23.Checked = Convert.ToBoolean(dr["cer13"]);
                    checkcash.Checked = Convert.ToBoolean(dr["cash_rec"]);
                    checkpayment.Checked = Convert.ToBoolean(dr["bank_payment"]);
                    checkexpd.Checked = Convert.ToBoolean(dr["exp_detail"]);
                    checksale.Checked = Convert.ToBoolean(dr["int_sale"]);
                    checkbpaid.Checked = Convert.ToBoolean(dr["bal_paid"]);
                    checkatt.Checked = Convert.ToBoolean(dr["Payr10"]);
                    checktiming.Checked = Convert.ToBoolean(dr["Payr11"]);


                    checkBox3.Checked = Convert.ToBoolean(dr["lib9"]);
                    checkBox4.Checked = Convert.ToBoolean(dr["lib10"]);
                    checkBox5.Checked = Convert.ToBoolean(dr["lib11"]);
                    checkBox6.Checked = Convert.ToBoolean(dr["lib12"]);
                    checkBox7.Checked = Convert.ToBoolean(dr["lib13"]);
                    checkBox8.Checked = Convert.ToBoolean(dr["lib14"]);
                    checkBox9.Checked = Convert.ToBoolean(dr["lib15"]);
                    checkBox15.Checked = Convert.ToBoolean(dr["schedule"]);

                    checkBox16.Checked = Convert.ToBoolean(dr["st1"]);
                    checkBox17.Checked = Convert.ToBoolean(dr["st2"]);
                    checkBox18.Checked = Convert.ToBoolean(dr["st3"]);
                    checkBox19.Checked = Convert.ToBoolean(dr["st4"]);
                    checkBox20.Checked = Convert.ToBoolean(dr["st5"]);
                    checkBox24.Checked = Convert.ToBoolean(dr["stu_sub"]);
                    checkBox25.Checked = Convert.ToBoolean(dr["saa"]);
                    checkBox26.Checked = Convert.ToBoolean(dr["bank"]);
                    checkBox27.Checked = Convert.ToBoolean(dr["sms_master"]);
                    checkBox28.Checked = Convert.ToBoolean(dr["stu_reportexam"]);
                    checkBox29.Checked = Convert.ToBoolean(dr["stu_exam"]);
                    checkBox31.Checked = Convert.ToBoolean(dr["stu_stock_re"]);

                    checkBox32.Checked = Convert.ToBoolean(dr["p_markssheet"]);
                    checkBox33.Checked = Convert.ToBoolean(dr["gatepass"]);
                    checkBox30.Checked = Convert.ToBoolean(dr["visitor"]);

                    checkBox34.Checked = Convert.ToBoolean(dr["stu_feehead"]);
                    checkBox35.Checked = Convert.ToBoolean(dr["stu_chkbounce"]);
                    checkBox36.Checked = Convert.ToBoolean(dr["stu_chkbt"]);
                    checkBox37.Checked = Convert.ToBoolean(dr["stu_feehwr"]);
                    checkBox38.Checked = Convert.ToBoolean(dr["stu_feedef"]);
                    checkBox39.Checked = Convert.ToBoolean(dr["stu_mfreport"]);

                    FSCM.Checked = Convert.ToBoolean(dr["FSCM"]);
                    FSYB.Checked = Convert.ToBoolean(dr["FSYB"]);
                    EUCo.Checked = Convert.ToBoolean(dr["EUCo"]);
                    EPCo.Checked = Convert.ToBoolean(dr["EPCo"]);
                    EERCard.Checked = Convert.ToBoolean(dr["EERCard"]);
                    Edesk.Checked = Convert.ToBoolean(dr["Edesk"]);
                    ESub.Checked = Convert.ToBoolean(dr["Esub"]);
                    ISItem.Checked = Convert.ToBoolean(dr["ISItem"]);
                    ISPurchase.Checked = Convert.ToBoolean(dr["ISPurchase"]);
                    ISIssue.Checked = Convert.ToBoolean(dr["ISIssue"]);
                    ISPurchase.Checked = Convert.ToBoolean(dr["ISPurchase"]);
                    PayEIcard.Checked = Convert.ToBoolean(dr["PayEIcard"]);
                    SSU.Checked = Convert.ToBoolean(dr["SSU"]);
                    SUSess.Checked = Convert.ToBoolean(dr["SUSess"]);

                    checkBox41.Checked = Convert.ToBoolean(dr["ENQ11"]);
                    checkBox40.Checked = Convert.ToBoolean(dr["ENQ12"]);
                    checkBox46.Checked = Convert.ToBoolean(dr["Atten1"]);
                    checkBox45.Checked = Convert.ToBoolean(dr["Atten2"]);
                    checkBox44.Checked = Convert.ToBoolean(dr["Atten3"]);
                    checkBox43.Checked = Convert.ToBoolean(dr["Atten4"]);
                    checkBox42.Checked = Convert.ToBoolean(dr["Atten5"]);
                    checkBox51.Checked = Convert.ToBoolean(dr["Web1"]);
                    checkBox50.Checked = Convert.ToBoolean(dr["Web2"]);
                    checkBox49.Checked = Convert.ToBoolean(dr["Web3"]);
                    checkBox48.Checked = Convert.ToBoolean(dr["Web4"]);
                   
                }
                sc.Close();
            }
        }



        private void btUpdate_Click(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "update_tblRights";
            cmd.Connection = sc;
            cmd.Parameters.AddWithValue("@id", txtId.Text);
            cmd.Parameters.AddWithValue("@M1", Convert.ToInt32(chkM1.Checked));
            cmd.Parameters.AddWithValue("@M2", Convert.ToInt32(chkM2.Checked));
            cmd.Parameters.AddWithValue("@M3", Convert.ToInt32(chkM3.Checked));
            cmd.Parameters.AddWithValue("@M4", Convert.ToInt32(chkM4.Checked));
            cmd.Parameters.AddWithValue("@M5", Convert.ToInt32(chkM5.Checked));
            cmd.Parameters.AddWithValue("@M6", Convert.ToInt32(chkM6.Checked));
            cmd.Parameters.AddWithValue("@M7", Convert.ToInt32(chkM7.Checked));
            cmd.Parameters.AddWithValue("@M8", Convert.ToInt32(chkM8.Checked));
            cmd.Parameters.AddWithValue("@M9", Convert.ToInt32(chkM9.Checked));
            cmd.Parameters.AddWithValue("@M10", Convert.ToInt32(chkM10.Checked));

            cmd.Parameters.AddWithValue("@REG1", Convert.ToInt32(chkReg1.Checked));
            cmd.Parameters.AddWithValue("@REG2", Convert.ToInt32(chkReg2.Checked));
            cmd.Parameters.AddWithValue("@REG3", Convert.ToInt32(chkReg4.Checked));
            cmd.Parameters.AddWithValue("@REG4", Convert.ToInt32(chkReg3.Checked));
            cmd.Parameters.AddWithValue("@REG5", Convert.ToInt32(chkReg5.Checked));
            cmd.Parameters.AddWithValue("@REG6", Convert.ToInt32(chkReg6.Checked));
            cmd.Parameters.AddWithValue("@REG7", Convert.ToInt32(chkReg7.Checked));

            cmd.Parameters.AddWithValue("@STU1", Convert.ToInt32(chkStu3.Checked));
            cmd.Parameters.AddWithValue("@STU2", Convert.ToInt32(chkStu4.Checked));
            cmd.Parameters.AddWithValue("@STU3", Convert.ToInt32(chkStu1.Checked));
            cmd.Parameters.AddWithValue("@STU4", Convert.ToInt32(chkStu2.Checked));
            cmd.Parameters.AddWithValue("@STU5", Convert.ToInt32(chkStu11.Checked));
            cmd.Parameters.AddWithValue("@STU6", Convert.ToInt32(chkStu19.Checked));
            cmd.Parameters.AddWithValue("@STU7", Convert.ToInt32(chkStu7.Checked));
            cmd.Parameters.AddWithValue("@STU8", Convert.ToInt32(chkStu9.Checked));
            cmd.Parameters.AddWithValue("@STU9", Convert.ToInt32(chkStu10.Checked));
            cmd.Parameters.AddWithValue("@STU10", Convert.ToInt32(chkStu15.Checked));
            cmd.Parameters.AddWithValue("@STU11", Convert.ToInt32(chkStu18.Checked));
            cmd.Parameters.AddWithValue("@STU12", Convert.ToInt32(chkStu12.Checked));
            cmd.Parameters.AddWithValue("@STU13", Convert.ToInt32(chkStu13.Checked));
            cmd.Parameters.AddWithValue("@STU14", Convert.ToInt32(chkStu14.Checked));
            cmd.Parameters.AddWithValue("@STU16", Convert.ToInt32(chkStu16.Checked));


            cmd.Parameters.AddWithValue("@FEE1", Convert.ToInt32(chkF1.Checked));
            cmd.Parameters.AddWithValue("@FEE2", Convert.ToInt32(chkF2.Checked));
            cmd.Parameters.AddWithValue("@FEE3", Convert.ToInt32(chkF3.Checked));
            cmd.Parameters.AddWithValue("@FEE4", Convert.ToInt32(chkF4.Checked));
            cmd.Parameters.AddWithValue("@FEE5", Convert.ToInt32(chkF5.Checked));
            cmd.Parameters.AddWithValue("@FEE6", Convert.ToInt32(chkF6.Checked));
            cmd.Parameters.AddWithValue("@FEE7", Convert.ToInt32(chkF7.Checked));
            cmd.Parameters.AddWithValue("@FEE8", Convert.ToInt32(chkF8.Checked));
            cmd.Parameters.AddWithValue("@FEE9", Convert.ToInt32(chkF9.Checked));
            cmd.Parameters.AddWithValue("@FEE10", Convert.ToInt32(chkF10.Checked));
            cmd.Parameters.AddWithValue("@FEE11", Convert.ToInt32(chkF11.Checked));
            cmd.Parameters.AddWithValue("@FEE12", Convert.ToInt32(chkF12.Checked));
            cmd.Parameters.AddWithValue("@FEE13", Convert.ToInt32(chkF13.Checked));
            cmd.Parameters.AddWithValue("@FEE14", Convert.ToInt32(chkF14.Checked));
            cmd.Parameters.AddWithValue("@FEE15", Convert.ToInt32(chkF15.Checked));

            cmd.Parameters.AddWithValue("@EXAM1", Convert.ToInt32(chkExam1.Checked));
            cmd.Parameters.AddWithValue("@EXAM2", Convert.ToInt32(chkExam2.Checked));
            cmd.Parameters.AddWithValue("@EXAM3", Convert.ToInt32(chkExam3.Checked));
            cmd.Parameters.AddWithValue("@EXAM4", Convert.ToInt32(chkExam4.Checked));
            cmd.Parameters.AddWithValue("@EXAM5", Convert.ToInt32(chkExam5.Checked));
            cmd.Parameters.AddWithValue("@EXAM6", Convert.ToInt32(chkExam6.Checked));
            cmd.Parameters.AddWithValue("@EXAM7", Convert.ToInt32(chkExam7.Checked));
            cmd.Parameters.AddWithValue("@EXAM8", Convert.ToInt32(chkExam8.Checked));
            cmd.Parameters.AddWithValue("@EXAM9", Convert.ToInt32(chkExam9.Checked));
            cmd.Parameters.AddWithValue("@EXAM10", Convert.ToInt32(chkExam10.Checked));
            cmd.Parameters.AddWithValue("@EXAM11", Convert.ToInt32(chkExam11.Checked));
            cmd.Parameters.AddWithValue("@EXAM12", Convert.ToInt32(chkExam12.Checked));
            cmd.Parameters.AddWithValue("@EXAM13", Convert.ToInt32(chkExam13.Checked));
            cmd.Parameters.AddWithValue("@EXAM14", Convert.ToInt32(chkExam14.Checked));

            cmd.Parameters.AddWithValue("@T1", Convert.ToInt32(chkT1.Checked));
            cmd.Parameters.AddWithValue("@T2", Convert.ToInt32(chkT2.Checked));
            cmd.Parameters.AddWithValue("@T3", Convert.ToInt32(chkT3.Checked));
            cmd.Parameters.AddWithValue("@T4", Convert.ToInt32(chkT4.Checked));
            cmd.Parameters.AddWithValue("@T5", Convert.ToInt32(chkT5.Checked));

            cmd.Parameters.AddWithValue("@SEQ1", Convert.ToInt32(chkChangePassword.Checked));
            cmd.Parameters.AddWithValue("@SEQ2", Convert.ToInt32(chkBackUp.Checked));
            cmd.Parameters.AddWithValue("@SEQ3", Convert.ToInt32(chkRestore.Checked));
            cmd.Parameters.AddWithValue("@SEQ4", Convert.ToInt32(chkCreateUser.Checked));
            cmd.Parameters.AddWithValue("@SEQ5", Convert.ToInt32(chkPermission.Checked));

            cmd.Parameters.AddWithValue("@Acc1", Convert.ToInt32(ckhAcc1.Checked));
            cmd.Parameters.AddWithValue("@Acc2", Convert.ToInt32(ckhAcc2.Checked));
            cmd.Parameters.AddWithValue("@Acc3", Convert.ToInt32(ckhAcc3.Checked));
            cmd.Parameters.AddWithValue("@Acc4", Convert.ToInt32(ckhAcc4.Checked));
            cmd.Parameters.AddWithValue("@Acc5", Convert.ToInt32(ckhAcc5.Checked));
            cmd.Parameters.AddWithValue("@Acc6", Convert.ToInt32(ckhAcc6.Checked));
            cmd.Parameters.AddWithValue("@Acc7", Convert.ToInt32(ckhAcc7.Checked));
            cmd.Parameters.AddWithValue("@Acc8", Convert.ToInt32(ckhAcc8.Checked));
            cmd.Parameters.AddWithValue("@Acc9", Convert.ToInt32(ckhAcc9.Checked));
            cmd.Parameters.AddWithValue("@Acc10", Convert.ToInt32(ckhAcc10.Checked));
            cmd.Parameters.AddWithValue("@cash_rec", Convert.ToInt32(checkcash.Checked));
            cmd.Parameters.AddWithValue("@bank_payment", Convert.ToInt32(checkpayment.Checked));
            cmd.Parameters.AddWithValue("@exp_detail", Convert.ToInt32(checkexpd.Checked));

            cmd.Parameters.AddWithValue("@Inv1", Convert.ToInt32(ckhInv1.Checked));
            cmd.Parameters.AddWithValue("@Inv2", Convert.ToInt32(ckhInv2.Checked));
            cmd.Parameters.AddWithValue("@Inv3", Convert.ToInt32(ckhInv3.Checked));
            cmd.Parameters.AddWithValue("@Inv4", Convert.ToInt32(ckhInv4.Checked));
            cmd.Parameters.AddWithValue("@Inv5", Convert.ToInt32(ckhInv5.Checked));
            cmd.Parameters.AddWithValue("@Inv6", Convert.ToInt32(ckhInv6.Checked));
            cmd.Parameters.AddWithValue("@Inv7", Convert.ToInt32(ckhInv7.Checked));
            cmd.Parameters.AddWithValue("@Inv8", Convert.ToInt32(ckhInv8.Checked));
            cmd.Parameters.AddWithValue("@Inv9", Convert.ToInt32(ckhInv9.Checked));

            cmd.Parameters.AddWithValue("@PayRoll1", Convert.ToInt32(ckhPr1.Checked));
            cmd.Parameters.AddWithValue("@PayRoll2", Convert.ToInt32(ckhPr2.Checked));
            cmd.Parameters.AddWithValue("@PayRoll3", Convert.ToInt32(ckhPr3.Checked));
            cmd.Parameters.AddWithValue("@PayRoll4", Convert.ToInt32(ckhPr4.Checked));
            cmd.Parameters.AddWithValue("@PayRoll5", Convert.ToInt32(ckhPr5.Checked));
            cmd.Parameters.AddWithValue("@PayRoll6", Convert.ToInt32(ckhPr6.Checked));

            cmd.Parameters.AddWithValue("@Lib1", Convert.ToInt32(chkLib1.Checked));
            cmd.Parameters.AddWithValue("@Lib2", Convert.ToInt32(chkLib2.Checked));
            cmd.Parameters.AddWithValue("@Lib3", Convert.ToInt32(chkLib3.Checked));
            cmd.Parameters.AddWithValue("@Lib4", Convert.ToInt32(chkLib4.Checked));
            cmd.Parameters.AddWithValue("@Lib5", Convert.ToInt32(chkLib5.Checked));
            cmd.Parameters.AddWithValue("@Lib6", Convert.ToInt32(chkLib6.Checked));
            cmd.Parameters.AddWithValue("@Lib7", Convert.ToInt32(chkLib7.Checked));
            cmd.Parameters.AddWithValue("@Lib8", Convert.ToInt32(chkLib8.Checked));


            cmd.Parameters.AddWithValue("@lib9", Convert.ToInt32(checkBox3.Checked));
            cmd.Parameters.AddWithValue("@lib10", Convert.ToInt32(checkBox4.Checked));
            cmd.Parameters.AddWithValue("@lib11", Convert.ToInt32(checkBox5.Checked));
            cmd.Parameters.AddWithValue("@lib12", Convert.ToInt32(checkBox6.Checked));
            cmd.Parameters.AddWithValue("@lib13", Convert.ToInt32(checkBox7.Checked));
            cmd.Parameters.AddWithValue("@lib14", Convert.ToInt32(checkBox8.Checked));
            cmd.Parameters.AddWithValue("@lib15", Convert.ToInt32(checkBox9.Checked));







            cmd.Parameters.AddWithValue("@sms1", Convert.ToInt32(chkSms1.Checked));
            cmd.Parameters.AddWithValue("@sms2", Convert.ToInt32(chkSms2.Checked));
            cmd.Parameters.AddWithValue("@sms3", Convert.ToInt32(chkSms3.Checked));
            cmd.Parameters.AddWithValue("@sms4", Convert.ToInt32(chkSms4.Checked));
            cmd.Parameters.AddWithValue("@sms5", Convert.ToInt32(chkSms5.Checked));

            cmd.Parameters.AddWithValue("@sms8", Convert.ToInt32(checkBox10.Checked));
            cmd.Parameters.AddWithValue("@sms9", Convert.ToInt32(checkBox11.Checked));
            cmd.Parameters.AddWithValue("@sms10", Convert.ToInt32(checkBox12.Checked));
            cmd.Parameters.AddWithValue("@sms11", Convert.ToInt32(checkBox13.Checked));
            cmd.Parameters.AddWithValue("@sms12", Convert.ToInt32(checkBox14.Checked));

            cmd.Parameters.AddWithValue("@Acc11", Convert.ToInt32(ckhAcc11.Checked));
            cmd.Parameters.AddWithValue("@Inv10", Convert.ToInt32(ckhInv10.Checked));
            cmd.Parameters.AddWithValue("@PayRoll7", Convert.ToInt32(ckhPr7.Checked));
            cmd.Parameters.AddWithValue("@PayRoll8", Convert.ToInt32(ckhPr8.Checked));
            cmd.Parameters.AddWithValue("@Reg8", Convert.ToInt32(chkReg8.Checked));
            cmd.Parameters.AddWithValue("@sms6", Convert.ToInt32(chkSms6.Checked));
            cmd.Parameters.AddWithValue("@sms7", Convert.ToInt32(chkSms7.Checked));
            cmd.Parameters.AddWithValue("@SEQ6", Convert.ToInt32(checkBox2.Checked));
            cmd.Parameters.AddWithValue("@SEQ7", Convert.ToInt32(checkBox1.Checked));

            cmd.Parameters.AddWithValue("@seh1", Convert.ToInt32(chksch1.Checked));
            cmd.Parameters.AddWithValue("@seh2", Convert.ToInt32(chksch2.Checked));
            cmd.Parameters.AddWithValue("@seh3", Convert.ToInt32(chksch3.Checked));
            cmd.Parameters.AddWithValue("@seh4", Convert.ToInt32(chksch4.Checked));

            cmd.Parameters.AddWithValue("@cer1", Convert.ToInt32(ckhcer1.Checked));
            cmd.Parameters.AddWithValue("@cer2", Convert.ToInt32(ckhcer2.Checked));
            cmd.Parameters.AddWithValue("@cer3", Convert.ToInt32(ckhcer3.Checked));
            cmd.Parameters.AddWithValue("@cer4", Convert.ToInt32(ckhcer4.Checked));
            cmd.Parameters.AddWithValue("@cer5", Convert.ToInt32(ckhcer5.Checked));
            cmd.Parameters.AddWithValue("@cer6", Convert.ToInt32(ckhcer6.Checked));
            cmd.Parameters.AddWithValue("@cer7", Convert.ToInt32(ckhcer7.Checked));
            cmd.Parameters.AddWithValue("@cer8", Convert.ToInt32(ckhcer8.Checked));
            cmd.Parameters.AddWithValue("@cer9", Convert.ToInt32(ckhcer9.Checked));
            cmd.Parameters.AddWithValue("@cer10", Convert.ToInt32(ckhcer10.Checked));
            cmd.Parameters.AddWithValue("@cer11", Convert.ToInt32(checkBox21.Checked));
            cmd.Parameters.AddWithValue("@cer12", Convert.ToInt32(checkBox22.Checked));
            cmd.Parameters.AddWithValue("@cer13", Convert.ToInt32(checkBox23.Checked));
            cmd.Parameters.AddWithValue("@int_sale", Convert.ToInt32(checksale.Checked));
            cmd.Parameters.AddWithValue("@bal_paid", Convert.ToInt32(checkbpaid.Checked));
            cmd.Parameters.AddWithValue("@Payr10", Convert.ToInt32(checkatt.Checked));
            cmd.Parameters.AddWithValue("@Payr11", Convert.ToInt32(checktiming.Checked));
            cmd.Parameters.AddWithValue("@schedule", Convert.ToInt32(checkBox15.Checked));


            cmd.Parameters.AddWithValue("@st1", Convert.ToInt32(checkBox16.Checked));
            cmd.Parameters.AddWithValue("@st2", Convert.ToInt32(checkBox17.Checked));
            cmd.Parameters.AddWithValue("@st3", Convert.ToInt32(checkBox18.Checked));
            cmd.Parameters.AddWithValue("@st4", Convert.ToInt32(checkBox19.Checked));
            cmd.Parameters.AddWithValue("@st5", Convert.ToInt32(checkBox20.Checked));

            cmd.Parameters.AddWithValue("@stu_sub", Convert.ToInt32(checkBox24.Checked));
            cmd.Parameters.AddWithValue("@saa", Convert.ToInt32(checkBox25.Checked));
            cmd.Parameters.AddWithValue("@bank", Convert.ToInt32(checkBox26.Checked));
            cmd.Parameters.AddWithValue("@sms_master", Convert.ToInt32(checkBox27.Checked));
            cmd.Parameters.AddWithValue("@stu_reportexam", Convert.ToInt32(checkBox28.Checked));
            cmd.Parameters.AddWithValue("@stu_exam", Convert.ToInt32(checkBox29.Checked));
            cmd.Parameters.AddWithValue("@stu_stock_re", Convert.ToInt32(checkBox31.Checked));
            cmd.Parameters.AddWithValue("@p_markssheet", Convert.ToInt32(checkBox32.Checked));
            cmd.Parameters.AddWithValue("@gatepass", Convert.ToInt32(checkBox33.Checked));
            cmd.Parameters.AddWithValue("@visitor", Convert.ToInt32(checkBox30.Checked));

            cmd.Parameters.AddWithValue("@stu_feehead", Convert.ToInt32(checkBox34.Checked));
            cmd.Parameters.AddWithValue("@stu_chkbounce", Convert.ToInt32(checkBox35.Checked));
            cmd.Parameters.AddWithValue("@stu_chkbt", Convert.ToInt32(checkBox36.Checked));
            cmd.Parameters.AddWithValue("@stu_feehwr", Convert.ToInt32(checkBox37.Checked));
            cmd.Parameters.AddWithValue("@stu_feedef", Convert.ToInt32(checkBox38.Checked));
            cmd.Parameters.AddWithValue("@stu_mfreport", Convert.ToInt32(checkBox39.Checked));

            cmd.Parameters.AddWithValue("@FSCM", Convert.ToInt32(FSCM.Checked));
            cmd.Parameters.AddWithValue("@FSYB", Convert.ToInt32(FSYB.Checked));
            cmd.Parameters.AddWithValue("@EUCo", Convert.ToInt32(EUCo.Checked));
            cmd.Parameters.AddWithValue("@EPCo", Convert.ToInt32(EPCo.Checked));
            cmd.Parameters.AddWithValue("@EERCard", Convert.ToInt32(EERCard.Checked));
            cmd.Parameters.AddWithValue("@Edesk", Convert.ToInt32(Edesk.Checked));
            cmd.Parameters.AddWithValue("@Esub", Convert.ToInt32(ESub.Checked));
            cmd.Parameters.AddWithValue("@ISItem", Convert.ToInt32(ISItem.Checked));
            cmd.Parameters.AddWithValue("@ISPurchase", Convert.ToInt32(ISPurchase.Checked));
            cmd.Parameters.AddWithValue("@ISIssue", Convert.ToInt32(ISIssue.Checked));
            cmd.Parameters.AddWithValue("@PayEIcard", Convert.ToInt32(PayEIcard.Checked));
            cmd.Parameters.AddWithValue("@SSU", Convert.ToInt32(SSU.Checked));
            cmd.Parameters.AddWithValue("@SUSess", Convert.ToInt32(SUSess.Checked));
            //cmd.Parameters.AddWithValue("@stu_mfreport", Convert.ToInt32(checkBox39.Checked));
            //cmd.Parameters.AddWithValue("@stu_mfreport", Convert.ToInt32(checkBox39.Checked));
            cmd.Parameters.AddWithValue("@ENQ11", Convert.ToInt32(checkBox40.Checked));
            cmd.Parameters.AddWithValue("@ENQ12", Convert.ToInt32(checkBox41.Checked));
            cmd.Parameters.AddWithValue("@Atten1", Convert.ToInt32(checkBox46.Checked));
            cmd.Parameters.AddWithValue("@Atten2", Convert.ToInt32(checkBox45.Checked));
            cmd.Parameters.AddWithValue("@Atten3", Convert.ToInt32(checkBox44.Checked));
            cmd.Parameters.AddWithValue("@Atten4", Convert.ToInt32(checkBox43.Checked));
            cmd.Parameters.AddWithValue("@Atten5", Convert.ToInt32(checkBox42.Checked));
            cmd.Parameters.AddWithValue("@Web1", Convert.ToInt32(checkBox51.Checked));
            cmd.Parameters.AddWithValue("@Web2", Convert.ToInt32(checkBox50.Checked));
            cmd.Parameters.AddWithValue("@Web3", Convert.ToInt32(checkBox49.Checked));
            cmd.Parameters.AddWithValue("@Web4", Convert.ToInt32(checkBox48.Checked));
          


            sc.Open();
            int w = cmd.ExecuteNonQuery();

            if (w > 0)
            {
                MsgBox.ShowBox("Record Updated...");
            }
            else
            {
                MsgBox.ShowBox("Updating Error...");
            }
            sc.Close();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void checkBox43_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox41_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox40_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox42_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox43_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void checkBox42_CheckedChanged_1(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void tabPage16_Click(object sender, EventArgs e)
        {

        }

        private void checkBox45_CheckedChanged(object sender, EventArgs e)
        {

        }
    }
}
