    using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducation.CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logicLayer;

        public CompanyJobEducationService()
        {
            var repo = new EFGenericRepository<CompanyJobEducationPoco>();
            _logicLayer = new CompanyJobEducationLogic(repo);
        }

        public override Task<CompanyJobEducationReply> GetCompanyJobEducation(CompanyJobEducationId request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetCompanyJobEducation(request, context);
        }

        private CompanyJobEducationReply FromPoco(CompanyJobEducationPoco poco)
        {
            return new CompanyJobEducationReply()
            {
               
                Id = poco.Id.ToString(),
                Job = poco.Job.ToString(),
                Major = poco.Major,
                Importance = poco.Importance.ToString(),
                
            };
        }

        private CompanyJobEducationPoco ToPoco(CompanyJobEducationReply reply)
        {
            return new CompanyJobEducationPoco()
            {
                Id = Guid.Parse(reply.Id),
                Job = Guid.Parse(reply.Job),
              
                Major = reply.Major,
                Importance = Int16.Parse(reply.Importance.ToString()),
            };
        }
    }
}
