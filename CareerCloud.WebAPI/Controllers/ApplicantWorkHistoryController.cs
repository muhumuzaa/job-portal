using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class ApplicantWorkHistoryController : ControllerBase
    {
        private readonly ApplicantWorkHistoryLogic _context;

        public ApplicantWorkHistoryController()
        {
            _context = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());
        }

        [HttpGet]
        public ActionResult GetAllApplicantWorkHistory()
        {
            List<ApplicantWorkHistoryPoco> poco = _context.GetAll();


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
        [Route("history/{id}")]
        public ActionResult GetApplicantWorkHistory(Guid id)
        {
            ApplicantWorkHistoryPoco poco = _context.Get(id);


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
        public ActionResult PostApplicantWorkHistory(ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
        {
            _context.Add(applicantWorkHistoryPocos);
            return Ok();
        }

        [HttpPut]
        [Route("history")]
        public ActionResult PutApplicantWorkHistory(ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
        {
            _context.Update(applicantWorkHistoryPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("history")]
        public ActionResult DeleteApplicantWorkHistory(ApplicantWorkHistoryPoco[] applicantWorkHistoryPocos)
        {
            _context.Delete(applicantWorkHistoryPocos);
            return Ok();
        }
    }
}
