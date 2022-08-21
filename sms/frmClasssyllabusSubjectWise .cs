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
using System.IO;
using System.Reflection;
using System.Net;
        

namespace sms
{
    public partial class frmClasssyllabusSubjectWise  : Form
    {   
        string dept;
        SqlDataReader dr;

        public frmClasssyllabusSubjectWise()
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
            Cancil_Enclosure();
            
        }       
        
        

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmEmpDetailReport_Load(object sender, EventArgs e)
        {

            bindclass();
            bindSub();
            LoadGridEnclosure();
        }

        private void btnsaveenclosure_Click(object sender, EventArgs e)
        {
            try
            {
                if (cmbclass.Text == "" || cmbclass.Text == "Select")
                {
                    MessageBox.Show("Please Select class ");
                    return;
                }
                if (cmbSub.Text == "" || cmbSub.Text == "Select")
                {
                    MessageBox.Show("Please Select Subject ");
                    return;
                }
                if (textBox2.Text == "" || textBox2.Text == "Select")
                {
                    MessageBox.Show("Please Select Topic");
                    return;
                }

                else
                {
                    byte[] byteImg1 = ImageToByteArray(pictureenclosure.Image, pictureenclosure);

                    dataclass dc = new dataclass();
                    SqlConnection sc = dc.con();
                    SqlCommand com = new SqlCommand("inser_class_syllabus", sc);
                    com.CommandType = CommandType.StoredProcedure;
                    com.Parameters.AddWithValue("@CLS_ID",Convert.ToInt32(cmbclass.SelectedValue));
                    com.Parameters.AddWithValue("@SUB_ID",Convert.ToInt32(cmbSub.SelectedValue));
                    com.Parameters.AddWithValue("@Topic",textBox2.Text);
                    com.Parameters.AddWithValue("@ImageUrl",byteImg1);
                    SqlDataReader dr;
                    sc.Open();
                    dr = com.ExecuteReader();
                    if (dr.RecordsAffected > 0)
                    {
                        MsgBox.ShowBox("Class syllabus Created...");
                        Cancil_Enclosure();
                        LoadGridEnclosure();
                    }
                    else
                    {
                        MsgBox.ShowBox("This Class syllabus  Name already exist");
                    }
                }

            }
            catch (Exception ex)

            { }
        }

        private byte[] ImageToByteArray(Image image, PictureBox pictureenclosure)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            if (pictureenclosure.Image != null)
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            return ms.ToArray();
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

