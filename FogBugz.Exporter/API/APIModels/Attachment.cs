using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FogBugz.Exporter.API.APIModels
{
    public class Attachment : ExportModels.Attachment
    {
        [JsonProperty("sFileName")]
        public override string Filename { get; set; }

        [JsonProperty("sURL")]
        public override string URL { get; set; }
    }
}