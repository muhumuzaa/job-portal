using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantProfileController : ControllerBase
    {
        private readonly ApplicantProfileLogic _context;
        public ApplicantProfileController()
        {
            _context = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        [HttpGet]
        public ActionResult GetAllApplicantProfile()
        {
            List<ApplicantProfilePoco> poco = _context.GetAll();


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
        [Route("profile/{id}")]
        public ActionResult GetApplicantProfile(Guid id)
        {
            ApplicantProfilePoco poco = _context.Get(id);


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
        [Route(("profile"))]
        public ActionResult PutApplicantProfile(ApplicantProfilePoco[] applicantProfilePoco)
        {
            _context.Update(applicantProfilePoco);
            return Ok();
        }


        [HttpPost]
        public ActionResult PostApplicantProfile(ApplicantProfilePoco[] applicantProfilePoco)
        {
            _context.Add(applicantProfilePoco);
            return Ok();
        }


        [HttpDelete]
        [Route("profile")]
        public ActionResult DeleteApplicantProfile(ApplicantProfilePoco[] applicantProfilePoco)
        {
            _context.Delete(applicantProfilePoco);
            return Ok();
        }
    }
}
