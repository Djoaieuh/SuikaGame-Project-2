using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    TMP_Text scoreText;

    void Start()
    {
        scoreText = GetComponent<TMP_Text>();
    }


    void Update()
    {
        scoreText.text = GameManager.instance.GetScore().ToString();
    }
}
