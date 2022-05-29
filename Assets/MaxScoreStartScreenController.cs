using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxScoreStartScreenController : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text m_text;

    private void Awake()
    {
        ScoreManager._instance.AddMaxScoreChangedChangeEvent(ChangeText);
    }

    private void OnDisable()
    {
        ScoreManager._instance.SetSuppressionMaxScoreChangedEvent(ChangeText, true);
    }

    private void OnEnable()
    {
        ScoreManager._instance.SetSuppressionMaxScoreChangedEvent(ChangeText, false);
        ChangeText(ScoreManager._instance.maxScore);
    }

    private void ChangeText(int val)
    {
        m_text.text = val.ToString();
    }
}
