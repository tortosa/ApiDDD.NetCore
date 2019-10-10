using Domain.Entities.UsersAgg.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.UsersAgg.Services
{
    public class UsersDomainFactory : IUsersDomainFactory
    {
        public User Create(string name, DateTime? birthDate)
        {
            var user = new User()
            {
                Name = name,
                Birthdate = birthDate
            };

            return user;
        }
    }
}