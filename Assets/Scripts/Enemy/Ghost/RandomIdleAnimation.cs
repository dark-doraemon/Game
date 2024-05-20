using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomIdleAnimation : MonoBehaviour
{
    private Animator ghostAnimator;

    private void Awake()
    {
        ghostAnimator = GetComponent<Animator>();
    }

    private void Start()
    {
        if (!ghostAnimator)
        {
            return;
        }

        AnimatorStateInfo state = ghostAnimator.GetCurrentAnimatorStateInfo(0);
        ghostAnimator.Play(state.fullPathHash,-1,Random.Range(0.0f, 1.0f)); 
    }
}
