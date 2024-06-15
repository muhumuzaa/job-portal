using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace CareerCloud.BusinessLogicLayer
{
	public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
	{
		public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository) : base(repository)
		{
		}
		public override void Add(CompanyProfilePoco[] pocos)
		{
			Verify(pocos);
			base.Add(pocos);
		}
		 
		public override void Update(CompanyProfilePoco[] pocos)
		{
			Verify(pocos);
			base.Update(pocos);
		}

		protected override void Verify(CompanyProfilePoco[] pocos)
		{
			List<ValidationException> exceptions = new List<ValidationException>();

			foreach (var poco in pocos)
			{
				// Rule 600: CompanyWebsite must end with valid extensions
				if (!IsValidWebsite(poco.CompanyWebsite))
				{
					exceptions.Add(new ValidationException(600, "CompanyWebsite must end with a valid extension."));
				}

				// Rule 601: ContactPhone must correspond to a valid phone number pattern
				if (!IsValidPhoneNumber(poco.ContactPhone))
				{
					exceptions.Add(new ValidationException(601, "ContactPhone must correspond to a valid phone number pattern."));
				}
			}

			if (exceptions.Count > 0)
			{
				throw new AggregateException(exceptions);
			}
		}

		private bool IsValidWebsite(string? website)
		{
			if (string.IsNullOrWhiteSpace(website))
			{
				return false;
			}

			string[] validExtensions = { ".ca", ".com", ".biz" };
			foreach (string extension in validExtensions)
			{
				if (website.EndsWith(extension, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}

			return false;
		}

		private bool IsValidPhoneNumber(string? phoneNumber)
		{
			if (string.IsNullOrWhiteSpace(phoneNumber))
			{
				return false;
			}

			// Phone number pattern validation using regex
			string pattern = @"^\d{3}-\d{3}-\d{4}$";
			return Regex.IsMatch(phoneNumber, pattern);
		}
	}
}