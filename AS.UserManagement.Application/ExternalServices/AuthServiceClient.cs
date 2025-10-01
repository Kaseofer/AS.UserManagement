// AS.UserManagement.Infrastructure/ExternalServices/AuthServiceClient.cs
using AS.UserManagement.Application.ExternalServices.Interfaces;
using AS.UserManagement.Application.Settings;
using AS.UserManagement.Application.ExternalServices.Dtos;
using AS.UserManagement.Infrastructure.Logger;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;

namespace AS.UserManagement.Infrastructure.ExternalServices
{
    public class AuthServiceClient : IAuthServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly IAppLogger<AuthServiceClient> _logger;
        private readonly AuthServiceSettings _settings;

        public AuthServiceClient(
            HttpClient httpClient,
            IAppLogger<AuthServiceClient> logger,
            IOptions<AuthServiceSettings> settings)
        {
            _httpClient = httpClient;
            _logger = logger;
            _settings = settings.Value;
        }

        public async Task NotifyUserCreatedAsync(UserCreatedDto newUserDto)
        {
            var endpoint = _settings.PacienteCreatedEndpoint;

            if (_settings.EnableLogging)
            {
                _logger.LogInformation($"Enviando notificación de paciente creado {newUserDto.FullName} a {endpoint}");
            }

            await ExecuteWithRetryAsync(async () =>
            {

                var response = await _httpClient.PostAsJsonAsync(endpoint, newUserDto);

                if (response.IsSuccessStatusCode)
                {
                    if (_settings.EnableLogging)
                    {
                        _logger.LogInformation($"usuario {newUserDto.FullName} notificado exitosamente al AuthService");
                    }
                }
                else
                {
                    _logger.LogWarning($"Error al notificar usuario {newUserDto.FullName}: {response.StatusCode} - {await response.Content.ReadAsStringAsync()}");
                    throw new HttpRequestException($"AuthService returned {response.StatusCode}");
                }
            });
        }

        private async Task ExecuteWithRetryAsync(Func<Task> operation)
        {
            var retrySettings = _settings.Retry;

            for (int attempt = 1; attempt <= retrySettings.MaxAttempts; attempt++)
            {
                try
                {
                    await operation();
                    return; // Éxito
                }
                catch (Exception ex) when (attempt < retrySettings.MaxAttempts)
                {
                    var delay = retrySettings.UseExponentialBackoff
                        ? TimeSpan.FromMilliseconds(retrySettings.DelayMilliseconds * Math.Pow(2, attempt - 1))
                        : TimeSpan.FromMilliseconds(retrySettings.DelayMilliseconds);

                    _logger.LogWarning($"Error en intento {attempt}/{retrySettings.MaxAttempts}: {ex.Message}. Reintentando en {delay.TotalMilliseconds}ms");

                    await Task.Delay(delay);
                }
                catch (Exception ex) when (attempt == retrySettings.MaxAttempts)
                {
                    _logger.LogError($"Error final después de {retrySettings.MaxAttempts} intentos", ex);
                    // No relanzar - es fire-and-forget
                }
            }
        }
    }
}