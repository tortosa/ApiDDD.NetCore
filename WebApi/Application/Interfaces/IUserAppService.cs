using Application.Dtos.UserAgg;
using Domain.Entities.UsersAgg;
using System.Collections.Generic;

namespace Application.Interfaces
{
    public interface IUserAppService
    {
        User Get(int id);

        UserDtoR GetDto(int id);

        public IEnumerable<User> GetAll();

        IEnumerable<UserDtoR> GetAllDto();

        User Create(UserDtoCU dto);

        UserDtoR CreateDto(UserDtoCU dto);

        UserDtoR Update(int id, UserDtoCU dto);

        bool Delete(int id);
    }
}