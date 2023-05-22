using EventsCRUD.Models;
namespace EventsCRUD.Services.Interfaces
{
    public interface IEvents
    {
        public Task<IReadOnlyList<EventsViewModel>> GetEvents(int limit,int page);
        public Task<int> AddEvent(EventsViewModel ev);

        public Task<EventsViewModel> EditEvent(int id);
        public Task<int?> UpdateEvent(EventsViewModel ev);
        public Task<bool?> DeleteEvent(int id);
    }
}
