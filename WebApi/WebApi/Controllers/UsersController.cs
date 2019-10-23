using System;
using System.Collections.Generic;
using System.Linq;
using Application.Dtos.UserAgg;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUserAppService _userAppService;

        public UsersController(ILogger<UsersController> logger, IUserAppService userAppService)
        {
            _logger = logger;
            _userAppService = userAppService;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var result = _userAppService.GetAllDto();
            return Ok(result);
        }


        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var result = _userAppService.GetDto(id);

            if (result == null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDtoCU dto)
        {
            var result = _userAppService.CreateDto(dto);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] UserDtoCU dto)
        {
            var result = _userAppService.Update(id, dto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _userAppService.Delete(id);
            return Ok(result);
        }
    }
}