using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FogBugz.Exporter.API.APIModels
{
    public class Case : ExportModels.Case
    {
        [JsonProperty("ixBug")]
        public override int Number { get; set; }

        [JsonProperty("ixBugParent")]
        public override int? Parent { get; set; }

        [JsonProperty("ixBugChildren")]
        public override List<int> Children { get; set; }

        [JsonProperty("fOpen")]
        public override bool IsOpen { get; set; }

        [JsonProperty("sTitle")]
        public override string Title { get; set; }

        [JsonProperty("sOriginalTitle")]
        public override string OriginalTitle { get; set; }

        [JsonProperty("sProject")]
        public override string Project { get; set; }

        [JsonProperty("sPersonAssignedTo")]
        public override string AssnignedTo { get; set; }

        [JsonProperty("sEmailAssignedTo")]
        public override string AssignedToMail { get; set; }

        [JsonProperty("sStatus")]
        public override string Status { get; set; }

        [JsonProperty("ixBugDuplicates")]
        public override int? Duplicate { get; set; }

        [JsonProperty("ixBugOriginal")]
        public override int? Original { get; set; }

        [JsonProperty("sPriority")]
        public override string Priority { get; set; }

        [JsonProperty("sFixFor")]
        public override string Milestone { get; set; }

        [JsonProperty("hrsOrigEst")]
        public override decimal OriginalEstimate { get; set; }

        [JsonProperty("hrsCurrEst")]
        public override decimal Estimate { get; set; }

        [JsonProperty("hrsElapsed")]
        public override decimal Elapsed { get; set; }

        [JsonProperty("sCustomerEmail")]
        public override string CustomerMail { get; set; }

        [JsonProperty("ixMailbox")]
        public override string Mailbox { get; set; }

        [JsonProperty("sCategory")]
        public override string Category { get; set; }

        [JsonProperty("dtOpened")]
        public override DateTime Opened { get; set; }

        [JsonProperty("dtResolved")]
        public override DateTime? Resolved { get; set; }

        [JsonProperty("dtClosed")]
        public override DateTime? Closed { get; set; }

        [JsonProperty("dblStoryPts")]
        public override decimal? StoryPoints { get; set; }

        [JsonProperty("sKanbanColumn")]
        public override string KanbanColumn { get; set; }

        public new List<Event> Events { get; set; }

        public ExportModels.Case ToExportCase()
        {
            var exportCase = new ExportModels.Case();

            Helper.CopyValues(this, exportCase);

            if (Events != null && Events.Count > 0)
            {
                exportCase.Events = new List<ExportModels.Event>();

                for (int i = 0; i < Events.Count; i++)
                {
                    var apiEvent = Events[i];

                    var exportEvent = new ExportModels.Event();

                    Helper.CopyValues(apiEvent, exportEvent);

                    if (apiEvent.Attachments != null && apiEvent.Attachments.Count > 0)
                    {
                        exportEvent.Attachments = new List<ExportModels.Attachment>();

                        for (int j = 0; j < apiEvent.Attachments.Count; j++)
                        {
                            var attachment = apiEvent.Attachments[j];

                            var exportAttachment = new ExportModels.Attachment();

                            Helper.CopyValues(attachment, exportAttachment);

                            exportEvent.Attachments.Add(exportAttachment);
                        }
                    }

                    exportCase.Events.Add(exportEvent);
                }
            }

            return exportCase;
        }
    }
}