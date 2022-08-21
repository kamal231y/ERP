using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace sms.DATALAYER
{
    class dataclass
    {

        public string system_name = Login.system_name, passwd = Login.passwd, userid = Login.userid, databasename = Login.databasename;

        public static string year;
        public SqlConnection con()
        {
            SqlConnection _cn;

            String constr = "server=" + system_name + ";password=" + passwd + ";user id=" + userid + ";database=" + databasename + ";";

            _cn = new SqlConnection(constr);

            return _cn;
        }



        //Iss Add a new function named connweb for upload
        public SqlConnection connWeb()
        {
            SqlConnection _cn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionWeb"]);
            return _cn;
        }
        //
        public SqlConnection conWeb()
        {
            SqlConnection _cn = new SqlConnection("Data Source=103.133.215.8;Initial Catalog=isssms;uid=isssms;pwd=ISS@infotech#1");
            return _cn;
        }
        public SqlConnection connsms()
        {
            SqlConnection _cn = new SqlConnection("Data Source=103.133.215.8;Initial Catalog=isssms;uid=isssms;pwd=ISS@infotech#1");
            return _cn;
        }
        public SqlConnection connR()
        {
            SqlConnection _cn = new SqlConnection(ConfigurationSettings.AppSettings["ConnectionR"]);
            return _cn;
        }
        public SqlConnection conTmp()
        {
            SqlConnection _cn = new SqlConnection("server=(local);integrated security=sspi;DATABASE=GMFRANCH");
            return _cn;
        }
        public string ConnectionString()
        {
            return ConfigurationSettings.AppSettings["Connection"];
        }

        public int Insert123(string sqlSP, ArrayList arrFields)
        {
            SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            sc.Close();
            return j;
        }

        public int InsertWebUpload(string sqlsp, ArrayList arrFields)
        {
            SqlConnection sc = connWeb();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlsp;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            sc.Close();
            return j;
        }
        public int DeleteWebUpload(string query, string invoiceno)
        {
            SqlConnection sc = connWeb();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = query;
            sc.Open();
            int j;
            com.Connection = sc;
            com.Parameters.AddWithValue("@InvoiceNo", invoiceno);
            j = com.ExecuteNonQuery();
            sc.Close();
            return j;
        }
        public int forinsert(ArrayList temp)
        {
            //SqlConnection sc = new SqlConnection(conf)
            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_AddCountry";
            com.Parameters.AddWithValue("@S_countryID", temp[0]);
            com.Parameters.AddWithValue("@S_countryName", temp[1]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            sc.Dispose();
            return i;

        }

        //public int forDelete(ArrayList temp)
        //{
        //    SqlConnection sc = new SqlConnection("server=infotech-41fb9d;uid=sa;pwd=sarikadam.1;database=bluebay");
        //    sc.Open();
        //    SqlCommand com = new SqlCommand();
        //    com.CommandType = CommandType.StoredProcedure;
        //    //com.CommandText = "Pro_DeleteCountry";
        //    com.CommandText = "ProDeleteCountry";             
        //    com.Parameters.Add("@sp_id", temp[0]);
        //    //com.Parameters.Add("@sp_Cotegoryname", temp[1]);
        //    com.Connection = sc;
        //    int i = com.ExecuteNonQuery();
        //    return i;
        //}

        // public int Up_countryMaster(ArrayList temp)
        //{

        //    //SqlConnection sc = con();
        //    //sc.Open();
        //    //SqlCommand com = new SqlCommand();
        //    //com.CommandType = CommandType.StoredProcedure;
        //    //com.CommandText = "Pro_updateCountryMaster";
        //    //com.Parameters.Add("@sp_id", temp[0].ToString());
        //    //com.Parameters.Add("@sp_Cotegoryname", temp[1]);
        //    //com.Connection = sc;
        //    //int i=com.ExecuteNonQuery();
        //    //return i;

        //}

        public int Is_statemaster(ArrayList temp)
        {
            //SqlConnection sc = new SqlConnection(conf)
            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_AddState";
            com.Parameters.AddWithValue("@S_stateID", temp[0]);
            com.Parameters.AddWithValue("@S_stateName", temp[1]);
            com.Parameters.AddWithValue("@alias", temp[2]);
            //com.Parameters.AddWithValue("@S_CountryName", temp[0]);

            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Up_statemaster(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Pro_stateUpdate";
            com.Parameters.AddWithValue("@sp_state_name", temp[0]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }

        public int Del_statemaster(ArrayList temp)
        {
            //SqlConnection sc = new SqlConnection(conf)
            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Pro_Del_state";
            com.Parameters.AddWithValue("@sp_state_name", temp[0]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }




        public int Is_District(ArrayList temp)
        {
            //SqlConnection sc = new SqlConnection(conf)
            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_AddDistrict";

            com.Parameters.AddWithValue("@S_districtID", temp[0]);
            com.Parameters.AddWithValue("@S_districtName", temp[1]);
            //com.Parameters.AddWithValue("@S_stateName", temp[0]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Up_Distmaster(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Up_District";
            com.Parameters.AddWithValue("@sp_district_name", temp[0]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Del_District(ArrayList temp)
        {
            //SqlConnection sc = new SqlConnection(conf)
            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "Pro_Del_District";
            com.Parameters.AddWithValue("@@sp_district_name", temp[0]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_City(ArrayList temp)
        {
            //SqlConnection sc = new SqlConnection(conf)
            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_AddCity";
            com.Parameters.AddWithValue("@S_cityID", temp[0]);
            com.Parameters.AddWithValue("@S_cityName", temp[1]);
            //com.Parameters.AddWithValue("@S_districtName", temp[0]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_Company(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_AddCompany";
            com.Parameters.AddWithValue("@S_companyID", temp[0]);
            com.Parameters.AddWithValue("@S_Name", temp[1]);
            com.Parameters.AddWithValue("@S_address", temp[2]);
            com.Parameters.AddWithValue("@S_city", temp[3]);
            com.Parameters.AddWithValue("@S_district", temp[4]);
            com.Parameters.AddWithValue("@S_state", temp[5]);
            com.Parameters.AddWithValue("@S_country", temp[6]);
            com.Parameters.AddWithValue("@S_pin", temp[7]);
            com.Parameters.AddWithValue("@S_phoneno", temp[8]);
            com.Parameters.AddWithValue("@S_mobileno", temp[9]);
            com.Parameters.AddWithValue("@S_email", temp[10]);
            com.Parameters.AddWithValue("@S_URL ", temp[11]);
            com.Parameters.AddWithValue("@S_tinno", temp[12]);
            com.Parameters.AddWithValue("@f_y", temp[13]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_vendor(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_AddVendor";
            com.Parameters.AddWithValue("@S_VendorID", temp[0]);
            com.Parameters.AddWithValue("@S_VendorName", temp[1]);
            com.Parameters.AddWithValue("@S_address", temp[2]);
            com.Parameters.AddWithValue("@S_city", temp[3]);
            com.Parameters.AddWithValue("@S_District", temp[4]);
            com.Parameters.AddWithValue("@S_state", temp[5]);
            com.Parameters.AddWithValue("@S_country", temp[6]);
            com.Parameters.AddWithValue("@S_pin", temp[7]);
            com.Parameters.AddWithValue("@S_phoneno", temp[8]);
            com.Parameters.AddWithValue("@S_mobileno", temp[9]);
            com.Parameters.AddWithValue("@S_email", temp[10]);
            com.Parameters.AddWithValue("@S_tinno", temp[11]);

            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_AddStore(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_AddStore";
            com.Parameters.AddWithValue("@S_storeName", temp[0]);
            com.Parameters.AddWithValue("@S_address", temp[1]);
            com.Parameters.AddWithValue("@S_city", temp[2]);
            com.Parameters.AddWithValue("@S_district", temp[3]);
            com.Parameters.AddWithValue("@S_state", temp[4]);
            com.Parameters.AddWithValue("@S_country", temp[5]);
            com.Parameters.AddWithValue("@S_pin", temp[6]);
            com.Parameters.AddWithValue("@S_phoneno", temp[7]);
            com.Parameters.AddWithValue("@S_mobileno", temp[8]);
            com.Parameters.AddWithValue("@S_email", temp[9]);
            com.Parameters.AddWithValue("@S_tinno", temp[10]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_Subcategory(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_insert_tblParam2";
            com.Parameters.AddWithValue("@param2_id_1", temp[0]);
            com.Parameters.AddWithValue("@param2_name_2", temp[1]);

            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }

        public int Is_Purchase(ArrayList temp)
        {
            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "insert_tblPurchaseMaster_1";
            com.Parameters.AddWithValue("@po_no_1", temp[0]);
            com.Parameters.AddWithValue("@po_date_2", temp[1]);
            com.Parameters.AddWithValue("@Vendor_code_3", temp[2]);
            com.Parameters.AddWithValue("@Remark_4", temp[3]);
            com.Parameters.AddWithValue("@Tqty_5", temp[4]);
            com.Parameters.AddWithValue("@Total_6", temp[5]);
            com.Parameters.AddWithValue("@Discp_7", temp[6]);
            com.Parameters.AddWithValue("@Discv_8", temp[7]);
            com.Parameters.AddWithValue("@NetAmt_9", temp[8]);
            com.Parameters.AddWithValue("@TaxVatP_10", temp[9]);
            com.Parameters.AddWithValue("@TaxVatV_11", temp[10]);
            com.Parameters.AddWithValue("@TaxCSTP_12", temp[11]);
            com.Parameters.AddWithValue("@TaxCSTV_13", temp[12]);
            com.Parameters.AddWithValue("@GTotal_14", temp[13]);
            com.Parameters.AddWithValue("@billNo_15", temp[14]);
            com.Parameters.AddWithValue("billDate_16", temp[15]);
            com.Parameters.AddWithValue("@sub1Taxfree_17", temp[16]);
            com.Parameters.AddWithValue("@sub1Slab1_18", temp[17]);
            com.Parameters.AddWithValue("@sub1Slab2_19", temp[18]);
            com.Parameters.AddWithValue("@sub1Slab3_20", temp[19]);
            com.Parameters.AddWithValue("@lessTaxfree_21", temp[20]);
            com.Parameters.AddWithValue("@lessSlab1_22", temp[21]);
            com.Parameters.AddWithValue("@lessSlab2_23", temp[22]);
            com.Parameters.AddWithValue("@lessSlab3_24", temp[23]);
            com.Parameters.AddWithValue("@sub2Taxfree_25", temp[24]);
            com.Parameters.AddWithValue("@sub2Slab1_26", temp[25]);
            com.Parameters.AddWithValue("@sub2Slab2_27", temp[26]);
            com.Parameters.AddWithValue("@sub2Slab3_28", temp[27]);
            com.Parameters.AddWithValue("@taxTaxfree_29", temp[28]);
            com.Parameters.AddWithValue("@taxSlab1_30", temp[29]);
            com.Parameters.AddWithValue("@taxSlab2_31", temp[30]);
            com.Parameters.AddWithValue("@taxSlab3_32", temp[31]);
            com.Parameters.AddWithValue("@tax1Taxfree_33", temp[32]);
            com.Parameters.AddWithValue("@tax1Slab1_34", temp[33]);
            com.Parameters.AddWithValue("@tax1Slab2_35", temp[34]);
            com.Parameters.AddWithValue("@tax1Slab3_36", temp[35]);
            com.Parameters.AddWithValue("@sub3Taxfree_37", temp[36]);
            com.Parameters.AddWithValue("@sub3Slab1_38", temp[37]);
            com.Parameters.AddWithValue("@sub3Slab2_39", temp[38]);
            com.Parameters.AddWithValue("@sub3Slab3_40", temp[39]);
            com.Parameters.AddWithValue("@freight_41", temp[40]);
            com.Parameters.AddWithValue("@cartridge_42", temp[41]);
            com.Parameters.AddWithValue("@misc_43", temp[42]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;
        }

        public int Is_Section(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_insert_tblParam1";
            com.Parameters.AddWithValue("@param1_id_1", temp[0]);
            com.Parameters.AddWithValue("@param1_name_2", temp[1]);
            com.Parameters.AddWithValue("@GroupTypeId", temp[2]);
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_Group(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_insert_tblParam3";
            com.Parameters.AddWithValue("@param3_id_1", temp[0]);
            com.Parameters.AddWithValue("@param3_name_2", temp[1]);

            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_Style(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_insert_tblParam4";
            com.Parameters.AddWithValue("@param4_id_1", temp[0]);
            com.Parameters.AddWithValue("@param4_name_2", temp[1]);

            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_Color(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = "sp_insert_tblParam5";
            com.Parameters.AddWithValue("@param5_id_1", temp[0]);
            com.Parameters.AddWithValue("@param5_name_2", temp[1]);

            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        //public int Is_Size(ArrayList temp)
        //{

        //    SqlConnection sc = con();
        //    sc.Open();
        //    SqlCommand com = new SqlCommand();
        //    com.CommandType = CommandType.StoredProcedure;
        //    com.CommandText = "sp_insert_tblSize";
        //    com.Parameters.AddWithValue("@size_range", temp[0]);
        //    com.Parameters.AddWithValue("@size_value", temp[1]);

        //    com.Connection = sc;
        //    int i = com.ExecuteNonQuery();
        //    return i;

        //}

        public int Is_PurchaseOrder(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;

            com.CommandText = "sp_insert_tblPOrder_Master";

            //com.CommandText = "sp_AddItemNewtemp";

            com.Parameters.AddWithValue("@po_no_1", temp[0]);

            com.Parameters.AddWithValue("@po_date_2", temp[1]);
            com.Parameters.AddWithValue("@vendor_code_3", temp[2]);
            com.Parameters.AddWithValue("@netQty_4", temp[3]);
            com.Parameters.AddWithValue("@Remark", temp[4]);
            com.Parameters.AddWithValue("@Total", temp[5]);

            //com.Parameters.AddWithValue("@Quality", Convert.ToString(temp[6]));
            //com.Parameters.AddWithValue("@Section", Convert.ToString(temp[7]));
            //com.Parameters.AddWithValue("@Group", Convert.ToString(temp[12]));
            //com.Parameters.AddWithValue("@SubSection", Convert.ToString(temp[9]));
            //com.Parameters.AddWithValue("@Colour", Convert.ToString(temp[10]));
            //com.Parameters.AddWithValue("@Size", Convert.ToString(temp[11]));
            //com.Parameters.AddWithValue("@Qty", Convert.ToDecimal(temp[8]));
            //com.Parameters.AddWithValue("@MRP", Convert.ToDecimal(temp[15]));
            //com.Parameters.AddWithValue("@PPrice", Convert.ToDecimal(temp[14]));
            com.Connection = sc;
            int i = com.ExecuteNonQuery();
            return i;

        }
        public int Is_ItemCreate(ArrayList temp)
        {

            SqlConnection sc = con();
            sc.Open();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            //com.CommandText = "sp_AddItemMaster";
            com.CommandText = "sp_AddItemMaster2";

            com.Parameters.AddWithValue("@ItemCode", Convert.ToString(temp[0]));
            com.Parameters.AddWithValue("@BarCode", Convert.ToString(temp[1]));
            com.Parameters.AddWithValue("@Brand", Convert.ToString(temp[2]));
            com.Parameters.AddWithValue("@Itemname", Convert.ToString(temp[3]));
            com.Parameters.AddWithValue("@type", Convert.ToString(temp[4]));
            com.Parameters.AddWithValue("@qtyinbox", Convert.ToString(temp[5]));
            com.Parameters.AddWithValue("@unit", Convert.ToString(temp[6]));
            com.Parameters.AddWithValue("@Colour", Convert.ToString(temp[7]));
            com.Parameters.AddWithValue("@Size", Convert.ToString(temp[8]));
            com.Parameters.AddWithValue("@Qty", Convert.ToDecimal(temp[9]));
            com.Parameters.AddWithValue("@MRP", Convert.ToDecimal(temp[10]));
            com.Parameters.AddWithValue("@PPrice", Convert.ToDecimal(temp[11]));




            com.Connection = sc;

            int i = com.ExecuteNonQuery();
            return i;

        }
        public int InsertRows(string sqlSP, ref ArrayList arrFields, bool returnValue)
        {
            SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            arrFields.Clear();
            arrFields.Add(com.Parameters[0].Value.ToString());
            sc.Dispose();
            return j;
        }

        public int InsertWithConn(string sqlSP, ref ArrayList arrFields, SqlConnection sc)
        {
            //SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            arrFields.Clear();
            arrFields.Add(com.Parameters[0].Value.ToString());
            sc.Dispose();
            return j;
        }

        public int InsertRows(string sqlSP, ArrayList arrFields)
        {
            SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Close();
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            sc.Dispose();
            return j;
        }
        public int InsertRowsweb(string sqlSP, ArrayList arrFields)
        {
            SqlConnection sc = connsms();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Close();
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            sc.Dispose();
            return j;
        }
        public string InsertRows1(string sqlSP, ArrayList arrFields)
        {
            string n = null;
            SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Close();
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            SqlDataReader dr = com.ExecuteReader();
            if (dr.Read())
                n = dr[0].ToString();

            sc.Dispose();
            return n;
        }

        public int OldPOrder(string sqlSP, string Fields)
        {
            SqlConnection sc = con();
            string str = sqlSP + "'" + Fields + "'";
            SqlCommand com = new SqlCommand(str);
            sc.Close();
            sc.Open();
            int j;
            com.Connection = sc;
            j = com.ExecuteNonQuery();
            sc.Dispose();
            return j;
        }
        public int InsertWithConn(string sqlSP, ArrayList arrFields, SqlConnection sc)
        {
            //SqlConnection sc = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlSP;
            sc.Open();
            int j;
            com.Connection = sc;
            SqlCommandBuilder.DeriveParameters(com);
            com.Parameters.RemoveAt(0);
            for (int i = 0; i < com.Parameters.Count; i++)
                com.Parameters[i].Value = arrFields[i];
            j = com.ExecuteNonQuery();
            sc.Close();
            return j;
        }
        public int forDelete(string sqlsp, string str, SqlConnection sc)
        {
            sc.Close();
            //SqlConnection ac = con();
            SqlCommand com = new SqlCommand();
            com.CommandType = CommandType.StoredProcedure;
            com.CommandText = sqlsp;
            com.Connection = sc;
            //sc.Open();
            //SqlCommandBuilder.DeriveParameters(com);
            //com.Parameters.RemoveAt(0);
            //for (int i = 0; i < com.Parameters.Count; i++)
            //    com.Parameters[i].Value = str;
            //SqlParameter pp = new SqlParameter();
            //  com.Parameters.Add(pp);
            // com.Parameters[pp].Value = str;
            com.Parameters.Add(new SqlParameter("@param6_name", SqlDbType.VarChar, 50));
            com.Parameters["@param6_name"].Value = str;
            sc.Open();
            int j = 0;
            // com.Parameters[0].Value=str;
            j = com.ExecuteNonQuery();
            sc.Close();
            return j;

        }



        internal DataRow NewRow()
        {
            throw new NotImplementedException();
        }
    }
}
