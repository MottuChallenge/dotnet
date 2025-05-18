namespace MottuGrid_Dotnet.Domain.DTO.Response
{
    public class YardResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public double Area { get; set; }

        public AddressResponse Address { get; set; }

        public YardResponse(Guid id, string name, double area, AddressResponse address)
        {
            Id = id;
            Name = name;
            Area = area;
            Address = address;
        }
    }
}
