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
        // ���������� ���������� ������� ��������� ����������
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(GetVisionArea(), _visionDistance);
    }

    public bool TrySeeTarget(out Transform target)
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
    private Vector2 GetVisionArea()
    {
        //����� (��������) ������� ��������� ���������� ��� ��������������
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
