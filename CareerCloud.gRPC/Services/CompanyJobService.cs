    using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJob.CompanyJobBase
    {
        private readonly CompanyJobLogic _logicLayer;

        public CompanyJobService()
        {
            var repo = new EFGenericRepository<CompanyJobPoco>();
            _logicLayer = new CompanyJobLogic(repo);
        }

        public override Task<CompanyJobReply> GetCompanyJob(CompanyJobId request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetCompanyJob(request, context);
        }

        private CompanyJobReply FromPoco(CompanyJobPoco poco)
        {
            return new CompanyJobReply()
            {
               
                Id = poco.Id.ToString(),
                ProfileCreated = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)poco.ProfileCreated, DateTimeKind.Utc)),
                IsInactive = poco.IsInactive,
                IsCompanyHidden = poco.IsCompanyHidden,
                Company = poco.Company.ToString(),
            };
        }

        private CompanyJobPoco ToPoco(CompanyJobReply reply)
        {
            return new CompanyJobPoco()
            {
                Id = Guid.Parse(reply.Id),
                ProfileCreated = reply.ProfileCreated.ToDateTime(),
                IsInactive = reply.IsInactive,
                IsCompanyHidden = reply.IsCompanyHidden,
                Company = Guid.Parse(reply.Company),
            };
        }
    }
}
