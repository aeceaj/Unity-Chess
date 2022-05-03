using System;
using UnityEngine;

public class KingMove : MoveController
{
    public override void Move(Vector2Int target)
    {
        if (Rule.Castle(target).valid)
        {
            Vector2Int rookTarget = new(target.x + Convert.ToInt32(target.x < 4) * 2 - 1, target.y);
            Rule.Castle(target).castle.Move(rookTarget);
        }
        base.Move(target);
    }
}
