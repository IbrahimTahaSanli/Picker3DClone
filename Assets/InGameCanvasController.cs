using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameCanvasController : MonoBehaviour
{
    [SerializeField] TMPro.TMP_Text m_Text;

    private void Awake()
    {
        m_Text.text = ScoreManager._instance.score.ToString();

        ScoreManager._instance.AddScoreChangeEvent(
            (val) =>
            {
                m_Text.text = val.ToString();
            });
        
    }
}
