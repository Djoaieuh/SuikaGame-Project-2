using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class FruitBehavior : MonoBehaviour
{

    float timer;
    float markTimer;

    bool isOOB;
    bool isActive;
    bool isMarked = false;

    void Start()
    {
        isOOB = false;
        isActive = false;
        timer = 0;
        markTimer = 0;
    }

    void Update()
    {
        if (isOOB && isActive)
        {
            timer += Time.deltaTime;
        }

        if (timer > 5)
        {
            GameManager.instance.GameOver();
        }

        if (isMarked)
        {
            markTimer = markTimer + Time.deltaTime;

            if (markTimer >= 2)
            {
                isMarked = false;
                markTimer = 0;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.transform.position.x == gameObject.transform.position.x)
        {
            transform.position = new Vector3(transform.position.x + 0.01f, transform.position.y, transform.position.z);
        }

        if (collision.gameObject.CompareTag(gameObject.tag) && !gameObject.CompareTag("Watermelon"))
        {
            float newX = (gameObject.transform.position.x + collision.transform.position.x) / 2;
            float newY = ((gameObject.transform.position.y + collision.transform.position.y) / 2);

            Vector3 newPos = new Vector3(newX, newY, 0);

            GameManager.instance.CombineFruits(gameObject, newPos);

            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            isOOB = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Limit"))
        {
            isOOB = false;
            timer = 0;
        }
    }


    public void ActivateFruit()
    {
        isActive = true;
        gameObject.AddComponent<Rigidbody2D>();
    }

    public void MarkFruit()
    {
        isMarked = true;
    }

    public bool IsMarked()
    {
        return isMarked;
    }
}
