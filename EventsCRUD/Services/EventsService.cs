using AuthenticationApi.Db;
using EventsCRUD.Models;
using EventsCRUD.Services.Interfaces;
using static AuthenticationApi.Db.AppDbContext;

namespace EventsCRUD.Services
{
    public class EventsService : IEvents
    {
        private AppDbContext _context;
        public EventsService(AppDbContext context)
        {
            this._context = context;
        }

        public async Task<IReadOnlyList<EventsViewModel>> GetEvents(int limit, int page)
        {
            try
            {

                if (page == 0)
                    page = 1;

                if (limit == 0)
                    limit = int.MaxValue;

                var skip = (page - 1) * limit;

                var events = _context.Events.Select(x => new EventsViewModel
                {
                    Title = x.EventName,
                    Id = x.EventId,
                    Description = x.EventDescription,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,

                }).OrderBy(x => x.StartDate).Skip(skip).Take(limit).ToList();
                
                return events;

            }
            catch (Exception ex)
            {

                throw new Exception();
            }

        }

        public async Task<int> AddEvent(EventsViewModel ev)
        {
            try
            {

                Event ev2 = new Event
                {
                    EventName = ev.Title,
                    EventDescription = ev.Description,
                    StartDate = ev.StartDate,
                    EndDate = ev.EndDate
                };
                _context.Events.Add(ev2);
                var result = await _context.SaveChangesAsync();
                return result;


            }
            catch (Exception ex)
            {

                throw new Exception();
            }

        }

        public async Task<EventsViewModel> EditEvent(int id)
        {
            try
            {
                var result = _context.Events.Where(x => x.EventId == id).Select(x => new EventsViewModel
                {
                    Title = x.EventName,
                    Id = x.EventId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    Description = x.EventDescription

                }).FirstOrDefault();
                return result;


            }
            catch (Exception ex)
            {

                throw new Exception();
            }

        }

        public async Task<int?> UpdateEvent(EventsViewModel ev)
        {
            try
            {

                var result = _context.Events.Where(x => x.EventId == ev.Id).FirstOrDefault();
                if (result == null)
                    return null;
                result.EventName = ev.Title;
                result.EventDescription = ev.Description;
                result.StartDate = ev.StartDate;
                result.EndDate = ev.EndDate;
                _context.Update(result);
                var res = await _context.SaveChangesAsync();
                return res;


            }
            catch (Exception ex)
            {

                throw new Exception();
            }

        }


        public async Task<bool?> DeleteEvent(int id)
        {
            try
            {
                using (var connection = _context)
                {
                    var result = _context.Events.Where(x => x.EventId == id).FirstOrDefault();
                    if (result == null)
                    {
                        return null;
                    }

                    connection.Events.Remove(result);
                    connection.SaveChanges();
                    return true;
                }

            }
            catch (Exception ex)
            {

                throw new Exception();
            }

        }
    }
}
