using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScoreManager : MonoBehaviour
{
    const string MAXSCOREKEY = "max_score_key";

    public static ScoreManager _instance;

    public int score { get; private set; } = 0;
    public int maxScore { get; private set; } = 0;


    public void Awake()
    {
        _instance = this;


        this.ScoreAddedEvents = new Dictionary<ScoreAddedEvent, bool>();
        this.AddMaxScoreChangedChangeEvent(
            (val) => {
                this.maxScore = val;
                PlayerPrefs.SetInt(MAXSCOREKEY, val);
                }
        );

        this.MaxScoreChangedEvents = new Dictionary<MaxScoreChangedEvent, bool>();
        this.AddScoreChangeEvent(
            (val) => {
                if (this.maxScore < val)
                    this.SetMaxScore(val);
                }
            );


        if (PlayerPrefs.HasKey(MAXSCOREKEY))
            this.SetMaxScore(PlayerPrefs.GetInt(MAXSCOREKEY));
        else
            PlayerPrefs.SetInt(MAXSCOREKEY, 0);

    }

    #region MaxScoreChangedEvent

    public delegate void MaxScoreChangedEvent(int changedScore);
    //Boolean for temporary disable event
    private Dictionary<MaxScoreChangedEvent, bool> MaxScoreChangedEvents;
    public void AddMaxScoreChangedChangeEvent(MaxScoreChangedEvent even, bool isSuspended = false)
    {
        MaxScoreChangedEvents[even] = isSuspended;
    }
    public void SetSuppressionMaxScoreChangedEvent(MaxScoreChangedEvent even, bool isSuspended)
    {
        MaxScoreChangedEvents[even] = isSuspended;
    }
    public void RemovePlatformMaxScoreChangedEvent(MaxScoreChangedEvent even)
    {
        MaxScoreChangedEvents.Remove(even);
    }

    public void SetMaxScore(int val)
    {
        this.maxScore = val;

        foreach (var elem in this.ScoreAddedEvents.Keys)
            if (!this.ScoreAddedEvents[elem])
                elem(val);
    }
    #endregion

    #region ScoreAddedEvent
    public delegate void ScoreAddedEvent(int changedScore);
    //Boolean for temporary disable event
    private Dictionary<ScoreAddedEvent, bool> ScoreAddedEvents;
    public void AddScoreChangeEvent(ScoreAddedEvent even, bool isSuspended = false)
    {
        ScoreAddedEvents[even] = isSuspended;
    }
    public void SetSuppressionScoreChangeEvent(ScoreAddedEvent even, bool isSuspended)
    {
        ScoreAddedEvents[even] = isSuspended;
    }
    public void RemovePlatformScoreChangeEvent(ScoreAddedEvent even)
    {
        ScoreAddedEvents.Remove(even);
    }

    public void AddScore(int val)
    {
        this.score += val;

        foreach (var elem in this.ScoreAddedEvents.Keys)
            if (! this.ScoreAddedEvents[elem])
                elem(val);
    }
    #endregion
}
