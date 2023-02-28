using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uwpIntentoNuevo.Ensayar;

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

                object[] buffer = new object[6];

                List<object[]> todo = new List<object[]>();

                while (reader.Read())
                {
                    reader.GetValues(buffer);
                    todo.Add(buffer);
                }


                foreach (object[] item in todo)
                {
                    values.Add(new EnsayosDBModel(item));
                }


                return values;
            }
            catch (MySqlException)
            {
                return null;
            }
            finally
            {
                connection.Close();
            }

        }
        public void sendData(EnsayoDbModel dataToSend)
        {
            connection.ConnectionString = stringConeccion;
            try
            {
                connection.Open();
                MySqlCommand command = connection.CreateCommand();
                command.CommandText = "INSERT INTO ensayos (NombreEnsayo, EstadoEnsayo, FechaEnsayo, ValorEnsayo, VerificationKey) VALUES (@valor1, @valor2, @valor3, @valor4, @valor5)";
                command.Parameters.AddWithValue("@Valor1", dataToSend.NombreEnnsayo);
                command.Parameters.AddWithValue("@Valor2", dataToSend.EstadoEnsayo);
                command.Parameters.AddWithValue("@Valor3", dataToSend.FechaEnsayo);
                command.Parameters.AddWithValue("@Valor4", dataToSend.ValorEnsayo);
                command.Parameters.AddWithValue("@Valor5", dataToSend.VerificacionKey);

                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                connection.Close();
            }

        }
    }
}

