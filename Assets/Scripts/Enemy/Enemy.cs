using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _speed = 3.0f;
    [SerializeField] private float _maxSqrDistance = 0.05f;
    [SerializeField] private Vector2 _visionDistance;
    [SerializeField] private LayerMask _targetLayer;

    private Rigidbody2D _rigidbody;
    private Transform _target;
    private bool _isTurnedRight = true;
    private int _wayPointIndex;

    private void Start()
    {
        // Получаем необходимые компоненты объекта
        _rigidbody = GetComponent<Rigidbody2D>();

        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        // Обнаружение игрока в зоне видимости и перемещение противника в направлении цели (заданная точка или игрок)
        if (TrySeeTarget(out Transform target))
        {
            Move(target);
        }
        else
            Move(_target);

        //Изменение целевой точки
        if (IsTargetReached())
            ChangeTarget();
    }

    private bool TrySeeTarget(out Transform target)
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

    private void Move(Transform target)
    {
        // Перемещение противника через смещение позиции противника в сторону новой точки (цели)
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
    }

    private bool IsTargetReached()
    {
        // Проверка достигнута ли цель (точка)
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;
        return sqrDistance <= _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        // Инкремент для смещение к новой точке в массиве точек пути противника
        _wayPointIndex++;

        // Проверка выхода за пределы массива и обнуление индекса, если индекс вышел за пределы массива
        if (_wayPointIndex >= _wayPoints.Length)
            _wayPointIndex = 0;

        // Присваивание кординанат новой точки
        _target = _wayPoints[_wayPointIndex].transform;

        // Смена направления взгляда врага
        if ((transform.position.x < _target.position.x && !_isTurnedRight) || (transform.position.x > _target.position.x && _isTurnedRight))
        {
            _isTurnedRight = !_isTurnedRight;
            transform.Flip();
        }
    }

    private Vector2 GetVisionArea()
    {
        //Офсет (смещение) области видимости противника для реалистичности
        float halfCoefficient = 1.8f;
        int directionCoefficient = _isTurnedRight ? 1 : -1;
        float originX = transform.position.x + _visionDistance.x / halfCoefficient * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }

    private void OnDrawGizmos()
    {
        // Визуальное отображеие области видимости противника
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetVisionArea(), _visionDistance);
    }
}
