using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uwpIntentoNuevo.DB
{
    class coneccion
    {
        MySqlConnection connection = new MySqlConnection();

        string stringConeccion = "server=localhost;port=3306;database=isometer;uid=root;password=";

        public Collection<EnsayosDBModel> getData()
        {
            try
            {
                Collection<EnsayosDBModel> values = new Collection<EnsayosDBModel>();
                connection.ConnectionString = stringConeccion;
                connection.Open();


                var command = new MySqlCommand("SELECT * FROM Ensayos;", connection);
                var reader = command.ExecuteReader();
                //reader.Read();
                ////var datos = reader.GetValues();

                    object[] buffer = new object[6];

                List<object[]> todo = new List<object[]>();

                while (reader.Read()) {
                    reader.GetValues(buffer);
                    todo.Add(buffer); 
                }


                foreach(object[] item in todo )
                {
                    values.Add(new EnsayosDBModel(item));
                }


                return values;
            }
            catch (MySqlException)
            {
                return null ;
            }

        }
    }
}

