using IncidentTracker.BusinessEntitiy;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using IncidentTracker.DAL.Repositiry;
using System.Threading.Tasks;

namespace IncidentTracker.DAL
{
    public class IncidentDAL : IIncidentDAL
    {
        IGenericRepository<Incident> _iGenericRepository;
        public IncidentDAL(IGenericRepository<Incident> iGenericRepository)
        {
            _iGenericRepository = iGenericRepository;
        }

        public async Task<bool> CreateIncident(Incident incObject)
        {
            var incDBobject = await _iGenericRepository.FindAll();
            var incObj = incDBobject.Select(x => x.IncidentID).ToList();
            var incID = incObj != null && incObj.Count != 0 ? incObj.Max() : 0;

            incObject.IncidentID = incID + 1;
            await _iGenericRepository.Create(incObject);
            return true;
         }


        public async Task<Incident> GetIncident(int ID)
        {
            return await _iGenericRepository.Find(ID);
        }

        public async Task<IEnumerable<Incident>> GetIncidents()
        {
            return await _iGenericRepository.FindAll();
        }

        public bool UpdateIncident(Incident incObject)
        {
            var incident = _iGenericRepository.Find(incObject.IncidentID);
            if (incident != null)
            {
                incident.Result.Title = incObject.Title;
                incident.Result.Detail = incObject.Detail;
                incident.Result.IncidentDateTime = incObject.IncidentDateTime;
                incident.Result.LocationID = incObject.LocationID;

                _iGenericRepository.Update(incident.Result);
                return true;
            }
            else
                return false;
        }


        public bool DeleteIncident(int incidentID)
        {
            var incident = _iGenericRepository.Find(incidentID);
            if (incident != null)
            {
                _iGenericRepository.Delete(incident.Result);
                return true;
            }
            else
                return false;
        }

    }
}
