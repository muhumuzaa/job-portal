    using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Faker;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCode.SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logicLayer;

        public SystemLanguageCodeService()
        {
            var repo = new EFGenericRepository<SystemLanguageCodePoco>();
            _logicLayer = new SystemLanguageCodeLogic(repo);
        }

        public override Task<SystemLanguageCodeReply> GetSystemLanguageCode(SystemLanguageCodeId request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetSystemLanguageCode(request, context);
        }

        private SystemLanguageCodeReply FromPoco(SystemLanguageCodePoco poco)
        {
            return new SystemLanguageCodeReply()
            {
               
                Id = poco.Id.ToString(),
                LanguageID = poco.LanguageID,
                NativeName = poco.NativeName,
                Name = poco.Name,

            };
        }

        private SystemLanguageCodePoco ToPoco(SystemLanguageCodeReply reply)
        {
            return new SystemLanguageCodePoco()
            {
                Id = Guid.Parse(reply.Id),
                LanguageID = reply.LanguageID,
                NativeName = reply.NativeName,
                Name = reply.Name,
            };
        }
    }
}
