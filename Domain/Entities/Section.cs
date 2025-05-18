using MottuGrid_Dotnet.Domain.DTO.Request;

namespace MottuGrid_Dotnet.Domain.Entities
{
    public class Section
    {
        public Guid Id { get; private set; }
        public string Color { get; private set; }
        public Double Area { get; private set; }
        public Guid YardId { get; private set; }
        public Yard Yard { get; private set; }
        public ICollection<Motorcycle> Motorcycles { get; private set; } = new List<Motorcycle>();
        public Section(string color, double area, Guid yardId)
        {
            ValidateColor(color);
            ValidateArea(area);
            this.Id = Guid.NewGuid();
            this.Color = color;
            this.Area = area;
            this.YardId = yardId;
        }

        public Section() { }

        private void ValidateColor(string color)
        {
            if (string.IsNullOrEmpty(color))
            {
                throw new ArgumentException("Color must not be null");
            }
            if (color.Length < 3 || color.Length > 20)
            {
                throw new ArgumentException("Color must have between 3 and 20 characters");
            }
        }

        private void ValidateArea(double area)
        {
            if (area <= 0)
            {
                throw new ArgumentException("Area must be greater than 0");
            }
        }
        public void Update(SectionRequest request)
        {
            Color = request.Color;
            Area = request.Area;
            YardId = request.YardId;
        }
    }
}
