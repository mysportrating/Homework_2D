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
        // �������� ����������� ���������� �������
        _mover = GetComponent<Mover>();
        _flipper = GetComponent<Flipper>();
        _vision = GetComponent<EnemyVision>();
        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        // ����������� ������ � ���� ��������� � ����������� ���������� � ����������� ���� (�������� ����� ��� �����)
        if (_vision.TrySeeTarget(out Transform target))
        {
            _mover.Move(target);
        }
        else
            _mover.Move(_target);

        //��������� ������� �����
        if (IsTargetReached())
            ChangeTarget();
    }

    private bool IsTargetReached()
    {
        // �������� ���������� �� ���� (�����)
        float sqrDistance = (transform.position - _target.position).sqrMagnitude;
        return sqrDistance <= _maxSqrDistance;
    }

    private void ChangeTarget()
    {
        // ��������� ��� �������� � ����� ����� � ������� ����� ���� ����������
        _wayPointIndex++;

        // �������� ������ �� ������� ������� � ��������� �������, ���� ������ ����� �� ������� �������
        if (_wayPointIndex >= _wayPoints.Length)
            _wayPointIndex = 0;

        // ������������ ���������� ����� �����
        _target = _wayPoints[_wayPointIndex].transform;

        // ����� ����������� ������� �����
        _flipper.LookAtTarget(_target.position);
    }
}