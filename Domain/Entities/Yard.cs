namespace MottuGrid_Dotnet.Domain.Entities
{
    public class Yard
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Double Area { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }
        public Guid BranchId { get; private set; }
        public Branch Branch { get; private set; }

        public Yard(string name, double area, Guid addressId, Guid branchId)
        {
            ValidateName(name);
            ValidateArea(area);
            this.Id = Guid.NewGuid();
            this.Name = name;
            this.Area = area;
            this.AddressId = addressId;
            this.BranchId = branchId;
        }

        private void ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ArgumentException("Name must not be null");
            }
        }

        private void ValidateArea(double area)
        {
            if (area <= 0)
            {
                throw new ArgumentException("Area must be greater than 0");
            }
        }
    }
}
