using UnityEngine;
using System.Linq;

public class BridgeBehaviour : MonoBehaviour
{
    [SerializeField] private Switch[] _switches; 

    public void FixedUpdate()
    {
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
