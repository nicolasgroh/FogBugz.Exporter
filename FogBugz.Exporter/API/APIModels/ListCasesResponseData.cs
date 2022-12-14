using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugz.Exporter.API.APIModels
{
    public class ListCasesResponseData
    {
        public int Count { get; set; }

        [Newtonsoft.Json.JsonProperty("totalHits")]
        public int Total { get; set; }

        public List<Case> Cases { get; set; }
    }
}