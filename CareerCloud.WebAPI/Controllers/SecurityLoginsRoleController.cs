using System;
using System.Collections.Generic;
using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Mvc;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/security/v1")]
    [ApiController]
    public class SecurityLoginsRoleController : ControllerBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleController()
        {
            var repo = new EFGenericRepository<SecurityLoginsRolePoco>();
            _logic = new SecurityLoginsRoleLogic(repo);
        }

        //Get on ID
        [HttpGet]
        [Route("loginsrole/{id}")]

        [ProducesResponseType(200, Type = typeof(SecurityLoginsRolePoco))]
        public ActionResult GetSecurityLoginsRole(Guid id)
        {
            SecurityLoginsRolePoco poco = _logic.Get(id);
            if (poco == null)
            {
                //404
                return NotFound();
            }
            else
            {
                //200
                return Ok(poco);
            }
        }

        //Get All
        [HttpGet]
        [Route("loginsrole")]

        [ProducesResponseType(200, Type = typeof(List<SecurityLoginsRolePoco>))]
        public ActionResult GetAllSecurityLoginsRole()
        {
            List<SecurityLoginsRolePoco> pocos = _logic.GetAll();
            if (pocos == null)
            {
                //404
                return NotFound();
            }
            else
            {
                //200
                return Ok(pocos);
            }

        }

        //Post

        [HttpPost]
        [Route("loginsrole")]
        public ActionResult PostSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] securityLoginsRolePocos)
        {
            _logic.Add(securityLoginsRolePocos);
            return Ok();
        }

        //Put

        [HttpPut]
        [Route("loginsrole")]
        public ActionResult PutSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] securityLoginsRolePocos)
        {
            _logic.Update(securityLoginsRolePocos);
            return Ok();
        }

        //Delete

        [HttpDelete]
        [Route("loginsrole")]
        public ActionResult DeleteSecurityLoginRole([FromBody] SecurityLoginsRolePoco[] securityLoginsRolePocos)
        {
            _logic.Delete(securityLoginsRolePocos);
            return Ok();
        }

    }
}