using UnityEngine;

public class CrosshairInteraction : MonoBehaviour
{
    public GameObject dotObject;
    public DotDirectionIndicator dotIndicator; // Kéo CrosshairDot vào đây (script mới)
    public GameObject promptText;
    public float interactDistance = 3f;
    public float proximityRange = 5f;
    public KeyCode interactKey = KeyCode.E;
    public LayerMask interactableLayer = ~0;

    private IInteractable currentTarget;

    void Update()
    {
        // Tìm vật tương tác gần nhất trong phạm vi
        Collider[] nearby = Physics.OverlapSphere(transform.position, proximityRange, interactableLayer);
        Transform nearestTarget = null;
        float nearestDist = Mathf.Infinity;

        foreach (var col in nearby)
        {
            if (col.GetComponent<IInteractable>() != null)
            {
                float dist = Vector3.Distance(transform.position, col.transform.position);
                if (dist < nearestDist)
                {
                    nearestDist = dist;
                    nearestTarget = col.transform;
                }
            }
        }

        bool hasNearbyInteractable = nearestTarget != null;

        // Raycast kiểm tra có đang ngắm thẳng vào vật tương tác không
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        bool isAiming = false;

        if (Physics.Raycast(ray, out hit, interactDistance))
        {
            IInteractable interactable = hit.collider.GetComponent<IInteractable>();
            if (interactable != null)
            {
                isAiming = true;
                currentTarget = interactable;

                if (Input.GetKeyDown(interactKey))
                {
                    currentTarget.Interact();
                }
            }
        }

        // Cập nhật hiển thị
        if (isAiming)
        {
            dotObject.SetActive(false);
            promptText.SetActive(true);
        }
        else if (hasNearbyInteractable)
        {
            dotObject.SetActive(true);
            promptText.SetActive(false);
            dotIndicator.UpdateDirection(nearestTarget.position);
        }
        else
        {
            dotObject.SetActive(false);
            promptText.SetActive(false);
            currentTarget = null;
        }
    }
}