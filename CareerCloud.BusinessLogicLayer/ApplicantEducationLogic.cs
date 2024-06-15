using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CareerCloud.BusinessLogicLayer
{
	public class ApplicantEducationLogic : BaseLogic<ApplicantEducationPoco>
	{
		public ApplicantEducationLogic(IDataRepository<ApplicantEducationPoco> repository) : base(repository)
		{
		}
		public override void Add(ApplicantEducationPoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);

		}
		public override void Update(ApplicantEducationPoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}

		protected override void Verify(ApplicantEducationPoco[] pocos)
		{
			List<ValidationException> ListofExceptions = new List<ValidationException>();
			foreach(var poco in pocos)
			{
				if(string.IsNullOrWhiteSpace(poco.Major) || poco.Major.Length<3)
				{
					
					ListofExceptions.Add(new ValidationException(107, "Major cannot be empty or less than 3 characters"));
				}
				if (poco.StartDate.HasValue && poco.StartDate.Value > DateTime.Today)
				{
					
					ListofExceptions.Add(new ValidationException(108, "StartDate cannot be greater than today"));
				}
				if(poco.StartDate.HasValue && poco.CompletionDate.HasValue && poco.CompletionDate < poco.StartDate)
				{
					ListofExceptions.Add(new ValidationException(109, "CompletionDate cannot be earlier than StartDate"));
					//var exp = new ValidationException(109, "CompletionDate cannot be earlier than StartDate");
					//ListofExceptions.Add(exp);
				}
				
			}
			if (ListofExceptions.Count > 0)
			{
				throw new AggregateException(ListofExceptions);
			}
		}
	}
}
