    using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Identity.Client;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplication.ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _logicLayer;

        public ApplicantJobApplicationService()
        {
            var repo = new EFGenericRepository<ApplicantJobApplicationPoco>();
            _logicLayer = new ApplicantJobApplicationLogic(repo);
        }

        public override Task<ApplicantJobApplicationReply> GetApplicantJobApplication(ApplicantJobApplicationId request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetApplicantJobApplication(request, context);
        }

        private ApplicantJobApplicationReply FromPoco(ApplicantJobApplicationPoco poco)
        {
            return new ApplicantJobApplicationReply()
            {

                Id = poco.Id.ToString(),
                ApplicationDate = poco.ApplicationDate == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.ApplicationDate, DateTimeKind.Utc)),
                Applicant = poco.Applicant.ToString(),
                Job = poco.Job.ToString(),
               
            };
        }

        private ApplicantJobApplicationPoco ToPoco(ApplicantJobApplicationReply reply)
        {
            return new ApplicantJobApplicationPoco()
            {
                Id = Guid.Parse(reply.Id),
                ApplicationDate = reply.ApplicationDate.ToDateTime(),
                Applicant = Guid.Parse(reply.Applicant),
                Job = Guid.Parse(reply.Job),
            };
        }
    }
}
