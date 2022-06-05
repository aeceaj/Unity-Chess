using UnityEngine;

// START之前启用 START之后销毁
public class Arranger : MonoBehaviour
{
    private MoveController moveCtrl;

    private void Awake()
    {
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
            if (!Board.OutOfRange(target) && !Board.IsOccupied(target))
            {
                moveCtrl.Move(target);
            }
            else
            {
                moveCtrl.Cancel();
            }
        }
        else if (!Board.IsActive)
        {
            moveCtrl.Pick();
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonUp(1))
        {
            if (moveCtrl.IsSelected)
            {
                moveCtrl.Cancel();
            }
            else if (!Board.IsActive && !name.Contains("King")) // 此处防止王被移除
            {
                Board.SetPiece(moveCtrl.Coord, null);
                Destroy(gameObject);
            }
        }
    }
}
