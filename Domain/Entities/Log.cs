namespace MottuGrid_Dotnet.Domain.Entities
{
    public class Log
    {
        public Guid Id { get; private set; }
        public string Message { get; private set; }
        public DateTime CreatedAt { get; private set; }
        public Guid MotorcycleId { get; private set; }
        public Motorcycle Motorcycle { get; private set; }

        public Log(string message, Guid motorcycleId)
        {
            ValidateMessage(message);
            this.Id = Guid.NewGuid();
            this.Message = message;
            this.CreatedAt = DateTime.UtcNow;
            this.MotorcycleId = motorcycleId;
        }

        private void ValidateMessage(string message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentException("Message must not be null");
            }
            if (message.Length > 150)
            {
                throw new ArgumentException("Message must have less than 150 characters");
            }
        }

    }
}
