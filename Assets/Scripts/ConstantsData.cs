using UnityEngine;

public static class ConstantsData
{
    // Данные для Аниматора
    public static class AnimatorParameters
    {
        public static readonly int Speed = Animator.StringToHash(nameof(Speed));
        public static readonly int IsOn = Animator.StringToHash(nameof(IsOn));
        public static readonly int IsOff = Animator.StringToHash(nameof(IsOff));
    }

    // Данные для ввода с клавиатуры
    public static class InputData
    {
        public const string HORIZONTAL_AXIS = "Horizontal";
        public const string VERTICAL_AXIS = "Vertical";
    }
}
