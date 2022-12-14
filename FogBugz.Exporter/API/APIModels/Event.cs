using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FogBugz.Exporter.API.APIModels
{
    public class Event : ExportModels.Event
    {
        [JsonProperty("ixBug")]
        public override string CaseNumber { get; set; }

        [JsonProperty("ixBugEvent")]
        public override int Id { get; set; }

        [JsonProperty("evt")]
        public override int EventCodeValue { get; set; }

        [JsonProperty("sVerb")]
        public override string DescriptionEnglish { get; set; }

        [JsonProperty("sPerson")]
        public override string Person { get; set; }

        [JsonProperty("dt")]
        public override DateTime Date { get; set; }

        [JsonProperty("s")]
        public override string ContentPlain { get; set; }

        [JsonProperty("sHTML")]
        public override string ContentHTML { get; set; }

        [JsonProperty("fEmail")]
        public override bool IsEMail { get; set; }

        [JsonProperty("fExternal")]
        public override bool IsExternal { get; set; }

        [JsonProperty("sChanges")]
        public override string Changes { get; set; }

        [JsonProperty("evtDescription")]
        public override string DescriptionGerman { get; set; }

        [JsonProperty("rgAttachments")]
        public new List<Attachment> Attachments { get; set; }

        [JsonProperty("sFrom")]
        public override string From { get; set; }

        [JsonProperty("sTo")]
        public override string To { get; set; }

        [JsonProperty("sCC")]
        public override string CC { get; set; }

        [JsonProperty("sBCC")]
        public override string BCC { get; set; }

        [JsonProperty("sReplyTo")]
        public override string ReplyTo { get; set; }

        [JsonProperty("sSubject")]
        public override string Subject { get; set; }

        [JsonProperty("sDate")]
        public override string MailDate { get; set; }

        [JsonProperty("sBodyText")]
        public override string BodyPlain { get; set; }

        [JsonProperty("sBodyHTML")]
        public override string BodyHTML { get; set; }
    }
}