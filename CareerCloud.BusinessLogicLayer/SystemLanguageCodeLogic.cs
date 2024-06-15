using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class SystemLanguageCodeLogic : BaseLogic<SystemLanguageCodePoco>
	{
		public SystemLanguageCodeLogic(IDataRepository<SystemLanguageCodePoco> repository) : base(repository)
		{
		}
		public override void Add(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}

		public override void Update(SystemLanguageCodePoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}

		public SystemLanguageCodePoco Get(string code)
		{
			if(code == null)
			{
				throw new ArgumentNullException("code");
			}
			return base._repository.GetSingle(value => value.LanguageID == code);
		}

		protected override void Verify(SystemLanguageCodePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (var poco in pocos)
			{
				// Rule 1000: LanguageID Cannot be empty
				if (string.IsNullOrEmpty(poco.LanguageID))
				{
					exceptions.Add(new ValidationException(1000, "LanguageID Cannot be empty"));
				}
				// Rule 1001: Name Cannot be empty
				if (string.IsNullOrEmpty(poco.Name))
				{
					exceptions.Add(new ValidationException(1001, "Name Cannot be empty"));
				}
				// Rule 1002: NativeName Cannot be empty
				if (string.IsNullOrEmpty(poco.NativeName))
				{
					exceptions.Add(new ValidationException(1002, "NativeName Cannot be empty"));
				}
			}

			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}
	}
}
