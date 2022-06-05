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
    /// Get all possible moves.
    /// </summary>
    /// <returns>A List of all possible moves. List.Count == 0 if there are no possible moves.</returns>
    public List<Vector2Int> GetAllMoves()
    {
        List<Vector2Int> allMoves = new();
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                Vector2Int target = new(i, j);
                if (IsValid(target) && IsLegal(target))
                {
                    allMoves.Add(target);
                }
            }
        }
        return allMoves;
    }

    /// <summary>
    /// Whether a move is legal, which means the king not being checked after the move.
    /// </summary>
    /// <param name="target">Target coordinate.</param>
    /// <returns>True if legal.</returns>
    public bool IsLegal(Vector2Int target)
    {
        MoveController targetTemp = Board.GetPiece(target);
        Vector2Int coordTemp = Entity.Coord;
        Board.SetPiece(Entity.Coord, null);
        Board.SetPiece(target, Entity);
        Entity.Coord = target;
        bool result = !Game.Check(tag);
        Entity.Coord = coordTemp;
        Board.SetPiece(target, targetTemp);
        Board.SetPiece(Entity.Coord, Entity);
        return result;
    }

    public bool IsLegal(int x, int y)
    {
        Vector2Int target = new(x, y);
        return IsLegal(target);
    }

    /// <summary>
    /// Whether a move is valid.
    /// </summary>
    /// <param name="target">Target coordinate.</param>
    /// <returns>True if valid.</returns>
    public virtual bool IsValid(Vector2Int target) => false;

    public bool IsValid(int x, int y)
    {
        Vector2Int target = new(x, y);
        return IsValid(target);
    }

    /// <summary>
    /// Whether the ordinary move of king is valid.
    /// </summary>
    /// <param name="target">Target coordinate.</param>
    /// <returns>True if valid.</returns>
    public virtual bool Ordinary(Vector2Int target) => false;

    /// <summary>
    /// Whether castle is valid.
    /// </summary>
    /// <param name="target">Target coordinate.</param>
    /// <returns>The MoveController of the rook if valid. Returns null otherwise.</returns>
    public virtual (bool valid, RookMove castle) Castle(Vector2Int target) => (false, null);

    public bool IsFriend(Vector2Int target)
    {
        return Board.IsOccupied(target) && Board.GetPiece(target).Rule.Colour == Colour;
    }

    public bool IsEnemy(Vector2Int target)
    {
        return Board.IsOccupied(target) && Board.GetPiece(target).Rule.Colour != Colour;
    }
}