            cmbclass.DataSource = ds.Tables[0];
            cmbclass.DisplayMember = "Class";
            cmbclass.ValueMember = "SEC_ID";
        }
        private void bindSub()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select * from SUBJECT_T order by SUB_ID", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["SUB_DESC"] = "Select";
            row["SUB_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);
            cmbSub.DataSource = ds.Tables[0];
            cmbSub.DisplayMember = "SUB_DESC";
            cmbSub.ValueMember = "SUB_ID";
        }    
         private void Cancil_Enclosure()
            {
             pictureenclosure.Image = GetDataToImage(Form1.cancel);
            btnsaveenclosure.Enabled = true;
            Updateenclosure.Enabled = false;
            Deleteenclosure.Enabled = false;
            cmbSub.Text = " ";
            textBox2.Text = " ";
            cmbclass.Text = " ";
           

            }

         private Image GetDataToImage(byte[] p)
         {
             try
             {
                 ImageConverter imgConverter = new ImageConverter();
                 return imgConverter.ConvertFrom(p) as Image;
             }
             catch (Exception ex)
             {
                 //MsgBox.Show(ex.Message, "Error", MsgBox.Buttons.OK, MsgBox.Icon.Error);
                 return null;
             }
         }
          private void LoadGridEnclosure()
          {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter("SELECT id,class,SUB_DESC,Topic,ImageUrl FROM View_class_syllabus ", sc);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gridenclosure.DataSource = dt;
            gridenclosure.Columns["id"].Visible = false;
            gridenclosure.Columns["class"].Width = 125;
            gridenclosure.Columns["SUB_DESC"].Width = 150;
            gridenclosure.Columns["Topic"].Width = 320;
            gridenclosure.Columns["ImageUrl"].Visible = false;
            gridenclosure.Columns["class"].HeaderText = "Class";
            gridenclosure.Columns["SUB_DESC"].HeaderText = "Subject";
            gridenclosure.Columns["Topic"].HeaderText = "Topic";
       
    }

          private void btnuploadEnclosure_Click(object sender, EventArgs e)
          {
              try
              {
                  OpenFileDialog openFileDialog1 = new OpenFileDialog();
                  openFileDialog1.Title = "Choose Image";
                  //openFileDialog1.Filter = "Images (*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;*.)|*.JPEG;*.BMP;*.JPG;*.GIF;*.PNG;.pdf";
                  if (openFileDialog1.ShowDialog() == DialogResult.OK)
                  {
                      Image img = new Bitmap(openFileDialog1.FileName);
                      pictureenclosure.Image = img;// resizeImage(img);
                  }
              }


              catch (Exception ex)
              {
                  MessageBox.Show("Error" + ex.Message.ToString());
              }
          }

          private void Updateenclosure_Click(object sender, EventArgs e)
          {
              try
              {
                  if (cmbclass.Text == "" || cmbclass.Text == "Select")
                  {
                      MessageBox.Show("Please Select Class ");
                      return;
                  }
                  if (txtenclosureid.Text == "")
                  {
                      MessageBox.Show("Please Fill ID. ");
                      return;
                  }
                  if (cmbSub.Text == "" || cmbSub.Text == "Select")
                  {
                      MessageBox.Show("Please Select Subject ");
                      return;
                  }
                  if (textBox2.Text == "" || textBox2.Text == "Select")
                  {
                      MessageBox.Show("Please Select Topic");
                      return;
                  }

                  else
                  {
                      byte[] byteImg1 = ImageToByteArray(pictureenclosure.Image, pictureenclosure);
                      dataclass dc = new dataclass();
                      SqlConnection sc = dc.con();
                      SqlCommand com = new SqlCommand("update_class_syllabus", sc);
                      com.CommandType = CommandType.StoredProcedure;
                      com.Parameters.AddWithValue("@id", txtenclosureid.Text);
                      com.Parameters.AddWithValue("@CLS_ID", Convert.ToInt32(cmbclass.SelectedValue));
                      com.Parameters.AddWithValue("@SUB_ID", Convert.ToInt32(cmbSub.SelectedValue));
                      com.Parameters.AddWithValue("@Topic", textBox2.Text);
                      com.Parameters.AddWithValue("@ImageUrl", byteImg1);
                      SqlDataReader dr;
                      sc.Open();

                      dr = com.ExecuteReader();
                      if (dr.RecordsAffected > 0)
                      {
                          MsgBox.ShowBox("Class syllabus Updated...");
                          Cancil_Enclosure();
                          LoadGridEnclosure();
                      }
                      else
                      {
                          MsgBox.ShowBox("This Class syllabus Name Not exist");
                      }
                  }

              }
              catch (Exception ex)

              { }
          }

          private void Deleteenclosure_Click(object sender, EventArgs e)
          {
              DialogResult dialogResult = MessageBox.Show("Are You Sure Delete Document!!!", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
              if (dialogResult == DialogResult.Yes)
              {
                  try
                  {
                      dataclass dc = new dataclass();
                      SqlConnection conn = dc.con();
                      if (txtenclosureid.Text == "")
                      {
                          MsgBox.ShowBox("Please Click to Detail Row!!!");
                          return;
                      }
                      SqlCommand objCmd = new SqlCommand("DELETE FROM class_syllabus WHERE Id=" + txtenclosureid.Text + "", conn);
                      SqlDataReader dr;
                      objCmd.Connection.Open();
                      dr = objCmd.ExecuteReader(CommandBehavior.CloseConnection);
                      if (dr.RecordsAffected > 0)
                      {
                          MsgBox.ShowBox("Class syllabus Deleted Successfully!!!");
                          Cancil_Enclosure();
                          LoadGridEnclosure();
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

          private void Closeenclosure_Click(object sender, EventArgs e)
          {
              this.Close();
          }

          private void Cancelenclosure_Click(object sender, EventArgs e)
          {
              Cancil_Enclosure();
          }

          private void gridenclosure_CellClick(object sender, DataGridViewCellEventArgs e)
          {
              try
              {
                  btnsaveenclosure.Enabled = false;
                  if (gridenclosure.Rows.Count > 0)
                  {
                      txtenclosureid.Text = gridenclosure.CurrentRow.Cells["Id"].Value.ToString();
                      cmbSub.Text = gridenclosure.CurrentRow.Cells["SUB_DESC"].Value.ToString();
                      textBox2.Text = gridenclosure.CurrentRow.Cells["Topic"].Value.ToString();
                      cmbclass.Text = gridenclosure.CurrentRow.Cells["CLass"].Value.ToString();
                      Updateenclosure.Enabled = true;
                      Deleteenclosure.Enabled = true;
                      byte[] img_arr1 = new byte[0];
                      img_arr1 = (byte[])gridenclosure.CurrentRow.Cells["ImageUrl"].Value;
                      pictureenclosure.Image = GetDataToImage(img_arr1);

                  }
              }
              catch (Exception ex)
              { MessageBox.Show(ex.ToString()); }
          }
          public void ImportGISFiles(string sourcePath, string destinationPath, string destinationFolder)
          {

              if (Directory.Exists(destinationPath + destinationFolder))
                  Directory.Delete(destinationPath + destinationFolder, true);
              Directory.CreateDirectory(destinationPath + "\\" + destinationFolder);
              string[] fileArray = Directory.GetFiles(sourcePath);
              string fileName = null; string destFile = null;
              foreach (string s in fileArray)
              {
                  try
                  {
                      string destinationpath = destinationPath + "\\" + destinationFolder;
                      // fileName = Path.GetFileName(fileArray[i].ToString());
                      fileName = System.IO.Path.GetFileName(s);
                      destFile = System.IO.Path.Combine(destinationpath, fileName);
                      System.IO.File.Copy(s, destFile, true);
                  }
                  catch (Exception ex) { }
              }
          }

          private void button1_Click(object sender, EventArgs e)
          {
              try
              {
                  FolderBrowserDialog openFile = new FolderBrowserDialog();
                  openFile.ShowDialog();
                  string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                  string logoimage = Path.Combine(outPutDirectory, "DocumentImage//" + cmbclass.Text.Trim().Replace(" ", ""));
                  Uri uri = new Uri(logoimage);
                  if (Directory.Exists(uri.LocalPath))
                  {
                      ImportGISFiles(uri.LocalPath, openFile.SelectedPath, cmbclass.Text.Trim().Replace(" ", ""));
                      MessageBox.Show("Document Downloaded ....");

                  }

              }
              catch (Exception ex)
              {
                  MessageBox.Show(ex.ToString());
              }
          }

         

          private void groupBox14_Enter(object sender, EventArgs e)
          {

          }

          private void cmbclass_Leave(object sender, EventArgs e)
          {
              dataclass dc = new dataclass();
              SqlConnection sc = dc.con();
              SqlDataAdapter da = new SqlDataAdapter("SELECT id,class,SUB_DESC,Topic,ImageUrl FROM View_class_syllabus where clS_ID='" + cmbclass.SelectedValue+ "' ", sc);
              DataTable dt = new DataTable();
              da.Fill(dt);
              gridenclosure.DataSource = dt;
              gridenclosure.Columns["id"].Visible = false;
              gridenclosure.Columns["class"].Width = 125;
              gridenclosure.Columns["SUB_DESC"].Width = 150;
              gridenclosure.Columns["Topic"].Width = 320;
              gridenclosure.Columns["ImageUrl"].Visible = false;
              gridenclosure.Columns["class"].HeaderText = "Class";
              gridenclosure.Columns["SUB_DESC"].HeaderText = "Subject";
              gridenclosure.Columns["Topic"].HeaderText = "Topic";
       

          }
}
}