namespace MottuGrid_Dotnet.Domain.DTO.Response
{
    public class BranchResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string CNPJ { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public AddressResponse Address { get; set; }
        public List<YardResponse> Yards { get; set; }

        public BranchResponse(Guid id, string name, string CNPJ, string phone, string email, AddressResponse address, List<YardResponse> yards)
        {
            this.Id = id;
            this.Name = name;
            this.CNPJ = CNPJ;
            this.Phone = phone;
            this.Email = email;
            this.Address = address;
            this.Yards = yards;
        }
    }
}
