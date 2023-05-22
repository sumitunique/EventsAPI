using EventsCRUD.Models;
using EventsCRUD.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [ApiController]
    public class EventsController : ControllerBase
    {
        private readonly IEvents _event;

        public EventsController(IEvents ev)
        {
            this._event = ev;
        }
        /// <summary>  
        /// Gets all events from Events table  
        /// </summary>  
        /// <returns></returns>  
        ///  
        //[HttpGet]
        //
        [AllowAnonymous]
        [HttpGet]
        [Route("api/Events/{limit}/{page}")]
        public IActionResult GetEvents(int limit, int page)
        {
            try
            {
                var eventsList = _event.GetEvents(limit, page);
                return Ok(eventsList);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// Save or update event.  
        /// </summary>  
        /// <param name="eventObject"></param>  
        /// <returns></returns>  
        ///  
        [HttpPost]  
        [Route("api/Events")]
        public IActionResult AddEvent(EventsViewModel ev)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest();
                }
             var result =    _event.AddEvent(ev);
                if (result.Result > 0)
                    return Ok("Success");
                else
                    return BadRequest("Error");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }


        [HttpGet]
        [Route("api/Events/{id}")]
        ///<summary>
        ///This function is used to get a particular student
        ///</summary>
        /// <param name="id">id to fetch a particualr institute</param>
        /// 
        public async Task<ActionResult<EventsViewModel>> GetEvent(int id)
        {
            try
            {
                var result = await _event.EditEvent(id);
                if (result == null)
                    return NotFound();
                else
                    return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpPut]
        [Route("api/Events/")]
        ///<summary>
        ///This function is used to update Student Info
        ///</summary>
        /// <param name="institute">institute which needs to be updated</param>
        public async Task<ActionResult> UpdateEvent(EventsViewModel ev)
        {
            try
            {
                var result = await _event.UpdateEvent(ev);
                if(result == null)
                    return NotFound();
                if (result > 0)
                    return Ok("Success");
                else
                    return BadRequest("Failed to update");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }




        /// <summary>  
        /// Delete an event based on the given id.  
        /// </summary>  
        /// <param name="eventId"></param>  
        /// <returns></returns>  
        ///  

        [HttpDelete]  
        [Route("api/Events/{id}")]
        public IActionResult DeleteEvent(int id)
        {
            try
            {
                var result = _event.DeleteEvent(id);
                if (result.Result == null)
                    return NotFound();
                if (result.Result != null && Convert.ToBoolean(result.Result))
                    return Ok("Success");
                else
                    return BadRequest("Failed to delete");
            }
            catch (Exception ex) 
            {

                throw;
            }
         
        }
        
    }
}
