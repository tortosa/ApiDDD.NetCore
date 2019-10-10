using Domain.Entities.UsersAgg;
using Domain.Entities.UsersAgg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repository.Entities.UsersAgg
{
    class UsersRepository : IUsersRepository
    {
        public User Create(User user)
        {
            throw new NotImplementedException();
        }

        public User Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public bool Remove(User user)
        {
            throw new NotImplementedException();
        }

        public User Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}