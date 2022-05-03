using System.Collections.Generic;
using UnityEngine;

public class RuleController : MonoBehaviour
{
    public int Colour { get; set; }
    public MoveController Entity { get; private set; }
    public int Eval { get; set; }

    protected virtual void Awake()
    {
        Colour = CompareTag("White") ? 1 : -1;
        Entity = GetComponent<MoveController>();
    }

    /// <summary>
    /// 获得所有可行的落子位置，返回非空列表，判断无可行落子时使用Count == 0
    /// </summary>
    /// <returns></returns>
    public List<Vector2Int> GetAllMoves()
    {
        List<Vector2Int> allMoves = new();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector2Int target = new(i, j);
                if (IsValid(target) && !IsIllegal(target))
                {
                    allMoves.Add(target);
                }
            }
        }
        return allMoves;
    }

    public bool IsIllegal(Vector2Int target)
    {
        MoveController targetTemp = Board.GetBoard(target);
        Vector2Int coordTemp = Entity.Coord;
        Board.SetBoard(Entity.Coord, null);
        Board.SetBoard(target, Entity);
        Entity.Coord = target;
        bool result = Game.Check(tag);
        Entity.Coord = coordTemp;
        Board.SetBoard(target, targetTemp);
        Board.SetBoard(Entity.Coord, Entity);
        return result;
    }

    public bool IsIllegal(int x, int y)
    {
        Vector2Int target = new(x, y);
        return IsIllegal(target);
    }

    public virtual bool IsValid(Vector2Int target) => false;

    public bool IsValid(int x, int y)
    {
        Vector2Int target = new(x, y);
        return IsValid(target);
    }

    /// <summary>
    /// 判断王的常规走法是否可行
    /// </summary>
    /// <param name="target"></param>
    /// <returns></returns>
    public virtual bool Ordinary(Vector2Int target) => false;

    public virtual (bool valid, RookMove castle) Castle(Vector2Int target) => (false, null);

    public bool IsFriend(Vector2Int target)
    {
        return Board.IsOccupied(target) && Board.GetBoard(target).Rule.Colour == Colour;
    }

    public bool IsEnemy(Vector2Int target)
    {
        return Board.IsOccupied(target) && Board.GetBoard(target).Rule.Colour != Colour;
    }
}
