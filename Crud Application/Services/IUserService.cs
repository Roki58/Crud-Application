
using Crud_Application.Models;

namespace Crud_Application.Services
{
    public interface IUserService
    {
        Task<ResponseDto> CreateUserAsync(User user);
        Task<ResponseDto> GetAllUsersAsync();
        Task<ResponseDto> GetUserByIdAsync(int id);
        Task GetUserByIdAsync(User user);
        Task<ResponseDto> UpdatedUserAsync(User user);
        Task<ResponseDto> DeleteUserByIdAsync(int id);
        Task<ResponseDto> SoftDeleteByIdAsync(int id);
    }
}
