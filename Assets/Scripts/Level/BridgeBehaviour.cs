using UnityEngine;

public class BridgeBehaviour : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
    }
}
