using System;
using UnityEngine;

public class QueenRule : RuleController
{
    protected override void Awake()
    {
        base.Awake();
        Eval = Colour * 90;
    }

    public override bool IsValid(Vector2Int target)
    {
        if (IsFriend(target))
        {
            return false;
        }
        if (target.x == Entity.Coord.x)
        {
            int step = Convert.ToInt32(Mathf.Sign(target.y - Entity.Coord.y));
            for (int i = Entity.Coord.y + step; i != target.y; i += step)
            {
                if (Board.IsOccupied(target.x, i))
                {
                    return false;
                }
            }
            return true;
        }
        if (target.y == Entity.Coord.y)
        {
            int step = Convert.ToInt32(Mathf.Sign(target.x - Entity.Coord.x));
            for (int i = Entity.Coord.x + step; i != target.x; i += step)
            {
                if (Board.IsOccupied(i, target.y))
                {
                    return false;
                }
            }
            return true;
        }
        if (Mathf.Abs(target.x - Entity.Coord.x) != Mathf.Abs(target.y - Entity.Coord.y))
        {
            return false;
        }
        int stepX = Convert.ToInt32(Mathf.Sign(target.x - Entity.Coord.x));
        int stepY = Convert.ToInt32(Mathf.Sign(target.y - Entity.Coord.y));
        for (int i = Entity.Coord.x + stepX, j = Entity.Coord.y + stepY; i != target.x && j != target.y; i += stepX, j += stepY)
        {
            if (Board.IsOccupied(i, j))
            {
                return false;
            }
        }
        return true;
    }
}
