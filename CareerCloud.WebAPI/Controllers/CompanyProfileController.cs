using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class CompanyProfileController : ControllerBase
    {
        private readonly CompanyProfileLogic _context;

        public CompanyProfileController()
        {
            _context = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        [HttpGet]
        public ActionResult GetAllCompanyProfile()
        {

            List<CompanyProfilePoco> poco = _context.GetAll();


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
        [Route("companyprofile/{id}")]
        public ActionResult GetCompanyProfile(Guid id)
        {

            CompanyProfilePoco poco = _context.Get(id);


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
        public ActionResult PostCompanyProfile(CompanyProfilePoco[] companyProfilePoco)
        {
            _context.Add(companyProfilePoco);
            return Ok();
        }

        [HttpPut]
        [Route("companyprofile")]
        public ActionResult PutCompanyProfile(CompanyProfilePoco[] companyProfilePoco)
        {
            _context.Update(companyProfilePoco);
            return Ok();
        }

        [HttpDelete]
        [Route("{companyprofile}")]
        public ActionResult DeleteCompanyProfile(CompanyProfilePoco[] companyProfilePocos)
        {
            _context.Delete(companyProfilePocos);
            return Ok();
        }
    }
}
