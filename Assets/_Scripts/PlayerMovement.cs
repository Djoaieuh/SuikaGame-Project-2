using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    [SerializeField] GameObject spawnPoint;

    GameObject currentFruit;
    float spawnCooldown;

    [SerializeField] float speed;

    void Start()
    {
        spawnCooldown = 1;
    }


    void Update()
    {
        if (GameManager.instance.isGameRunning)
        {
            if (transform.position.x < 3.78 && transform.position.x > -2.25)
            {
                transform.Translate(speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 0);
            }
            else if (transform.position.x > 3.78 && Input.GetAxisRaw("Horizontal") <= 0)
            {
                transform.Translate(speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 0);
            }
            else if (transform.position.x < -2.25 && Input.GetAxisRaw("Horizontal") >= 0)
            {
                transform.Translate(speed * Input.GetAxisRaw("Horizontal") * Time.deltaTime, 0, 0);
            }

            if (spawnCooldown < 1)
            {
                spawnCooldown += Time.deltaTime;
            }
            else if (transform.childCount == 1)
            {
                GameManager.instance.Refill();
            }

            if (Input.GetKeyDown("space") && spawnCooldown >= 1)
            {

                currentFruit = transform.GetChild(1).gameObject;

                currentFruit.GetComponent<FruitBehavior>().ActivateFruit();

                currentFruit.transform.parent = null;

                spawnCooldown = 0;

                PlayerPrefs.SetInt("fruitDrops", PlayerPrefs.GetInt("fruitDrops") + 1);

                SoundManager.instance.PlaySound("Drop");
            }
        }

    }
}
