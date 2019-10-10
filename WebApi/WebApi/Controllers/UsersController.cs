using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;

        public UsersController(ILogger<UsersController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<Users> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Users
            {
                Name = "Nombre",
                Birthdate = DateTime.UtcNow
            })
            .ToArray();
        }
    }
}