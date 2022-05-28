using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasePlatformController : AnimAbstractClass
{
    [SerializeField] private AnimAbstractClass Left;
    [SerializeField] private AnimAbstractClass Right;

    [SerializeField] private AnimAbstractClass FakePlatform;

    public override void PlayAnim()
    {
        Left.PlayAnim();
        Right.PlayAnim();
        FakePlatform.PlayAnim();
    }
}
