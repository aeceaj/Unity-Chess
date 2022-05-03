using UnityEngine;
using UnityEngine.UI;

public class DisableStart : MonoBehaviour
{
    private Button button;

    private bool last;
    private bool now;

    private void Awake()
    {
        button = GetComponent<Button>();
    }

    private void LateUpdate()
    {
        last = now;
        now = Board.IsActive;

        if (last != now && !now)
        {
            if (Game.Check("White") || Game.Check("Black"))
            {
                button.enabled = false;
            }
            else
            {
                button.enabled = true;
            }
        }
    }
}
