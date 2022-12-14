using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugz.Exporter.ExportModels
{
    public class Attachment
    {
        public virtual string Filename { get; set; }

        public virtual string URL { get; set; }
    }
}