using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.UsersAgg.Interfaces
{
    public interface IUsersRepository
    {
        User Get(int id);

        IEnumerable<User> GetAll();

        User Create(User user);

        User Update(User user);

        bool Remove(User user);
    }
}