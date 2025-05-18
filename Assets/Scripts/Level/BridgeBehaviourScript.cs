using UnityEngine;
using System.Linq;

public class BridgeBehaviourScript : MonoBehaviour
{
    [SerializeField] private SwitchBehaviourScript[] _switches; 

    public void FixedUpdate()
    {
        // Убираем препятствие (опускаем мост), если все переключатели включены
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
