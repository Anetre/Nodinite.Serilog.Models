using System;
using System.Collections.Generic;
using System.Text;

namespace Nodinite.Serilog.Models
{
    public class NodiniteLogEventSettings
    {
        public int? LogAgentValueId { get; set; }
        public string EndPointName { get; set; }
        public string EndPointUri { get; set; }
        public int? EndPointDirection { get; set; }
        public int? EndPointTypeId { get; set; }
        public string OriginalMessageTypeName { get; set; }
        public string ProcessingUser { get; set; }
        public string ProcessName { get; set; }
        public string ProcessingMachineName { get; set; }
        public string ProcessingModuleName { get; set; }
        public string ProcessingModuleType { get; set; }
    }
}
