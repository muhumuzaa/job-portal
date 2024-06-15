using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]


    public class ApplicantEducationController : ControllerBase
    {
        private readonly ApplicantEducationLogic _context;
        public ApplicantEducationController()
        {
            _context = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }

        [HttpGet]
        public ActionResult GetAllApplicantEducation()
        {
            List<ApplicantEducationPoco> poco = _context.GetAll();


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
        [Route("education/{id}")]
        public ActionResult GetApplicantEducation(Guid id)
        {
            ApplicantEducationPoco poco = _context.Get(id);


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
        [Route(("job"))]
        public ActionResult PutApplicantEducation(ApplicantEducationPoco[] applicantEducationPoco)
        {
            _context.Update(applicantEducationPoco);
            return Ok();
        }


        [HttpPost]
        public ActionResult PostApplicantEducation(ApplicantEducationPoco[] applicantEducationPoco)
        {
            _context.Add(applicantEducationPoco);
            return Ok();
        }


        [HttpDelete]
        [Route("job")]
        public ActionResult DeleteApplicantEducation(ApplicantEducationPoco[] applicantEducationPoco)
        {
            _context.Delete(applicantEducationPoco);
            return Ok();
        }
    }
}
