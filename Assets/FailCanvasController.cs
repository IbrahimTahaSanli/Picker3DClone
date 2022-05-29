using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FailCanvasController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text MaxScoreText;
    [SerializeField] private TMPro.TMP_Text ScoreText;
    [SerializeField] private string MaxScoreLabel;
    [SerializeField] private string ScoreLabel;


    private void Awake()
    {
        ScoreManager._instance.AddMaxScoreChangedChangeEvent(MaxScoreEvent);
        ScoreManager._instance.AddScoreChangeEvent(ScoreEvent);
    }

    private void OnDisable()
    {
        ScoreManager._instance.SetSuppressionMaxScoreChangedEvent(MaxScoreEvent, true);
        ScoreManager._instance.SetSuppressionScoreChangeEvent(ScoreEvent, true);

    }

    private void OnEnable()
    {
        ScoreManager._instance.SetSuppressionMaxScoreChangedEvent(MaxScoreEvent, false);
        ScoreManager._instance.SetSuppressionScoreChangeEvent(ScoreEvent, false);
        MaxScoreText.text = MaxScoreLabel + ScoreManager._instance.maxScore.ToString();
        ScoreText.text = ScoreLabel + ScoreManager._instance.score.ToString();
    }

    private void MaxScoreEvent(int val)
    {
        MaxScoreText.text = MaxScoreLabel + val.ToString();
    }

    private void ScoreEvent(int val)
    {
        ScoreText.text = ScoreLabel + val.ToString();
    }

}
