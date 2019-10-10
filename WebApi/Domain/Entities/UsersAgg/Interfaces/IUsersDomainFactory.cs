using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities.UsersAgg.Interfaces
{
    public interface IUsersDomainFactory
    {
        User Create(string name, DateTime? birthday);
    }
}