using UnityEngine;

public class PlayerAnimController
{
    private Animator _animator;
    private const float _transitionDuration = 0.1f;

    public static readonly int Idle = Animator.StringToHash("Idle");
    public static readonly int Run = Animator.StringToHash("Run");
    public static readonly int Victory = Animator.StringToHash("Victory");


    public PlayerAnimController(Animator animator)
    {
        _animator = animator;
    }

    public void PlayAnim(int animHash, float transitionTime = _transitionDuration)
    {
        _animator.CrossFade(animHash, _transitionDuration);
    }

    public void SetBlendValue(float value)
    {
        _animator.SetFloat("BlendValue", value);
    }
}