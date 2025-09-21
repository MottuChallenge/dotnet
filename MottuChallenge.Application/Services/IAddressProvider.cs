using MottuChallenge.Domain.Entities;

namespace MottuChallenge.Application.Services
{
    public interface IAddressProvider
    {
        Task<Address> GetAddressByCepAsync(string cep, string number);
    }
}
