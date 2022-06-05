using UnityEngine;

public class Initiate : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 60;
        Board.board = new MoveController[9, 8]; // 多一行放置另外生成的棋子防止越界
        // Board.SelectedPiece = null;
    }
}
