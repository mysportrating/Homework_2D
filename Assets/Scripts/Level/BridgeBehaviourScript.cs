using UnityEngine;
using System.Linq;

public class BridgeBehaviourScript : MonoBehaviour
{
    [SerializeField] private SwitchBehaviourScript[] _switches; 

    public void FixedUpdate()
    {
        // ������� ����������� (�������� ����), ���� ��� ������������� ��������
        if (_switches.All(i => i.IsActive))
        {
            gameObject.SetActive(false);
        } 
        else 
        {
            gameObject.SetActive(true);
        }
    }
}
