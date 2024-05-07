using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
    }


    [SerializeField] GameObject cherry, strawberry, grape, orange, apple, peach, pineapple, melon, watermelon, player, playerHand, spawnPoint, retryPanel;

    GameObject nextFruit, newFruit;

    [SerializeField] TMP_Text scoreText;


    int countCheck;
    int scoreMult;

    int randFruitID;
    int fruitCount;

    public bool isGameRunning;

    int score;

    void Start()
    {
        countCheck = 0;
        fruitCount = 0;

        isGameRunning = true;

        SpawnFruit();

        score = 0;
        scoreMult = 1;
    }


    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            GameOver();
        }
    }

    public void CombineFruits(GameObject fruit, Vector3 pos)
    {
        countCheck++;

        string fruitType = fruit.tag;

        if (fruit.GetComponent<FruitBehavior>().IsMarked())
        {
            scoreMult += 1;
        }

        if (countCheck == 2)
        {

            countCheck = 0;

            if (fruitType == "Cherry")
            {
                newFruit = Instantiate(strawberry, pos, Quaternion.identity);

                AddScore(5 * scoreMult);
            }
            else if (fruitType == "Strawberry")
            {
                newFruit = Instantiate(grape, pos, Quaternion.identity);

                AddScore(10 * scoreMult);
            }
            else if (fruitType == "Grape")
            {
                newFruit = Instantiate(orange, pos, Quaternion.identity);

                AddScore(20 * scoreMult);
            }
            else if (fruitType == "Orange")
            {
                newFruit = Instantiate(apple, pos, Quaternion.identity);

                AddScore(50 * scoreMult);
            }
            else if (fruitType == "Apple")
            {
                newFruit = Instantiate(peach, pos, Quaternion.identity);

                AddScore(100 * scoreMult);
            }
            else if (fruitType == "Peach")
            {
                newFruit = Instantiate(pineapple, pos, Quaternion.identity);

                AddScore(250 * scoreMult);
            }
            else if (fruitType == "Pineapple")
            {
                newFruit = Instantiate(melon, pos, Quaternion.identity);

                AddScore(500 * scoreMult);
            }
            else if (fruitType == "Melon")
            {
                newFruit = Instantiate(watermelon, pos, Quaternion.identity);

                AddScore(1000 * scoreMult);
            }

            if (score > PlayerPrefs.GetInt("highscore"))
            {
                PlayerPrefs.SetInt("highscore", score);
            }

            PlayerPrefs.SetInt("combinedFruits", PlayerPrefs.GetInt("combinedFruits") + 1);

            if (scoreMult > 1)
            {
                SoundManager.instance.PlaySound("Combo");
            }
            else
            {
                SoundManager.instance.PlaySound("Combine");
            }

            scoreMult = 1;

            newFruit.GetComponent<FruitBehavior>().ActivateFruit();
            newFruit.GetComponent<FruitBehavior>().MarkFruit();


        }
    }

    void SpawnFruit()
    {
        randFruitID = Random.Range(0, 101);

        if (fruitCount >= 21 && randFruitID >= 75)
        {
            nextFruit = Instantiate(orange, spawnPoint.transform.position, Quaternion.identity);
        }
        else if (fruitCount >= 14 && randFruitID >= 50)
        {
            nextFruit = Instantiate(grape, spawnPoint.transform.position, Quaternion.identity);
        }
        else if (fruitCount >= 7 && randFruitID >= 25)
        {
            nextFruit = Instantiate(strawberry, spawnPoint.transform.position, Quaternion.identity);
        }
        else
        {
            nextFruit = Instantiate(cherry, spawnPoint.transform.position, Quaternion.identity);
        }
        fruitCount++;
    }

    public void Refill()
    {
        nextFruit.transform.position = playerHand.transform.position;

        nextFruit.transform.SetParent(player.transform);

        SpawnFruit();
    }

    void AddScore(int bonus)
    {
        score += bonus;
    }

    public int GetScore()
    {
        return score;
    }

    public void GameOver()
    {
        isGameRunning = false;
        retryPanel.SetActive(true);
        scoreText.ClearMesh();
    }

    public void Reset()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
