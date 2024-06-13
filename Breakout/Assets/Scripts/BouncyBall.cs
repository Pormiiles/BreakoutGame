using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BouncyBall : MonoBehaviour
{
    public float minY = -5.5f;
    public float maxVelocity = 15f;

    Rigidbody2D rb;

    public TextMeshProUGUI scoreText;
    public GameObject[] livesImage;

    int score = 0;
    int lives = 5;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Vector2.down * 10f;
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.y < minY)
        {
            if(lives <= 0)
            {
                GameOver();
            } else
            {
                transform.position = Vector3.zero;
                rb.velocity = Vector2.down * 10f;
                lives--;
                livesImage[lives].SetActive(false);
            }
        }

        if(rb.velocity.magnitude > maxVelocity)
        {
            rb.velocity = Vector3.ClampMagnitude(rb.velocity, maxVelocity);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Brick"))
        {
            Destroy(collision.gameObject);
            score += 10;
            scoreText.text = score.ToString("00000");
        }
    }

    void GameOver()
    {
        Time.timeScale = 0;
        Destroy(gameObject);
    }
}
