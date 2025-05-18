namespace MottuGrid_Dotnet.Domain.DTO.Response
{
    public class AddressResponse
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }

        public AddressResponse(Guid id, string street, string neighborhood, string city, string state, string postalCode, string country)
        {
            this.Id = id;
            this.Street = street;
            this.Neighborhood = neighborhood;
            this.City = city;
            this.State = state;
            this.PostalCode = postalCode;
            this.Country = country;
        }
    }
}
