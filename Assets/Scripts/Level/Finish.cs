using UnityEngine;

public class Finish : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        gameObject.SetActive(false);
    }
}
