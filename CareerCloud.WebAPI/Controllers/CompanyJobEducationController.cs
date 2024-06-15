using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class CompanyJobEducationController : ControllerBase
    {
        private readonly CompanyJobEducationLogic _context;

        public CompanyJobEducationController()
        {
            _context = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
        }

        [HttpGet]
        public ActionResult GetAllCompanyJobEducation()
        {

            List<CompanyJobEducationPoco> poco = _context.GetAll();


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
        [Route("jobeducation/{id}")]
        public ActionResult GetCompanyJobEducation(Guid id)
        {

            CompanyJobEducationPoco poco = _context.Get(id);


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
        public ActionResult PostCompanyJobEducation(CompanyJobEducationPoco[] CompanyJobEducationPocos)
        {
            _context.Add(CompanyJobEducationPocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobeducation")]
        public ActionResult PutCompanyJobEducation(CompanyJobEducationPoco[] CompanyJobEducationPocos)
        {
            _context.Update(CompanyJobEducationPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobeducation")]
        public ActionResult DeleteCompanyJobEducation(CompanyJobEducationPoco[] CompanyJobEducationPocos)
        {
            _context.Delete(CompanyJobEducationPocos);
            return Ok();
        }
    }
}
