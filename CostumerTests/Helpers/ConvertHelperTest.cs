using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Application.Helpers;
using System.Application.Helpers.Costumers;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace BuySytemTests.Helpers
{
    [TestClass]
    public class ConvertHelperTest :BuySystemTests
    {
        [TestMethod]
        public void HttpConvert_Helper_SuccessTrue()
        {
            _defaultResponse.success = true;

            var response =  _defaultResponse.Convert();

           int StatusCode = (int)response.GetType().GetProperty("StatusCode").GetValue(response);

            Assert.AreEqual(StatusCode,200);
        }

        [TestMethod]
        public void HttpConvert_Helper_SuccessFalse()
        {
            _defaultResponse.success = false;

            var response = _defaultResponse.Convert();

            int StatusCode = (int)response.GetType().GetProperty("StatusCode").GetValue(response);

            Assert.AreEqual(StatusCode, 400);
        }
    }
}
