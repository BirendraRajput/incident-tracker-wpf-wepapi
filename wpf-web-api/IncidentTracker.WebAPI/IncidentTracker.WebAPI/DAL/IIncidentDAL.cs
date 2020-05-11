using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using IncidentTracker.BusinessEntitiy;

namespace IncidentTracker.DAL
{
    public interface IIncidentDAL
    {
        Task<bool> CreateIncident(Incident incObject);
        Task<Incident> GetIncident(int incidentID);

        Task<IEnumerable<Incident>> GetIncidents();

        bool UpdateIncident(Incident incObject);

        bool DeleteIncident(int incidentID);
    }
}
