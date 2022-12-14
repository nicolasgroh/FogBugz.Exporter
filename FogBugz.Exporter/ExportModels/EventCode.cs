using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FogBugz.Exporter.ExportModels
{
    public enum EventCode
    {
        Opened = 1,
        Edited = 2,
        Assigned = 3,
        Reactivated = 4,
        Reopened = 5,
        Closed = 6,
        Moved = 7, // ‘ 2.0 or earlier. From 3.0 on this was recorded as an Edit
        Unknown = 8, // ‘ Not quite sure what happened; display sVerb in the UI
        Replied = 9,
        Forwarded = 10,
        Received = 11,
        Sorted = 12,
        NotSorted = 13,
        Resolved = 14,
        Emailed = 15,
        ReleaseNoted = 16,
        DeletedAttachment = 17
    }
}