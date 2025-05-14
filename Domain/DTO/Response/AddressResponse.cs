namespace MottuGrid_Dotnet.Domain.DTO.Response
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public AddressResponse(Guid id, string street)
        {
            this.Id = id;
            this.Street = street;
        }
    }
}
