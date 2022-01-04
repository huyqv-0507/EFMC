using System;
namespace EFMC.Service.Common.Constants
{
    public class JwtClaimConstant
    {
        public static readonly string USER_ID = "UserId";
        public static readonly string USER_NAME = "UserName";
    }
    public class JwtConfConstant
    {
        public static readonly string KEY = "Jwt:Key";
        public static readonly string EXPIRE_DAYS = "Jwt:ExpireDays";
        public static readonly string ISSUER = "Jwt:Issuer";
        public static readonly string AUDIENCE = "Jwt:Audience";
    }
}
