using UnityEngine;

// 此处不能生成王，会与Game.Kings产生冲突
public class Spawn : MonoBehaviour
{
    public QueenRule queenPre;
    public BishopRule bishopPre;
    public KnightRule knightPre;
    public RookRule rookPre;
    public PawnRule pawnPre;

    public Sprite queenBlack;
    public Sprite bishopBlack;
    public Sprite knightBlack;
    public Sprite rookBlack;
    public Sprite pawnBlack;

    private static Vector3 GetMousePosition()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

    public void SpawnQueen(string param)
    {
        QueenRule inst = Instantiate(queenPre, GetMousePosition(), Quaternion.identity);
        inst.tag = param;
        inst.Colour = 1;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = queenBlack;
            inst.Colour = -1;
        }
        inst.Entity.Pick();
    }

    public void SpawnBishop(string param)
    {
        BishopRule inst = Instantiate(bishopPre, GetMousePosition(), Quaternion.identity);
        inst.tag = param;
        inst.Colour = 1;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = bishopBlack;
            inst.Colour = -1;
        }
        inst.Entity.Pick();
    }

    public void SpawnKnight(string param)
    {
        KnightRule inst = Instantiate(knightPre, GetMousePosition(), Quaternion.identity);
        inst.tag = param;
        inst.Colour = 1;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = knightBlack;
            inst.Colour = -1;
        }
        inst.Entity.Pick();
    }

    public void SpawnRook(string param)
    {
        RookRule inst = Instantiate(rookPre, GetMousePosition(), Quaternion.identity);
        inst.tag = param;
        inst.Colour = 1;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = rookBlack;
            inst.Colour = -1;
        }
        inst.Entity.Pick();
    }

    public void SpawnPawn(string param)
    {
        PawnRule inst = Instantiate(pawnPre, GetMousePosition(), Quaternion.identity);
        inst.tag = param;
        inst.Colour = 1;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = pawnBlack;
            inst.Colour = -1;
        }
        inst.Entity.Pick();
    }
}
