using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrigger : MonoBehaviour
{
    [SerializeField] int scoreMultiplier = 1;

    private void OnTriggerEnter(Collider other)
    {
        ScoreManager._instance.AddScore(scoreMultiplier);
    }
}
