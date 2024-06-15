using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class SecurityLoginController : ControllerBase
    {
        private readonly SecurityLoginLogic _context;

        public SecurityLoginController()
        {
            _context = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }

        [HttpGet]
        public ActionResult GetAllSecurityLogin()
        {

            List<SecurityLoginPoco> poco = _context.GetAll();


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
        [Route("login/{id}")]
        public ActionResult GetSecurityLogin(Guid id)
        {

            SecurityLoginPoco poco = _context.Get(id);


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
        public ActionResult PostSecurityLogin(SecurityLoginPoco[] SecurityLoginPocos)
        {
            _context.Add(SecurityLoginPocos);
            return Ok();
        }

        [HttpPut]
        [Route("login")]
        public ActionResult PutSecurityLogin(SecurityLoginPoco[] SecurityLoginPocos)
        {
            _context.Update(SecurityLoginPocos);
            return Ok();
        }

        [HttpDelete]
        [Route("login")]
        public ActionResult DeleteSecurityLogin(SecurityLoginPoco[] SecurityLoginPocos)
        {
            _context.Delete(SecurityLoginPocos);
            return Ok();
        }
    }
}
