using System.Collections.Generic;

namespace DemoSecurity.Repository
{
    public class UserRepository : IUserRepository
    {
        private static readonly IDictionary<string, User> users = new Dictionary<string, User>();

        public IEnumerable<User> GetAll()
        {
            return users.Values;
        }

        public User Find(string encryptedUsername)
        {
            if (users.ContainsKey(encryptedUsername))
            {
                return users[encryptedUsername];
            }

            return null;
        }

        public void AddUser(User user)
        {
            users.Add(user.Username, user);
        }
    }
}
