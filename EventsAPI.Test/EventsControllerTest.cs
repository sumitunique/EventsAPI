using Events.Controllers;
using EventsCRUD.Services;
using EventsCRUD.Services.Interfaces;
using Xunit;

namespace EventsAPI.Test
{
    public class EventsControllerTest
    {
        EventsController _controller;
        IEvents _service;

        public EventsControllerTest()
        {
            //_service = new EventsService();
            //_controller = new EventsController(_service);

        }

        [Fact]
        public void GetAllEvents()
        {

        }
    }
}