using MottuGrid_Dotnet.Domain.DTO.Response;
using MottuGrid_Dotnet.Domain.Entities;
using System.Text.Json;

namespace MottuGrid_Dotnet.Services
{
    public class CepService
    {
        private readonly HttpClient _httpClient;

        public CepService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<Address> GetAddressByCep(string cep, string number)
        {
            var url = $"https://viacep.com.br/ws/{cep}/json/";
            var response = await _httpClient.GetStringAsync(url);

            var viacepResponse = JsonSerializer.Deserialize<ViaCepResponse>(response);

            return new Address
            (
                viacepResponse.Logradouro,
                number,
                viacepResponse.Bairro,
                viacepResponse.Localidade,
                viacepResponse.Uf,
                cep,
                "Brasil"
            );
        }
    }
}
