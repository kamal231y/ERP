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
    public partial class Frmperiod : Form
    {
        public Frmperiod()
        {
            InitializeComponent();
            //Cancil_form();
            InitGrid();
            // bindDay();
            bindstatus();
        }
        private void Cancil_form()
        {
            //txtstrength.Text = "";
            //cmbTeacher.Text = "-Select-";
            // cmb_class.Text = "Select";
            //cmb_busstop.Text = "Select";
            // cmb_rollno.Text = "Select";           
            //btnUpdate.Enabled = false;       
            // btn_delete.Enabled = false;
            /// cmbvehicle.Text = "Select";
        }


        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["LECTURE_ID"].Visible = false;
            dataGridView1.Columns["LECTURE_DESC"].Width = 150;
            dataGridView1.Columns["LECTURE_FROM"].Width = 150;
            dataGridView1.Columns["LECTURE_TO"].Width = 150;
        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from lecture_t order by lecture_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void bindstatus()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from lecture_t order by lecture_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["LECTURE_DESC"] = "select";
            row["lecture_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmb_class.DataSource = ds.Tables[0];
            cmb_class.DisplayMember = "LECTURE_DESC";
            cmb_class.ValueMember = "lecture_ID";
        }

        

        private void btn_clsoe_Click(object sender, EventArgs e)
        {
            this.Close();

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
                    if (cmb_class.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM LECTURE_T WHERE LECTURE_ID=" + txtid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Lecture Deleted Successfully!!!");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("Deleting Error!!!");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }


            }
        }

         private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void btExit_Click(object sender, EventArgs e)
        {

        }

     
        private void save_btn_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(cmb_timeto.Text);
            DateTime dt2 = Convert.ToDateTime(cmb_timefrom.Text);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlDataReader dr;
            string str = sc.DataSource.ToString();
            SqlCommand cmd = new SqlCommand("insert into LECTURE_T values('"+cmb_class.Text+"','" + cmb_timefrom.Text + "','" + cmb_timeto.Text + "')", sc);
            sc.Open();
            try
            {

                dr = cmd.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Lecture Created");
                    Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("Not created");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
            {
                txtid.Text = dataGridView1.CurrentRow.Cells["LECTURE_ID"].Value.ToString();
                cmb_class.Text = dataGridView1.CurrentRow.Cells["LECTURE_DESC"].Value.ToString();
                cmb_timefrom.Text = dataGridView1.CurrentRow.Cells["LECTURE_FROM"].Value.ToString();
                cmb_timeto.Text = dataGridView1.CurrentRow.Cells["LECTURE_TO"].Value.ToString();
               
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DateTime dt1 = Convert.ToDateTime(cmb_timeto.Text);
            DateTime dt2 = Convert.ToDateTime(cmb_timefrom.Text);
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            SqlDataReader dr;
            string str = sc.DataSource.ToString();
            SqlCommand cmd = new SqlCommand("UPDATE_LECTURE_T " + txtid.Text + ",'" + cmb_timefrom.Text + "','" + cmb_timeto.Text + "'", sc);
            sc.Open();
            try
            {

                dr = cmd.ExecuteReader();
                if (dr.RecordsAffected > 0)
                {
                    MsgBox.ShowBox("Record Updated...");
                    Cancil_form();
                    LoadGrid();
                }
                else
                {
                    MsgBox.ShowBox("Record already exit");
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






            


     
       

     

      

     
            
          

        

          
   

        

        

       
          

       
    


    
