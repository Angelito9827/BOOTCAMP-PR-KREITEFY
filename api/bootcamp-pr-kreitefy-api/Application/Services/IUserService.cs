﻿using bootcamp_framework.Application.Services;
using bootcamp_pr_kreitefy_api.Application.Dtos;

namespace bootcamp_pr_kreitefy_api.Application.Services
{
    public interface IUserService : IGenericService<UserDto>
    {
        List<UserDto> GetAllUsers();
        UserDto RegisterUser(UserDto userDto);
    }
}
