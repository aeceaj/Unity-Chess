using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// START之前禁用
public class Game : MonoBehaviour
{
    public const int Arrange = 0;
    public const int WhiteTurn = 1;
    public const int BlackTurn = -1;
    public const int End = -2;

    public static int State { get; set; }
    public static Dictionary<string, KingRule> Kings { get; set; }
    public static Transform Mark { get; set; }

    private static Text message;

    private bool last;
    private bool now;

    private void Awake()
    {
        State = Arrange;
        Kings = new Dictionary<string, KingRule>();
        message = GameObject.Find("UICanvas/EndMessage").GetComponent<Text>();
    }

    private void OnEnable()
    {
        Toggle init = GameObject.Find("Initiative/WhiteFirst").GetComponent<Toggle>();
        Mark = GameObject.Find("Mark").transform;
        if (init.isOn)
        {
            State = WhiteTurn;
            Mark.position = new Vector3(-5, -3);
        }
        else
        {
            State = BlackTurn;
            Mark.position = new Vector3(-5, 3);
        }
    }

    private void LateUpdate()
    {
        last = now;
        now = Board.IsActive;

        if (last != now) // Input.GetMouseButtonUp(0)
        {
            if (Checkmate("White"))
            {
                message.text = "BLACK WINS";
                message.enabled = true;
                State = End;
            }
            else if (Checkmate("Black"))
            {
                message.text = "WHITE WINS";
                message.enabled = true;
                State = End;
            }
        }
    }

    public static void Exchange()
    {
        State *= -1;
        Mark.position = new Vector3(-5, -Mark.position.y);
    }

    public static bool Check(string colour)
    {
        foreach (MoveController obj in Board.board)
        {
            if (obj && !obj.CompareTag(colour))
            {
                // 调用King的IsValid()会使Castle()和Check()产生死循环，故分开讨论
                if (!obj.name.Contains("King") && obj.Rule.IsValid(Kings[colour].Entity.Coord))
                {
                    return true;
                }
                if (obj.name.Contains("King") && obj.Rule.Ordinary(Kings[colour].Entity.Coord))
                {
                    return true;
                }
            }
        }
        return false;
    }

    public static bool Checkmate(string colour)
    {
        if (!Check(colour))
        {
            return false;
        }
        foreach (MoveController obj in Board.board)
        {
            if (obj && obj.CompareTag(colour))
            {
                for (int i = 0; i < 8; i++)
                {
                    for (int j = 0; j < 8; j++)
                    {
                        if (obj.Rule.IsValid(i, j) && !obj.Rule.IsIllegal(i, j))
                        {
                            return false;
                        }
                    }
                }
            }
        }
        return true;
    }
}
