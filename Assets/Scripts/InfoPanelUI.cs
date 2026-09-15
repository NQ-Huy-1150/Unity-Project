using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InfoPanelUI : MonoBehaviour
{
    public GameObject panelRoot;
    public Image displayImage;
    public TextMeshProUGUI contentText;

    void Start()
    {
        panelRoot.SetActive(false);
    }

    public void Show(Sprite image, string message)
    {
        if (image != null)
        {
            displayImage.sprite = image;
            displayImage.gameObject.SetActive(true);
        }
        else
        {
            displayImage.gameObject.SetActive(false);
        }

        contentText.text = message;
        panelRoot.SetActive(true);
    }

    public void Hide()
    {
        panelRoot.SetActive(false);
    }
}