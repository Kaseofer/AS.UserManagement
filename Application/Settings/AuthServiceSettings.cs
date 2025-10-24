// AS.UserManagement.Application/Settings/AuthServiceSettings.cs
namespace AS.UserManagement.Application.Settings
{
    public class AuthServiceSettings
    {
        public const string SectionName = "AuthService";

        public string BaseUrl { get; set; } = string.Empty;
        public int TimeoutSeconds { get; set; } = 30;
        public string ApiKey { get; set; } = string.Empty;
        public string PacienteCreatedEndpoint { get; set; } = "/api/auth/paciente-created";
        public RetrySettings Retry { get; set; } = new();
        public bool EnableLogging { get; set; } = true;
    }

    public class RetrySettings
    {
        public int MaxAttempts { get; set; } = 3;
        public int DelayMilliseconds { get; set; } = 1000;
        public bool UseExponentialBackoff { get; set; } = true;
    }
}
