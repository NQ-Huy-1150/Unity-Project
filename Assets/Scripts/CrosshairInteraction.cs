using UnityEngine;

public class CrosshairInteraction : MonoBehaviour
{
    public GameObject promptText;     // InteractPrompt - chữ "E"
    public float interactDistance = 3f;
    public KeyCode interactKey = KeyCode.E;

    private IInteractable currentTarget;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool foundTarget = false;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                foundTarget = true;
                currentTarget = interactable;

                if (Input.GetKeyDown(interactKey))
                {
                    currentTarget.Interact();
                }
            }
        }

        // LUÔN cập nhật trạng thái hiện/ẩn mỗi frame - không để sót nhánh nào
        if (promptText.activeSelf != foundTarget)
        {
            promptText.SetActive(foundTarget);
        }

        if (!foundTarget)
        {
            currentTarget = null;
        }
    }
}