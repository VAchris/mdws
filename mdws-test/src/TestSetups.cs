using System;
using System.Collections.Generic;
using System.Text;
using Spring.Context;
using Spring.Context.Support;
using gov.va.medora.mdo;

namespace gov.va.medora.mdws.test
{
    public class TestSetups
    {
        internal static User getTestUser(string userTag)
        {
            IApplicationContext ctx = ContextRegistry.GetContext();
            StringTestObject testObj = (StringTestObject)ctx.GetObject(userTag);
            User user = new User();
            user.Uid = testObj.get("userDUZ");
            user.Name = new PersonName(testObj.get("userName"));
            user.SSN = new SocSecNum(testObj.get("userSSN"));
            user.LogonSiteId = new SiteId(testObj.get("siteCode"), testObj.get("siteName"));
            user.UserName = testObj.get("accessCode");
            user.Pwd = testObj.get("verifyCode");
            user.PermissionString = testObj.get("context");
            return user;
        }

    }
}
