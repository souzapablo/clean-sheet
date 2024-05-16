﻿using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Errors;

public static class CareerErrors
{
    public static Error CareerNotFound(Guid id) =>
        new Error("CareerNotFound", $"Career with id {id} not found.");
}