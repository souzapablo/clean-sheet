using CleanSheet.Domain.Entities;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Newtonsoft.Json;

namespace CleanSheet.Infrastructure.Extensions;

public class InitialTeamConverter() : ValueConverter<IReadOnlyCollection<Player>, string>(
    players => JsonConvert.SerializeObject(players),
    json => JsonConvert.DeserializeObject<List<Player>>(json)!);