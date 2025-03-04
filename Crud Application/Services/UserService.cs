using Crud_Application.Models;
using Crud_Application.Repositories;

namespace Crud_Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepo _userRepo;
        public UserService(IUserRepo userRepo)
        {
            _userRepo=userRepo;
        }
        public async Task<ResponseDto> CreateUserAsync(User user)
        {
            var response = await _userRepo.CreateUserAsync(user);
            ResponseDto res = new ResponseDto();
            if (response != null && response == 1)
            {
                res.StatusCode = "201";
                res.Msg = "User Successfully created";
            }
            else
            {
                res.StatusCode= "500";
                res.Msg= "User not created";
            }

            return res;

        }
        public async Task<ResponseDto> GetAllUsersAsync()
        {
            var response= await _userRepo.GetAllUsersAsync();
            return response;
           
        }
        public async Task<ResponseDto> GetUserByIdAsync(int id)
        {
            var response = await _userRepo.GetUserByIdAsync(id);
            return response;

        }

    }
}
