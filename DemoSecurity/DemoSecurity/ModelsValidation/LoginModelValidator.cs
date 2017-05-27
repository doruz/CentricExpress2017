using DemoSecurity.Models;
using DemoSecurity.Repository;
using DemoSecurity.Security;
using FluentValidation;

namespace DemoSecurity.ModelsValidation
{
    public class LoginModelValidator : AbstractValidator<LoginModel>
    {
        private readonly IUserRepository userRepository;

        public LoginModelValidator(IUserRepository userRepository)
        {
            this.userRepository = userRepository;

            RuleFor(m => m).Must(HaveValidCredentials).WithMessage("username or password are invalid.");
        }

        private bool HaveValidCredentials(LoginModel loginModel)
        {
            if (string.IsNullOrEmpty(loginModel.Username) || string.IsNullOrEmpty(loginModel.Password))
            {
                return false;
            }

            var user = userRepository.Find(loginModel.Username.Encrypt());
            return user != null && user.Password == loginModel.Password.Hash();
        }
    }
}
