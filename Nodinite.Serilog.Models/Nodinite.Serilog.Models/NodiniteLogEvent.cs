﻿using Serilog.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Nodinite.Serilog.ApiSink.models
{
    public class NodiniteLogEvent
    {
        public NodiniteLogEvent()
        {

        }

        public NodiniteLogEvent(int logAgentValueId)
        {
            LogAgentValueId = logAgentValueId;
        }

        public NodiniteLogEvent(string message, LogEvent logEvent, NodiniteLogEventSettings settings)
        {
            LogAgentValueId = settings.LogAgentValueId.Value;
            EndPointName = settings.EndPointName;
            EndPointUri = settings.EndPointUri;
            EndPointDirection = settings.EndPointDirection.HasValue ? settings.EndPointDirection.Value : 0;
            EndPointTypeId = settings.EndPointTypeId.HasValue ? settings.EndPointTypeId.Value : 0;
            OriginalMessageTypeName = string.IsNullOrWhiteSpace(settings.OriginalMessageTypeName) ? "Nodinite.Serilog.LogEvent" : settings.OriginalMessageTypeName;
            LogDateTime = DateTimeOffset.UtcNow;
            ProcessingUser = settings.ProcessingUser;
            SequenceNo = 0;
            EventNumber = 0;
            ApplicationInterchangeId = Guid.NewGuid().ToString();
            LocalInterchangeId = Guid.NewGuid();
            ProcessName = settings.ProcessName;
            ProcessingMachineName = settings.ProcessingMachineName;
            ProcessingModuleName = settings.ProcessingModuleName;
            ProcessingModuleType = settings.ProcessingModuleType;

            Context = new System.Collections.Generic.Dictionary<string, string>();
            foreach (var property in logEvent.Properties)
            {
                Context.Add(property.Key, property.Value.ToString().Replace("\"", ""));
            }

            ProcessingTime = 0;
            LogText = message;

            switch (logEvent.Level)
            {
                case LogEventLevel.Error:
                    LogStatus = -1;
                    break;
                case LogEventLevel.Fatal:
                    LogStatus = -2;
                    break;
                case LogEventLevel.Warning:
                    LogStatus = 1;
                    break;
                case LogEventLevel.Debug:
                case LogEventLevel.Information:
                case LogEventLevel.Verbose:
                default:
                    LogStatus = 0;
                    break;
            }
        }

        public int LogAgentValueId { get; set; }
        public string EndPointName { get; set; }
        public string EndPointUri { get; set; }
        public int EndPointDirection { get; set; }
        public int EndPointTypeId { get; set; }
        public string OriginalMessageTypeName { get; set; }
        public DateTimeOffset LogDateTime { get; set; }
        public string ProcessingUser { get; set; }
        public int SequenceNo { get; set; }
        public int EventNumber { get; set; }
        public string LogText { get; set; }
        public string ApplicationInterchangeId { get; set; }
        public Guid LocalInterchangeId { get; set; }
        public int LogStatus { get; set; }
        public string ProcessName { get; set; }
        public string ProcessingMachineName { get; set; }
        public string ProcessingModuleName { get; set; }
        public string ProcessingModuleType { get; set; }
        public Guid ServiceInstanceActivityId { get; set; }
        public int ProcessingTime { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Context { get; set; }
    }
}
