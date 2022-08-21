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
using sms.BLClass;
using Excel = Microsoft.Office.Interop.Excel;
using Microsoft.Office.Interop.Excel;


namespace sms
{
    public partial class FrmStuBusstop : Form
    {
        string Stu_id;
        string ROLL;
        public FrmStuBusstop()
        {
            InitializeComponent();
            //Cancil_form();
            InitGrid();
            bindclass();
        }
        private void Cancil_form()
        {
            cancel();
        }


        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["STU_ID"].Visible = false;
            dataGridView1.Columns["STU_ADM_NO"].Width = 100;
            dataGridView1.Columns["StudentName"].Width = 150;
            dataGridView1.Columns["Class"].Width = 50;
            dataGridView1.Columns["Roll_No"].Width = 100;
            dataGridView1.Columns["STOP_DESC"].Width = 100;
            dataGridView1.Columns["RT_DESC"].Width = 100;
            dataGridView1.Columns["VEHICLE_NO"].Width = 170;          
            dataGridView1.Columns["Stop_Fare"].Width = 60;
            dataGridView1.Columns["STU_FATHER_NAME"].Width = 200;
            //dataGridView1.Columns["date"].Width = 80;
           // dataGridView1.Columns["terminalid"].Width = 100;
        }
        private void LoadGrid()
        {
            string Adm;
            if (txtstuno.Text == "")
            {
                Adm = "%";
            }


            else
            {
                Adm = txtstuno.Text;
            }
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID,Stu_ADM_NO,(STU_FIRST_NAME+STU_MID_NAME+STU_LAST_NAME) as StudentName,(CLS_DESC+' '+SEC_DESC) as Class,STC_ROLL_NO as Roll_No,STOP_DESC,RT_DESC,VEHICLE_NO,Stop_Fare,STU_FATHER_NAME,SEC_ID,stop_type,Vehicle_id  from View_tblstubusstop where stu_adm_no like '" + Adm + "' order by StudentName ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            dataGridView1.Visible = true;
           dataGridView1.DataSource = dt;
            label19.Text = "Total No Of Student : " + dt.Rows.Count.ToString();
           

        }
        private void bindclass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select (CLS_DESC + SEC_DESC) as Class ,* from CLASS_T ct,SECTION_T st where ct.CLS_ID =st.SEC_CLS_ID  order by SEC_ID ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["Class"] = "Select";
            row["SEC_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "Class";
            cmb_class.ValueMember = "SEC_ID";
        }
        private void bindbusstop()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from BUS_STOP_T order by STOP_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["STOP_DESC"] = "Select";
            row["STOP_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_busstop.DataSource = ds.Tables[0];
            cmb_busstop.DisplayMember = "STOP_DESC";
            cmb_busstop.ValueMember = "STOP_ID";           
        }

        private void BindRoute()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from route_t order by rt_desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["rt_DESC"] = "Select";
            row["rt_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbRoute.DataSource = ds.Tables[0];
            cmbRoute.DisplayMember = "rt_DESC";
            cmbRoute.ValueMember = "rt_ID";
        }
        
