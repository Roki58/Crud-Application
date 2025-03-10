﻿using Crud_Application.Models;

namespace Crud_Application.Repositories
{
    public interface IUserRepo
    {
        Task<int> CreateUserAsync(User user);
        Task<ResponseDto> GetAllUsersAsync();
        Task<ResponseDto> GetUserByIdAsync(int id);
    }
}
