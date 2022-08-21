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

namespace sms
{
    public partial class frmBookNameMaster : Form
    {
        public static int subject = 1;
        public static int   publication=1;
        BLclass bl = new BLclass();
        validation vl = new validation();

        public frmBookNameMaster()
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
            Cancil_form();        
        }
        public void bindlanguage()
             {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from LANGUAGE_T order by LANG_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            cmb_language.DataSource = ds.Tables[0];
            cmb_language.DisplayMember = "LANG_DESC";
            cmb_language.ValueMember = "LANG_ID";

              }

          public void bindsubject()
             {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" Select * from SUBJECT_T order by sub_DESC", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["sub_DESC"] = "Select";
            row["sub_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmb_subjectname.DataSource = ds.Tables[0];
            cmb_subjectname.DisplayMember = "sub_DESC";
            cmb_subjectname.ValueMember = "sub_ID";
                    
             }
          public void Publisher()
            {
             
              dataclass dc = new dataclass();
              SqlConnection sc = dc.con();
              SqlDataAdapter da = new SqlDataAdapter(" select * from Public_T ", sc);
              DataSet ds = new DataSet();
              da.Fill(ds);
              DataRow row = ds.Tables[0].NewRow();              
              cmb_publishname.DataSource = ds.Tables[0];
              cmb_publishname.DisplayMember = "Publication_NAME";
              cmb_publishname.ValueMember = "Publication_Id";
          }
          private void bindclass()
          {
              dataclass dc = new dataclass();
              SqlConnection sc = dc.con();
              SqlDataAdapter da = new SqlDataAdapter(" select CLS_ID,CLS_DESC  from CLASS_T order by cls_sequence ", sc);
              DataSet ds = new DataSet();
              da.Fill(ds);
              DataRow row = ds.Tables[0].NewRow();
              row["CLS_DESC"] = "ALL";
              row["CLS_ID"] = 0;
              ds.Tables[0].Rows.InsertAt(row, 0);

              cmb_class.DataSource = ds.Tables[0];
              cmb_class.DisplayMember = "CLS_DESC";
              cmb_class.ValueMember = "CLS_ID";
          }
          private void bindcategory()
          {
              dataclass dc = new dataclass();
              SqlConnection sc = dc.con();
              SqlDataAdapter da = new SqlDataAdapter(" select *  from book_cat_t order by bc_desc ", sc);
              DataSet ds = new DataSet();
              da.Fill(ds);
              DataRow row = ds.Tables[0].NewRow();             

              cmb_category.DataSource = ds.Tables[0];
              cmb_category.DisplayMember = "bc_desc";
              cmb_category.ValueMember = "bc_ID";
          }
          public void place()
          {
              dataclass dc = new dataclass();
              SqlConnection sc = dc.con();
              sc.Open();
              SqlDataAdapter adap = new SqlDataAdapter("select Publication_Address from Public_T where Publication_Id='" + cmb_publishname.SelectedValue + "' ", sc);
              DataTable dt = new DataTable();
              adap.Fill(dt);
              if (dt.Rows.Count > 0)
              {
                 textBox2.Text = ((dt.Rows[0][0].ToString())).ToString();

              }
          }
    
        private void Cancil_form()
        {
            txt_nopages.Text = "";
            btnCreate.Enabled = true;
            btnUpdate.Enabled = false;
            btnDelete.Enabled = false;
            txtBarcode.Text = "";            
            textBox1.Text = "";        
            textBox2.Text = "";
            txt_volume.Text = "";
            txt_source.Text = "";
            txt_section.Text = "";
            txt_remark.Text = "";
            txt_mrp.Text = "";
            txt_bookno.Text = "";
            txtbooktitle.Text = "";
            txtisbnno.Text = "";
            txtisbnno.Text = "";
            txtyear.Text = "";

        }
      
        public void InitGrid()
        {
            //LoadGrid();
            //dataGridView1.Columns["LANG_ID"].Visible = false;
            //dataGridView1.Columns["LANG_DESC"].Width = 260;
            //dataGridView1.Columns["LANG_DESC"].HeaderText = "LANGUAGE";
            
        }
        private void LoadGrid()
       {
            string barcode, sub;
            if (txtBarcode.Text == "")
            {
                barcode = "%";
            }
            else
            {
                barcode = txtBarcode.Text.ToString();
            }
            if (cmb_subjectname.Text == "" || cmb_subjectname.Text == "Select")
            {
                sub = "%";
            }
            else
            {
                sub = cmb_subjectname.Text;
            }

            
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("select * from View_bmaster where sub_desc like '" + sub + "' and Barcode_No like '" + barcode + "' ORDER BY sub_desc", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
         
        }
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Test!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialogResult == DialogResult.Yes)
            {
                try
                {
                    dataclass dc = new dataclass();
                    SqlConnection conn = dc.con();
                    if (txtcityid.Text == "")
                    {
                        MsgBox.ShowBox("Please Click to Detail Row!!!");
                        return;
                    }
                    SqlCommand objCmd = new SqlCommand("DELETE FROM BMaster_T WHERE B_ID=" + txtcityid.Text + "", conn);
                    SqlDataReader dr;
                    objCmd.Connection.Open();
                    dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox(" Book Deleted Successfully!!!");
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

        private void btnCreate_Click(object sender, EventArgs e)
        {
            try
            {

            if (txt_mrp.Text == "")
                txt_mrp.Text = "0";
            if (txt_section.Text == "")
                txt_section.Text = "0";
            DateTime dt1 = Convert.ToDateTime(DateTime.Now, System.Globalization.CultureInfo.InvariantCulture);
            
            if (txtBarcode.Text == "")
            {
                MsgBox.ShowBox("Barcode Name Should not be blank...");
                return;
            }
            if (cmb_subjectname.Text == "" || cmb_subjectname.Text == "select")
            {
                MsgBox.ShowBox("Subject  Should not be blank...");
                return;
            }
            if (cmb_publishname.Text == "" || cmb_publishname.Text == "select")
            {
                MsgBox.ShowBox("Publisher  Should not be blank...");
                return;
            }
            if (txtbooktitle.Text == "" )
            {
                MsgBox.ShowBox("Book Title Should not be blank...");
                return;
            }
            if (cmb_class.Text == "" || cmb_class.Text == "All")
            {
                MsgBox.ShowBox("Class Should not be blank...");
                return;
            }
            if (cmb_class.Text == "" || cmb_class.Text == "All")
            {
                MsgBox.ShowBox("Class Should not be blank...");
                return;
            }
            else
            {
                //SqlCommand com = new SqlCommand("insert_BMaster_T '" + txtBarcode.Text + "','" + cmb_subjectname.SelectedValue + "','" + textBox1.Text + "',''" + txtbooktitle.Text + "'','" + txt_volume.Text + "','" + cmb_publishname.SelectedValue + "','" + txtyear.Text + "','" + textBox2.Text + "','" + txt_nopages.Text + "','" + txtisbnno.Text + "','" + txt_bookno.Text + "','" + Convert.ToDecimal(txt_mrp.Text) + "','" + cmb_language.SelectedValue + "','" + cmb_category.SelectedValue + "','" + txt_source.Text + "','" + Convert.ToInt16(txt_section.Text) + "','" + txt_remark.Text + "','" + cmb_class.SelectedValue + "','" + sms.Form1.GlobalLoginUserName.ToString() + "'", sc);
                DATALAYER.dataclass dc = new sms.DATALAYER.dataclass();
                 SqlConnection sc = dc.con();

                    System.Collections.ArrayList  arr = new System.Collections.ArrayList();
                    arr.Add(txtBarcode.Text.Trim().Replace(" ", ""));

                    arr.Add(Convert.ToInt32(cmb_subjectname.SelectedValue));
                    arr.Add(textBox1.Text);
                    arr.Add(txtbooktitle.Text);
                    arr.Add(txt_volume.Text);
                    arr.Add(Convert.ToInt32(cmb_publishname.SelectedValue));
                    arr.Add(txtyear.Text);
                    arr.Add(textBox2.Text);
                    arr.Add(txt_nopages.Text);
                    arr.Add(txtisbnno.Text);
                    arr.Add(txt_bookno.Text);

                    arr.Add(Convert.ToDecimal(txt_mrp.Text));
                    arr.Add(Convert.ToDecimal(cmb_language.SelectedValue));
                    arr.Add(Convert.ToDecimal(cmb_category.SelectedValue));
                    arr.Add(txt_source.Text);
                    arr.Add(Convert.ToInt16(txt_section.Text));
                    arr.Add(txt_remark.Text);
                    arr.Add(Convert.ToInt32(cmb_class.SelectedValue));
                    arr.Add(sms.Form1.GlobalLoginUserName.ToString());
                    int n = dc.InsertRows("insert_BMaster_T", arr);
                    if (n > 0)
                    {
                         MsgBox.ShowBox("Record  Save  Saved.!!!");
                        
                    }
                    else
                    {
                        MsgBox.ShowBox("Record  Not Saved.!!!");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                MsgBox.ShowBox(ex.ToString());
            }

            }

        

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            dataclass dd = new dataclass();
            SqlConnection sc = dd.con();
            if (txtcityid.Text == "")
            {
                MsgBox.ShowBox("Please Click to Detail Row!!!");
                return;
            }
            if (txtBarcode.Text == "")
            {
                MsgBox.ShowBox("Barcode Should not be blank...");
            }
            else
            {
                SqlCommand com = new SqlCommand("update_BMaster_T '" + Convert.ToInt32(txtcityid.Text) + "','" + txtBarcode.Text + "','"+cmb_subjectname.SelectedValue+"','"+textBox1.Text+"','"+txtbooktitle.Text+"','"+txt_volume.Text+"','"+cmb_publishname.SelectedValue+"','"+txtyear.Text+"','"+textBox2.Text+"','"+txt_nopages.Text+"','"+txtisbnno.Text+"','"+txt_bookno .Text+"','"+Convert.ToDecimal(txt_mrp.Text)+"','"+cmb_language.SelectedValue+"','"+cmb_category.SelectedValue+"','"+txt_source.Text+"','"+Convert.ToInt16(txt_section.Text)+"','"+txt_remark.Text+"','"+cmb_class.SelectedValue+"','" + sms.Form1.GlobalLoginUserName.ToString() + "'",sc);
                SqlDataReader dr;
                sc.Open();
                try
                {
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Data Updated...");
                        Cancil_form();
                        LoadGrid();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Book Name already exist");
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

        private void dataGridView1_Click(object sender, EventArgs e)
        {

         
            btnCreate.Enabled = false;
            if (dataGridView1.Rows.Count > 0)
            {
                txtcityid.Text = dataGridView1.CurrentRow.Cells["B_ID"].Value.ToString();
                txtBarcode.Text = dataGridView1.CurrentRow.Cells["Barcode_No"].Value.ToString();
                cmb_subjectname.SelectedValue = Convert.ToInt16(dataGridView1.CurrentRow.Cells["subject_name"].Value.ToString());

                textBox1.Text = dataGridView1.CurrentRow.Cells["Author_Name"].Value.ToString();
                txtbooktitle.Text = dataGridView1.CurrentRow.Cells["Book_title"].Value.ToString();
                txt_volume.Text = dataGridView1.CurrentRow.Cells["Volume"].Value.ToString();
                txtBarcode.Text = dataGridView1.CurrentRow.Cells["Barcode_No"].Value.ToString();
                cmb_publishname.SelectedValue = Convert.ToInt16(dataGridView1.CurrentRow.Cells["P_Name"].Value.ToString());
                txtyear.Text = dataGridView1.CurrentRow.Cells["Year"].Value.ToString();
                textBox2.Text = dataGridView1.CurrentRow.Cells["Place"].Value.ToString();
                txt_nopages.Text = dataGridView1.CurrentRow.Cells["pages"].Value.ToString();
                txtisbnno.Text = dataGridView1.CurrentRow.Cells["Isbn_No"].Value.ToString();
                txt_bookno.Text = dataGridView1.CurrentRow.Cells["Book_No"].Value.ToString();
                txt_mrp.Text = dataGridView1.CurrentRow.Cells["MRP"].Value.ToString();
                cmb_language.SelectedValue = Convert.ToInt16(dataGridView1.CurrentRow.Cells["Language"].Value.ToString());
                cmb_category.SelectedValue = Convert.ToInt16(dataGridView1.CurrentRow.Cells["category"].Value.ToString());
                txt_source.Text = dataGridView1.CurrentRow.Cells["source"].Value.ToString();
                txt_section.Text = dataGridView1.CurrentRow.Cells["quantity"].Value.ToString();
                txt_remark.Text = dataGridView1.CurrentRow.Cells["Remarks"].Value.ToString();
                cmb_class.SelectedValue = Convert.ToInt16(dataGridView1.CurrentRow.Cells["cls_id"].Value.ToString());
                btnUpdate.Enabled = true;
                btnDelete.Enabled = true;



            }
        }
        private void frmCityMaster_Load(object sender, EventArgs e)
        {
            LoadGrid(); bindclass(); Publisher(); bindsubject(); bindlanguage(); bindcategory();
        }

        private void cmbState_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void txtcity_KeyUp(object sender, KeyEventArgs e)
        {
            LoadGrid();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void cmb_subjectname_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void txtBarcode_TextChanged(object sender, EventArgs e)
        {
            LoadGrid();
        }



        private void btncancel_Click(object sender, EventArgs e)
        {
            Cancil_form();
        }

        private void cmb_publishname_SelectedIndexChanged(object sender, EventArgs e)
        {
        }

        private void cmb_publishname_Leave(object sender, EventArgs e)
        {
            place();
            place();
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from public_t where publication_name='" + cmb_publishname.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmb_publishname.Text != "Select")
                {
                    cmb_publishname.Text = "";
                    MsgBox.ShowBox("Publisher Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Publisher ??"))
                    {
                        publication = 0;
                        frmPublicationMaster obj = new frmPublicationMaster(); 
                        obj.ShowDialog();
                         Publisher();
                        cmb_publishname.Focus();
                    }
                }
            }


        }

        private void cmb_subjectname_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            LoadGrid();
        }

        private void cmb_subjectname_Leave(object sender, EventArgs e)
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();

            SqlDataAdapter da = new SqlDataAdapter("select * from subject_t where sub_desc='" + cmb_subjectname.Text + "' ", sc);
            DataTable dt = new DataTable();

            da.Fill(dt);
            if (dt.Rows.Count > 0)
            {

            }

            else
            {
                if (cmb_subjectname.Text != "Select")
                {
                    cmb_subjectname.Text = "";
                    MsgBox.ShowBox("Subject Not Found !!!!");
                    if (DialogResult.Yes == MsgBoxRetType.ShowBox("Do you want to create New Subject ??"))
                    {
                        subject = 0;
                        frmSubjectMaster obj = new frmSubjectMaster();
                        obj.ShowDialog();
                        bindsubject();
                        cmb_subjectname.Focus();
                    }
                }
            }

        }

        private void txtbooktitle_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtBarcode_Leave(object sender, EventArgs e)
        {

        }
      
    }
}
