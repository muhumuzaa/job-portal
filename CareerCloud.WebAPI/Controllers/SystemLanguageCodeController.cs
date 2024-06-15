using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.Pocos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;

namespace CareerCloud.WebAPI.Controllers
{
    [Route("api/careercloud/[controller]/v1")]
    [ApiController]
    public class SystemLanguageCodeController : ControllerBase
    {
        private readonly SystemLanguageCodeLogic _context;

        public SystemLanguageCodeController()
        {
            _context = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());

        }

        [HttpGet]
        public ActionResult GetAllSystemLanguageCode()
        {
            return Ok(_context.GetAll());
        }

        [HttpGet]
        [Route("languagecode/{id}")]
        public ActionResult GetSystemLanguageCode(string id)
        {
            SystemLanguageCodePoco poco = _context.Get(id);


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
        public ActionResult PostSystemLanguageCode(SystemLanguageCodePoco[] systemLanguageCodePocos)
        {
            _context.Add(systemLanguageCodePocos);
            return Ok();
        }

        [HttpPut]
        [Route("languagecode")]
        public ActionResult PutSystemLanguageCode(SystemLanguageCodePoco[] systemLanguageCodePocos)
        {
            _context.Update(systemLanguageCodePocos);
            return Ok();
        }

        [HttpDelete]
        [Route("languagecode")]
        public ActionResult DeleteSystemLanguageCode(SystemLanguageCodePoco[] systemLanguageCodePocos)
        {
            _context.Delete(systemLanguageCodePocos);
            return Ok();
        }
    }
}
