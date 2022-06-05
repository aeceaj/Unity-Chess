using UnityEngine;

// START之后挂载
public class PlayerAction : MonoBehaviour
{
    private RuleController ruleCtrl;
    private MoveController moveCtrl;

    private void Awake()
    {
        ruleCtrl = GetComponent<RuleController>();
        moveCtrl = GetComponent<MoveController>();
    }

    private void Update()
    {
        if (moveCtrl.IsSelected)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePos.z = 0;
            transform.position = mousePos;
        }
    }

    private void OnMouseUpAsButton()
    {
        if (moveCtrl.IsSelected)
        {
            Vector2Int target = Board.PosToCrd(transform.position);
            if (!Board.OutOfRange(target) && ruleCtrl.IsValid(target) && ruleCtrl.IsLegal(target))
            {
                if (ruleCtrl.IsEnemy(target))
                {
                    moveCtrl.Capture(target);
                }
                moveCtrl.Move(target);
                Game.Exchange();
            }
            else
            {
                moveCtrl.Cancel();
            }
        }
        else if (!Board.IsActive && Game.State == ruleCtrl.Colour)
        {
            moveCtrl.Pick();
        }
    }

    private void OnMouseOver()
    {
        if (moveCtrl.IsSelected && Input.GetMouseButtonUp(1))
        {
            moveCtrl.Cancel();
        }
    }
}
