using Serilog.Events;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace Nodinite.Serilog.Models.UnitTests
{
    [TestClass]
    public class NodiniteLogEventTests
    {
        NodiniteLogEventSettings _settings;

        [TestInitialize]
        public void Init()
        {
            _settings = new NodiniteLogEventSettings();

            _settings.LogAgentValueId = 1;
            _settings.EndPointDirection = 1;
            _settings.EndPointName = "Nodinite.Serilog.Models.UnitTests";
            _settings.EndPointTypeId = 1;
            _settings.EndPointUri = "testuri";
            _settings.OriginalMessageTypeName = "TestMethod";
            _settings.ProcessingMachineName = "TestMachine";
            _settings.ProcessingModuleName = "Nodinite.Serilog.Models.UnitTests";
            _settings.ProcessingModuleType = "dotnet core test project";
            _settings.ProcessingUser = "Testuser";
            _settings.ProcessName = "Nodinite.Serilog.Models.UnitTests.Test";
        }

        [TestMethod]
        public void Set_BodyTest()
        {
            MessageTemplate messageTemplate = MessageTemplate.Empty;
            var serilogLogEvent = new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Information, null, messageTemplate, new List<LogEventProperty>());
            
            serilogLogEvent.AddOrUpdateProperty(new LogEventProperty("body", new ScalarValue("hello world")));
            
            NodiniteLogEvent nle = new NodiniteLogEvent("Hello world", serilogLogEvent, _settings);

            Assert.AreEqual(System.Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes("hello world")), nle.Body);
        }

        [TestMethod]
        public void Set_MessageTypeTest()
        {
            MessageTemplate messageTemplate = MessageTemplate.Empty;
            var serilogLogEvent = new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Information, null, messageTemplate, new List<LogEventProperty>());

            serilogLogEvent.AddOrUpdateProperty(new LogEventProperty("originalmessagetype", new ScalarValue("hello world")));

            NodiniteLogEvent nle = new NodiniteLogEvent("Hello world", serilogLogEvent, _settings);

            Assert.AreEqual("hello world", nle.OriginalMessageTypeName);
        }

        [TestMethod]
        public void Set_StatusCode_Ok()
        {
            MessageTemplate messageTemplate = MessageTemplate.Empty;
            var serilogLogEvent = new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Information, null, messageTemplate, new List<LogEventProperty>());

            serilogLogEvent.AddOrUpdateProperty(new LogEventProperty("originalmessagetype", new ScalarValue("hello world")));

            NodiniteLogEvent nle = new NodiniteLogEvent("Hello world", serilogLogEvent, _settings);

            Assert.AreEqual(0, nle.LogStatus);
        }

        [TestMethod]
        public void Set_StatusCode_Error()
        {
            MessageTemplate messageTemplate = MessageTemplate.Empty;
            var serilogLogEvent = new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Error, null, messageTemplate, new List<LogEventProperty>());

            serilogLogEvent.AddOrUpdateProperty(new LogEventProperty("originalmessagetype", new ScalarValue("hello world")));

            NodiniteLogEvent nle = new NodiniteLogEvent("Hello world", serilogLogEvent, _settings);

            Assert.AreEqual(-1, nle.LogStatus);
        }

        [TestMethod]
        public void Set_ApplicationInterchangeId()
        {
            MessageTemplate messageTemplate = MessageTemplate.Empty;
            var serilogLogEvent = new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Information, null, messageTemplate, new List<LogEventProperty>());

            serilogLogEvent.AddOrUpdateProperty(new LogEventProperty("ApplicationInterchangeId", new ScalarValue("testappid")));

            NodiniteLogEvent nle = new NodiniteLogEvent("Hello world", serilogLogEvent, _settings);

            Assert.AreEqual("testappid", nle.ApplicationInterchangeId);
        }

        [TestMethod]
        public void Override_EndpointNameTest()
        {
            MessageTemplate messageTemplate = MessageTemplate.Empty;
            var serilogLogEvent = new LogEvent(DateTimeOffset.UtcNow, LogEventLevel.Information, null, messageTemplate, new List<LogEventProperty>());

            serilogLogEvent.AddOrUpdateProperty(new LogEventProperty("endpointname", new ScalarValue("testname")));

            NodiniteLogEvent nle = new NodiniteLogEvent("Hello world", serilogLogEvent, _settings);

            Assert.AreEqual("testname", nle.EndPointName);
        }
    }
}
