using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyJobController : ControllerBase
    {
        private readonly CompanyJobLogic _context;
        public CompanyJobController()
        {
            _context = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        [HttpGet]
        public ActionResult GetAllCompanyJob()
        {
            List<CompanyJobPoco> poco = _context.GetAll();


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
        [Route("companyjob/{id}")]
        public ActionResult GetCompanyJob(Guid id)
        {
            CompanyJobPoco poco = _context.Get(id);


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


        [HttpPut]
        [Route(("companyjob"))]
        public ActionResult PutCompanyJob(CompanyJobPoco[] CompanyJobPoco)
        {
            _context.Update(CompanyJobPoco);
            return Ok();
        }


        [HttpPost]
        public ActionResult PostCompanyJob(CompanyJobPoco[] CompanyJobPoco)
        {
            _context.Add(CompanyJobPoco);
            return Ok();
        }


        [HttpDelete]
        [Route("companyjob")]
        public ActionResult DeleteCompanyJob(CompanyJobPoco[] CompanyJobPoco)
        {
            _context.Delete(CompanyJobPoco);
            return Ok();
        }
    }
}
