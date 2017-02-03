using System.Web.Http;
using Welo.Application.Interfaces;

namespace Welo.Bot.Controllers
{
    public class LeadsController : ApiController
    {
        private readonly ILeadAppService _appService;

        public LeadsController()
        {
        }

        //public IEnumerable<LeadModel> Get()
        //{
        //    var retorno = Mapper.Map<IEnumerable<LeadEntity>, IEnumerable<LeadModel>>(_appService.GetAll());
        //    return retorno;
        //}

        //public LeadModel Get(int id) =>
        //    Mapper.Map<LeadEntity, LeadModel>(_appService.Get(id));

        //public void Post([FromBody]LeadModel value)
        //{
        //    var objToInsert = Mapper.Map<LeadModel, LeadEntity>(value);
        //    _appService.Add(objToInsert);
        //}

        //public void Put(int id, [FromBody]LeadModel value)
        //{
        //    var objToUpdate = Mapper.Map<LeadModel, LeadEntity>(value);
        //    _appService.Update(objToUpdate);
        //}

        //public void Delete(int id)
        //{
        //    _appService.Remove(id);
        //}
    }
}