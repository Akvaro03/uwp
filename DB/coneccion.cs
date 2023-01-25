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

        public MySqlConnection establecerConeccion()
        {
            try
            {
                connection.ConnectionString = stringConeccion;
                connection.Open();
            }
            catch (MySqlException)
            {
                //MessageBox.Show(e.Message);
            }
            return connection;
        }

    }
}
