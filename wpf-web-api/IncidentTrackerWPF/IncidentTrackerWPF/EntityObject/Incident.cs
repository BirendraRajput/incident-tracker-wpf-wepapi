using System;
using System.Collections.Generic;
using System.Text;

namespace IncidentTrackerWPF.EntityObject
{
    public class Incident
    {
        public int IncidentID { get; set; }
        public string Title { get; set; }
        public string Detail { get; set; }
        public DateTime? IncidentDateTime { get; set; }
        public string sIncidentDateTime => IncidentDateTime != null ? IncidentDateTime.Value.ToString("MM/dd/yyyy HH:mm:ss") : string.Empty;
        public int LocationID { get; set; }
        public string LocationName { get; set; }
    }
}
