using UnityEngine;

public class LightSwitch : MonoBehaviour, IInteractable
{
    public Light roomLight;
    private bool isOn = false;

    void Start()
    {
        if (roomLight != null)
            roomLight.enabled = isOn;
    }

    public void Interact()
    {
        isOn = !isOn;
        roomLight.enabled = isOn;
    }
}