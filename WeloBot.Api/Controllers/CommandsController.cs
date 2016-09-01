using System.Collections.Generic;
using System.Web.Http;
using AutoMapper;
using WeloBot.Api.ViewModels;
using WeloBot.Application.Interfaces;
using WeloBot.Domain.Entities;

namespace WeloBot.Api.Controllers
{
    public class CommandsController : ApiController
    {
        private readonly IStandardCommandAppService _appService;

        public CommandsController(IStandardCommandAppService appService)
        {
            _appService = appService;
        }

        public IEnumerable<StandardCommandViewModel> Get()
        {
            var retorno = Mapper.Map<IEnumerable<StandardCommandEntity>, IEnumerable<StandardCommandViewModel>>(_appService.GetAll());
            return retorno;
        }

        public StandardCommandViewModel Get(int id) =>
            Mapper.Map<StandardCommandEntity, StandardCommandViewModel>(_appService.Get(id));

        public void Post([FromBody]StandardCommandViewModel value)
        {
            var objToInsert = Mapper.Map<StandardCommandViewModel, StandardCommandEntity>(value);
            _appService.Add(objToInsert);
        }

        public void Put(int id, [FromBody]StandardCommandViewModel value)
        {
            var objToUpdate = Mapper.Map<StandardCommandViewModel, StandardCommandEntity>(value);
            _appService.Update(objToUpdate);
        }

        public void Delete(int id)
        {
            _appService.Remove(id);
        }

        [HttpGet]
        [Route("api/Commands/Find")]
        public string GetResponseMessage(string trigger)
        {
            return _appService.GetResponseMessageToTrigger(trigger);
        }
    }
}