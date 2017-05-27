using System;

namespace DemoSecurity.Security
{
    public class Token
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public DateTime ValidUntil { get; set; } = DateTime.UtcNow.AddSeconds(20);
    }
}
