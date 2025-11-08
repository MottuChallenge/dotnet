using System.Threading.Tasks;

namespace MottuChallenge.Application.Interfaces
{
    public interface IDbProceduresService
    {
        Task<string?> GetSpotJsonAsync(string spotId);
        Task<string?> ValidatePlateAsync(string plate);
        // Executes a procedure that prints JSON to DBMS_OUTPUT; output may not be captured.
        Task ExecuteSpotsWithSectorJsonAsync();
        Task ExecuteRelatorioMotosPorVagaAsync();
    }
}
