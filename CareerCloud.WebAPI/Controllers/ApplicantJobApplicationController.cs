using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CareerCloud.Pocos;

using CareerCloud.BusinessLogicLayer;
using CareerCloud.DataAccessLayer;
using CareerCloud.EntityFrameworkDataAccess;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class ApplicantJobApplicationController : ControllerBase
    {
        private readonly ApplicantJobApplicationLogic _context;

        public ApplicantJobApplicationController()
        {
            _context = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
        }

        [HttpGet]
        public ActionResult GetAllApplicantJobApplication()
        {
            List<ApplicantJobApplicationPoco> poco = _context.GetAll();


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
        [Route("job/{id}")]
        public ActionResult GetApplicantJobApplication(Guid id)
        {
            ApplicantJobApplicationPoco poco = _context.Get(id);


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
        [Route("job")]
        public ActionResult PutApplicantJobApplication(ApplicantJobApplicationPoco[] applicantJobApplicationPoco)
        {
            _context.Update(applicantJobApplicationPoco);
            return Ok();
        }


        [HttpPost]
        public ActionResult PostApplicantJobApplication(ApplicantJobApplicationPoco[] applicantJobApplicationPoco)
        {
            _context.Add(applicantJobApplicationPoco);
            return Ok();
        }

   
        [HttpDelete]
        [Route("job")]
        public ActionResult DeleteApplicantJobApplication(ApplicantJobApplicationPoco[] applicantJobApplicationPoco)
        {
            _context.Delete(applicantJobApplicationPoco);
            return Ok();
        }

       
    }
}
