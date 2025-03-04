
using Crud_Application.Models;

namespace Crud_Application.Services
{
    public interface IUserService
    {
        Task<ResponseDto> CreateUserAsync(User user);
        Task<ResponseDto> GetAllUsersAsync();
        Task<ResponseDto> GetUserByIdAsync(int id);
    }
}
