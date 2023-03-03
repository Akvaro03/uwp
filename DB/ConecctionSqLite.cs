using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using MySqlConnector;
using uwpIntentoNuevo.Ensayar;
using Windows.Storage;
using Xamarin.Forms;

namespace uwpIntentoNuevo.DB
{
    public class ConecctionSqLite
    {
        public ConecctionSqLite()
        {
            CreateFile();
        }   

        public async void  CreateFile()
        {
            //var file = await ApplicationData.Current.LocalFolder.GetFileAsync("myDbSQLite.db");
            string pathFile = ApplicationData.Current.LocalFolder.Path + "\\myDbSQLite.db";
            if (!File.Exists(pathFile))
            {
                await ApplicationData.Current.LocalFolder.CreateFileAsync("myDbSQLite.db", Windows.Storage.CreationCollisionOption.ReplaceExisting);
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "myDbSQLite.db");

                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                {
                    con.Open();
                    string initCMD =
                        "CREATE TABLE IF NOT EXISTS " +
                        "ensayos (NombreEnsayo VARCHAR(255)," +
                        "EstadoEnsayo VARCHAR(5)," +
                        "FechaEnsayo datetime," +
                        "ValorEnsayo double," +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT," +
                        "VerificationKey VARCHAR(255))";


                    SqliteCommand CMDcreateTable = new SqliteCommand(initCMD, con);

                    CMDcreateTable.ExecuteReader();
                    con.Close();
                }

            }

        }

        public static void addRecord(EnsayoDbModel dataToSend)
        {
            if(!dataToSend.Equals(""))
            {
                string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "myDbSQLite.db");

                try
                {
                using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
                    {
                        con.Open();
                        SqliteCommand CMD_Insert = new SqliteCommand();
                        CMD_Insert.Connection = con;
                        CMD_Insert.CommandText = "INSERT INTO ensayos (NombreEnsayo, EstadoEnsayo, FechaEnsayo, ValorEnsayo, VerificationKey) VALUES (@valor1, @valor2, @valor3, @valor4, @valor5)";
                        CMD_Insert.Parameters.AddWithValue("@valor1", dataToSend.NombreEnnsayo);
                        CMD_Insert.Parameters.AddWithValue("@valor2", dataToSend.EstadoEnsayo);
                        CMD_Insert.Parameters.AddWithValue("@valor3", dataToSend.FechaEnsayo);
                        CMD_Insert.Parameters.AddWithValue("@valor4", dataToSend.ValorEnsayo);
                        CMD_Insert.Parameters.AddWithValue("@valor5", dataToSend.VerificacionKey);

                        CMD_Insert.ExecuteReader();
                        con.Close();
                    }
                }
                catch(MySqlException)
                {

                }


            }
        }


        public static Collection<EnsayosDBModel> GetRecords()
        {
            Collection<EnsayosDBModel> values = new Collection<EnsayosDBModel>();
            List<object[]> todo = new List<object[]>();

            string pathToDB = Path.Combine(ApplicationData.Current.LocalFolder.Path, "myDbSQLite.db");

            using (SqliteConnection con = new SqliteConnection($"Filename={pathToDB}"))
            {
                con.Open();

                String selectCmd = "SELECT * FROM ensayos";
                SqliteCommand cmd_getRec = new SqliteCommand(selectCmd, con);

                SqliteDataReader reader = cmd_getRec.ExecuteReader();

                object[] buffer = new object[6];
                while (reader.Read())
                {
                    buffer = new object[6];
                    //var dato = reader.GetValues(buffer);
                    reader.GetValues(buffer);
                    todo.Add(buffer);
                }

                foreach (object[] item in todo)
                {
                    values.Add(new EnsayosDBModel(item));
                }


                con.Close();
            }
            return values;
        }

    }
}
