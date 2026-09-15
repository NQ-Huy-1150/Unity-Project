using UnityEngine;

public class PaintingInteract : MonoBehaviour, IInteractable
{
    [TextArea(3, 10)]
    public string infoMessage = "Đây là bức tranh về sự kiện lịch sử...";

    public void Interact()
    {
        
    }
}