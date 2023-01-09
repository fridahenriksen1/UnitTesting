using System;
using System.IO;
using Core;
using Xunit;

namespace Tests
{
    public class UserTest
    {
        private User newUser;
        private readonly UserRegistry userRegistry;

        public UserTest()
        {
            File.Delete(@"names.txt");
            userRegistry = new UserRegistry();
            newUser = new User("Frida123", "Lucy123", DateTime.Today);
        }

        // 1. Kunna registrera ny användare & lösenord

        [Fact]
        public void UsernameAndPasswordRegistrationTest()
        {
            var newUser = new User("Frida123", "Lucy123!", DateTime.Today);

            Assert.True(userRegistry.register(newUser));
        }

        // 2. Kunna logga in med användare & lösenord

        [Fact]
        public void CanLoggInWithUsernameAndPassword()
        {
            var newUser = new User("Frida123", "Lucy123!", DateTime.Today);
            userRegistry.register(newUser);

            Assert.True(userRegistry.login(newUser));
        }
        // 3. Inte kunna registrera samma användare två gånger om

        [Fact]
        public void CantUseSameUserTwice()
        {
            var newUser1 = new User("Birk123", "Hello123!", DateTime.Today);

            Assert.True(userRegistry.register(newUser1));
            Assert.False(userRegistry.register(newUser1));
        }

        // 4. Bara kunna registrera användarnamn med engelska bokstäver (a-z, A-Z) siffor (0-9) och
        //specialtecken(-_) som är max 16 karaktärer långa

        [Fact]
        public void UsernameInEngAndCharactersTest()
        {
            var validUser = new User("Fridaz123_-", "Lucy123!", DateTime.Today);
            var invalidUser1 = new User("#", "Lucy123!", DateTime.Today);
            var invalidUser2 = new User("Fridahenriksen_17", "Lucy123!", DateTime.Today);
            var invalidUser3 = new User("Fridaö", "Lucy123", DateTime.Today);

            Assert.True(userRegistry.register(validUser));
            Assert.False(userRegistry.register(invalidUser1));
            Assert.False(userRegistry.register(invalidUser2));
            Assert.False(userRegistry.register(invalidUser3));
        }

        // 5. Bara kunna registrera lösenord med bokstäver (a-z, A-Z) siffor (0-9) och specialtecken
        //(!”#¤%&/()=?-_*’) som är max 16 karaktärer långa

        [Fact]
        public void PwdInEngAndCharactersTest()
        {
            var validUser = new User("Fridaz123_", "Lucy123!#*", DateTime.Today);
            var invalidUser1 = new User("Fridaz123_", "LucyÖ", DateTime.Today);
            var invalidUser2 = new User("Fridaz123_", "Lucykljhgvbnmhg17", DateTime.Today);

            Assert.True(userRegistry.register(validUser));
            Assert.False(userRegistry.register(invalidUser1));
            Assert.False(userRegistry.register(invalidUser2));
        }

        // 6. Bara kunna registrera lösenord med minst längd 8 och minst en siffra och ett specialtecken

        [Fact]
        public void PwdMinimunWithNumbAndCharacter()
        {
            var validUser = new User("Fridaz123_", "Lucyyy1#", DateTime.Today);
            var invalidUser1 = new User("Fridaz123_", "Lucy1%7", DateTime.Today);
            var invalidUser2 = new User("Fridaz123_", "Lucyyyy1", DateTime.Today);
            var invalidUser3 = new User("Fridaz123_", "Lucyyyy%", DateTime.Today);

            Assert.True(userRegistry.register(validUser));
            Assert.False(userRegistry.register(invalidUser1));
            Assert.False(userRegistry.register(invalidUser2));
            Assert.False(userRegistry.register(invalidUser3));
        }

        //8. Kolla nersparade användares lösenord vid inloggning

        [Fact]
        public void CheckSavedPwd()
        {
            var newUser1 = new User("Molly123", "Lucy123!", DateTime.Today);
            userRegistry.register(newUser1);

            Assert.True(userRegistry.login(newUser1));
        }

        //9. Inaktivera användarens lösenord efter ett år.r

        [Fact]
        public void DisabledPasswordTest()
        {
            var newUser1 = new User("Sally123", "Hellu321!", DateTime.Today);
            var newUser2 = new User("Sanja123", "Blomma123!", DateTime.Today - TimeSpan.FromDays(365));

            Assert.True(userRegistry.register(newUser1));
            Assert.True(userRegistry.register(newUser2));

            Assert.True(userRegistry.login(newUser1));
            Assert.False(userRegistry.login(newUser2));
        }
    }
}