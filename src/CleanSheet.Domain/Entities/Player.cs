using CleanSheet.Domain.Enums;
using CleanSheet.Domain.Primitives;

namespace CleanSheet.Domain.Entities;

public class Player(
    string name,
    int kitNumber,
    int overall,
    DateOnly birthday,
    PlayerPosition position) : Entity
{
    public string Name { get; private set; } = name;
    public int KitNumber { get; private set; } = kitNumber;
    public int Overall { get; private set; } = overall;
    public DateOnly Birthday { get; private set; } = birthday;
    public PlayerPosition Position { get; private set; } = position;
}