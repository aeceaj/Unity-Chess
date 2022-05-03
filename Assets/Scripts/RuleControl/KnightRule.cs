using UnityEngine;

public class KnightRule : RuleController
{
    protected override void Awake()
    {
        base.Awake();
        Eval = Colour * 30;
    }

    public override bool IsValid(Vector2Int target)
    {
        if (target.x == Entity.Coord.x || target.y == Entity.Coord.y)
        {
            return false;
        }
        if (Mathf.Abs(target.x - Entity.Coord.x) + Mathf.Abs(target.y - Entity.Coord.y) == 3 && !IsFriend(target))
        {
            return true;
        }
        return false;
    }
}
