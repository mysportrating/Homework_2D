using UnityEngine;

[RequireComponent (typeof(Mover), typeof(Flipper), typeof(EnemyVision))]
public class Enemy : MonoBehaviour
{
    [SerializeField] private WayPoint[] _wayPoints;
    [SerializeField] private float _maxSqrDistance = 0.05f;

    private Mover _mover;
    private Transform _target;
    private Flipper _flipper;
    private EnemyVision _vision;
    private int _wayPointIndex;

    private void Start()
    {
        // ѕолучаем необходимые компоненты объекта
        _mover = GetComponent<Mover>();
        _flipper = GetComponent<Flipper>();
        _vision = GetComponent<EnemyVision>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        // ќбнаружение игрока в зоне видимости и перемещение противника в направлении цели (заданна€ точка или игрок)
        if (_vision.TrySeeTarget(out Transform target))
        {
            _mover.Move(target);
        }
        else
            _mover.Move(_target);

        //»зменение целевой точки
        if (IsTargetReached())
            ChangeTarget();
    }

    private bool IsTargetReached()
    {
        // ѕроверка достигнута ли цель (точка)
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;
        return sqrDistance <= _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        // »нкремент дл€ смещение к новой точке в массиве точек пути противника
        _wayPointIndex++;

        // ѕроверка выхода за пределы массива и обнуление индекса, если индекс вышел за пределы массива
        if (_wayPointIndex >= _wayPoints.Length)
            _wayPointIndex = 0;

        // ѕрисваивание кординанат новой точки
        _target = _wayPoints[_wayPointIndex].transform;

        // —мена направлени€ взгл€да врага
        _flipper.LookAtTarget(_target.position);
    }
}