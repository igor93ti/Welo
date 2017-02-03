using System.Web.Http;
using Welo.Application.Interfaces;

namespace Welo.Bot.Controllers
{
    public class CommandsController : ApiController
    {
        private readonly IStandardCommandsAppService _service;

        public CommandsController(IStandardCommandsAppService service)
        {
            _service = service;
        }

        //public IEnumerable<StandardCommandModel> Get()
        //{
        //    var retorno = Mapper.Map<IEnumerable<StandardCommandEntity>, IEnumerable<StandardCommandModel>>(_service.GetAll());
        //    return retorno;
        //}

        //public StandardCommandModel Get(int id) =>
        //    Mapper.Map<StandardCommandEntity, StandardCommandModel>(_service.Get(id));

        //public void Post([FromBody]StandardCommandModel value)
        //{
        //    var objToInsert = Mapper.Map<StandardCommandModel, StandardCommandEntity>(value);
        //    _service.Add(objToInsert);
        //}

        //public void Put(int id, [FromBody]StandardCommandModel value)
        //{
        //    var objToUpdate = Mapper.Map<StandardCommandModel, StandardCommandEntity>(value);
        //    _service.Update(objToUpdate);
        //}

        //public void Delete(int id)
        //{
        //    _service.Remove(id);
        //}
    }
}