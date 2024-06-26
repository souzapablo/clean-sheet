﻿using CleanSheet.Domain.Shared;
using MediatR;

namespace CleanSheet.Application.Features.InitialTeams.Queries.Get;

public record GetInitialTeamsQuery : IRequest<Result<IEnumerable<InitialTeamResponse>>>;