using UnityEngine;

public class EnemyVision : MonoBehaviour
{

    [SerializeField] private Vector2 _visionDistance;
    [SerializeField] private LayerMask _targetLayer;

    private Flipper _flipper;

    void Start()
    {
        _flipper = GetComponent<Flipper>();
    }

    private void OnDrawGizmos()
    {
        // Визуальное отображеие области видимости противника
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetVisionArea(), _visionDistance);
    }

    public bool TrySeeTarget(out Transform target)
    {
        // Обнуляем значение таргета (цели)
        target = null;

        // Обпределение области видения противника
        Collider2D hit = Physics2D.OverlapBox(GetVisionArea(), _visionDistance, 0, _targetLayer);

        if (hit != null)
        {
            // Направляем луч в сторону цели (игрока) через Reycast
            Vector2 raycastDirection = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, raycastDirection, _visionDistance.x, ~(1 << gameObject.layer));

            // Проверяем получилось ли у нас попасть в игрока
            if (hit2D.collider != null)
            {
                // Визуальное отображение луча в сторону цели (игрока)
                if (hit2D.collider == hit)
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.red);
                    // Присваеваем позицию игрока в качестве цели
                    target = hit2D.transform;
                    return true;
                }
                else
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.white);
                }
            }
        }

        return false;
    }
    private Vector2 GetVisionArea()
    {
        //Офсет (смещение) области видимости противника для реалистичности
        float halfCoefficient = 1.8f;
        float originX = 0;
        if (_flipper != null)
        {
            float directionCoefficient = _flipper.IsTurnedRight ? 1 : -1;
            originX = transform.position.x + _visionDistance.x / halfCoefficient * directionCoefficient;
        }
        else
        {
            originX = transform.position.x + _visionDistance.x / halfCoefficient;
        }

        return new Vector2(originX, transform.position.y);
    }
}
