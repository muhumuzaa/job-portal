    using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : ApplicantEducation.ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic _logicLayer;

        public ApplicantEducationService()
        {
            var repo = new EFGenericRepository<ApplicantEducationPoco>();
            _logicLayer = new ApplicantEducationLogic(repo);
        }

        public override Task<ApplicantEducationReply> GetApplicantEducation(ApplicantEducationId request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetApplicantEducation(request, context);
        }

        private ApplicantEducationReply FromPoco(ApplicantEducationPoco poco)
        {
            return new ApplicantEducationReply()
            {
               
                Id = poco.Id.ToString(),
                Applicant = poco.Applicant.ToString(),
                CertificateDiploma = poco.CertificateDiploma,
                Major = poco.Major,
                StartDate = poco.StartDate == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.StartDate, DateTimeKind.Utc)),
                CompletionDate = poco.CompletionDate == null ? null : Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.CompletionDate, DateTimeKind.Utc)),
                CompletionPercent = poco.CompletionPercent == null ? 0 : (byte)poco.CompletionPercent,
               // Timestamp = ByteString.CopyFrom(poco.TimeStamp)
            };
        }

        private ApplicantEducationPoco ToPoco(ApplicantEducationReply reply)
        {
            return new ApplicantEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Applicant = Guid.Parse(reply.Applicant),
                CertificateDiploma = reply.CertificateDiploma,
                Major = reply.Major,
                StartDate = reply.StartDate.ToDateTime(),
                CompletionDate = reply.CompletionDate.ToDateTime(),
                CompletionPercent = (byte?)reply.CompletionPercent,
            };
        }
    }
}
