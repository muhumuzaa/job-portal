using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud[controller]/v1")]
    [ApiController]
    public class SecurityRoleController : ControllerBase
    {
        private readonly SecurityRoleLogic _context;

        public SecurityRoleController()
        {
            _context = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());
        }

        [HttpGet]
        public ActionResult GetAllSecurityRole()
        {

            List<SecurityRolePoco> poco = _context.GetAll();


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
        [Route("role/{id}")]
        public ActionResult GetSecurityRole(Guid id)
        {

            SecurityRolePoco poco = _context.Get(id);


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
        public ActionResult PostSecurityRole(SecurityRolePoco[] SecurityRolePocos)
        {
            _context.Add(SecurityRolePocos);
            return Ok();
        }

        [HttpPut]
        [Route("role")]
        public ActionResult PutSecurityRole(SecurityRolePoco[] SecurityRolePocos)
        {
            _context.Update(SecurityRolePocos);
            return Ok();
        }

        [HttpDelete]
        [Route("role")]
        public ActionResult DeleteSecurityRole(SecurityRolePoco[] SecurityRolePocos)
        {
            _context.Delete(SecurityRolePocos);
            return Ok();
        }
    }
}
