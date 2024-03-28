using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatedInteractive : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] Animator targetAnimator;
    [SerializeField] string functionName;
    bool debugBool1;
    bool debugBool2;

    public string ReturnFunctionName()
    {
        return functionName;
    }

    public void ChangeBool(string boolname, bool value)
    {
        targetAnimator.SetBool(boolname, value);
    }

    public void ChangeBoolBasedOnAnimatorBool(string boolname)
    {
        debugBool1 = !debugBool1;

        targetAnimator.SetBool(boolname, debugBool1);
    }

    public void PlayAnimation(string animationname)
    {
        targetAnimator.Play(animationname);
    }
}