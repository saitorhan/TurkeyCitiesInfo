using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using ExcelDataReader;
using Ionic.Zip;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string tempPath = Environment.GetFolderPath(Environment.SpecialFolder.Templates);
            string path = Path.Combine(tempPath, "pk_list.zip");
            string extPath = Path.Combine(tempPath, "pk_list");
            DataSet dataSet;

            if (Directory.Exists(extPath))
            {
                Directory.Delete(extPath, true);
            }

            using (WebClient webClient = new WebClient())
            {
                webClient.DownloadFile(Properties.Settings.Default.Url, path);

                using (ZipFile zip = ZipFile.Read(path))
                {
                    zip.ExtractAll(extPath);
                }
            }

            string listExcel = Directory.GetFiles(extPath).FirstOrDefault();
            if (listExcel == null)
            {
                return;
            }

            using (var stream = File.Open(listExcel, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    dataSet = reader.AsDataSet();
                    dataSet.Tables[0].Rows[0].Delete();
                }
            }

            SqlConnection sqlConnection = new SqlConnection(Properties.Settings.Default.ConnectionString);
            SqlBulkCopy bulkCopy = new SqlBulkCopy(sqlConnection);
            bulkCopy.DestinationTableName = "pk_list";

           
           bulkCopy.ColumnMappings.Add("Column0","il");
           bulkCopy.ColumnMappings.Add("Column1", "ilce");
           bulkCopy.ColumnMappings.Add("Column2", "semt_bucak_belde");
           bulkCopy.ColumnMappings.Add("Column3", "Mahalle");
           bulkCopy.ColumnMappings.Add("Column4", "PK");

            sqlConnection.Open();
            bulkCopy.WriteToServer(dataSet.Tables[0]);
            sqlConnection.Close();
        }
    }
}
