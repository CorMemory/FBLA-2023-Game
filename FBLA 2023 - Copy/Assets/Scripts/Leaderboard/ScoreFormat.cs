using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreFormat : MonoBehaviour
{
    public TMP_Text scoreText;

    public void Start()
    {
        float currentTime = float.Parse(scoreText.text);
        float minutes = Mathf.FloorToInt(currentTime / 60);
        float seconds = Mathf.FloorToInt(currentTime % 60);

        scoreText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
