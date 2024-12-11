using bootcamp_framework.Domain.Persistence;
using bootcamp_framework.Infraestructure.Persistence;
using bootcamp_pr_kreitefy_api.Application.Services;
using bootcamp_pr_kreitefy_api.Domain.Entities;
using bootcamp_pr_kreitefy_api.Domain.Persistence;
using bootcamp_pr_kreitefy_api.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace bootcamp_pr_kreitefy_api.Infrastructure.Persitence
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly ApplicationContext _applicationContext;
        private readonly IPasswordHasher _passwordHasher;

        public UserRepository(ApplicationContext context, IPasswordHasher passwordHasher) : base(context)
        {
            _applicationContext = context;
            _passwordHasher = passwordHasher;
        }

        public List<User> GetAllUsers()
        {
            return _applicationContext.Users
                .Include(u => u.Role)
                .ToList();
        }

        public override User GetById(long id)
        {
            var user = _applicationContext.Users.Include(u => u.Role).SingleOrDefault(u => u.Id == id);
            if (user == null) throw new ElementNotFoundException();
            return user;
        }

        public User? GetUserByEmail(string email)
        {
            return _applicationContext.Users
                .Include(u => u.Role)
                .SingleOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
        }

        public override User Insert(User user)
        {
            user.Password = _passwordHasher.HashPassword(user.Password);
            _applicationContext.Users.Add(user);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(user).Reference(u => u.Role).Load();
            return user;
        }

        public override User Update(User user)
        {
            user.Password = _passwordHasher.HashPassword(user.Password);
            _applicationContext.Users.Update(user);
            _applicationContext.SaveChanges();
            _applicationContext.Entry(user).Reference(u => u.Role).Load();
            return user;
        }
    }
}
