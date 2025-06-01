using UnityEngine;

public class Flipper : MonoBehaviour
{
    public bool IsTurnedRight { get; private set; } = true;

    public void LookAtTarget (Vector2 targetPosition)
    {
        // Смена направления взгляда врага
        if ((transform.position.x < targetPosition.x && !IsTurnedRight) || (transform.position.x > targetPosition.x && IsTurnedRight))
        {
            IsTurnedRight = !IsTurnedRight;
            transform.Flip();
        }
    }
}
