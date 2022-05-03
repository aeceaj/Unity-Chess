using UnityEngine;

public class HighLight : MonoBehaviour
{
    private bool isOn;
    private RuleController rule;

    private void Awake()
    {
        rule = GetComponent<RuleController>();
    }

    private void OnMouseEnter()
    {
        if (Game.State == Game.Arrange || Game.State == rule.Colour)
        {
            // TODO
            isOn = true;
        }
    }

    private void OnMouseExit()
    {
        if (isOn)
        {
            // TODO
            isOn = false;
        }
    }
}
