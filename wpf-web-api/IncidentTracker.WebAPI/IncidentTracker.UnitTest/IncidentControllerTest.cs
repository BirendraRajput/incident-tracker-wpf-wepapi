using IncidentTracker.BusinessEntitiy;
using IncidentTracker.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using System.Linq;
using IncidentTracker.DAL;
using System.Threading.Tasks;

namespace IncidentTracker.UnitTest
{
    public class IncidentControllerTest
    {
        private readonly List<Incident> _incidents;
        private readonly Mock<ILogger<IncidentController>> _mockLogger;
        private readonly Mock<IIncidentDAL> _mockIncidentDAL;
        private readonly IncidentController _incidentController;

        public IncidentControllerTest()
        {
            _incidents = new List<Incident>()
            {
                new Incident() { IncidentID = 1, Title = "Incident1", Detail="Inident Details1", IncidentDateTime =  DateTime.Now,LocationID=1 },
                new Incident() { IncidentID = 2, Title = "Incident2", Detail="Inident Details2", IncidentDateTime =  DateTime.Now,LocationID=1 },
                new Incident() { IncidentID = 3, Title = "Incident3", Detail="Inident Details3", IncidentDateTime =  DateTime.Now,LocationID=1 }
            };

            _mockLogger = new Mock<ILogger<IncidentController>>();
            _mockIncidentDAL = new Mock<IIncidentDAL>();
            _incidentController = new IncidentController(_mockIncidentDAL.Object, _mockLogger.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockIncidentDAL.Setup(p => p.GetIncidents()).ReturnsAsync(_incidents);

            // Act
            var okResult = _incidentController.GetAll().Result;
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        
        [Fact]
        public void GetById_UnknownIDPassed_ReturnsNotFoundResult()
        {
            var testIncidentID = 300;
            // Arrange
            _mockIncidentDAL.Setup(p => p.GetIncident(testIncidentID)).ReturnsAsync(_incidents.Where(x => x.IncidentID == testIncidentID).FirstOrDefault());
            // Act
            var notFoundResult = _incidentController.Get(testIncidentID).Result;

            // Assert
            Assert.IsType<NotFoundResult>(notFoundResult);
        }

        [Fact]
        public void GetById_ExistingIDPassed_ReturnsOkResult()
        {
            // Arrange
            var testIncidentID = 1;
            _mockIncidentDAL.Setup(p => p.GetIncident(testIncidentID)).ReturnsAsync(_incidents.Where(x => x.IncidentID == testIncidentID).FirstOrDefault());

            // Act
            var okResult = _incidentController.Get(testIncidentID).Result;

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

       
        [Fact]
        public void Add_ValidObjectPassed_ReturnsCreatedResponse()
        {
            // Arrange
            Incident testItem = new Incident()
            {
                Title = "Incident5",
                Detail = "Inident Details5",
                IncidentDateTime = DateTime.Now,
                LocationID = 1
            };

            _mockIncidentDAL.Setup(p => p.CreateIncident(testItem)).ReturnsAsync(true);

            // Act
            var createdResponse = _incidentController.Post(testItem).Result;

            // Assert
            Assert.IsType<OkResult>(createdResponse);
        }

        

        [Fact]
        public void Remove_NotExistingIDPassed_ReturnsNotFoundResponse()
        {
            // Arrange
            var notExistingID = 300;
            _mockIncidentDAL.Setup(p => p.DeleteIncident(notExistingID)).Returns(false);
            // Act
            var badResponse = _incidentController.Delete(notExistingID);

            // Assert
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public void Remove_ExistingIDPassed_ReturnsOkResult()
        {
            // Arrange
            var existingID = 1;
            _mockIncidentDAL.Setup(p => p.DeleteIncident(existingID)).Returns(true);
            // Act
            var okResponse = _incidentController.Delete(existingID);

            // Assert
            Assert.IsType<OkResult>(okResponse);
        }

    }
}
