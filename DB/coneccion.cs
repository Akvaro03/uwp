using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwpIntentoNuevo.DB
{
    class coneccion
    {
        MySqlConnection connection = new MySqlConnection();

        string stringConeccion = "server=localhost;port=3306;database=isometer;uid=root;password=";

        public string getData()
        {
            try
            {
                connection.ConnectionString = stringConeccion;
                connection.Open();


                var command = new MySqlCommand("SELECT * FROM Ensayos;", connection);
                var reader = command.ExecuteReader();
                reader.Read();
                var datos = reader.GetString("EstadoEnsayo");

                return datos;
            }
            catch (MySqlException)
            {
                //MessageBox.Show(e.Message);
                return "no";
            }

        }
    }
}

