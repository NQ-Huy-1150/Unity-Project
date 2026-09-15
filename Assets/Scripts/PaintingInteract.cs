using UnityEngine;
using TMPro;

public class PaintingInteract : MonoBehaviour, IInteractable
{
    public Sprite paintingImage;
    public TextMeshPro sourceText; // đổi từ TextMeshProUGUI thành TextMeshPro (3D)

    private static InfoPanelUI infoPanel;
    private bool isShowing = false;

    public void Interact()
    {
        if (infoPanel == null)
            infoPanel = FindFirstObjectByType<InfoPanelUI>();

        if (infoPanel == null) return;

        isShowing = !isShowing;

        if (isShowing)
        {
            string message = sourceText != null ? sourceText.text : "";
            infoPanel.Show(paintingImage, message);
        }
        else
        {
            infoPanel.Hide();
        }
    }
}