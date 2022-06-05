using UnityEngine;

public class KingRule : RuleController
{
    public RookMove[] CastleRooks { get; set; }

    protected override void Awake()
    {
        base.Awake();
        Eval = Colour * 900;
    }

    private void Start()
    {
        CastleRooks = new RookMove[2];
        Game.Kings.Add(tag, this); // Game.cs启用前添加
    }

    public override bool Ordinary(Vector2Int target)
    {
        if (IsFriend(target))
        {
            return false;
        }
        if (Mathf.Abs(target.x - Entity.Coord.x) > 1 || Mathf.Abs(target.y - Entity.Coord.y) > 1)
        {
            return false;
        }
        return true;
    }

    public override (bool valid, RookMove castle) Castle(Vector2Int target) // 由于调用了Check()会产生死循环
    {
        if (Entity.HaveMoved || Game.Check(tag))
        {
            return (false, null);
        }
        if (CastleRooks[0] && !CastleRooks[0].HaveMoved && target == new Vector2Int(2, Entity.Coord.y))
        {
            for (int i = 3; i > 1; i--)
            {
                if (Board.IsOccupied(i, Entity.Coord.y) || !IsLegal(i, Entity.Coord.y))
                {
                    return (false, null);
                }
            }
            if (Board.IsOccupied(1, Entity.Coord.y))
            {
                return (false, null);
            }
            return (true, CastleRooks[0]);
        }
        if (CastleRooks[1] && !CastleRooks[1].HaveMoved && target == new Vector2Int(6, Entity.Coord.y))
        {
            for (int i = 5; i < 7; i++)
            {
                if (Board.IsOccupied(i, Entity.Coord.y) || !IsLegal(i, Entity.Coord.y))
                {
                    return (false, null);
                }
            }
            return (true, CastleRooks[1]);
        }
        return (false, null);
    }

    public override bool IsValid(Vector2Int target)
    {
        return Ordinary(target) || Castle(target).valid;
    }
}
