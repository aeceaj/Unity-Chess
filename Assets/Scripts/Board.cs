using System;
using UnityEngine;

public static class Board
{
    public static MoveController[,] board;
    public static bool IsActive { get; set; }
    public static RuleController SelectedPiece { get; set; }

    public static MoveController GetBoard(Vector2Int crd)
    {
        return board[crd.x, crd.y];
    }

    public static void SetBoard(Vector2Int crd, MoveController obj)
    {
        board[crd.x, crd.y] = obj;
    }

    public static bool IsOccupied(Vector2Int crd)
    {
        return board[crd.x, crd.y] != null;
    }
    public static bool IsOccupied(int x, int y)
    {
        return board[x, y] != null;
    }

    public static bool OutOfRange(Vector2Int crd)
    {
        return crd.x < 0 || crd.x > 7 || crd.y < 0 || crd.y > 7;
    }

    public static bool OutOfRange(Vector3 pos)
    {
        return pos.x < -4 || pos.x > 4 || pos.y < -4 || pos.y > 4;
    }

    public static Vector2Int PosToCrd(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x + 3.5f);
        int y = Mathf.RoundToInt(pos.y + 3.5f);
        return new Vector2Int(x, y);
    }

    public static Vector3 CrdToPos(Vector2Int crd)
    {
        float x = crd.x - 3.5f;
        float y = crd.y - 3.5f;
        return new Vector3(x, y, 0);
    }

    public static Vector2Int NameToCrd(string n)
    {
        int x = Convert.ToInt32(n[0]) - 65;
        int y = int.Parse($"{n[1]}\0") - 1;
        return new Vector2Int(x, y);
    }

    /// <summary>
    /// 计算当前棋局分值
    /// </summary>
    /// <returns></returns>
    public static int Evaluate()
    {
        int score = 0;
        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                if (IsOccupied(i, j))
                {
                    score += board[i, j].Rule.Eval;
                }
            }
        }
        return score;
    }
}
