﻿using CleanSheet.Domain.Entities;

namespace CleanSheet.Domain.Repositories;

public interface ICareerRepository
{
    Task AddAsync(Career career, CancellationToken cancellationToken);
}