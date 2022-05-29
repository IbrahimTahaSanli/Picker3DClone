using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhasePlatformController : AnimAbstractClass
{
    [SerializeField] private AnimAbstractClass Left;
    [SerializeField] private AnimAbstractClass Right;

    [SerializeField] private AnimAbstractClass FakePlatform;

    [SerializeField] private Color FailColor;
    [SerializeField] private Color SuccessColor;

    [SerializeField] private TMPro.TMP_Text currentBall;
    [SerializeField] private TMPro.TMP_Text NeededBall;
    [SerializeField] private TMPro.TMP_Text sepperatorBall;

    [SerializeField] public float difficulty = 1.0f;

    [SerializeField] private GameObject CheckPoint;


    public uint ballCount { get; private set; } = 0;

    public uint neededCount { get; private set; } = 0;
    public uint BallInPhaseCount { get; private set; } = 0;

    public bool IsPhasePassed { get; private set; } = false;

    public void setBallInPhase(uint i)
    {
        BallInPhaseCount = i;

        IsPhasePassed = i == 0 ? true : false;

        neededCount = (uint)Mathf.FloorToInt(BallInPhaseCount * difficulty);

        this.NeededBall.text = neededCount.ToString();
        
        if (ballCount >= neededCount)
            this.colorTexts(SuccessColor);
    }
    public void AddBall(uint i)
    {
        ballCount++;
        this.currentBall.text = this.ballCount.ToString();

        if (ballCount >= neededCount)
        {
            this.colorTexts(SuccessColor);
            this.IsPhasePassed = true;
        }
    }

    private void colorTexts(Color col)
    {
        this.currentBall.color = col;
        this.sepperatorBall.color = col;
        this.NeededBall.color = col;
    }



    public override void PlayAnim()
    {
        CheckPoint.SetActive(false);
        Left.PlayAnim();
        Right.PlayAnim();
        FakePlatform.PlayAnim();
    }
}
