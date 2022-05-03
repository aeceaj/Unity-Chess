using UnityEngine;

public class Square : MonoBehaviour
{
    private Vector2Int coord;
    private SpriteRenderer rend;

    // 用于标记是否进行了操作
    private bool last;
    private bool now;

    private void Awake()
    {
        string sqName = name[7..];
        coord = Board.NameToCrd(sqName);
        rend = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        now = Board.IsActive;
    }

    private void LateUpdate()
    {
        last = now;
        now = Board.IsActive;

        // 去掉外层if也可正常变化，此处仅作为添加触发入口
        if (last != now) // Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)
        {
            switch (Game.State)
            {
            case Game.Arrange:
                if (CheckSquare())
                {
                    rend.color = Color.red;
                }
                else
                {
                    rend.color = Color.clear;
                }
                break;
            default:
                if (Board.IsActive)
                {
                    if (coord == Board.SelectedPiece.Entity.Coord)
                    {
                        rend.color = Color.blue;
                    }
                    else if (ValidSquare())
                    {
                        rend.color = new Color(0, 0.8f, 0.6f);
                    }
                }
                else if (CheckSquare())
                {
                    rend.color = Color.red;
                }
                else
                {
                    rend.color = Color.clear;
                }
                break;
            }
        }
    }

    private bool ValidSquare()
    {
        return Board.SelectedPiece.IsValid(coord) && !Board.SelectedPiece.IsIllegal(coord);
    }

    private bool CheckSquare()
    {
        if (!Board.IsOccupied(coord))
        {
            return false;
        }
        if (Game.Check("White") && Board.GetBoard(coord).name == "WhiteKing")
        {
            return true;
        }
        if (Game.Check("Black") && Board.GetBoard(coord).name == "BlackKing")
        {
            return true;
        }
        return false;
    }
}
