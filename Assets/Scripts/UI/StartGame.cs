using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour
{
    public Sprite frame;

    public void Begin()
    {
        // 销毁Arranger.cs
        foreach (MoveController obj in Board.board)
        {
            if (obj)
            {
                Destroy(obj.GetComponent<Arranger>());
            }
        }

        // AI开关，若不使用AI则挂载PlayerAction.cs
        Toggle toggleWhite = GameObject.Find("UICanvas/Start/AIToggleWhite").GetComponent<Toggle>();
        Toggle toggleBlack = GameObject.Find("UICanvas/Start/AIToggleBlack").GetComponent<Toggle>();
        if (toggleWhite.isOn)
        {
            ArtificialIdiot playerWhite = GameObject.Find("ArtificialIdiotWhite").GetComponent<ArtificialIdiot>();
            playerWhite.enabled = true;
        }
        else
        {
            foreach (MoveController obj in Board.board)
            {
                if (obj && obj.CompareTag("White"))
                {
                    obj.gameObject.AddComponent<PlayerAction>();
                }
            }
        }
        if (toggleBlack.isOn)
        {
            ArtificialIdiot playerBlack = GameObject.Find("ArtificialIdiotBlack").GetComponent<ArtificialIdiot>();
            playerBlack.enabled = true;
        }
        else
        {
            foreach (MoveController obj in Board.board)
            {
                if (obj && obj.CompareTag("Black"))
                {
                    obj.gameObject.AddComponent<PlayerAction>();
                }
            }
        }
    }
}
