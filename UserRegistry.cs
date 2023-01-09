using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Core
{
    public class UserRegistry
    {
        public List<User> userList;

        public UserRegistry()
        {
            userList = LoadUsersFromTxtFile();
        }

        private List<User> LoadUsersFromTxtFile()
        {
            var users = new List<User>();

            if (File.Exists(@"names.txt"))
            {
                using var SR = new StreamReader(@"names.txt");

                var line = SR.ReadLine();

                while (line != null)
                {
                    var stringData = line.Split(",");
                    var user = new User("Username", "Password", DateTime.Today);

                    user.Username = stringData[0];
                    users.Add(user);

                    line = SR.ReadLine();
                }
            }

            return users;
        }


        public bool register(User newUser)
        {
            SaveUserAndPwdToTxtFile(newUser);

            var regex = new Regex("^(?!^.{17})[a-zA-Z0-9-_]+$");
            var validUsername = regex.IsMatch(newUser.Username);

            var validPassword = registerPwd(newUser) && registerPwd2(newUser);

            if (validUsername && validPassword)
            {
                foreach (var user in userList)
                    if (user.Username == newUser.Username)
                        return false;

                userList.Add(newUser);


                return true;
            }

            return false;
        }

        public bool registerPwd(User newUser)
        {
            var regex = new Regex("^(?!^.{17})[a-zA-Z0-9-_!”#¤%&/()=?*’]+$");

            var validPassword = regex.IsMatch(newUser.Password);
            if (validPassword) return true;

            return false;
        }

        public bool registerPwd2(User newUser)
        {
            var regex = new Regex(@"^(?=.*[a-zA-Z])(?=.*\d)(?=.*[$@$!%*?&¤/()'=#_-]).{8,}$");

            var valisPassword2 = regex.IsMatch(newUser.Password);

            if (valisPassword2) return true;

            return false;
        }
        public bool login(User userLogin)
        {
            var oneYear = DateTime.Today - TimeSpan.FromDays(365);

            foreach (var users in userList)
                if (users.Username == userLogin.Username
                    && users.Password == userLogin.Password
                    && users.RegisteredAt > oneYear)
                    return true;
            return false;
        }

        public void SaveUserAndPwdToTxtFile(User user)
        {
            using var SW = new StreamWriter(@"names.txt");
            SW.WriteLine($"{user.Username},{user.Password}, {user.RegisteredAt}");
        }
    }
}