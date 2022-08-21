using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using MsgBoxRet;

namespace sms
{
    class CreateBackup
    {
        public static void Backup(string BackupNameWithFullPath)
        {
            using (SqlConnection conn = new DAL.dataclass().con())
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "BACKUP DATABASE " + conn.Database + " TO DISK = '" + BackupNameWithFullPath + "'";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static void Restore(string BackupNameWithFullPath)
        {
            using (SqlConnection conn = new DAL.dataclass().con())
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.CommandText = "RESTORE DATABASE " + conn.Database + " TO DISK = '" + BackupNameWithFullPath + "'";
                conn.Open();
                cmd.ExecuteNonQuery();
                conn.Close();
            }
        }

        public static int AutoBackup()
        { try
                    {
            DAL.dataclass dc=new sms.DAL.dataclass ();
           SqlConnection sc=dc.con();
                SqlDataAdapter da = new SqlDataAdapter("select lastbackuppath,lastScheduledBackupTime,scheduledBackupPath from tblDataBackupPath", sc);
                 DataTable  dt =new DataTable ();
                 da.Fill(dt);
                 if (dt.Rows.Count > 0)
                 {
                     string lastBackup = dt.Rows[0]["lastbackuppath"].ToString();
                     if (Directory.Exists(lastBackup))
                     {
                         SqlCommand cmd = new SqlCommand();
                         sc.Open();
                         cmd = new SqlCommand("BACKUP DATABASE " + sc.Database + " TO DISK = '" + lastBackup + "\\" + "Auto" + DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + "'",sc);
                         cmd.ExecuteNonQuery();
                         sc.Close();
                         sc.Open();
                         cmd = new SqlCommand("update tblDataBackupPath set lastScheduledBackupTime='" + DateTime.Now.ToString("MM/dd/yyyy") + "'", sc);
                         cmd.ExecuteNonQuery();
                         sc.Close();
                         return 0;
                     }
                     else
                     {
                         return 1;
                     }

                 }
                 else
                 {
                     return 1;
                 }
                 
                        
                    }
            catch(Exception ex)
          {
              return 1;
            
         }
         }
        }
        }
 