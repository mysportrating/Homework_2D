using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Animator))]

public class SwitchBehaviourScript : MonoBehaviour, IInteractable
{
    private Animator _animator;
    public bool IsActive {  get; private set; }

    private void Awake()
    {
        _animator = GetComponent<Animator> ();
    }

    public void Interact()
    {
        IsActive = !IsActive;

        // Запускаем анимацию при взаимодействии с Switch
        if (IsActive)
            _animator.SetTrigger(ConstantsData.AnimatorParameters.IsOn);
        else
            _animator.SetTrigger(ConstantsData.AnimatorParameters.IsOff);
    }
}
