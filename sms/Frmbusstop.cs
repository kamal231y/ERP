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
    public partial class Frmbusstop : Form
    {
        public Frmbusstop()
        {
            InitializeComponent();
            InitGrid();
            Cancil_form();
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["STOP_ID"].Width = 90;
            dataGridView1.Columns["STOP_DESC"].Width = 120;
            dataGridView1.Columns["RT_DESC"].Width = 200;
            dataGridView1.Columns["STOP_KM"].Width = 90;
            dataGridView1.Columns["STOP_FARE"].Width = 90;

        }
        private void BindRoute()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from ROUTE_T", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["RT_DESC"] = "Select";
            row["RT_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbroute.DataSource = ds.Tables[0];
            cmbroute.DisplayMember = "RT_DESC";
            cmbroute.ValueMember = "RT_ID";
        }
        private void Cancil_form()
        {

            btn_create.Enabled = true;
            Update.Enabled = false;
            btn_delete.Enabled = false;
            txt_stopname.Text = "";
            txt_fare.Text = "";
            cmbroute.Text = "";
            txtkm.Text = "";


        }
        private void LoadGrid()
        {
            string str;
            if (cmbroute.Text == "" || cmbroute.Text == "Select")
                str = "%";
            else
                str = cmbroute.Text;


            dataclass dc = new dataclass();

            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select bt.STOP_ID,bt.STOP_DESC,rt.RT_DESC,bt.STOP_KM,bt.STOP_FARE,bt.INTIME,bt.Outtime,bt.Return_arrival,bt.Return_depart from BUS_STOP_T bt, ROUTE_T rt where   bt.STOP_ROUTE=rt.RT_ID and bt.STOP_DESC like '" + txt_stopname.Text + "%' and RT_DESC like '" + str + "'  ORDER BY bt.STOP_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

        private void txt_fare_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txt_stopname.Text == "")
            {
                MsgBox.ShowBox("stop name Should not be find...");
            }
            else
            {
                SqlCommand com = new SqlCommand("Insert into BUS_STOP_T values( '" + txt_stopname.Text + "'," + cmbroute.SelectedValue + "," + txtkm.Text + "," + txt_fare.Text + ",'" + dtp1.Text + "','" + dt2.Text + "','" + dt3.Text + "','" + dt4.Text + "',0)", sc);
                // SqlCommand com = new SqlCommand("Insert_tblbusstop '" + txt_stopname.Text + "','" + txt_fare.Text + "'", sc);

                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Stop Name Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Stop Name already exist");
                    }

                }
                catch (Exception ex)
                { }
                finally
                {
                    sc.Close();
                }
            }
        }

        private void Delete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txt_Id.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM BUS_STOP_T WHERE STOP_ID=" + txt_Id.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Stop Name Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("Deleting Error!!!");
                    }
                }
                catch (Exception ex)
                { }
            }
            else if (dialogResult == DialogResult.No)
            {
                //do something else
            }


        }

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            btn_create.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txt_stopname.Text = dataGridView1.CurrentRow.Cells["STOP_DESC"].Value.ToString();
                txt_fare.Text = dataGridView1.CurrentRow.Cells["STOP_FARE"].Value.ToString();
                cmbroute.Text = dataGridView1.CurrentRow.Cells["RT_DESC"].Value.ToString();
                txtkm.Text = dataGridView1.CurrentRow.Cells["STOP_KM"].Value.ToString();
                txt_Id.Text = dataGridView1.CurrentRow.Cells["STOP_ID"].Value.ToString();
                dtp1.Text = dataGridView1.CurrentRow.Cells["INTIME"].Value.ToString();
                dt2.Text = dataGridView1.CurrentRow.Cells["Outtime"].Value.ToString();
                dt3.Text = dataGridView1.CurrentRow.Cells["Return_arrival"].Value.ToString();
                dt4.Text = dataGridView1.CurrentRow.Cells["Return_depart"].Value.ToString();
                Update.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

        private void txt_stopname_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txt_Id.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txt_Id.Text == "")
            {
                MsgBox.ShowBox("State Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update BUS_STOP_T set STOP_DESC='" + txt_stopname.Text + "',STOP_FARE='" + txt_fare.Text + "',STOP_ROUTE=" + cmbroute.SelectedValue + ",STOP_KM=" + txtkm.Text + ",INTIME='" + dtp1.Text + "',Outtime='" + dt2.Text + "',Return_arrival='" + dt3.Text + "',Return_depart='" + dt4.Text + "' where STOP_ID=" + txt_Id.Text + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Stop Name Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Stop Name already exist");
                    }

                }
                catch (Exception ex)
                { }
                finally
                {
                    sc.Close();
                }
            }

        }

        private void Frmbusstop_Load(object sender, EventArgs e)
        {
            BindRoute();
            LoadGrid();
        }

        private void cmbroute_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }
      /*  private DataTable getdataindatatable(string query)
        {

            dataclass dc1 = new dataclass();
            SqlConnection conn = new SqlConnection();
            conn = dc1.con();
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            da.Fill(ds, "tblstocktransfertemp1");

            dt = ds.Tables["tblstocktransfertemp1"];
            return dt;

        }*/





        private void button1_Click(object sender, EventArgs e)
        {
            crybustime.ROUTE = cmbroute.SelectedValue.ToString();
            crybustime obj = new crybustime();
            obj.ShowDialog();



        }

        private void button2_Click(object sender, EventArgs e)
        {
            string str1;
            if (cmbroute.Text == "" || cmbroute.Text == "Select")
                str1 = "%";
            else
                str1 = cmbroute.Text;

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            dataGridView1.Visible = true;
            string str = "select bt.STOP_ID,bt.STOP_DESC,rt.RT_DESC,bt.STOP_KM,bt.STOP_FARE,bt.INTIME,bt.Outtime,bt.Return_arrival,bt.Return_depart from BUS_STOP_T bt, ROUTE_T rt where   bt.STOP_ROUTE=rt.RT_ID and bt.STOP_DESC like '" + txt_stopname.Text + "%' and RT_DESC like '" + str1 + "'  ORDER BY bt.STOP_DESC";
            SqlDataAdapter da = new SqlDataAdapter(str, sc);
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

    }
}
 



 








       

        
       
  