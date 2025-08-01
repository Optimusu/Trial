﻿using Trial.Domain.Entities;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfaceEntities;

public interface IManagerService
{
    Task<ActionResponse<IEnumerable<Manager>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Manager>> GetAsync(int id);

    Task<ActionResponse<Manager>> UpdateAsync(Manager modelo, string frontUrl);

    Task<ActionResponse<Manager>> AddAsync(Manager modelo, string frontUrl);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}