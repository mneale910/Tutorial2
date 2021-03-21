using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PlayerScript : MonoBehaviour
{

    private Rigidbody2D rd2d;

    public float speed;

    public Text score;

    public AudioClip One;

    public AudioClip Two;

    public AudioSource MusicPlayer;

    Animator anim;

    private int scoreValue = 0;

    public Text winText;

    public Text loseText;

    public Text lives;

    public Text countText;

    private int livesValue = 3;

    private bool facingRight = true;

    private bool jump = true;

    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = "Score:" + scoreValue.ToString();
        lives.text = "Lives:" + livesValue.ToString();
        winText.text = "";
        loseText.text = "";
        anim = GetComponent<Animator>();
        MusicPlayer.clip = One;
        MusicPlayer.Play();
        MusicPlayer.loop = true; 
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
          anim.SetInteger("State", 1);
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
          anim.SetInteger("State", 0);
        }
    }
    void FixedUpdate()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.AddForce(new Vector2(hozMovement * speed, vertMovement * speed));
        if (livesValue == 0)
        {
            loseText.text = "You Lose, Try Again (R) Created by Maxwell Neale";
            Destroy (gameObject);
        }
        if (facingRight == false && hozMovement > 0)
        {
            Flip();
        }
        else if (facingRight == true && hozMovement < 0)
        {
            Flip();
        }
        if (jump == false && vertMovement == 0)
        {
            anim.SetInteger("State", 0);
        }
        else if (jump == true && vertMovement > 0)
        {
            anim.SetInteger("State", 2);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       if (collision.collider.tag == "Coin")
        {
            scoreValue += 1;

            score.text = "Score:" + scoreValue.ToString();
            Destroy(collision.collider.gameObject);
            if (scoreValue == 4)
            {
                livesValue = 3;
                lives.text = "Lives:" + livesValue.ToString();
                transform.position = new Vector2 (76.69f, 1.04f);
            }
            if (scoreValue == 8)
            {
                MusicPlayer.clip = Two;
                MusicPlayer.Play();
                winText.text = "You Win! Game Created By Maxwell Neale";
            }
        }

        else if (collision.collider.tag == "Enemy")
        {
            livesValue -= 1;
            Destroy(collision.collider.gameObject);
            lives.text = "Lives:" + livesValue.ToString();
        }
        
    }

    void Flip()
   {
     facingRight = !facingRight;
     Vector2 Scaler = transform.localScale;
     Scaler.x = Scaler.x * -1;
     transform.localScale = Scaler;
   }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.AddForce(new Vector2(0, 3), ForceMode2D.Impulse);
            }
        }
    }
}