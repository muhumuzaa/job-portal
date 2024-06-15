using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class CompanyJobSkillController : ControllerBase
    {
        private readonly CompanyJobSkillLogic _context;

        public CompanyJobSkillController()
        {
            _context = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }

        [HttpGet]
        public ActionResult GetAllCompanyJobSkill()
        {

            List<CompanyJobSkillPoco> poco = _context.GetAll();


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
        [Route("jobskill/{id}")]
        public ActionResult GetCompanyJobSkill(Guid id)
        {

            CompanyJobSkillPoco poco = _context.Get(id);


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
        public ActionResult PostCompanyJobSkill(CompanyJobSkillPoco[] CompanyJobSkillPocos)
        {
            _context.Add(CompanyJobSkillPocos);
            return Ok();
        }

        [HttpPut]
        [Route("jobskill")]
        public ActionResult PutCompanyJobSkill(CompanyJobSkillPoco[] CompanyJobSkillPocos)
        {
            _context.Update(CompanyJobSkillPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("jobskill")]
        public ActionResult DeleteCompanyJobSkill(CompanyJobSkillPoco[] CompanyJobSkillPocos)
        {
            _context.Delete(CompanyJobSkillPocos);
            return Ok();
        }
    }
}
