using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugz.Exporter.ExportModels
{
    public class Case
    {
        public virtual int Number { get; set; }

        public virtual int? Parent { get; set; }

        public virtual List<int> Children { get; set; }

        public virtual int? Duplicate { get; set; }

        public virtual int? Original { get; set; }

        public virtual List<string> Tags { get; set; }

        public virtual string Title { get; set; }

        public virtual string OriginalTitle { get; set; }

        public virtual bool IsOpen { get; set; }

        public virtual string Project { get; set; }

        public virtual string Category { get; set; }

        public virtual string Status { get; set; }

        public virtual string Priority { get; set; }

        public virtual string KanbanColumn { get; set; }

        public virtual string AssnignedTo { get; set; }

        public virtual string AssignedToMail { get; set; }

        public virtual DateTime Opened { get; set; }

        public virtual DateTime? Resolved { get; set; }

        public virtual DateTime? Closed { get; set; }

        public virtual string CustomerMail { get; set; }

        public virtual string Mailbox { get; set; }

        public virtual decimal Estimate { get; set; }

        public virtual decimal OriginalEstimate { get; set; }

        public virtual decimal Elapsed { get; set; }

        public virtual string Milestone { get; set; }

        public virtual decimal? StoryPoints { get; set; }

        public virtual List<Event> Events { get; set; }
    }
}