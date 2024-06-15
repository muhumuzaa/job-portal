    using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLog.SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logicLayer;

        public SecurityLoginsLogService()
        {
            var repo = new EFGenericRepository<SecurityLoginsLogPoco>();
            _logicLayer = new SecurityLoginsLogLogic(repo);
        }

        public override Task<SecurityLoginsLogReply> GetSecurityLoginsLog(SecurityLoginsLogId request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetSecurityLoginsLog(request, context);
        }

        private SecurityLoginsLogReply FromPoco(SecurityLoginsLogPoco poco)
        {
            return new SecurityLoginsLogReply()
            {
                Id = poco.Id.ToString(),
                SourceIP = poco.SourceIP,
                LogonDate = Timestamp.FromDateTime(poco.LogonDate.ToUniversalTime()), // Convert DateTime to Timestamp
                IsSuccesful = poco.IsSuccesful,
                Login = poco.Login.ToString()
            };
        }

        private SecurityLoginsLogPoco ToPoco(SecurityLoginsLogReply reply)
        {
            return new SecurityLoginsLogPoco()
            {
                Id = Guid.Parse(reply.Id),
                SourceIP = reply.SourceIP,
                LogonDate = reply.LogonDate.ToDateTime(), // Convert Timestamp to DateTime
                IsSuccesful = reply.IsSuccesful,
                Login = Guid.Parse(reply.Login)
            };
        }
    }
}
