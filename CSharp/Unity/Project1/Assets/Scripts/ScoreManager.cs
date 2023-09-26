using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {
    private static int score;
    private Text text;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        ResetPoints();
	}
	
	// Update is called once per frame
	void Update () {
        if (score < 0)
            score = 0;

        text.text = $"{score.ToString()}";
	}

    public static void AddPoints(int points)
    {
        score += points;
    }

    public static void RemovePoints(int points)
    {
        score -= points;
    }

    public static void ResetPoints()
    {
        score = 0;
    }
}
