using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] GameObject statsMenu;

    [SerializeField] TMP_Text highscore, fruitsDrop, fruitsCombine;

    void Start()
    {

    }

    void Update()
    {
        highscore.text = PlayerPrefs.GetInt("highscore").ToString();
        fruitsDrop.text = PlayerPrefs.GetInt("fruitDrops").ToString();
        fruitsCombine.text = PlayerPrefs.GetInt("combinedFruits").ToString();
    }

    public void GoToGame()
    {
        SceneManager.LoadScene(1);
    }

    public void OpenStats()
    {
        statsMenu.SetActive(true);
    }

    public void CloseStats()
    {
        statsMenu.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
