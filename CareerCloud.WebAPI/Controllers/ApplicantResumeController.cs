using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class ApplicantResumeController : ControllerBase
    {
        private readonly ApplicantResumeLogic _context;

        public ApplicantResumeController()
        {
            _context = new ApplicantResumeLogic(new EFGenericRepository<ApplicantResumePoco>());
        }

        [HttpGet]
        public ActionResult GetAllApplicantResume()
        {
            List<ApplicantResumePoco> poco = _context.GetAll();


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
        [Route("resume/{id}")]
        public ActionResult GetApplicantResume(Guid id)
        {
            ApplicantResumePoco poco = _context.Get(id);


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
        public ActionResult PostApplicantResume(ApplicantResumePoco[] applicantResumePocos)
        {
            _context.Add(applicantResumePocos);
            return Ok();
        }

        [HttpPut]
        [Route("resume")]
        public ActionResult PutApplicantResume(ApplicantResumePoco[] applicantResumePocos)
        {
            _context.Update(applicantResumePocos);
            return Ok();
        }

        [HttpDelete]
        [Route("resume")]
        public ActionResult DeleteApplicantResume(ApplicantResumePoco[] applicantResumePocos)
        {
            _context.Delete(applicantResumePocos);
            return Ok();
        }
    }
}
