using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class SystemCountryCodeLogic : BaseLogic<SystemCountryCodePoco>
	{
		public SystemCountryCodeLogic(IDataRepository<SystemCountryCodePoco> repository) : base(repository)
		{
		}
		public override void Add(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}

        public object Get(Func<object, bool> value)
        {
            throw new NotImplementedException();
        }

        public override void Update(SystemCountryCodePoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}

        public SystemCountryCodePoco Get(string code)
        {
			if(code == null)
			{
				throw new ArgumentNullException("id");
			}
			return base._repository.GetSingle(value => value.Code == code);
        }



        protected override void Verify(SystemCountryCodePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (var poco in pocos)
			{
				// Rule 900: Code Cannot be empty
				if (string.IsNullOrEmpty(poco.Code))
				{
					exceptions.Add(new ValidationException(900, "Code Cannot be empty"));
				}
				// Rule 901: Name Cannot be empty
				if (string.IsNullOrEmpty(poco.Name))
				{
					exceptions.Add(new ValidationException(901, "Name Cannot be empty"));
				}
			}

			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
	}
}
