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
    public partial class FrmSetBusRoute : Form
    {
        public FrmSetBusRoute()
        {

            InitializeComponent();
            InitGrid();
            Cancil_form();
        }

        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["VEHICLE_NO"].Width =200;
            dataGridView1.Columns["RT_DESC"].Width = 500;
            //  dataGridView1.Columns["RT_DESC"].Width = 90;
            //dataGridView1.Columns["STOP_KM"].Width = 90;
            //dataGridView1.Columns["STOP_FARE"].Width = 90;
            //dataGridView1.Columns["userid"].Width = 90;
            //dataGridView1.Columns["date"].Width = 90;
            //dataGridView1.Columns["terminalid"].Width = 90;
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
        private void BindVehicle()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select VEHICLE_ID,VEHICLE_NO from VEHICLE_T   order by VEHICLE_ID ", sc);
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
        private void Cancil_form()
        {    
           
            btn_delete.Enabled = false;
            cmbvehicle.Text = "Select";
            cmbroute.Text = "Select";
            txt_Id.Text = "";


        }
        private void LoadGrid()
        {
            dataclass dc = new dataclass();
            SqlDataAdapter da;
            SqlConnection sc = dc.con();
            string route, vehicle;
            if (cmbroute.Text == "" || cmbroute.Text == "Select")
                route = "%";
            else
                route = cmbroute.Text;
            if (cmbvehicle.Text == "" || cmbvehicle.Text == "Select")
                vehicle = "%";
            else
                vehicle = cmbvehicle.Text;


            da = new SqlDataAdapter("select vh.VEHICLE_NO,rt.RT_DESC from  VEHICLE_ROUTE_T vt,ROUTE_T rt, VEHICLE_T vh where vt.VR_RT_ID=rt.RT_ID and vh.VEHICLE_ID=vt.VR_VH_ID and  rt.RT_DESC like '" + route + "' and vh.VEHICLE_NO like '"+vehicle+"' ", sc);
            
                DataSet ds = new DataSet();
                 da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
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
                    if (cmbroute.Text == "" || cmbroute.Text == "Select")
                    {
                        MsgBox.ShowBox("Please Select Route Field!!!");
                        return;
                    }
                    else
                        if (cmbvehicle.Text == "" || cmbvehicle.Text == "Select")
                        {
                            MsgBox.ShowBox("Please Select Vehicle Field!!!");
                            return;

                        }
                        else
                        {
                            SqlCommand objCmd = new SqlCommand("delete from  VEHICLE_ROUTE_T  where VR_VH_ID=" + cmbvehicle.SelectedValue + " and VR_RT_ID=" + cmbroute.SelectedValue +" ", conn);
                            SqlDataReader dr;
                            objCmd.Connection.Open();
                            dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                            if (dr.RecordsAffected > 0)
                            {
                                MsgBox.ShowBox(" Record Deleted Successfully!!!");
                                Cancil_form();
                                InitGrid();
                            }
                            else
                            {
                                MsgBox.ShowBox(" Deleting Error!!!");
                            }
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
               if (dataGridView1.Rows.Count > 0)
            {
                cmbroute.Text = dataGridView1.CurrentRow.Cells["RT_DESC"].Value.ToString();
                cmbvehicle.Text = dataGridView1.CurrentRow.Cells["VEHICLE_NO"].Value.ToString();
                 Update.Enabled = true;
                btn_delete.Enabled = true;
            }
        }

       

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void Update_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (cmbroute.Text == "" || cmbroute.Text == "Select")
            {
                MsgBox.ShowBox("Please Select Vehicle Field!!!");
                return;
            }
            else
                if (cmbvehicle.Text == "" || cmbvehicle.Text == "Select")
                {
                    MsgBox.ShowBox("Please Select Route Field!!!");
                    return;

                }
            else
            {
                SqlCommand com = new SqlCommand(" Set_Vehicle_Route " + cmbroute.SelectedValue + " ," + cmbvehicle.SelectedValue + "", sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Vehicle Route  Saved...");
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
            BindRoute(); BindVehicle(); LoadGrid();
        }

        private void cmbroute_Leave(object sender, EventArgs e)
        {
            //if (cmbroute.SelectedValue != null)
            //{
            //    dataclass dc = new dataclass();
            //    SqlConnection sc = dc.con();
            //    SqlDataAdapter da = new SqlDataAdapter(" select VEHICLE_ID,VEHICLE_NO from VEHICLE_T where OnRoute=0  order by VEHICLE_ID ", sc);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds);
            //    DataRow row = ds.Tables[0].NewRow();
            //    row["VEHICLE_NO"] = "Select";
            //    row["VEHICLE_ID"] = 0;
            //    ds.Tables[0].Rows.InsertAt(row, 0);

            //    cmbvehicle.DataSource = ds.Tables[0];
            //    cmbvehicle.DisplayMember = "VEHICLE_NO";
            //    cmbvehicle.ValueMember = "VEHICLE_ID";
            //    LoadGrid();
            //}
        }

        private void cmbroute_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmbvehicle_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }



    }
}



       

        
       
  