using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb; 
    private int count;
    public float gameTime = 30;
    private bool gameOver = false;
    private float movementX;
    private float movementY;
    public float speed = 0; 
    public TextMeshProUGUI countText;
    public TextMeshProUGUI speedText;
    public TextMeshProUGUI timerText;
    public GameObject loseTextObject;
    public GameObject winTextObject;

    // Start is called before the first frame update
    void Start()
    {
        count = 0;
        rb = GetComponent<Rigidbody>(); 
        SetCountText();
        SetSpeedText();
        SetTimerText();
        winTextObject.SetActive(false);
        loseTextObject.SetActive(false);
    }

    void OnMove (InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x; 
        movementY = movementVector.y; 
    }

    void SetCountText() 
    {
        countText.text =  "Count: " + count.ToString();
        if (count >= 12)
        {
            winTextObject.SetActive(true);
        }
    }

    void SetSpeedText() 
    {
        speedText.text =  "Speed: " + speed.ToString();
    }

    void SetTimerText()
    {
        timerText.text = "Time: " + Mathf.Round(gameTime).ToString() + "s";
    }
    
    private void FixedUpdate() 
    {
        if (!gameOver) {
            Vector3 movement = new Vector3 (movementX, 0.0f, movementY);

            rb.AddForce(movement * speed);

            gameTime -= Time.deltaTime;
            SetTimerText();

            if (gameTime <= 0)
            {
                gameTime = 0;
                gameOver = true;
                loseTextObject.SetActive(true);
            }
        }
    }

    void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.CompareTag("PickUp")) 
        {
            other.gameObject.SetActive(false);
            count = count + 1;
            SetCountText();
        }
        if (other.gameObject.CompareTag("SpeedUp")) 
        {
            other.gameObject.SetActive(false);
            speed = speed * 2;
            SetSpeedText();
        }
        if (other.gameObject.CompareTag("SpeedDown")) 
        {
            other.gameObject.SetActive(false);
            speed = speed / 2;
            SetSpeedText();
        }
        if (other.gameObject.CompareTag("Heal")) 
        {
            other.gameObject.SetActive(false);
            speed = 10;
            SetSpeedText();
        }
    }
}
