using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerStats
{
    [SerializeField] public static int score;


    public static int GetScore()
    {
        return score;
    }

    public static void SetScore(int _score)
    {
        score = _score;
    }
}
