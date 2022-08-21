using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//using sms.DAL;
using System.Data.SqlClient;
using MsgBoxRet;
using sms.BLClass;
using System.Collections;
using sms.DATALAYER;
using System.Reflection;
using System.IO;
using System.Data.OleDb;

namespace sms
{
    public partial class SUBJECTExcelsheet : Form
    {
        BLclass bl = new BLclass();
        validation vl = new validation();
        public SUBJECTExcelsheet()
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
            bindstate();
            InitGrid();
        }
      
        private void bindstate()
        {
            dataclass dc = new dataclass();
            SqlConnection sc = dc.con();
            SqlDataAdapter da = new SqlDataAdapter(" select cls_ID,cls_desc  from CLASS_T where CLS_ID  in (11,13) ", sc);
            DataSet ds = new DataSet();
            da.Fill(ds);
            DataRow row = ds.Tables[0].NewRow();
            row["cls_desc"] = "-Select-";
            row["cls_ID"] = 0;
            ds.Tables[0].Rows.InsertAt(row, 0);

            cmbState.DataSource=ds.Tables[0];
            cmbState.DisplayMember = "cls_desc";
            cmbState.ValueMember = "cls_ID";
        }
        
        public void InitGrid()
        {
         
            
        }
       
        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            if (cmbState.Text == "Select" || cmbState.Text == "")
            {
                MessageBox.Show("Pls Select Class ..");
                return;
            }
            else
            {

                try
                {
                  List<string > obj=new List<string >() ;
                    string fileName = "";
                    string outPutDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
                    string logoimage = Path.Combine(outPutDirectory, "upload");
                    Uri uri = new Uri(logoimage);
                    OpenFileDialog OpenFD = new OpenFileDialog();
                    OpenFD.Title = "Specify Backup Location...";
                   // OpenFD.Filter = ".xls|*.xlsx";
                    OpenFD.FileName = null;
                    if (OpenFD.ShowDialog() != DialogResult.Cancel)
                    {
                        StringBuilder qu = new StringBuilder();
                        fileName = OpenFD.FileName;
                        System.IO.File.Copy(fileName, uri.LocalPath+"//"+Path.GetFileName(fileName), true);
                        logoimage = Path.Combine(outPutDirectory, "upload\\" + Path.GetFileName(fileName));
                        string imagepath = new Uri(logoimage).LocalPath;
                        string ext = Path.GetExtension(fileName);

                        if (File.Exists(imagepath))
                        {
                            OleDbConnection oledbConn;
                            if (ext == ".xls")
                                oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + imagepath + ";Extended Properties=Excel 8.0");
                            else
                                oledbConn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.12.0;Data Source=" + imagepath + ";Extended Properties=Excel 12.0 Xml;HDR=YES");
                            oledbConn.Open();
                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [Sheet1$]", oledbConn);
                            OleDbDataAdapter oleda = new OleDbDataAdapter();
                            oleda.SelectCommand = cmd;
                            DataSet ds = new DataSet();
                            oleda.Fill(ds);
                            DataTable dtdata = ds.Tables[0];
                            DateTime newdate; dataclass dc = new dataclass();
                            SqlConnection sc = dc.con();
                            if (dtdata.Rows.Count > 0)
                            {
                                int count = 0;
                                for (int i = 0; i < dtdata.Rows.Count; i++)
                                {
                                    if (dtdata.Rows[i]["SERIAL"].ToString() != "")
                                    {
                                        
                                        try
                                        {
                                            SqlDataAdapter da;
                                            da = new SqlDataAdapter("INSERT_REGNO", sc);
                                            da.SelectCommand.CommandTimeout = 300;
                                            da.SelectCommand.CommandType = CommandType.StoredProcedure;
                                            da.SelectCommand.Parameters.AddWithValue("@REG_ID", dtdata.Rows[i]["SERIAL"].ToString().Trim());
                                            da.SelectCommand.Parameters.AddWithValue("@REG_NO", dtdata.Rows[i]["IREGDNO"].ToString().Trim());
                                            DataTable dt = new DataTable();
                                            da.Fill(dt);
                                            obj.Add(dtdata.Rows[i]["SERIAL"].ToString());


                                        }
                                        catch (Exception ex)
                                        {
                                           
                                        }
                                    }
                                }
                                SqlDataAdapter daa = new SqlDataAdapter("SET_REGNO", sc);
                                daa.SelectCommand.CommandTimeout = 300;
                                daa.SelectCommand.CommandType = CommandType.StoredProcedure;
                                daa.SelectCommand.Parameters.AddWithValue("@class", cmbState.SelectedValue);
                                DataTable dta = new DataTable();
                                daa.Fill(dta);

                            }

                        }
                    }
                    DataTable name = new DataTable();
                    name.Columns.Add("SERIAL");
                    for (int i = 0; i < obj.Count; i++)
                    {
                        name.Rows.Add();
                        name.Rows[i]["SERIAL"] = obj[i].ToString();
                    }
                    dataGridView1.DataSource = name;
                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmCityMaster_Load(object sender, EventArgs e)
        {
           
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_Click(object sender, EventArgs e)
        {

        }

       
    }
}
