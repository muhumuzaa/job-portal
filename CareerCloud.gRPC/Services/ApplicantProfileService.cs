using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService:ApplicantProfile.ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logicLayer;

        public ApplicantProfileService()
        {
            var repo = new EFGenericRepository<ApplicantProfilePoco>();
            _logicLayer = new ApplicantProfileLogic(repo);
        }

        public override Task<ApplicantProfileReply> GetApplicantProfile(ApplicantProfileId request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return GetApplicantProfile(request, context);
        }

        private ApplicantProfileReply FromPoco(ApplicantProfilePoco poco)
        {
            return new ApplicantProfileReply()
            {
                Id = poco.Id.ToString(),
                CurrentSalary = poco.CurrentSalary.HasValue ? poco.CurrentSalary.Value.ToString() : null,
                CurrentRate = poco.CurrentRate.HasValue ? poco.CurrentRate.Value.ToString() : null,
                Country = poco.Country,
                Province = poco.Province,
                Street = poco.Street,
                City = poco.City,
                PostalCode = poco.PostalCode,
                Login = poco.Login.ToString(),
                Currency = poco.Currency,
            };
        }

        private ApplicantProfilePoco ToPoco(ApplicantProfileReply reply)
        {
            return new ApplicantProfilePoco()
            {
                Id = Guid.Parse(reply.Id),
                CurrentSalary = string.IsNullOrEmpty(reply.CurrentSalary) ? (decimal?)null : decimal.Parse(reply.CurrentSalary),
                CurrentRate = string.IsNullOrEmpty(reply.CurrentRate) ? (decimal?)null : decimal.Parse(reply.CurrentRate),
                Country = reply.Country,
                Province = reply.Province,
                Street = reply.Street,
                City = reply.City,
                PostalCode = reply.PostalCode,
                Login = Guid.Parse(reply.Login),
                Currency = reply.Currency,
            };

        }
    }
}
