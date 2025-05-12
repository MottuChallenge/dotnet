using System.Text.RegularExpressions;
using System.Xml.Serialization;
using MottuGrid_Dotnet.Domain.Enums;
using MottuGrid_Dotnet.Domain.Exceptions;

namespace MottuGrid_Dotnet.Domain.Entities
{
    public class Motorcycle
    {
        public Guid Id { get; private set; }
        public string Model { get; private set; }
        public EngineType EngineType { get; private set; }
        public string Plate { get; private set; }
        public DateTime LastRevisionDate { get; private set; }
        public ICollection<Log> Logs { get; private set; } = new List<Log>();
        public Guid SectionId { get; private set; }
        public Section Section { get; private set; }

        public Motorcycle(string model, string engineType, string plate, DateTime lastRevisionDate, Guid sectionId)
        {
            ValidatePlate(plate);
            ValidateEngineType(engineType);
            ValidateLastRevisionDate(lastRevisionDate);
            this.Id = Guid.NewGuid();
            this.Model = model;
            this.EngineType = Enum.Parse<EngineType>(engineType.ToUpper());
            this.Plate = plate;
            this.LastRevisionDate = lastRevisionDate;
            this.SectionId = sectionId;
        }

        private void ValidatePlate(string plate)
        {
            if (string.IsNullOrEmpty(plate))
            {
                throw new PlateException("Plate must not be null");
            }
            if (plate.Length != 8)
            {
                throw new PlateException("License plate must have 8 characters");
            }

            if (char.IsLetter(plate, 4))
            {
                var padraoMercosul = new Regex("[a-zA-Z]{3}[0-9]{1}[a-zA-Z]{1}[0-9]{2}");
                if (!padraoMercosul.IsMatch(plate)) throw new PlateException("Placa inválida");
            }
            else
            {
                var padraoNormal = new Regex("[a-zA-Z]{3}[0-9]{4}");
                if (!padraoNormal.IsMatch(plate)) throw new PlateException("Placa inválida!");
            }
        }

        private void ValidateEngineType(string engineType)
        {
            if (string.IsNullOrEmpty(engineType))
            {
                throw new EngineTypeException("Engine type must not be null");
            }
            if (!Enum.TryParse<EngineType>(engineType.ToUpper(), out _))
            {
                throw new EngineTypeException("Invalid engine type");
            }
        }

        private void ValidateLastRevisionDate(DateTime lastRevisionDate)
        {
            if (lastRevisionDate > DateTime.Now.AddDays(1))
            {
                throw new ArgumentException("Last revision date must be in the past");
            }
        }
    }
}
