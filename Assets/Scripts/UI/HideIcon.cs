using UnityEngine;
using UnityEngine.UI;

public class HideIcon : MonoBehaviour
{
    private Image image;

    private void Awake()
    {
        image = GetComponent<Image>();
    }

    public void Hide(bool isOn)
    {
        if (isOn)
        {
            image.enabled = false;
        }
        else
        {
            image.enabled = true;
        }
    }
}
