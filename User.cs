using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public DateTime RegisteredAt { get; set; }


        public User(string username, string password, DateTime registeredAt)
        {
            Username = username;
            Password = password;
            RegisteredAt = registeredAt;    
        }
      
    }

   
}
