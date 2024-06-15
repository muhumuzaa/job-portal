using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class CompanyJobsDescriptionController : ControllerBase
    {
        private readonly CompanyJobDescriptionLogic _context;

        public CompanyJobsDescriptionController()
        {
            _context = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }

        [HttpGet]
        public ActionResult GetCompanyJobsDescription()
        {
            List<CompanyJobDescriptionPoco> poco = _context.GetAll();


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
        [Route("jobdescription/{id}")]
        public ActionResult GetCompanyJobsDescription(Guid id)
        {
            CompanyJobDescriptionPoco poco = _context.Get(id);


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
        public ActionResult PostCompanyJobsDescription([FromBody] CompanyJobDescriptionPoco[] CompanyJobDescriptionPocos)
        {
            _context.Add(CompanyJobDescriptionPocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobdescription")]
        public ActionResult PutCompanyJobsDescription(CompanyJobDescriptionPoco[] CompanyJobDescriptionPocos)
        {
            _context.Update(CompanyJobDescriptionPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobdescription")]
        public ActionResult DeleteCompanyJobsDescription(CompanyJobDescriptionPoco[] CompanyJobDescriptionPocos)
        {
            _context.Delete(CompanyJobDescriptionPocos);
            return Ok();
        }
    }
}
