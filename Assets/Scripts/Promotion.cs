using UnityEngine;

public class Promotion : MonoBehaviour
{
    public static Promotion Panel { get; private set; }

    // Prefabs
    public QueenRule queenPre;
    public KnightRule knightPre;
    public RookRule rookPre;
    public BishopRule bishopPre;

    // Sprites
    public Sprite queenBlack;
    public Sprite knightBlack;
    public Sprite rookBlack;
    public Sprite bishopBlack;

    public PawnMove ProPawn { get; set; }

    private void Awake()
    {
        Panel = this;
        gameObject.SetActive(false);
    }

    public void ExitPromotion()
    {
        Board.SelectedPiece = null;
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Initialize the promoted piece.
    /// </summary>
    /// <param name="instance">The RuleController of the piece.</param>
    private void Setup(RuleController instance)
    {
        // RuleController.Awake()调用无效，需要重新设定Colour
        instance.Colour = ProPawn.Rule.Colour;
        Destroy(instance.GetComponent<Arranger>());
        instance.gameObject.AddComponent<PlayerAction>();
        Destroy(ProPawn.gameObject);
    }

    public void PromoteToQueen()
    {
        QueenRule inst = Instantiate(queenPre, ProPawn.transform.position, Quaternion.identity);
        inst.tag = ProPawn.tag;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = queenBlack;
        }
        Setup(inst);
        ExitPromotion();
    }

    public void PromoteToKnight()
    {
        KnightRule inst = Instantiate(knightPre, ProPawn.transform.position, Quaternion.identity);
        inst.tag = ProPawn.tag;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = knightBlack;
        }
        Setup(inst);
        ExitPromotion();
    }

    public void PromoteToRook()
    {
        RookRule inst = Instantiate(rookPre, ProPawn.transform.position, Quaternion.identity);
        inst.tag = ProPawn.tag;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = rookBlack;
        }
        Setup(inst);
        ExitPromotion();
    }

    public void PromoteToBishop()
    {
        BishopRule inst = Instantiate(bishopPre, ProPawn.transform.position, Quaternion.identity);
        inst.tag = ProPawn.tag;
        if (inst.CompareTag("Black"))
        {
            inst.GetComponent<SpriteRenderer>().sprite = bishopBlack;
        }
        Setup(inst);
        ExitPromotion();
    }
}
