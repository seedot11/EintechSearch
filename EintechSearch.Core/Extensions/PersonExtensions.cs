using EintechSearch.Core.ViewModels;
using EintechSearch.Entity.Models;
using System.Collections.Generic;
using System.Linq;

namespace EintechSearch.Core.Extensions
{
    public static class PersonExtensions
    {
        public static PersonViewModel ToViewModel(this Person person)
        {
            return new PersonViewModel
            {
                Id = person.Id,
                Created = person.Created,
                FirstName = person.FirstName,
                LastName = person.LastName,
                GroupName = person.Group.Name
            };
        }

        public static IEnumerable<PersonViewModel> ToViewModel(this IEnumerable<Person> people)
        {
            foreach (var person in people)
            {
                yield return person.ToViewModel();
            }
        }

        public static GroupViewModel ToViewModel(this Group group)
        {
            return new GroupViewModel
            {
                Id = group.Id,
                Name = group.Name
            };
        }

        public static IEnumerable<GroupViewModel> ToViewModel(this IEnumerable<Group> groups)
        {
            foreach (var group in groups)
            {
                yield return group.ToViewModel();
            }
        }
    }
}
