using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescription.CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logicLayer;

        public CompanyDescriptionService()
        {
            var repo = new EFGenericRepository<CompanyDescriptionPoco>();
            _logicLayer = new CompanyDescriptionLogic(repo);
        }

        public override Task<CompanyDescriptionReply> GetCompanyDescription(CompanyDescriptionId request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetCompanyDescription(request, context);
        }

        private CompanyDescriptionReply FromPoco(CompanyDescriptionPoco poco)
        {
            return new CompanyDescriptionReply()
            {
               
                Id = poco.Id.ToString(),
                CompanyName = poco.CompanyName,
                CompanyDescription = poco.CompanyDescription,
                Company = poco.Company.ToString(),
                LanguageId = poco.LanguageId,
                

            };
        }

        private CompanyDescriptionPoco ToPoco(CompanyDescriptionReply reply)
        {
            return new CompanyDescriptionPoco()
            {
                Id = Guid.Parse(reply.Id),
                CompanyName = reply.CompanyName,
                CompanyDescription = reply.CompanyDescription,
                Company = Guid.Parse(reply.Company),
                LanguageId = reply.LanguageId,
            };
        }
    }
}
