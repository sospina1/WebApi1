using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.DAL.Entities;

public interface IStateService
{
    Task<IEnumerable<State>> GetStatesAsync();

    Task<IEnumerable<State>> GetStatesByCountryIdAsync(Guid countryId);

    Task<State> GetStateByNameAsync(string name);

    Task<State> CreateStateAsync(State state);

    Task<State> EditStateAsync(State state);

    Task<State> DeleteStateAsync(Guid id);
}

