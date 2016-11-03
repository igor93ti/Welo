using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using Welo.Application.AppServices;
using Welo.Application.Interfaces;
using Welo.Bot.ViewModels;
using Welo.Domain.Entities;

namespace Welo.Bot.Controllers
{
    public class LeadsController : ApiController
    {
        private readonly ILeadAppService _appService;

        public LeadsController()
        {
            _appService = LeadAppService.Intance;
        }

        public IEnumerable<LeadViewModel> Get()
        {
            var retorno = Mapper.Map<IEnumerable<LeadEntity>, IEnumerable<LeadViewModel>>(_appService.GetAll());
            return retorno;
        }

        public LeadViewModel Get(int id) =>
            Mapper.Map<LeadEntity, LeadViewModel>(_appService.Get(id));

        public void Post([FromBody]LeadViewModel value)
        {
            var objToInsert = Mapper.Map<LeadViewModel, LeadEntity>(value);
            _appService.Add(objToInsert);
        }

        public void Put(int id, [FromBody]LeadViewModel value)
        {
            var objToUpdate = Mapper.Map<LeadViewModel, LeadEntity>(value);
            _appService.Update(objToUpdate);
        }

        public void Delete(int id)
        {
            _appService.Remove(id);
        }
    }
}