﻿

using Codeflix.Catalog.Domain.Entity;

namespace Codeflix.Catalog.Domain.SeedWork;

public interface IGenericRepository<TAggregate> :IRepository
{
    public Task Insert(TAggregate aggregate, CancellationToken Canceltoken);

}
