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
        // �������� ����������� ���������� �������
        _rigidbody = GetComponent<Rigidbody2D>();

        _target = _wayPoints[_wayPointIndex].transform;
    }

    private void FixedUpdate()
    {
        // ����������� ������ � ���� ��������� � ����������� ���������� � ����������� ���� (�������� ����� ��� �����)
        if (TrySeeTarget(out Transform target))
        {
            Move(target);
        }
        else
            Move(_target);

        //��������� ������� �����
        if (IsTargetReached())
            ChangeTarget();
    }

    private bool TrySeeTarget(out Transform target)
    {
        // �������� �������� ������� (����)
        target = null;

        // ������������ ������� ������� ����������
        Collider2D hit = Physics2D.OverlapBox(GetVisionArea(), _visionDistance, 0, _targetLayer);

        if (hit != null)
        {
            // ���������� ��� � ������� ���� (������) ����� Reycast
            Vector2 raycastDirection = (hit.transform.position - transform.position).normalized;
            RaycastHit2D hit2D = Physics2D.Raycast(transform.position, raycastDirection, _visionDistance.x, ~(1 << gameObject.layer));

            // ��������� ���������� �� � ��� ������� � ������
            if (hit2D.collider != null)
            {
                // ���������� ����������� ���� � ������� ���� (������)
                if (hit2D.collider == hit)
                {
                    Debug.DrawLine(transform.position, hit2D.point, Color.red);
                    // ����������� ������� ������ � �������� ����
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
        // ����������� ���������� ����� �������� ������� ���������� � ������� ����� ����� (����)
        Vector2 newPosition = Vector2.MoveTowards(transform.position, target.position, _speed * Time.deltaTime);
        _rigidbody.MovePosition(newPosition);
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
        if ((transform.position.x < _target.position.x && !_isTurnedRight) || (transform.position.x > _target.position.x && _isTurnedRight))
        {
            _isTurnedRight = !_isTurnedRight;
            transform.Flip();
        }
    }

    private Vector2 GetVisionArea()
    {
        //����� (��������) ������� ��������� ���������� ��� ��������������
        float halfCoefficient = 1.8f;
        int directionCoefficient = _isTurnedRight ? 1 : -1;
        float originX = transform.position.x + _visionDistance.x / halfCoefficient * directionCoefficient;
        return new Vector2(originX, transform.position.y);
    }

    private void OnDrawGizmos()
    {
        // ���������� ���������� ������� ��������� ����������
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetVisionArea(), _visionDistance);
    }
}
