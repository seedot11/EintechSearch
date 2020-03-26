using EintechSearch.Core.Services;
using EintechSearch.Core.ViewModels;
using EintechSearch.Entity.Models;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace EintechSearch.Test
{
    public class Tests
    {
        private DbContextOptions<EintechSearchContext> options;

        [SetUp]
        public void Setup()
        {
            options = new DbContextOptionsBuilder<EintechSearchContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            SetupTestDatabase();
        }

        [Test]
        public void InsertPerson()
        {
            using (var context = new EintechSearchContext(options))
            {
                var service = GetService(context);
                service.Insert(new CreatePersonViewModel 
                {
                    FirstName = "Ein",
                    LastName = "Tech",
                    GroupId = context.Group.First().Id
                });
                Assert.AreEqual(6, context.Person.Count());
            }
        }
        [Test]
        public void InsertBadPerson()
        {
            using (var context = new EintechSearchContext(options))
            {
                var service = GetService(context);
                var badPerson = new CreatePersonViewModel
                {
                    FirstName = "Bad",
                    LastName = "Person",
                };
                Assert.Throws<InvalidOperationException>(() => service.Insert(badPerson));
            }
        }

        [Test]
        public void GetGroups()
        {
            using (var context = new EintechSearchContext(options))
            {
                var service = GetService(context);
                var groups = service.GetGroups();
                Assert.IsTrue(groups.Any(x => x.Name == "Tigers"));
                Assert.IsTrue(groups.Any(x => x.Name == "Lions"));
            }
        }

        [Test]
        public void GetAll()
        {
            using (var context = new EintechSearchContext(options))
            {
                var service = GetService(context);
                var people = service.GetAll();
                Assert.IsTrue(people.Any(x => x.FirstName == "Tommy"));
                Assert.IsTrue(people.Any(x => x.FirstName == "Tammy"));
                Assert.IsTrue(people.Any(x => x.LastName == "Mith"));
                Assert.IsTrue(people.Any(x => x.LastName == "Smit"));
                Assert.IsTrue(people.Any(x => x.GroupName == "Lions"));
                Assert.IsTrue(people.Any(x => x.GroupName == "Tigers"));
                Assert.AreEqual(5, people.Count());
            }
        }

        [TestCase("mmy", 3, "Tammy")]
        [TestCase("smit", 2, "Timmy")]
        [TestCase("orn", 2, "Tim")]
        [TestCase("iger", 2, "Thomas")]
        [TestCase("m", 5, "Tommy")]
        public void Search(string term, int expectedCount, string expectedFirstName)
        {
            using (var context = new EintechSearchContext(options))
            {
                var service = GetService(context);
                var results = service.Search(term);
                Assert.AreEqual(expectedCount, results.Count());
                Assert.IsTrue(results.Any(x => x.FirstName == expectedFirstName));
            }
        }

        private IPeopleService GetService(EintechSearchContext context)
        {
            return new PeopleService(context);
        }

        private void SetupTestDatabase()
        {
            using (var context = new EintechSearchContext(options))
            {
                var g1 = new Group
                {
                    Name = "Lions"
                };
                var g2 = new Group
                {
                    Name = "Tigers"
                };
                context.AddRange(g1, g2);
                context.SaveChanges();

                var p1 = new Person
                {
                    FirstName = "Tommy",
                    LastName = "Smith",
                    Created = DateTime.Now,
                    Group = g1
                };
                var p2 = new Person
                {
                    FirstName = "Tim",
                    LastName = "Thorne",
                    Created = DateTime.Now,
                    Group = g1
                };
                var p3 = new Person
                {
                    FirstName = "Tammy",
                    LastName = "Borne",
                    Created = DateTime.Now,
                    Group = g1
                };
                var p4 = new Person
                {
                    FirstName = "Timmy",
                    LastName = "Smit",
                    Created = DateTime.Now,
                    Group = g2
                };
                var p5 = new Person
                {
                    FirstName = "Thomas",
                    LastName = "Mith",
                    Created = DateTime.Now,
                    Group = g2
                };

                context.AddRange(p1, p2, p3, p4, p5);
                context.SaveChanges();
            }
        }
    }
}