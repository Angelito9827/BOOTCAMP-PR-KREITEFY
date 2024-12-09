﻿using bootcamp_framework.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Domain.Entities;

namespace bootcamp_pr_kreitefy_api.Domain.Persistence
{
    public interface IUserRepository : IGenericRepository<User>
    {
        List<User> GetAllUsers();
        User? GetUserByEmail(string email);
    }
}
