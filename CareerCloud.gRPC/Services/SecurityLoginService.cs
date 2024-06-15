    using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLogin.SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logicLayer;

        public SecurityLoginService()
        {
            var repo = new EFGenericRepository<SecurityLoginPoco>();
            _logicLayer = new SecurityLoginLogic(repo);
        }

        public override Task<SecurityLoginReply> GetSecurityLogin(SecurityLoginId request, ServerCallContext context)
        {
            SecurityLoginPoco poco = _logicLayer.Get(Guid.Parse(request.Id));
            return base.GetSecurityLogin(request, context);
        }

        private SecurityLoginReply FromPoco(SecurityLoginPoco poco)
        {
            return new SecurityLoginReply()
            {
                Id = poco.Id.ToString(),
                Created = Timestamp.FromDateTime(poco.Created.ToUniversalTime()), // Convert DateTime to Timestamp
                PasswordUpdate = poco.PasswordUpdate.HasValue ? Timestamp.FromDateTime(poco.PasswordUpdate.Value.ToUniversalTime()) : null,
                AgreementAccepted = poco.AgreementAccepted.HasValue ? Timestamp.FromDateTime(poco.AgreementAccepted.Value.ToUniversalTime()) : null,
                IsLocked = poco.IsLocked,
                IsInactive = poco.IsInactive,
                EmailAddress = poco.EmailAddress,
                PhoneNumber = poco.PhoneNumber,
                FullName = poco.FullName,
                ForceChangePassword = poco.ForceChangePassword,
                PrefferredLanguage = poco.PrefferredLanguage,
                Login = poco.Login,
                Password = poco.Password
            };
        }

        private SecurityLoginPoco ToPoco(SecurityLoginReply reply)
        {
            return new SecurityLoginPoco()
            {
                Id = Guid.Parse(reply.Id),
                Created = reply.Created.ToDateTime(), // Convert Timestamp to DateTime
                PasswordUpdate = reply.PasswordUpdate != null ? (DateTime?)reply.PasswordUpdate.ToDateTime() : null,
                AgreementAccepted = reply.AgreementAccepted != null ? (DateTime?)reply.AgreementAccepted.ToDateTime() : null,
                IsLocked = reply.IsLocked,
                IsInactive = reply.IsInactive,
                EmailAddress = reply.EmailAddress,
                PhoneNumber = reply.PhoneNumber,
                FullName = reply.FullName,
                ForceChangePassword = reply.ForceChangePassword,
                PrefferredLanguage = reply.PrefferredLanguage,
                Login = reply.Login,
                Password = reply.Password
            };
        }
    }
}
