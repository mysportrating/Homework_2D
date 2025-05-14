using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    public void SetSpeed (float speed)
    {
        _animator.SetFloat(ConstantsData.AnimatorParameters.Speed, speed);
    }
}
