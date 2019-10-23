using Domain.Entities.UsersAgg;
using Domain.Entities.UsersAgg.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TortosaApi.Infrastructure.Repository.Entities.UsersAgg;

namespace Infrastructure.Repository.Entities.UsersAgg
{
    class UsersRepository : IUsersRepository
    {
        private readonly UsersDbContext _db;

        public UsersRepository(UsersDbContext db)
        {
            _db = db;
        }

        public User Create(User user)
        {
            _db.Users.Add(user);
            _db.SaveChanges(true);
            return user;
        }

        public User Get(int id)
        {
            return _db.Find<User>(id);
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users.AsEnumerable();
        }

        public bool Remove(User user)
        {
            _db.Users.Remove(user);
            return _db.SaveChanges(true) != 0;
        }

        public User Update(User user)
        {
            _db.Users.Update(user);
            _db.SaveChanges(true);
            return user;
        }
    }
}