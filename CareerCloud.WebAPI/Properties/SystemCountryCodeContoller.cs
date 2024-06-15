using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Properties
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SystemCountryCodeContoller : ControllerBase
    {
        private readonly SystemCountryCodeLogic _context;

        public SystemCountryCodeContoller()
        {
            _context = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());
        }

        [HttpGet]
        public ActionResult GetAll()
        {

            return Ok(_context.GetAll());
        }

        [HttpGet]
        [Route("country/{id}")]
        public ActionResult GetSystemCountryCodePoco(Guid id)
        {
            return Ok(_context.Get(id));
        }


        [HttpPut("country")]
        public ActionResult PutSystemCountryCodePoco(SystemCountryCodePoco[] systemCountryCodePoco)
        {



            _context.Update(systemCountryCodePoco);
            return Ok();


        }


        [HttpPost]
        public ActionResult PostSystemCountryCodePoco(SystemCountryCodePoco[] systemCountryCodePoco)
        {
            _context.Add(systemCountryCodePoco);



            return Created("GetSystemCountryCodePoco", systemCountryCodePoco); //return Ok
        }


        [HttpDelete]
        [Route("country")]
        public ActionResult DeleteSystemCountryCodePoco(SystemCountryCodePoco[] systemCountryCodePoco)
        {



            _context.Delete(systemCountryCodePoco);


            return Ok();
        }


    }
}

