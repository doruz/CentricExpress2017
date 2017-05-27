using System.Collections.Generic;

namespace DemoSecurity.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();

        User Find(string encryptedUsername);

        void AddUser(User user);
    }
}
