using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    public readonly int Speed = Animator.StringToHash(nameof(Speed));

    [SerializeField] private Animator _animator;

    public void SetSpeed (float speed)
    {
        _animator.SetFloat(Speed, speed);
    }
}
