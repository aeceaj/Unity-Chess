using UnityEngine;

public class PawnRule : RuleController
{
    protected override void Awake()
    {
        base.Awake();
        Eval = Colour * 10;
    }

    public override bool IsValid(Vector2Int target)
    {
        if (target == new Vector2Int(Entity.Coord.x, Entity.Coord.y + Colour) && !Board.IsOccupied(target))
        {
            return true;
        }
        if (Entity.Coord == new Vector2Int(target.x, (Colour + 7) % 7) && target.y - Entity.Coord.y == 2 * Colour && !Board.IsOccupied(target) && !Board.IsOccupied(target.x, target.y - Colour))
        {
            return true;
        }
        if (Mathf.Abs(target.x - Entity.Coord.x) == 1 && target.y - Entity.Coord.y == Colour && IsEnemy(target))
        {
            return true;
        }
        return false;
    }
}
