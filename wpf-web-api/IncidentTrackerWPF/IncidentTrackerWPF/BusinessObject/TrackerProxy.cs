using IncidentTrackerWPF.EntityObject;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;

namespace IncidentTrackerWPF.BusinessObject
{
    public class TrackerProxy
    {
        private HttpClient _httpClient;
        private string _baseUrl;
        public TrackerProxy()
        {
            _httpClient = new HttpClient();
            _baseUrl = ConfigurationManager.AppSettings.Get("trackerAPI");
        }

        public async Task<IEnumerable<Incident>> GetAllIncident()
        {
            IEnumerable<Incident> _incidents = null;
            try
            {
                HttpResponseMessage _response = await _httpClient.GetAsync($"{_baseUrl}/Incident");
                if (_response.IsSuccessStatusCode)
                    _incidents = await _response.Content.ReadAsAsync<IEnumerable<Incident>>();
            }
            catch (Exception ex)
            {
                //log               
            }

            return _incidents;
        }

        public async Task<Incident> GetIncident(int incidentId)
        {
            Incident _incident = null;
            try
            {
                HttpResponseMessage _response = await _httpClient.GetAsync($"{_baseUrl}/Incident/{incidentId}");
                if (_response.IsSuccessStatusCode)
                {
                    _incident = await _response.Content.ReadAsAsync<Incident>();
                }
            }
            catch (Exception ex)
            {
                // Log 
            }
            return _incident;
        }

        public async Task<bool> AddIncident(Incident objIncident)
        {
            var _status = false;
            try
            {
                HttpResponseMessage _response = await _httpClient.PostAsJsonAsync($"{_baseUrl}/Incident", objIncident);
                if (_response.IsSuccessStatusCode)
                    _status = true;
            }
            catch (Exception ex)
            {
                // Log
                _status = false;
            }

            return _status;
        }

        public async Task<bool> UpdateIncident(Incident objIncident)
        {
            var _status = false;
            try
            {
                HttpResponseMessage _response = await _httpClient.PutAsJsonAsync($"{_baseUrl}/Incident", objIncident);
                if (_response.IsSuccessStatusCode)
                    _status = true;
            }
            catch (Exception ex)
            {
                // Log 
                _status = false;
            }

            return _status;
        }

        public async Task<bool> DeleteIncident(int incidentId)
        {
            var _status = false;
            try
            {
                HttpResponseMessage _response = await _httpClient.DeleteAsync($"{_baseUrl}/Incident/{incidentId}");
                if (_response.IsSuccessStatusCode)
                    _status = true;
            }
            catch (Exception ex)
            {
                // Log
                _status = false;
            }

            return _status;
        }


        public async Task<IEnumerable<Location>> GetAllLocation()
        {
            IEnumerable<Location> _locations = null;
            try
            {
                HttpResponseMessage _response = await _httpClient.GetAsync($"{_baseUrl}/Location");
                if (_response.IsSuccessStatusCode)
                    _locations = await _response.Content.ReadAsAsync<IEnumerable<Location>>();
            }
            catch (Exception ex)
            {
                // Log 
            }
            return _locations;
        }
    }
}
