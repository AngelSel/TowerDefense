using System;
using UnityEngine;


public class ScoreManager: IScoreManager
{
    public event  Action<int> OnScoreUpdate = delegate(int score) {  };
    private int m_currentScore = 0;

    public int CurrentScore
    {
        get => m_currentScore;
        set
        {
            m_currentScore = value;
        }
    }

    public void AddScore(int score)
    {
        CurrentScore += score;
        OnScoreUpdate.Invoke(m_currentScore);
    }

    public void InitScore()
    {
        CurrentScore = 0;
    }

    public void PrintScore()
    {
        Debug.Log(CurrentScore);
    }
}
