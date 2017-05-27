using System.Collections.Generic;
using DemoSecurity.Models;
using DemoSecurity.Repository;
using DemoSecurity.Security;
using Microsoft.AspNetCore.Mvc;

namespace DemoSecurity.Controllers
{
    [Route("api/[controller]")]
    public class UsersController : Controller
    {
        private readonly IUserRepository userRepository;

        public UsersController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        [HttpGet]
        [Route("all")]
        public IActionResult GetAll()
        {
            var users = userRepository.GetAll();

            return Ok(users);
        }

        [HttpGet]
        [Route("allDecrypted")]
        [TokenAuthorization]
        public IActionResult GetAllDecrypted()
        {
            var users = DecryptUsers(userRepository.GetAll());

            return Ok(users);
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var newUser = CreateEncryptedUser(model);
            userRepository.AddUser(newUser);

            return Ok();
        }

        [HttpGet]
        [Route("login")]
        public IActionResult Login(LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(AuthorizationTokenHelper.GenerateToken());
        }

        private User CreateEncryptedUser(RegisterModel model)
        {
            var newUser = new User
            {
                Username = model.Username.Encrypt(),
                Password = model.Password.Hash(),
                Email = model.Email.Encrypt()
            };

            return newUser;
        }

        private IEnumerable<User> DecryptUsers(IEnumerable<User> users)
        {
            foreach (var user in users)
            {
                yield return new User
                {
                    Username = user.Username.Decrypt(),
                    Password = user.Password,
                    Email = user.Email.Decrypt()
                };
            }
        }
    }
}
