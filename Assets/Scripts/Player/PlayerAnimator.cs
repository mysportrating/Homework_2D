using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetSpeed (float speed)
    {
        // Передаем значение переменной в параметры Аниматора
        _animator.SetFloat(ConstantsData.AnimatorParameters.Speed, speed);
    }
}
