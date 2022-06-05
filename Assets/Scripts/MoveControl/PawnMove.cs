using UnityEngine;

public class PawnMove : MoveController
{
    public override void Move(Vector2Int target)
    {
        base.Move(target);
        if ((Rule.Colour + 8) % 9 + target.y == 7)
        {
            Board.SelectedPiece = Rule;
            Promotion.Panel.gameObject.SetActive(true);
            Promotion.Panel.ProPawn = this;
        }
    }
}
