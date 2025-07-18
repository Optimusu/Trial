﻿using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfaceEntities;
using Trial.UnitOfWork.InterfaceEntities;

namespace Trial.UnitOfWork.ImplementEntities;

public class CountryUnitOfWork : ICountryUnitOfWork
{
    private readonly ICountryServices _countriesService;

    public CountryUnitOfWork(ICountryServices countriesService)
    {
        _countriesService = countriesService;
    }

    public async Task<ActionResponse<IEnumerable<Country>>> ComboAsync() => await _countriesService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Country>>> GetAsync(PaginationDTO pagination) => await _countriesService.GetAsync(pagination);

    public async Task<ActionResponse<Country>> GetAsync(int id) => await _countriesService.GetAsync(id);

    public async Task<ActionResponse<Country>> UpdateAsync(Country modelo) => await _countriesService.UpdateAsync(modelo);

    public async Task<ActionResponse<Country>> AddAsync(Country modelo) => await _countriesService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _countriesService.DeleteAsync(id);
}