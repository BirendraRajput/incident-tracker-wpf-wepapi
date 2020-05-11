using IncidentTracker.BusinessEntitiy;
using IncidentTracker.DAL.Repositiry;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace IncidentTracker.DAL
{
    public class LocationDAL : ILocationDAL
    {
        IGenericRepository<Location> _iGenericRepository;

        public LocationDAL(IGenericRepository<Location> iGenericRepository)
        => _iGenericRepository = iGenericRepository;

        public async Task<bool> AddLocation(Location locObject)
        {
            var locDBobject = await _iGenericRepository.FindAll();
            var locObj = locDBobject.Select(x => x.LocationID).ToList();
            var locID = locObj != null && locObj.Count != 0 ? locObj.Max() : 0;

            locObject.LocationID = locID + 1;
            await _iGenericRepository.Create(locObject);
            return true;
        }

        public async Task<IEnumerable<Location>> GetLocations()
        =>  await _iGenericRepository.FindAll();

       public bool DeleteLocation(int locationID)
        {
            var location = _iGenericRepository.Find(locationID);
            if (location != null)
            {
                _iGenericRepository.Delete(location.Result);
                return true;
            }
            else
                return false;
        }
    }
}
