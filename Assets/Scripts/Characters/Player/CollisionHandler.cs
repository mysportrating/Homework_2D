using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> ObjectReached;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Если сталкиваемся с объектом, то проверяем наличие скрипта на объекте и вызываем/возвращаем этот скрипт
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            ObjectReached?.Invoke(interactable);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // Если столкновение прекрощается, то возвращаем null
        if (collision.gameObject.TryGetComponent(out IInteractable _))
        {
            ObjectReached?.Invoke(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Если игрок входит в область объекта, то проверяем наличие скрипта на объекте и вызываем/возвращаем этот скрипт
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            ObjectReached?.Invoke(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Если игрок выходит из области объекта, то возвращаем null
        if (collision.TryGetComponent(out IInteractable _))
        {
            ObjectReached?.Invoke(null);
        }
    }
}
