using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class CompanyLocationController : ControllerBase
    {
        private readonly CompanyLocationLogic _context;

        public CompanyLocationController()
        {
            _context = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());
        }

        [HttpGet]
        public ActionResult GetAllCompanyLocation()
        {
            List<CompanyLocationPoco> poco = _context.GetAll();


            if (poco == null)
            {
                // 404
                return NotFound();
            }
            else
            {
                // 200
                return Ok(poco);
            }
        }

        [HttpGet]
        [Route("companylocation/{id}")]
        public ActionResult GetCompanyLocation(Guid id)
        {
            CompanyLocationPoco poco = _context.Get(id);


            if (poco == null)
            {
                // 404
                return NotFound();
            }
            else
            {
                // 200
                return Ok(poco);
            }
        }

        [HttpPost]
        public ActionResult PostCompanyLocation(CompanyLocationPoco[] CompanyLocationPocos)
        {
            _context.Add(CompanyLocationPocos);
            return Ok();
        }

        [HttpPut]
        [Route("companylocation")]
        public ActionResult PutCompanyLocation(CompanyLocationPoco[] CompanyLocationPocos)
        {
            _context.Update(CompanyLocationPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("companylocation")]
        public ActionResult DeleteCompanyLocation(CompanyLocationPoco[] CompanyLocationPocos)
        {
            _context.Delete(CompanyLocationPocos);
            return Ok();
        }
    }
}
