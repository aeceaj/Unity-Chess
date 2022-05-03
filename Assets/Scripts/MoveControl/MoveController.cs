using UnityEngine;

public class MoveController : MonoBehaviour
{
    public Vector2Int Coord { get; set; }
    public bool HaveMoved { get; set; }
    public bool IsSelected { get; set; }

    public RuleController Rule { get; private set; }

    private SpriteRenderer rend;

    private void Awake()
    {
        Coord = Board.OutOfRange(transform.position) ? new Vector2Int(8, 0) : Board.PosToCrd(transform.position); // 另外生成的棋子标记在board第9行的缓冲区
        HaveMoved = false;
        IsSelected = false;
        rend = GetComponent<SpriteRenderer>();
        Rule = GetComponent<RuleController>();
    }

    protected virtual void Start()
    {
        Board.SetBoard(Coord, this);
    }

    public void Pick()
    {
        rend.sortingOrder = 3;
        IsSelected = true;
        Board.SelectedPiece = Rule;
        Board.IsActive = true;
    }

    public virtual void Move(Vector2Int target)
    {
        Board.SetBoard(Coord, null);
        Board.SetBoard(target, this);
        Coord = target;
        transform.position = Board.CrdToPos(target);
        rend.sortingOrder = 2;
        IsSelected = false;
        Board.IsActive = false;
        if (!HaveMoved)
        {
            HaveMoved = true;
        }
    }

    public void Cancel()
    {
        // 另外生成且还未放上棋盘的棋子右键应直接销毁
        if (Coord.x > 7)
        {
            Destroy(gameObject);
        }
        else
        {
            transform.position = Board.CrdToPos(Coord);
            rend.sortingOrder = 2;
            IsSelected = false;
        }
        Board.IsActive = false;
    }

    public void Capture(Vector2Int target)
    {
        Destroy(Board.GetBoard(target).gameObject);
    }
}
