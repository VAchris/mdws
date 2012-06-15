
#region Imports
using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

// Spring Imports
using AopAlliance.Intercept;
using Spring.Core;
using Spring.Aop;
using Common.Logging;

using Spring.Context;
using Spring.Context.Support;
#endregion

using gov.va.medora;
using gov.va.medora.TOReflection;

namespace gov.va.medora.mdws
{
    /// <summary>
    /// Summary description for AOPLoggers
    /// </summary>
    public class AOPLoggers
    {
        public AOPLoggers()
        {
            //
            // TODO: Add constructor logic here
            //
        }
    }

    public class BaseServiceLogger : IMethodInterceptor
    {
        private static readonly ILog LOG = LogManager.GetLogger(typeof(BaseServiceLogger));
        // private static readonly ILog LOG = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public object Invoke(IMethodInvocation invocation)
        {
            System.Console.WriteLine("I'm in the AOP Invoke");
            LOG.Debug(String.Format("BaseServiceLogger intercepted call : about to invoke method '{0}'", invocation.Method.Name));
            object retVal = invocation.Proceed();
            string retString = new TORenderer(retVal).ToString();
            LOG.Debug(String.Format("BaseServiceLogger intercepted call : returned '{0}'", retString));
            return retVal;
        }
    }

    public class MockServiceLogger : IMethodInterceptor
    {
        private static readonly ILog LOG = LogManager.GetLogger(typeof(MockServiceLogger));
        // private static readonly ILog LOG = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public object Invoke(IMethodInvocation invocation)
        {
            LOG.Debug(String.Format("Intercepted call : about to invoke method '{0}'", invocation.Method.Name));
            object retVal = invocation.Proceed();
            LOG.Debug(String.Format("Intercepted call : returned '{0}'", retVal));
            return retVal;
        }
    }

    // Van's test logger lifted from Spring.NET's examples
    #region License

/*
 * Copyright © 2002-2006 the original author or authors.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */

    #endregion
    /// <summary>
    /// Simple implementation of the <see cref="AopAlliance.Intercept.IMethodInterceptor"/> interface 
    /// for a logging aspect using <see cref="System.Console"/>.
    /// </summary>
    /// <author>Rick Evans</author>
    /// <version>$Id: ConsoleLoggingAroundAdvice.cs,v 1.2 2006/12/03 23:56:17 bbaia Exp $</version>
    public class ConsoleLoggingAroundAdvice : IMethodInterceptor
    {
        private static readonly ILog LOG = LogManager.GetLogger(
    System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public object Invoke(IMethodInvocation invocation)
        {
            LOG.Debug(String.Format(
                "AOP Intercepted call : about to invoke method '{0}'"
                , invocation.Method.Name));
            object returnValue = invocation.Proceed();
            //string retString = new TORenderer(returnValue).ToString();
            //LOG.Debug(String.Format(
            //    "AOP Intercepted call : returned '{0}'", retString));
            return returnValue;
        }
    }
}
