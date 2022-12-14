using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugz.Exporter.API
{
    public static class Helper
    {
        public static List<string> CaseListExportColumns = new()
        {
            "ixBug", // Case-Nr.
            "ixBugParent", // Parent Case-Nr.
            "ixBugChildren", // Sub Case-Nrs.
            "tags", // A list of Tags
            "fOpen", // True if the case is open; false if it is closed
            "sTitle", // Title
            "sOriginalTitle", // Original Title (The title the case had, when it was created)
            "sProject", // The name of the Project it was assigned to
            "sPersonAssignedTo", // Name of the person assigned to the case
            "sEmailAssignedTo", // Email address of the person assigned to the case
            "sStatus", // The name of the status
            "ixBugDuplicates", // Cases that are closed as duplicates of this one
            "ixBugOriginal", // The case that this case was a duplicate of
            "sPriority", // The name of the Priority
            "sFixFor", // Name of the milestone this case is assigned to
            "hrsOrigEst", // Hours of original estimate (0 if no estimate)
            "hrsCurrEst", // Hours of current estimate
            "hrsElapsed", // Total elapsed hours — includes all the time from time intervals PLUS hrsElapsedExtra time
            "sCustomerEmail", // If there is a customer correspondent for this case, this is their email
            "ixMailbox", // If this case came in via dispatch, the mailbox to which it was delivered
            "sCategory", // The name of the Category
            "dtOpened", // Date opened
            "dtResolved", // Date resolved
            "dtClosed", // Date closed
            "dblStoryPts", // Story points set for this case
            "events", // All of the events for a case
            "sKanbanColumn" // The name of the Kanban column that the case is currently assigned to. See listKanbanColumns.
        };

        public static void CopyValues<T>(T source, T target)
        {
            var type = target.GetType();

            var properties = type.GetProperties();

            for (int i = 0; i < properties.Length; i++)
            {
                var property = properties[i];

                if (!property.PropertyType.IsAssignableTo(typeof(ICollection)) && !property.PropertyType.IsEnum)
                {
                    var value = property.GetValue(source);

                    property.SetValue(target, value);
                }
            }
        }
    }
}