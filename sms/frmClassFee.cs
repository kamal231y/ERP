using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.DAL;
using sms.BLClass;
using System.Collections;

namespace sms
{
    public partial class frmClassFee : Form
    {
        public frmClassFee()
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
            //InitGrid();
           
        }
        private void LoadGrid()
        {
            string value;
            if (cmbClass.Text == "-Select-")
            {
                value = "%";

            }
            else
            {
                value = cmbClass.SelectedValue.ToString();
 
            }

            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            sc.Open();
            string query = string.Empty; ;
            query = "Select * from View_tblClassFee where CLF_CLS_ID Like '" + value + "' order by CLF_FEE_ID asc";
            SqlDataAdapter da = new SqlDataAdapter(query, sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }
        private void Cancil_form()
        {
            btn_create.Enabled = true;
            cmbClass.Text = "-Select-";
            btnUpdate.Enabled = false;
            btn_delete.Enabled = false;
           

        }
        public void InitGrid()
        {
            LoadGrid();
            dataGridView1.Columns["CLF_ID"].Visible = false;
            dataGridView1.Columns["CLS_DESC"].Visible = false;
            dataGridView1.Columns["FEE_DESC"].Width = 200;
            dataGridView1.Columns["FEE_DESC"].HeaderText = "Fee Description";
            dataGridView1.Columns["CLF_D_AMOUNT"].Width = 200;
            dataGridView1.Columns["CLF_D_AMOUNT"].HeaderText = "Day Scholar Amount";
            dataGridView1.Columns["CLF_H_AMOUNT"].Width = 150;
            dataGridView1.Columns["CLF_H_AMOUNT"].HeaderText = "Hostler Amount";
            dataGridView1.Columns["FEE_PERIOD"].Width = 150;
            dataGridView1.Columns["FEE_PERIOD"].HeaderText = "Period";
            dataGridView1.Columns["userid"].Visible = false;
            dataGridView1.Columns["date"].Visible = false;
            dataGridView1.Columns["CLF_FEE_ID"].Visible = false;
            dataGridView1.Columns["CLF_CLS_ID"].Visible = false;
           
           
        }

        private void frmClassFee_Load(object sender, EventArgs e)
        {
            bindClass();
            InitGrid();
        }
        private void bindClass()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from tblClassMaster", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["CLS_DESC"] = "-Select-";
            row["CLS_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbClass.DataSource = ds.Tables[0];
            cmbClass.DisplayMember = "CLS_DESC";
            cmbClass.ValueMember = "CLS_ID";
        }
        private void btn_create_Click(object sender, EventArgs e)
        {

        }

        private void btn_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_cancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void cmbClass_Leave(object sender, EventArgs e)
        {
            InitGrid();
            //LoadGrid();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["CLF_ID"].Index)
            {
                this.dataGridView1.CellEndEdit += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellEndEdit);
            }
        }

        private void dataGridView1_DoubleClick(object sender, EventArgs e)
        {
            if (dataGridView1.Rows.Count > 0)
                if (dataGridView1.CurrentCell.ColumnIndex == dataGridView1.Columns["CLF_ID"].Index)
                    dataGridView1.BeginEdit(true);
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (txtid.Text == "-1")
                return;
            dataclass dc = new dataclass();
            ArrayList arr = new ArrayList();
            arr.Add(dataGridView1.CurrentRow.Cells["CLF_ID"].Value);
            arr.Add(dataGridView1.CurrentRow.Cells["CLF_CLS_ID"].Value);
            arr.Add(dataGridView1.CurrentRow.Cells["CLF_FEE_ID"].Value);
            if (dataGridView1.CurrentRow.Cells["CLF_D_AMOUNT"].Value.ToString() == "")
            {
                arr.Add(dataGridView1.CurrentRow.Cells["0"].Value);
            
            }
            else
            {
                arr.Add(dataGridView1.CurrentRow.Cells["CLF_D_AMOUNT"].Value);
            }
            if (dataGridView1.CurrentRow.Cells["CLF_H_AMOUNT"].Value.ToString() == "")
            {
                arr.Add(Convert.ToInt32(dataGridView1.CurrentRow.Cells["CLF_H_AMOUNT"].Value=0));
            }
            else
            {
                arr.Add(dataGridView1.CurrentRow.Cells["CLF_H_AMOUNT"].Value);
            }
          
            arr.Add(Convert.ToInt32(dataGridView1.CurrentRow.Index) + 1);
            int j = dc.InsertRows("Sp_update_tblClassFee", ref arr, false);
            //txtPOTotalQty.Text = Convert.ToString(Convert.ToInt32(txtPOTotalQty.Text) + Convert.ToInt32(dataGridView1.CurrentRow.Cells["Qty"].Value.ToString()));
            // txtTotalAmt.Text = Convert.ToString(Convert.ToDouble(txtTotalAmt.Text) + (Convert.ToInt32(dataGridView1.CurrentRow.Cells["Qty"].Value.ToString()) * Convert.ToDouble(dataGridView1.CurrentRow.Cells["MRP"].Value.ToString())));
            //Calculation();
            txtid.Text = "-1";
            return;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        

        
    }
}
