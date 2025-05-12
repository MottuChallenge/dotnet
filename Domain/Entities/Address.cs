namespace MottuGrid_Dotnet.Domain.Entities
{
    public class Address
    {
        public Guid Id { get; private set; }
        public string Street { get; private set; }
        public string Number { get; private set; }
        public string Neighborhood { get; private set; }
        public string City { get; private set; }
        public string State { get; private set; }
        public string ZipCode { get; private set; }
        public string Country { get; private set; }
        public Yard? Yard { get; private set; }
        public Branch? Branch { get; private set; }

        public Address(string street, string number, string neighborhood, string city, string state, string zipCode, string country)
        {
            ValidateNumber(number);
            ValidateZipCode(zipCode);
            this.Id = Guid.NewGuid();
            this.Street = street;
            this.Number = number;
            this.Neighborhood = neighborhood;
            this.City = city;
            this.State = state;
            this.ZipCode = zipCode;
            this.Country = country;
        }

        private void ValidateNumber(string number)
        {
            if (string.IsNullOrEmpty(number))
            {
                throw new ArgumentException("Number must not be null");
            }
        }

        private void ValidateZipCode(string zipCode)
        {
            if (string.IsNullOrEmpty(zipCode))
            {
                throw new ArgumentException("Zip code must not be null");
            }
            if (zipCode.Length != 8)
            {
                throw new ArgumentException("Zip code must have 8 characters");
            }
        }

    }
}
