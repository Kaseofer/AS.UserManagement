using AS.UserManagement.Application.ExternalServices.Dtos;

namespace AS.UserManagement.Application.ExternalServices.Interfaces
{
    public interface IAuthServiceClient
    {
        Task NotifyUserCreatedAsync(UserCreatedDto newUserDto);
    }
}