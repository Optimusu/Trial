﻿using Trial.Domain.EntitiesGen;
using Trial.DomainLogic.Pagination;
using Trial.DomainLogic.TrialResponse;

namespace Trial.Services.InterfacesGen;

public interface IIndicationService
{
    Task<ActionResponse<IEnumerable<Indication>>> ComboAsync();

    Task<ActionResponse<IEnumerable<Indication>>> GetAsync(PaginationDTO pagination);

    Task<ActionResponse<Indication>> GetAsync(int id);

    Task<ActionResponse<Indication>> UpdateAsync(Indication modelo);

    Task<ActionResponse<Indication>> AddAsync(Indication modelo);

    Task<ActionResponse<bool>> DeleteAsync(int id);
}