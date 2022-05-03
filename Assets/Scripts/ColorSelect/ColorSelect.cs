using UnityEngine;
using UnityEngine.UI;

public class ColorSelect : MonoBehaviour
{
    public string colorName;

    private Image colorPanel;
    private SpriteRenderer rend;

    private void Awake()
    {
        colorPanel = GetComponentInParent<Image>();
        rend = GameObject.Find("ChessBoard").GetComponent<SpriteRenderer>();
        Color preColor = rend.material.GetColor(colorName);
        colorPanel.color = preColor;
        transform.GetChild(0).GetComponent<Slider>().value = preColor.r;
        transform.GetChild(1).GetComponent<Slider>().value = preColor.g;
        transform.GetChild(2).GetComponent<Slider>().value = preColor.b;
    }

    public void Red(float r)
    {
        Color newColor = new(r, colorPanel.color.g, colorPanel.color.b);
        colorPanel.color = newColor;
        rend.material.SetColor(colorName, newColor);
    }

    public void Green(float g)
    {
        Color newColor = new(colorPanel.color.r, g, colorPanel.color.b);
        colorPanel.color = newColor;
        rend.material.SetColor(colorName, newColor);
    }

    public void Blue(float b)
    {
        Color newColor = new(colorPanel.color.r, colorPanel.color.g, b);
        colorPanel.color = newColor;
        rend.material.SetColor(colorName, newColor);
    }
}
