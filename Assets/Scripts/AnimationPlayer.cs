using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    public Animator animator;

    public void AnimationMove(float value)
    {
        animator.SetFloat("Move", value);
    }

    public void AnimationJump(bool canJump)
    {
        animator.SetBool("Jump", canJump);
    }
}
