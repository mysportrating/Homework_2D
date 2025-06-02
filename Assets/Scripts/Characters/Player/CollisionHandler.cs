using System;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public event Action<IInteractable> ObjectReached;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // ���� ������������ � ��������, �� ��������� ������� ������� �� ������� � ��������/���������� ���� ������
        if (collision.gameObject.TryGetComponent(out IInteractable interactable))
        {
            ObjectReached?.Invoke(interactable);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // ���� ������������ ������������, �� ���������� null
        if (collision.gameObject.TryGetComponent(out IInteractable _))
        {
            ObjectReached?.Invoke(null);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // ���� ����� ������ � ������� �������, �� ��������� ������� ������� �� ������� � ��������/���������� ���� ������
        if (collision.TryGetComponent(out IInteractable interactable))
        {
            ObjectReached?.Invoke(interactable);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // ���� ����� ������� �� ������� �������, �� ���������� null
        if (collision.TryGetComponent(out IInteractable _))
        {
            ObjectReached?.Invoke(null);
        }
    }
}
