using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantSkillController : ControllerBase
    {
        private readonly ApplicantSkillLogic _context;

        public ApplicantSkillController()
        {
            _context = new ApplicantSkillLogic(new EFGenericRepository<ApplicantSkillPoco>());
        }

        [HttpGet]
        public ActionResult GetAllApplicantSkill()
        {
            List<ApplicantSkillPoco> poco = _context.GetAll();


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
        [Route("skill/{id}")]
        public ActionResult GetApplicantSkill(Guid id)
        {
            ApplicantSkillPoco poco = _context.Get(id);


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
        public ActionResult PostApplicantSkill(ApplicantSkillPoco[] applicantSkillPocos)
        {
            _context.Add(applicantSkillPocos);
            return Ok();
        }

        [HttpPut]
        [Route("skill")]
        public ActionResult PutApplicantSkill(ApplicantSkillPoco[] applicantSkillPocos)
        {
            _context.Update(applicantSkillPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("skill")]
        public ActionResult DeleteApplicantSkill(ApplicantSkillPoco[] applicantSkillPocos)
        {
            _context.Delete(applicantSkillPocos);
            return Ok();
        }

    }
}
