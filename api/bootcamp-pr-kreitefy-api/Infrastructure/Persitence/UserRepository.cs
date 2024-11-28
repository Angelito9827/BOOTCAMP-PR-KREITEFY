using bootcamp_framework.Domain.Persistence;
using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Application.Dtos;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private ApplicationContext _applicationContext;
        public UserRepository(ApplicationContext context) : base(context)
        {
            _applicationContext = context;
        }

        public List<UserDto> GetAllUsers()
        {
            return _applicationContext.Users
                .Select(u => new UserDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    LastName = u.LastName,
                    Email = u.Email,
                    Password = u.Password,
                    RoleId = u.RoleId,
                    RoleName = u.Role.Name
                })
                .ToList();
        }

        public override User GetById(long id)
        {
            var user = _applicationContext.Users.Include(i => i.Role).SingleOrDefault(i => i.Id == id);

            if (user == null)
            {
                throw new ElementNotFoundException();
            }
            return user;
        }

        public override User Insert(User user)
        {
            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(user).Reference(i => i.Role).Load();
            return user;
        }

        public override User Update(User user)
        {
            _applicationContext.Users.Update(user);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(user).Reference(i => i.Role).Load();
            return user;
        }
    }
}
