using System;
using System.Collections.Generic;
using System.Linq;
using EintechSearch.Core.Services;
using EintechSearch.Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace EintechSearch.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PersonController : ControllerBase
    {
        private readonly ILogger<PersonController> _logger;
        private readonly IPeopleService _peopleService;

        public PersonController(ILogger<PersonController> logger, IPeopleService peopleService)
        {
            _logger = logger;
            _peopleService = peopleService;
        }

        [HttpGet]
        [Route("")]
        public IEnumerable<PersonViewModel> GetAll()
        {
            var response = _peopleService.GetAll();
            return response;
        }

        [HttpGet]
        [Route("groups")]
        public IEnumerable<GroupViewModel> GetAllGroups()
        {
            var response = _peopleService.GetGroups();
            return response;
        }

        [HttpGet]
        [Route("search/{searchTerm}")]
        public IEnumerable<PersonViewModel> Search(string searchTerm)
        {
            var response = _peopleService.Search(searchTerm);
            return response;
        }

        [HttpPost]
        [Route("")]
        public PersonViewModel Post(CreatePersonViewModel request)
        {
            var response = _peopleService.Insert(request);
            return response;
        }
    }
}
