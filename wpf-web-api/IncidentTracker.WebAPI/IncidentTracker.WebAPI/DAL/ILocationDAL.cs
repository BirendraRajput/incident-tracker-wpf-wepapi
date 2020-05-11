using IncidentTracker.BusinessEntitiy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IncidentTracker.DAL
{
    public interface ILocationDAL
    {
       Task<bool> AddLocation(Location locObject);

        Task<IEnumerable<Location>> GetLocations();

        bool DeleteLocation(int locationID);
    }
}
