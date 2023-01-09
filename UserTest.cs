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

        // 1. Kunna registrera ny anv�ndare & l�senord

        [Fact]
        public void UsernameAndPasswordRegistrationTest()
        {
            var newUser = new User("Frida123", "Lucy123!", DateTime.Today);

            Assert.True(userRegistry.register(newUser));
        }

        // 2. Kunna logga in med anv�ndare & l�senord

        [Fact]
        public void CanLoggInWithUsernameAndPassword()
        {
            var newUser = new User("Frida123", "Lucy123!", DateTime.Today);
            userRegistry.register(newUser);

            Assert.True(userRegistry.login(newUser));
        }
        // 3. Inte kunna registrera samma anv�ndare tv� g�nger om

        [Fact]
        public void CantUseSameUserTwice()
        {
            var newUser1 = new User("Birk123", "Hello123!", DateTime.Today);

            Assert.True(userRegistry.register(newUser1));
            Assert.False(userRegistry.register(newUser1));
        }

        // 4. Bara kunna registrera anv�ndarnamn med engelska bokst�ver (a-z, A-Z) siffor (0-9) och
        //specialtecken(-_) som �r max 16 karakt�rer l�nga

        [Fact]
        public void UsernameInEngAndCharactersTest()
        {
            var validUser = new User("Fridaz123_-", "Lucy123!", DateTime.Today);
            var invalidUser1 = new User("#", "Lucy123!", DateTime.Today);
            var invalidUser2 = new User("Fridahenriksen_17", "Lucy123!", DateTime.Today);
            var invalidUser3 = new User("Frida�", "Lucy123", DateTime.Today);

            Assert.True(userRegistry.register(validUser));
            Assert.False(userRegistry.register(invalidUser1));
            Assert.False(userRegistry.register(invalidUser2));
            Assert.False(userRegistry.register(invalidUser3));
        }

        // 5. Bara kunna registrera l�senord med bokst�ver (a-z, A-Z) siffor (0-9) och specialtecken
        //(!�#�%&/()=?-_*�) som �r max 16 karakt�rer l�nga

        [Fact]
        public void PwdInEngAndCharactersTest()
        {
            var validUser = new User("Fridaz123_", "Lucy123!#*", DateTime.Today);
            var invalidUser1 = new User("Fridaz123_", "Lucy�", DateTime.Today);
            var invalidUser2 = new User("Fridaz123_", "Lucykljhgvbnmhg17", DateTime.Today);

            Assert.True(userRegistry.register(validUser));
            Assert.False(userRegistry.register(invalidUser1));
            Assert.False(userRegistry.register(invalidUser2));
        }

        // 6. Bara kunna registrera l�senord med minst l�ngd 8 och minst en siffra och ett specialtecken

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

        //8. Kolla nersparade anv�ndares l�senord vid inloggning

        [Fact]
        public void CheckSavedPwd()
        {
            var newUser1 = new User("Molly123", "Lucy123!", DateTime.Today);
            userRegistry.register(newUser1);

            Assert.True(userRegistry.login(newUser1));
        }

        //9. Inaktivera anv�ndarens l�senord efter ett �r.r

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