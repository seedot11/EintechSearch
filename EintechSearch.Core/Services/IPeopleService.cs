using EintechSearch.Core.ViewModels;
using System.Collections.Generic;

namespace EintechSearch.Core.Services
{
    public interface IPeopleService
    {
        IEnumerable<PersonViewModel> GetAll();

        IEnumerable<PersonViewModel> Search(string searchTerm);

        IEnumerable<GroupViewModel> GetGroups();

        PersonViewModel Insert(CreatePersonViewModel createPerson);
    }
}
