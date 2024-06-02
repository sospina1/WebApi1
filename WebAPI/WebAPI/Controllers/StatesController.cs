using Microsoft.AspNetCore.Mvc;
using WebAPI.DAL.Entities;
using WebAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")] // Este es el nombre inicial de mi RUTA, URL o PATH
    [ApiController]
    public class StatesController : Controller
    {
        private readonly IStateService _stateService;

        public StatesController(IStateService stateService)
        {
            _stateService = stateService;
        }

        [HttpGet, ActionName("Get")]
        [Route("GetAll")]
        public async Task<ActionResult<IEnumerable<State>>> GetStatesAsync()
        {
            var states = await _stateService.GetStatesAsync();

            if (states == null || !states.Any()) return NotFound();

            return Ok(states);
        }

        [HttpGet, ActionName("GetByCountry")]
        [Route("GetByCountryId/{countryId}")]
        public async Task<ActionResult<IEnumerable<State>>> GetStatesByCountryIdAsync(Guid countryId)
        {
            var states = await _stateService.GetStatesByCountryIdAsync(countryId);

            if (states == null || !states.Any()) return NotFound();

            return Ok(states);
        }

        [HttpGet, ActionName("GetByName")]
        [Route("GetByName/{name}")]
        public async Task<ActionResult<State>> GetStateByNameAsync(string name)
        {
            var state = await _stateService.GetStateByNameAsync(name);

            if (state == null) return NotFound(); // NotFound = Status Code 404

            return Ok(state); // Ok = Status Code 200
        }

        [HttpPost, ActionName("Create")]
        [Route("Create")]
        public async Task<ActionResult<State>> CreateStateAsync(State state)
        {
            try
            {
                var newState = await _stateService.CreateStateAsync(state);
                if (newState == null) return NotFound();
                return Ok(newState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", state.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpPut, ActionName("Edit")]
        [Route("Edit")]
        public async Task<ActionResult<State>> EditStateAsync(State state)
        {
            try
            {
                var editedState = await _stateService.EditStateAsync(state);
                if (editedState == null) return NotFound();
                return Ok(editedState);
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("duplicate"))
                    return Conflict(String.Format("{0} ya existe", state.Name));

                return Conflict(ex.Message);
            }
        }

        [HttpDelete, ActionName("Delete")]
        [Route("Delete/{id}")]
        public async Task<ActionResult<State>> DeleteStateAsync(Guid id)
        {
            if (id == Guid.Empty) return BadRequest();

            var deletedState = await _stateService.DeleteStateAsync(id);

            if (deletedState == null) return NotFound();

            return Ok(deletedState);
        }
    }
}
