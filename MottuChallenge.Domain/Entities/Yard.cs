using MottuChallenge.Domain.Exceptions;
using MottuChallenge.Domain.Validations;
using MottuChallenge.Domain.ValueObjects;

namespace MottuChallenge.Domain.Entities
{
    public class Yard
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public Guid AddressId { get; private set; }
        public Address Address { get; private set; }

        private readonly List<Sector> _sectors = new();
        public IReadOnlyCollection<Sector> Sectors => _sectors.AsReadOnly();

        private readonly List<PolygonPoint> _points = new();
        public IReadOnlyCollection<PolygonPoint> Points => _points.AsReadOnly();

        public Yard(string name)
        {
            Guard.AgainstNullOrWhitespace(name, nameof(name), nameof(Yard));
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public Yard() { }

        public void SetAddress(Address address)
        {
            if (address == null) 
                throw new DomainValidationException("Address cannot be null", nameof(address), nameof(Yard));
            this.Address = address;
            this.AddressId = address.Id;
        }

        public void AddSector(Sector sector)
        {
            _sectors.Add(sector);
        }

        public void AddPoint(PolygonPoint point)
        {
            _points.Add(point);
        }
    }
}
