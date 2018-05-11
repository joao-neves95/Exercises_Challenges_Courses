using OwaspHeaders.Core.Enums;
using OwaspHeaders.Core.Extensions;
using OwaspHeaders.Core.Models;

namespace WebServer.Services
{
    public class Security
    {
        public static SecureHeadersMiddlewareConfiguration SecureHeadersConfiguration()
        {
            return SecureHeadersMiddlewareBuilder
                .CreateBuilder()
                .UseHsts()
                .UseXFrameOptions(XFrameOptions.Deny)
                .UseXSSProtection(XssMode.oneBlock)
                .UseContentTypeOptions()
                .UsePermittedCrossDomainPolicies(XPermittedCrossDomainOptionValue.masterOnly)
                .UseReferrerPolicy(ReferrerPolicyOptions.sameOrigin)
                .Build();
        }
    }
}
