using DemoSecurity.Models;
using DemoSecurity.Repository;
using DemoSecurity.Security;
using FluentValidation;

namespace DemoSecurity.ModelsValidation
{
    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        private readonly IUserRepository userRepository;

        public RegisterModelValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(m => m.Username).NotEmpty();
            RuleFor(m => m.Password).NotEmpty();
            RuleFor(m => m.Username).Must(NotExist);
        }

        private bool NotExist(string username)
        {
            var encryptedUsername = username.Encrypt();
            return userRepository.Find(encryptedUsername) == null;
        }
    }
}
