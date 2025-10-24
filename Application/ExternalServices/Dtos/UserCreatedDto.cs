namespace AS.UserManagement.Application.ExternalServices.Dtos
{ 
    public class UserCreatedDto : IEventDto
    {
        public string EventId { get; set; } = Guid.NewGuid().ToString();
        public DateTime EventTimestamp { get; set; } = DateTime.UtcNow;
        public string EventType => "UserCreatedEvent";

        // Datos específicos del evento
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string FullName { get; set; } = null!;
        public string RoleName { get; set; } = null!; // 'Patient', 'Professional', 'ScheduleManager'
    }
}