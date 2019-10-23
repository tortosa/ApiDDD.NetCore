using Application.Dtos.UserAgg;
using Application.Interfaces;
using Domain.Entities.UsersAgg;
using Domain.Entities.UsersAgg.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Application.AppServices
{
    public class UserAppService : IUserAppService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IUsersDomainFactory _usersDomainFactory;

        public UserAppService(
            IUsersRepository usersRepository,
            IUsersDomainFactory usersDomainFactory)
        {
            _usersRepository = usersRepository;
            _usersDomainFactory = usersDomainFactory;
        }

        public User Get(int id)
        {
            var user = _usersRepository.Get(id);
            return user;
        }

        public UserDtoR GetDto(int id)
        {
            var user = Get(id);
            
            return user == null ?  null : new UserDtoR()
            {
                Id = user.Id,
                Name = user.Name,
                Birthdate = user.Birthdate
            };
        }

        public IEnumerable<User> GetAll()
        {
            var users = _usersRepository.GetAll();
            return users;
        }

        public IEnumerable<UserDtoR> GetAllDto()
        {
            var users = GetAll();

            var usersDto = users.Select(user => new UserDtoR()
            {
                Id = user.Id,
                Name = user.Name,
                Birthdate = user.Birthdate
            });

            return usersDto;
        }

        public User Create(UserDtoCU dto)
        {
            var user = _usersDomainFactory.Create(dto.Name, dto.Birthdate);
            var userCreated = _usersRepository.Create(user);

            return Get(userCreated.Id);
        }

        public UserDtoR CreateDto(UserDtoCU dto)
        {
            var user = _usersDomainFactory.Create(dto.Name, dto.Birthdate);
            var userCreated = _usersRepository.Create(user);
            return GetDto(userCreated.Id);
        }

        public UserDtoR Update(int id, UserDtoCU dto)
        {/*
            var user = _usersRepository.Get(id);
            user.Name = dto.Name ?? user.Name;
            user.Birthdate = dto.Birthdate ?? user.Birthdate;
            _usersRepository.Update(user);

            return Get(user.Id);
            */
            return null;
        }

        public bool Delete(int id)
        {/*
            var user = _usersRepository.Get(id);
            return _usersRepository.Remove(user);
            */
            return false;
        }
    }
}