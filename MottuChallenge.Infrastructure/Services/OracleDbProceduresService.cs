using System;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using MottuChallenge.Application.Interfaces;

namespace MottuChallenge.Infrastructure.Services
{
    public class OracleDbProceduresService : IDbProceduresService
    {
        private readonly string _connectionString;

        public OracleDbProceduresService(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public async Task<string?> GetSpotJsonAsync(string spotId)
        {
            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            // packaged function
            cmd.CommandText = "SELECT pkg_spots.fn_spot_para_json(:p_spot_id) FROM dual";
            var p = new OracleParameter("p_spot_id", OracleDbType.Varchar2, spotId, System.Data.ParameterDirection.Input);
            cmd.Parameters.Add(p);
            var result = await cmd.ExecuteScalarAsync();
            return result?.ToString();
        }

        public async Task<string?> ValidatePlateAsync(string plate)
        {
            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            // packaged function
            cmd.CommandText = "SELECT pkg_utils.fn_validate_plate(:p_plate) FROM dual";
            var p = new OracleParameter("p_plate", OracleDbType.Varchar2, plate, System.Data.ParameterDirection.Input);
            cmd.Parameters.Add(p);
            var result = await cmd.ExecuteScalarAsync();
            return result?.ToString();
        }

        public async Task ExecuteSpotsWithSectorJsonAsync()
        {
            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            // Note: original procedure writes to DBMS_OUTPUT. That output isn't captured via standard ExecuteNonQuery.
            // We simply invoke the procedure; if you need the JSON returned, change the PL/SQL to return via OUT parameter or function.
            // packaged procedure
            cmd.CommandText = "BEGIN pkg_spots.prc_spots_com_sector_json; END;";
            await cmd.ExecuteNonQueryAsync();
        }

        public async Task ExecuteRelatorioMotosPorVagaAsync()
        {
            await using var conn = new OracleConnection(_connectionString);
            await conn.OpenAsync();
            await using var cmd = conn.CreateCommand();
            // packaged procedure
            cmd.CommandText = "BEGIN pkg_spots.relatorio_motos_por_vaga; END;";
            await cmd.ExecuteNonQueryAsync();
        }
    }
}
