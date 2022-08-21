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



namespace sms
{
    public partial class frmroute : Form
    {
        public frmroute()
        {
            InitializeComponent();
            InitGrid();
            Cancil_form();
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["RT_ID"].Width = 100;
            dataGridView1.Columns["RT_DESC"].Width = 400;
        }
        private void Cancil_form()
        {

            btn_create.Enabled = true;
              Update.Enabled = false;
            btn_delete.Enabled = false;
            txt_stopname.Text = "";
           


        }
        private void LoadGrid()
        {


            dataclass dc = new dataclass();
   
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from ROUTE_T where RT_DESC like '" + txt_stopname.Text + "%' ORDER BY RT_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }

       

        private void btn_create_Click(object sender, EventArgs e)
        {

            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            string str = sc.DataSource.ToString();
            if (txt_stopname.Text == "")
            {
                MsgBox.ShowBox("Route name Should not be find...");
            }
            else
            {
                SqlCommand com = new SqlCommand("insert into ROUTE_T values('" + txt_stopname.Text + "')", sc);

                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Route Name Created...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Route Name already exist");
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
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Route!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
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
                    SqlCommand objCmd = new SqlCommand("DELETE FROM ROUTE_T WHERE RT_ID=" + txt_Id.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Route Name Deleted Successfully!!!");
                        Cancil_form();
                        InitGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox(" Deleting Error!!!");
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
                txt_stopname.Text = dataGridView1.CurrentRow.Cells["RT_DESC"].Value.ToString();
                txt_Id.Text = dataGridView1.CurrentRow.Cells["RT_ID"].Value.ToString();

                // txtstateid.Text = dataGridView1.CurrentRow.Cells["id"].Value.ToString();
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
                SqlCommand com = new SqlCommand("update ROUTE_T set RT_DESC='" + txt_stopname.Text + "' where RT_ID= " + txt_Id.Text + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Route Name Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Route Name already exist");
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

        private void frmroute_Load(object sender, EventArgs e)
        {
            LoadGrid();
        }



    }
}



       

        
       
  