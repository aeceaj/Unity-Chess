using System.Collections.Generic;
using UnityEngine;

public class ArtificialIdiot : MonoBehaviour
{
    private const float TimeInterval = 0.5f;

    private string colourTag;
    private int colour;
    private float timer;
    private bool selectDone;
    private PieceMove bestMove;

    private struct PieceMove
    {
        public MoveController piece;
        public Vector2Int target;

        public PieceMove(MoveController _piece, Vector2Int _target)
        {
            piece = _piece;
            target = _target;
        }
    }

    private void Start()
    {
        colourTag = name[15..];
        colour = colourTag == "White" ? 1 : -1;
        timer = 0;
        selectDone = false;
    }

    private void Update()
    {
        if (Game.State == colour)
        {
            if (!Board.IsActive && !selectDone)
            {
                // 此处加入延时，否则无法确保上一帧Destroy()完成，遍历棋子可能得到null对象
                timer += Time.deltaTime;
                if (timer < TimeInterval)
                {
                    return;
                }
                GetBestMove();
                Board.SelectedPiece = bestMove.piece.Rule;
                selectDone = true;
                timer = 0;
            }
            if (selectDone)
            {
                // 落子延时，同时防止IsActive的变化周期在同一帧内完成，让Square.cs得以捕捉来完成相应棋盘效果
                timer += Time.deltaTime;
                if (timer < TimeInterval)
                {
                    return;
                }
                if (bestMove.piece.Rule.IsEnemy(bestMove.target))
                {
                    bestMove.piece.Capture(bestMove.target);
                }
                bestMove.piece.Move(bestMove.target);
                Game.Exchange();
                selectDone = false;
                timer = 0;
            }
        }
    }

    private MoveController[] GetAllMoveCons()
    {
        GameObject[] myPieces = GameObject.FindGameObjectsWithTag(colourTag);
        MoveController[] allMoveCons = new MoveController[myPieces.Length];
        for (int i = 0; i < myPieces.Length; i++)
        {
            allMoveCons[i] = myPieces[i].GetComponent<MoveController>();
        }
        return allMoveCons;
    }

    private int EvaluateMove(PieceMove pm)
    {
        MoveController targetTemp = Board.GetPiece(pm.target);
        Vector2Int coordTemp = pm.piece.Coord;
        Board.SetPiece(pm.piece.Coord, null);
        Board.SetPiece(pm.target, pm.piece);
        pm.piece.Coord = pm.target;
        int result = Board.Evaluate();
        pm.piece.Coord = coordTemp;
        Board.SetPiece(pm.target, targetTemp);
        Board.SetPiece(pm.piece.Coord, pm.piece);
        return result;
    }

    private PieceMove GetRandomMove()
    {
        MoveController[] allMoveCons = GetAllMoveCons();
        MoveController piece;
        List<Vector2Int> moves;
        do
        {
            piece = allMoveCons[Random.Range(0, allMoveCons.Length)];
            moves = piece.Rule.GetAllMoves();
        } while (moves.Count == 0);
        Vector2Int target = moves[Random.Range(0, moves.Count)];
        return new PieceMove(piece, target);
    }

    private void GetBestMove()
    {
        int bestScore = -9999;
        for (int i = 0; i < 100; i++)
        {
            PieceMove newMove = GetRandomMove();
            int newScore = EvaluateMove(newMove) * colour;
            if (newScore > bestScore)
            {
                bestMove = newMove;
                bestScore = newScore;
            }
        }
    }
}
