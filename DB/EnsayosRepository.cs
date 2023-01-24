using uwpIntentoNuevo.DB;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace IsometerMobile.DB
{
    public class EnsayosRepository
    {
        private string _connectionString = "server=localhost;port=3306;database=isometer;uid=root;password=";
        public MySqlConnection dbConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public async Task<bool> InsertNewEnsayo(EnsayosDBModel ensayo)
        {
            var db = dbConnection();
            var querySQL = @"
                          INSERT INTO Ensayos (NombreEnsayo, FechaEnsayo, ValorEnsayo, EstadoEnsayo, VerificacionKey)
                          VALUES (@nombreEnsayo, @fechaEnsayo, @valorEnsayo, @estadoEnsayo, @VerificacionKey)";

            var result = await db.ExecuteAsync(querySQL, new { ensayo.NombreEnsayo, ensayo.FechaEnsayo, ensayo.ValorEnsayo, ensayo.EstadoEnsayo, ensayo.VerificacionKey });
            return result > 0;
        }


        public async Task<IEnumerable<EnsayosDBModel>> GetAllEnsayos(string dateOrder)
        {
            var db = dbConnection();

            var querySQL =
                @"SELECT * FROM Ensayos";
            return await db.QueryAsync<EnsayosDBModel>(OrderByDate(dateOrder, querySQL));
        }

        private string OrderByDate(string dateOrder, string query)
        {
            if (dateOrder == "Ascendente")
            {
                return $@"{query} ORDER BY FechaEnsayo ASC";
            }
            else
            {
                return $@"{query} ORDER BY FechaEnsayo DESC";
            }

        }

    }
}