      private void Bindstopside()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from BUS_STOP_TYPE order by id", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
           cmbtype.DataSource = ds.Tables[0];
           cmbtype.DisplayMember = "type";
           cmbtype.ValueMember = "ID";
        }


        private void Update_Click(object sender, EventArgs e)
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            txtvid.Text = "1";

            int mt1 = 0, mt2 = 0, mt3 = 0, mt4 = 0, mt5 = 0, mt6 = 0, mt7 = 0, mt8 = 0, mt9 = 0, mt10 = 0, mt11 = 0, mt12 = 0;
            if (chkApr.Checked)
                mt1 = 1;
            if (chkMay.Checked)
                mt2 = 1;
            if (chkJune.Checked)
                mt3 = 1;
            if (chkjuly.Checked)
                mt4 = 1;
            if (chkAug.Checked)
                mt5 = 1;
            if (chkSep.Checked)
                mt6 = 1;
            if (chkOct.Checked)
                mt7 = 1;
            if (chkNov.Checked)
                mt8 = 1;
            if (chkDec.Checked)
                mt9 = 1;
            if (chkJan.Checked)
                mt10 = 1;
            if (chkFeb.Checked)
                mt11 = 1;
            if (chkMar.Checked)
                mt12 = 1;

            int value;
         
            if (cmb_class.Text == "" || cmb_class.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Class Field!!!");
                return;
            }
            else
                if (cmb_rollno.Text == "" || cmb_rollno.Text == "Select")
                {
                    MsgBox.ShowBox("Please Select RollNo Field!!!");
                    return;
                }
                else
                    if (cmb_busstop.Text == "" || cmb_busstop.Text == "Select")
                    {
                        MsgBox.ShowBox("Please Select Bus Stop Field!!!");
                        return;
                    }
                    else
                        if (cmbvehicle.Text == "" )
                        {
                            MsgBox.ShowBox("Please Select Vehicle Field!!!");
                            return;

                        }
                        else
                        {
                            dataclass dd = new dataclass();
                            SqlConnection sc = dd.con();
                            string str = sc.DataSource.ToString();
                            SqlCommand com = new SqlCommand("insert_StudentBusStop '" + txtvehicle.Text + "'," + cmb_busstop.SelectedValue + "," + txtvid.Text + "," + cmbtype.SelectedValue + ",'" + cmbperiod.Text+ "','" + mt1 + "','" + mt2 + "','" + mt3 + "','" + mt4 + "','" + mt5 + "','" + mt6 + "','" + mt7 + "','" + mt8 + "','" + mt9 + "','" + mt10 + "','" + mt11 + "','" + mt12 + "'", sc);
                            //SqlCommand com = new SqlCommand("Insert_Subject '" + cmbSubject.SelectedValue + "', '" + cmbClass.SelectedValue + "','" + Convert.ToInt32(value) + "','" + Convert.ToInt32(value1) + "','" + SchoolManagementSystem.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
                            SqlDataReader dr;
                            sc.Open();

                            try
                            {
                                dr = com.ExecuteReader();
                                if (dr.RecordsAffected > 0)
                                {
                                    MsgBox.ShowBox("Record Created...");
                                    Cancil_form();
                                    LoadGrid();
                                }
                                else
                                {
                                    MsgBox.ShowBox("This Record Name Not exist");
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                sc.Close();
                            }

                        }
        }
        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cmb_rollno_SelectedIndexChanged(object sender, EventArgs e)
        {
            //lblrollnumber.Text = cmbRollNo.Text;
           // lblrollno.Text = " cmb_rollno.SelectedValue";
        }
       
        private void FrmStuBusstop_Load(object sender, EventArgs e)
        {
            BindRoute(); Bindstopside(); listBox1.Visible = false; bindbusstop();
            cmbperiod.Text = "ALL";      
        }

        private void cmb_class_Leave(object sender, EventArgs e)
        {
            string StateName;
            if (cmb_class.Text == "-Select-" || cmb_class.Text == "")
            {
                StateName = "%";
            }
            else
            {
                StateName = cmb_class.SelectedValue.ToString();
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from STUDENT_CLASS_T where STC_SEC_ID like  '" + StateName + "' order by  STC_ROLL_NO", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);

            //DataRow row = dt.NewRow();
            //row["STC_ROLL_NO"] = "-Select-";
            //row["STC_STU_ID"] = 0;
            //dt.Rows.InsertAt(row, 0);
            cmb_rollno.DataSource = dt;
            cmb_rollno.DisplayMember = "STC_ROLL_NO";
            cmb_rollno.ValueMember = "STC_STU_ID";
        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                cmb_class.SelectedValue = dataGridView1.CurrentRow.Cells["Sec_ID"].Value.ToString();
                cmb_rollno.Text = dataGridView1.CurrentRow.Cells["roll_no"].Value.ToString();
                cmb_busstop.Text = dataGridView1.CurrentRow.Cells["STOP_DESC"].Value.ToString();
                cmb_busstop.Leave += new EventHandler(cmb_busstop_Leave);

                txtvehicle.Text = dataGridView1.CurrentRow.Cells["STU_ID"].Value.ToString();
                txtstuno.Text = dataGridView1.CurrentRow.Cells["Stu_ADM_NO"].Value.ToString();
                txtsname.Text = dataGridView1.CurrentRow.Cells["Studentname"].Value.ToString();
                cmbRoute.Text = dataGridView1.CurrentRow.Cells["RT_DESC"].Value.ToString();
                txtfname.Text = dataGridView1.CurrentRow.Cells["STU_FATHER_NAME"].Value.ToString();
                cmbtype.Text = dataGridView1.CurrentRow.Cells["stop_type"].Value.ToString();
                cmbvehicle.SelectedText = dataGridView1.CurrentRow.Cells["VEHICLE_NO"].Value.ToString();
                txtvid.Text = dataGridView1.CurrentRow.Cells["VEHICLE_ID"].Value.ToString();

                Update.Enabled = true;
                btn_delete.Enabled = true;
                listBox1.Visible = false;
                //-------------------------------------------------------------------------------------------
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select  * from BUS_FEE_DETAIL_T where STU_ID like '" + txtvehicle.Text + "'", sc);
                System.Data.DataTable dt = new System.Data.DataTable();
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    panel4.Visible = true;
                    if (dt.Rows[0]["APR"].ToString() == true.ToString())
                    {
                        chkApr.Checked = true;
                    }
                    else
                    {
                        chkApr.Checked = false;
                    }
                    if (dt.Rows[0]["MAY"].ToString() == true.ToString())
                    {
                        chkMay.Checked = true;
                    }
                    else
                    {
                        chkMay.Checked = false;
                    }
                    if (dt.Rows[0]["JUN"].ToString() == true.ToString())
                    {
                        chkJune.Checked = true;
                    }
                    else
                    {
                        chkJune.Checked = false;
                    }
                    if (dt.Rows[0]["JUL"].ToString() == true.ToString())
                    {
                        chkjuly.Checked = true;
                    }
                    else
                    {
                        chkjuly.Checked = false;
                    }
                    if (dt.Rows[0]["AUG"].ToString() == true.ToString())
                    {
                        chkAug.Checked = true;
                    }
                    else
                    {
                        chkAug.Checked = false;
                    }
                    if (dt.Rows[0]["SEP"].ToString() == true.ToString())
                    {
                        chkSep.Checked = true;
                    }
                    else
                    {
                        chkSep.Checked = false;
                    }
                    if (dt.Rows[0]["OCT"].ToString() == true.ToString())
                    {
                        chkOct.Checked = true;
                    }
                    else
                    {
                        chkOct.Checked = false;
                    }
                    if (dt.Rows[0]["NOV"].ToString() == true.ToString())
                    {
                        chkNov.Checked = true;
                    }
                    else
                    {
                        chkNov.Checked = false;
                    }
                    if (dt.Rows[0]["DEC"].ToString() == true.ToString())
                    {
                        chkDec.Checked = true;
                    }
                    else
                    {
                        chkDec.Checked = false;
                    }
                    if (dt.Rows[0]["JAN"].ToString() == true.ToString())
                    {
                        chkJan.Checked = true;
                    }
                    else
                    {
                        chkJan.Checked = false;
                    }
                    if (dt.Rows[0]["FEB"].ToString() == true.ToString())
                    {
                        chkFeb.Checked = true;
                    }
                    else
                    {
                        chkFeb.Checked = false;
                    }
                    if (dt.Rows[0]["MAR"].ToString() == true.ToString())
                    {
                        chkMar.Checked = true;
                    }
                    else
                    {
                        chkMar.Checked = false;
                    }
                }
                cmbperiod.Text = "ALL";
            }
        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Record!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    if (txtvehicle.Text == "")
                    {
                        MsgBox.ShowBox("Please Select Student Record !!!");
                        return;
                    }

                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.StoredProcedure;
                    cmd1.CommandText = "delete_transport";
                    cmd1.Connection = sc;
                    sc.Open();
                    cmd1.Parameters.AddWithValue("@stu_id", Convert.ToInt32(txtvehicle.Text.Trim()));
                    int i = cmd1.ExecuteNonQuery();
                    if (i > 0)
                    {
                        MsgBox.ShowBox(" Record Delete  Successfully!!!");

                          LoadGrid();

                    }
                    else
                    {
                        MsgBox.ShowBox(" Record can't be Canceled .!!!");
                    }
                    cancel();
                }
                catch (Exception ex)
                { }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }
        }

        private void cmb_busstop_Leave(object sender, EventArgs e)
        {
            string vid;
            if (cmb_busstop.Text == "" || cmb_busstop.Text == "Select")
                vid = "0";
            else
                vid = cmb_busstop.SelectedValue.ToString();
            {
                dataclass dc = new dataclass();
                SqlConnection sc = dc.con();
                SqlDataAdapter da = new SqlDataAdapter(" select * from View_bindVehicle where VEHICLE_LOAD_CAPACITY >0 and STOP_ID=" + vid + "   order by VEHICLE_ID ", sc);
                DataSet ds = new DataSet();
                da.Fill(ds);
                DataRow row = ds.Tables[0].NewRow();
                row["VEHICLE_NO"] = "Select";
                row["VEHICLE_ID"] = 0;
                ds.Tables[0].Rows.InsertAt(row, 0);

                cmbvehicle.DataSource = ds.Tables[0];
                cmbvehicle.DisplayMember = "VEHICLE_NO";
                cmbvehicle.ValueMember = "VEHICLE_ID";

            }
        }

        private void txtstuno_Leave(object sender, EventArgs e)
        {
            bindstuadmleave();

        }
        private void bindstuadmleave()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select *  from View_STUDENTREPORT where  ADMNO like '" + txtstuno.Text.Trim().Replace(" ", "") + "'  ", sc);
            System.Data.DataTable dt = new System.Data.DataTable();
            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                Stu_id = dt.Rows[0]["STU_ID"].ToString();
                txtvehicle.Text = dt.Rows[0]["STU_ID"].ToString();
                txtcardNo.Text = dt.Rows[0]["ADMNO"].ToString();
                txtcardNo.Enabled = false;
                cmb_class.SelectedValue = dt.Rows[0]["STC_SEC_ID"].ToString();
                cmb_class.Enabled = false;
                cmb_rollno.SelectedText = dt.Rows[0]["ROLL"].ToString();
                cmb_rollno.Enabled = false;
                txtsname.Text = dt.Rows[0]["Student_Name"].ToString();
                txtfname.Text = dt.Rows[0]["FatherName"].ToString();
                LoadGrid();
                //cmbRoute.Text = dt.Rows[0]["RT_DESC"].ToString();
                //cmbvehicle.Text = dt.Rows[0]["STOP_DESC"].ToString();
                // BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
            else
            {
                cmb_class.SelectedValue = 0;
                ROLL = "0";
                Stu_id = "0";
                // stuname.Text = "";
                // BIND_DATA();
                //cmb_rollno.Enabled = false;
                //cmb_class.Enabled = false;
            }
            listBox1.Visible = false;
        
      
            
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            cancel();
        }
        private void cancel()
        {
            chkall.Checked = false;
            txtcardNo.Enabled = true;
            txtcardNo.Text = "";
            cmb_rollno.Enabled = true;
            cmb_class.Enabled = true;
            cmb_class.SelectedValue = 0;
            cmb_rollno.SelectedValue = 0;
            cmbRoute.SelectedValue = 0;
            txtstuno.Enabled = true;
            txtstuno.Text = "";
            txtsname.Text = "";
            txtfname.Text = "";
            txtsname.Enabled = true;
            txtfname.Enabled = true;
            listBox1.Visible = false;
            chkApr.Checked = false;
            chkMay.Checked = false;
            chkJune.Checked = false;
            chkjuly.Checked = false;
            chkAug.Checked = false;
            chkSep.Checked = false;
            chkOct.Checked = false;
            chkNov.Checked = false;
            chkDec.Checked = false;
            chkJan.Checked = false;
            chkFeb.Checked = false;
            chkMar.Checked = false;
            panel4.Visible = false;
            cmbperiod.Text = "ALL"; 
        }
        private void cmbRoute_Leave(object sender, EventArgs e)
        {
            bindbusstop();
        }

        private void txtstuno_Enter(object sender, EventArgs e)
        {

        }

        private void txtsname_TextChanged(object sender, EventArgs e)
        {
            {
                listBox1.Visible = true;
                string Name, CLS;

                if (txtsname.Text == "-Select-" || txtsname.Text == "" || txtsname.Text == "System.Data.DataRowView")
                {
                    Name = "%";
                }
                else
                {
                    Name = txtsname.Text.Trim().ToString();
                }
                if (cmb_class.Text == "Select" || cmb_class.Text == "" || cmb_class.Text == "System.Data.DataRowView")
                {
                    CLS = "%";
                }
                else
                {
                    CLS = cmb_class.SelectedValue.ToString();
                }
            
                dataclass dd = new dataclass();

                SqlConnection sc1 = dd.con();
                int i, j;
                listBox1.Items.Clear();
                sc1.Open();
                SqlDataAdapter daa = new SqlDataAdapter("select AdmNo,Student_Name,FatherName,class from view_studentreport where  Student_Name LIKE '" + Name + "' + '%' and stu_cls_id like'" + CLS + "' and stu_status='y' order by roll ", sc1);
                DataSet ds1 = new DataSet();
                i = 0;
                j = 0;
                daa.Fill(ds1);
                i = Convert.ToInt32(ds1.Tables[0].Rows.Count);

                while (i > 0 && j < i)
                {
                    listBox1.Items.Add(ds1.Tables[0].Rows[j][0].ToString() + '/' + ds1.Tables[0].Rows[j][1].ToString() + '/' + ds1.Tables[0].Rows[j][2].ToString() + '/' + ds1.Tables[0].Rows[j][3].ToString());


                    j = j + 1;
                }
                if (i > 0)
                {
                    listBox1.Top = txtsname.Top + txtsname.Height;
                   // listBox1.Width = txtsname.Width;
                    listBox1.Left = txtsname.Left;
                    listBox1.Visible = true;
                }
                else
                {
                    listBox1.Items.Clear();
                    listBox1.Visible = false;
                }


            }
           
        }

        private void listBox1_DoubleClick(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    txtstuno.Text = str[0].ToString();
                    listBox1.Visible = false;
                    bindstuadmleave();

                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
            listBox1.Visible = false;
        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void chkMt_CheckedChanged(object sender, EventArgs e)
        {

           
        }

        private void cmbperiod_Leave(object sender, EventArgs e)
        {
            if (cmbperiod.Text == "More Time")
                panel4.Visible = true;
            else
                panel4.Visible = false;  

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void btnexport_Click(object sender, EventArgs e)
        {
         /*   string str1;
            if (cmbroute.Text == "" || cmbroute.Text == "Select")
                str1 = "%";
            else
                str1 = cmbroute.Text;*/
            string Adm;
            if (txtstuno.Text == "")
            {
                Adm = "%";
            }


            else
            {
                Adm = txtstuno.Text;
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select STU_ID,Stu_ADM_NO,(STU_FIRST_NAME+STU_MID_NAME+STU_LAST_NAME) as StudentName,(CLS_DESC+' '+SEC_DESC) as Class,STC_ROLL_NO as Roll_No,STU_MOBILE,STOP_DESC,RT_DESC,VEHICLE_NO,Stop_Fare,STU_FATHER_NAME,SEC_ID,stop_type,Vehicle_id  from View_tblstubusstop where stu_adm_no like '" + Adm + "' order by StudentName ", sc);
           
            dataGridView1.Visible = true;
            System.Data.DataSet dt = new System.Data.DataSet();
            da.Fill(dt);
            if (dt.Tables[0].Rows.Count > 0)
            {
                ExportDataSetToExcel(dt);
            }
        }
        private void ExportDataSetToExcel(DataSet ds)
        {
            try
            {
                Microsoft.Office.Interop.Excel.ApplicationClass ExcelApp = new Microsoft.Office.Interop.Excel.ApplicationClass();
                Workbook xlWorkbook = ExcelApp.Workbooks.Add(Microsoft.Office.Interop.Excel.XlWBATemplate.xlWBATWorksheet);

                // Loop over DataTables in DataSet.
                DataTableCollection collection = ds.Tables;

                for (int i = collection.Count; i > 0; i--)
                {
                    Sheets xlSheets = null;
                    Worksheet xlWorksheet = null;
                    //Create Excel Sheets
                    xlSheets = ExcelApp.Sheets;
                    xlWorksheet = (Worksheet)xlSheets.Add(xlSheets[1],
                                   Type.Missing, Type.Missing, Type.Missing);

                    System.Data.DataTable table = collection[i - 1];
                    xlWorksheet.Name = table.TableName;

                    for (int j = 1; j < table.Columns.Count + 1; j++)
                    {
                        ExcelApp.Cells[1, j] = table.Columns[j - 1].ColumnName;
                    }

                    // Storing Each row and column value to excel sheet
                    for (int k = 0; k < table.Rows.Count; k++)
                    {
                        for (int l = 0; l < table.Columns.Count; l++)
                        {
                            ExcelApp.Cells[k + 2, l + 1] =
                            table.Rows[k].ItemArray[l].ToString();
                        }
                    }
                    ExcelApp.Columns.AutoFit();
                }
                ((Worksheet)ExcelApp.ActiveWorkbook.Sheets[ExcelApp.ActiveWorkbook.Sheets.Count]).Delete();
                ExcelApp.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dataGridView1_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmb_busstop_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void listBox1_Leave(object sender, EventArgs e)
        {
            if (listBox1.Visible == true)
            {
                try
                {
                    string[] str = listBox1.SelectedItem.ToString().Split('/');
                    txtstuno.Text = str[0].ToString();
                    listBox1.Visible = false;
                    bindstuadmleave();

                }
                catch
                { }
            }
            if (listBox1.SelectedIndex == 0)
            {
                //txt_vendorname.Focus();
            }
            listBox1.Visible = false;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (chkall.Checked)
            {
                int mt1 = 0, mt2 = 0, mt3 = 0, mt4 = 0, mt5 = 0, mt6 = 0, mt7 = 0, mt8 = 0, mt9 = 0, mt10 = 0, mt11 = 0, mt12 = 0;
                if (chkApr.Checked)
                    mt1 = 1;
                if (chkMay.Checked)
                    mt2 = 1;
                if (chkJune.Checked)
                    mt3 = 1;
                if (chkjuly.Checked)
                    mt4 = 1;
                if (chkAug.Checked)
                    mt5 = 1;
                if (chkSep.Checked)
                    mt6 = 1;
                if (chkOct.Checked)
                    mt7 = 1;
                if (chkNov.Checked)
                    mt8 = 1;
                if (chkDec.Checked)
                    mt9 = 1;
                if (chkJan.Checked)
                    mt10 = 1;
                if (chkFeb.Checked)
                    mt11 = 1;
                if (chkMar.Checked)
                    mt12 = 1;

                                dataclass dd = new dataclass();
                                SqlConnection sc = dd.con();
                                string str = sc.DataSource.ToString();
                                SqlCommand com = new SqlCommand("UPDATE BUS_FEE_DETAIL_T SET APR = '" + mt1 + "',MAY='" + mt2 + "',JUN='" + mt3 + "',JUL='" + mt4 + "',AUG='" + mt5 + "',SEP='" + mt6 + "',OCT='" + mt7 + "',NOV='" + mt8 + "',Dec='" + mt9 + "',JAN='" + mt10 + "',Feb='" + mt11 + "',Mar='" + mt12 + "'; UPDATE fee_detail_t SET APR = '" + mt1 + "',MAY='" + mt2 + "',JUN='" + mt3 + "',JUL='" + mt4 + "',AUG='" + mt5 + "',SEP='" + mt6 + "',OCT='" + mt7 + "',NOV='" + mt8 + "',Dec='" + mt9 + "',JAN='" + mt10 + "',Feb='" + mt11 + "',Mar='" + mt12 + "' where fee_id=4; ", sc);
                               // SqlCommand com1 = new SqlCommand("", sc);
              
                //SqlCommand com = new SqlCommand("Insert_Subject '" + cmbSubject.SelectedValue + "', '" + cmbClass.SelectedValue + "','" + Convert.ToInt32(value) + "','" + Convert.ToInt32(value1) + "','" + SchoolManagementSystem.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
                                SqlDataReader dr;
                                sc.Open();

                                try
                                {
                                    dr = com.ExecuteReader();
                                    if (dr.RecordsAffected > 0)
                                    {
                                        MsgBox.ShowBox("Record Created...");
                                        Cancil_form();
                                        LoadGrid();
                                    }
                                    else
                                    {
                                        MsgBox.ShowBox("This Record Name Not exist");
                                    }

                                }
                                catch (Exception ex)
                                {
                                    MessageBox.Show(ex.Message);
                                }
                                finally
                                {
                                    sc.Close();
                                }

                            
            }

        }

        private void btnclasswise_Click(object sender, EventArgs e)
        {
            DateTime dtdob = Convert.ToDateTime(dob.Value, System.Globalization.CultureInfo.InvariantCulture);
            txtvid.Text = "1";

            int mt1 = 0, mt2 = 0, mt3 = 0, mt4 = 0, mt5 = 0, mt6 = 0, mt7 = 0, mt8 = 0, mt9 = 0, mt10 = 0, mt11 = 0, mt12 = 0;
            if (chkApr.Checked)
                mt1 = 1;
            if (chkMay.Checked)
                mt2 = 1;
            if (chkJune.Checked)
                mt3 = 1;
            if (chkjuly.Checked)
                mt4 = 1;
            if (chkAug.Checked)
                mt5 = 1;
            if (chkSep.Checked)
                mt6 = 1;
            if (chkOct.Checked)
                mt7 = 1;
            if (chkNov.Checked)
                mt8 = 1;
            if (chkDec.Checked)
                mt9 = 1;
            if (chkJan.Checked)
                mt10 = 1;
            if (chkFeb.Checked)
                mt11 = 1;
            if (chkMar.Checked)
                mt12 = 1;

            int value;

            if (cmb_class.Text == "" || cmb_class.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Class Field!!!");
                return;
            }
             else
                        {
                            dataclass dd = new dataclass();
                            SqlConnection sc = dd.con();
                            string str = sc.DataSource.ToString();
                            SqlCommand com = new SqlCommand("update_StudentBusStopclasswise '" + cmb_class.SelectedValue + "','" + mt1 + "','" + mt2 + "','" + mt3 + "','" + mt4 + "','" + mt5 + "','" + mt6 + "','" + mt7 + "','" + mt8 + "','" + mt9 + "','" + mt10 + "','" + mt11 + "','" + mt12 + "'", sc);
                            //SqlCommand com = new SqlCommand("Insert_Subject '" + cmbSubject.SelectedValue + "', '" + cmbClass.SelectedValue + "','" + Convert.ToInt32(value) + "','" + Convert.ToInt32(value1) + "','" + SchoolManagementSystem.Form1.GlobalLoginUserName.ToString() + "','" + dt1 + "','" + sc.WorkstationId + "'", sc);
                            SqlDataReader dr;
                            sc.Open();

                            try
                            {
                                dr = com.ExecuteReader();
                                if (dr.RecordsAffected > 0)
                                {
                                    MsgBox.ShowBox("Record Created...");
                                    Cancil_form();
                                    LoadGrid();
                                }
                                else
                                {
                                    MsgBox.ShowBox("This Record Name Not exist");
                                }

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                            finally
                            {
                                sc.Close();
                            }

                        }
        }

    }
}
