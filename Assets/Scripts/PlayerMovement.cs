using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D  rb;
    public Animator anim;

    private float hozMove;
    private float verMove;
    public float moveSpeed;
    private bool facingRight = false;

    private float score = 0;
    public Text keyText;
    public GameObject winText;
    public GameObject loseText;

    [SerializeField] private float mainTimer;
    [SerializeField] private float timer;
    private bool canCount = true;
    private bool doOnce;
    private bool timerIsActivce = true;
    [SerializeField] private Text timerText;

    private bool keyCollected = false;
    public GameObject collectEffect;

    public AudioSource audioSource;
    public AudioClip loseSound;
    public AudioClip winSound;
    public AudioClip collectSound;

    public bool gameWon = false;
    public bool gameLost = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        winText.SetActive(false);
        loseText.SetActive(false);

        timer = mainTimer;
    }

    void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    // Update is called once per frame
    void Update()
    {
        hozMove = Input.GetAxis("Horizontal");
        verMove = Input.GetAxis("Vertical");

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        if(score >= 5)
        {
            winText.SetActive(true);
            gameObject.SetActive(false);    
        }

        if(timerIsActivce == true && gameWon == false)
        {
            if(timer >= 0.0f && canCount == true)
            {
                timer -= Time.deltaTime;
                timerText.text = "Time Left: " + timer.ToString("F");
            }

            else if(timer <= 0.0f && !doOnce)
            {
                canCount = false;
                doOnce = true;
                timerText.text = "Time Left: 0.0";
                timer = 0;

                gameLost = true;
                loseText.SetActive(true);
                PlaySound(loseSound);
                rb.constraints = RigidbodyConstraints2D.FreezePosition;
             }
        }

        if (!facingRight && hozMove > 0)
		{
			// ... flip the player.
			Flip();
		}
		// Otherwise if the input is moving the player left and the player is facing right...
		else if (facingRight && hozMove < 0)
		{
			// ... flip the player.
			Flip();
		}

        if(Input.GetAxis("Horizontal") > 0 || Input.GetAxis("Horizontal") < 0)
        {
            anim.SetBool("isRunning", true);
        }
        else 
            anim.SetBool("isRunning", false);
    }

    void FixedUpdate()
    {
        Vector2 position = transform.position;
        position.x = position.x + moveSpeed * hozMove * Time.deltaTime;
        position.y = position.y + moveSpeed * verMove * Time.deltaTime;  

        rb.MovePosition(position);      
    }

    void OnTriggerEnter2D (Collider2D collider)
    {
        if(collider.GetComponent<Collider2D>().tag == "Key")
        {
            PlaySound(collectSound);
            keyCollected = true;
            Destroy(collider.GetComponent<Collider2D>().gameObject);
            GameObject collectEffectPrefab = Instantiate(collectEffect, rb.position, Quaternion.identity);  
            keyText.text = "Escape!"; 
        }

        if(collider.GetComponent<Collider2D>().tag == "Exit" && keyCollected == true)
        {
            gameWon = true;
            PlaySound(winSound);
            winText.SetActive(true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
    }
}
