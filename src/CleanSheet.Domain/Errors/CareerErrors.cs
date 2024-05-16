﻿using CleanSheet.Domain.Shared;

namespace CleanSheet.Domain.Errors;

public static class CareerErrors
{
    public static Error CareerNotFound(Guid id) => new("CareerNotFound", $"Career with id {id} not found.");
}