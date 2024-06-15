using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class SecurityLoginsLogController : ControllerBase
    {
        private readonly SecurityLoginsLogLogic _context;

        public SecurityLoginsLogController()
        {
            _context = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }

        [HttpGet]
        public ActionResult GetAllSecurityLoginLog()
        {
            List<SecurityLoginsLogPoco> poco = _context.GetAll();


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
        [Route("loginslog/{id}")]
        public ActionResult GetSecurityLoginLog(Guid id)
        {
            SecurityLoginsLogPoco poco = _context.Get(id);


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
        public ActionResult PostSecurityLoginLog(SecurityLoginsLogPoco[] SecurityLoginsLogPocos)
        {
            _context.Add(SecurityLoginsLogPocos);
            return Ok();
        }

        [HttpPut]
        [Route("loginslog")]
        public ActionResult PutSecurityLoginLog(SecurityLoginsLogPoco[] SecurityLoginsLogPocos)
        {
            _context.Update(SecurityLoginsLogPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("loginslog")]
        public ActionResult DeleteSecurityLoginLog(SecurityLoginsLogPoco[] SecurityLoginsLogPocos)
        {
            _context.Delete(SecurityLoginsLogPocos);
            return Ok();
        }
    }
}
