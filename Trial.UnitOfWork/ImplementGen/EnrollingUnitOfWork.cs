﻿using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;
using Trial.Services.InterfacesGen;
using Trial.UnitOfWork.InterfacesGen;

namespace Trial.UnitOfWork.ImplementGen;

public class EnrollingUnitOfWork : IEnrollingUnitOfWork
{
    private readonly IEnrollingService _enrollingService;

    public EnrollingUnitOfWork(IEnrollingService enrollingService)
    {
        _enrollingService = enrollingService;
    }

    public async Task<ActionResponse<IEnumerable<Enrolling>>> ComboAsync() => await _enrollingService.ComboAsync();

    public async Task<ActionResponse<IEnumerable<Enrolling>>> GetAsync(PaginationDTO pagination) => await _enrollingService.GetAsync(pagination);

    public async Task<ActionResponse<Enrolling>> GetAsync(int id) => await _enrollingService.GetAsync(id);

    public async Task<ActionResponse<Enrolling>> UpdateAsync(Enrolling modelo) => await _enrollingService.UpdateAsync(modelo);

    public async Task<ActionResponse<Enrolling>> AddAsync(Enrolling modelo) => await _enrollingService.AddAsync(modelo);

    public async Task<ActionResponse<bool>> DeleteAsync(int id) => await _enrollingService.DeleteAsync(id);
}