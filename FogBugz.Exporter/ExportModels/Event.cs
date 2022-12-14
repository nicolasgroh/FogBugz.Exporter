using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugz.Exporter.ExportModels
{
    public class Event
    {
        public virtual string CaseNumber { get; set; }

        public virtual int Id { get; set; }

        public virtual int EventCodeValue { get; set; }

        public EventCode EventCode
        {
            get { return (EventCode)EventCodeValue; }
        }

        public virtual string DescriptionEnglish { get; set; }

        public virtual string DescriptionGerman { get; set; }

        public virtual string Person { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Changes { get; set; }

        public virtual bool IsEMail { get; set; }

        public virtual bool IsExternal { get; set; }

        public virtual string From { get; set; }

        public virtual string To { get; set; }

        public virtual string CC { get; set; }

        public virtual string BCC { get; set; }

        public virtual string ReplyTo { get; set; }

        public virtual string Subject { get; set; }

        public virtual string MailDate { get; set; }

        public virtual string BodyPlain { get; set; }

        public virtual string BodyHTML { get; set; }

        public virtual string ContentPlain { get; set; }

        public virtual string ContentHTML { get; set; }

        public virtual List<Attachment> Attachments { get; set; }
    }
}