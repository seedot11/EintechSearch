using EintechSearch.Core.Extensions;
using EintechSearch.Core.ViewModels;
using EintechSearch.Entity.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EintechSearch.Core.Services
{
    public class PeopleService : IPeopleService
    {
        private readonly EintechSearchContext context;

        public PeopleService(EintechSearchContext context)
        {
            this.context = context;
        }

        public IEnumerable<PersonViewModel> GetAll()
        {
            return context.Person.Include(a => a.Group).ToViewModel().ToList();
        }

        public IEnumerable<PersonViewModel> Search(string searchTerm)
        {
            var results = from p in context.Person
                         join g in context.Group on p.GroupId equals g.Id
                           where EF.Functions.Like(g.Name, $"%{searchTerm}%")
                           || EF.Functions.Like(p.FirstName, $"%{searchTerm}%")
                           || EF.Functions.Like(p.LastName, $"%{searchTerm}%")
                         select new PersonViewModel
                         {
                             Id = p.Id,
                             FirstName = p.FirstName,
                             LastName = p.LastName,
                             Created = p.Created,
                             GroupName = g.Name
                         };

            return results.Distinct().ToList();
        }

        public IEnumerable<GroupViewModel> GetGroups()
        {
            return context.Group.ToViewModel().ToList();
        }

        public PersonViewModel Insert(CreatePersonViewModel createPerson)
        {
            var group = context.Group.First(x => x.Id == createPerson.GroupId);
            var newPerson = new Person
            {
                Created = DateTime.Now,
                FirstName = createPerson.FirstName,
                LastName = createPerson.LastName,
                Group = group
            };
            context.Add(newPerson);
            context.SaveChanges();
            return newPerson.ToViewModel();
        }
    }
}
