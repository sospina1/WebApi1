using Microsoft.EntityFrameworkCore;
using WebAPI.DAL;
using WebAPI.DAL.Entities;
using WebAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Domain.Services
{
    public class StateService : IStateService
    {
        private readonly DataBaseContext _context;

        public StateService(DataBaseContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<State>> GetStatesAsync()
        {
            try
            {
                return await _context.States.ToListAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId)
        {
            try
            {
                return await _context.States.Where(s => s.CountryId == countryId).ToListAsync();
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> GetStateByNameAsync(string name)
        {
            try
            {
                return await _context.States.FirstOrDefaultAsync(s => s.Name == name);
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> CreateStateAsync(State state)
        {
            try
            {
                state.Id = Guid.NewGuid();
                state.CreatedDate = DateTime.Now;
                _context.States.Add(state);

                await _context.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> EditStateAsync(State state)
        {
            try
            {
                state.ModifiedDate = DateTime.Now;
                _context.States.Update(state);

                await _context.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }

        public async Task<State> DeleteStateAsync(Guid id)
        {
            try
            {
                var state = await _context.States.FindAsync(id);
                if (state == null)
                {
                    return null;
                }

                _context.States.Remove(state);
                await _context.SaveChangesAsync();
                return state;
            }
            catch (DbUpdateException dbUpdateException)
            {
                throw new Exception(dbUpdateException.InnerException?.Message ?? dbUpdateException.Message);
            }
        }
    }
}
