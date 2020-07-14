using ConsoleApp;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SamuraiApp.Data;
using SamuraiApp.Domain;
using System;
using System.Linq;

namespace Tests
{
    [TestClass]
    public class DatabaseTests
    {
        [TestMethod]
        public void CaninsertSamuraiIntoDatabase()
        {
            var _ctx = new SamuraiContext();

            var samurai = new Samurai()
            {
                Name = "George"
            };
            Console.WriteLine($"Before: {samurai.Id}");
            _ctx.Add(samurai);
            _ctx.SaveChanges();
            Console.WriteLine($"After: {samurai.Id}");

            Assert.AreNotEqual(0, samurai.Id);
        }

        [TestMethod]
        public void CanInsertSamuraiUsingInMemoryDatabase()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertSamurai");
            using(var context = new SamuraiContext(builder.Options))
            {
                var samurai = new Samurai();
                Console.WriteLine($"before: {samurai.Id}");
                context.Samurais.Add(samurai);
                context.SaveChanges();
                Console.WriteLine($"after: ");
                Assert.AreNotEqual(0, samurai.Id);
            }
        }
        [TestMethod]
        public void InsertUsingBusinessLogic()
        {
            var builder = new DbContextOptionsBuilder();
            builder.UseInMemoryDatabase("CanInsertMultipleSamurais");
            using (var context = new SamuraiContext(builder.Options))
            {
                var bz = new BusinessDataLogic();
                var namesList = new string[] { "Alex", "Vlad", "Iulian" };
                var result = bz.InsertSamurai(namesList);

                Assert.AreEqual(namesList.Count(), result);
              }
        }
    }
}
