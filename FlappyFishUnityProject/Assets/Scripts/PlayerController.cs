using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour
{

    //[HideInInspector] public bool 

    private float jumpForce = 250f;
    private float jumpRate = 0.1f;
	private float maxYVelocity = 2.75f;

    public AudioClip[] FlapSounds;

    private AudioSource playerAudioSource;
    private Rigidbody2D rb2d;
    private Animator playerAnimator;
    private bool areWeJumping = false;
    private float timeTillNextJumpAllowed;


    private bool inputReceived;

    // Use this for initialization
    void Start()
    {
        // assign our rigidbody 2d
        rb2d = GetComponent<Rigidbody2D>();

        rb2d.AddForce(new Vector2(0f, 200f));
        // rb2d.AddForce(new Vector2(0f, -200f));

        // apply our starting forward force -> note: this failed with adding a box collider cieling; was 100f
        //rb2d.AddForce(new Vector2(horizontalStartingForce, 0f));

        playerAudioSource = GetComponent<AudioSource>();

        playerAnimator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        inputReceived = false;
        foreach (Touch touch in Input.touches)
        {
            if( touch.phase == TouchPhase.Began )
            {
                inputReceived = true;
                break;
            }
        }

        if (inputReceived == false && (Input.GetButtonDown("Jump") || Input.GetMouseButtonDown(0)))
        {
            inputReceived = true;
        }


        if (inputReceived &&
                Time.time > timeTillNextJumpAllowed &&
                !GameSingleton.instance.IsGameOver)
        {
			//print ("---------Jump Pressed");
            areWeJumping = true;
            timeTillNextJumpAllowed = Time.time + jumpRate;


            // Play Jump sound
            if (FlapSounds.Length > 0)
            {
                int iSoundIndex = Random.Range(0, FlapSounds.Length);

                playerAudioSource.clip = FlapSounds[iSoundIndex];

                playerAnimator.SetTrigger("birdFlapWings");
            }
            playerAudioSource.Play();
        }

        if (inputReceived &&
                GameSingleton.instance.CanStartGameOver)
        {

            // start the game afresh
            GameSingleton.instance.RestartGame();
        }

    }

    void FixedUpdate()
    {
        if (!GameSingleton.instance.IsGameOver)
        {
            //apply our fixed forward movement speed
            Vector2 newVelocity = rb2d.velocity;
            newVelocity.x = ScoreManager.instance.PlayersForwardSpeed;
            rb2d.velocity = newVelocity;
        }


        if (areWeJumping == true)
        {
            areWeJumping = false;
			
            rb2d.AddForce(new Vector2(0f, jumpForce));
			//print("====================applying force");
        }

		//print ("Y Veloc: " + rb2d.velocity.y);

		if (rb2d.velocity.y > maxYVelocity)
		{
			rb2d.velocity = new Vector2(rb2d.velocity.x, maxYVelocity);
		}


        // attempt at fixing the Y coordinates position; didn't work; MaxY was 3.5f
        /*
        if (transform.position.y > maxY)
        {
            transform.position = new Vector3(transform.position.x, maxY, transform.position.z);
        }
        */


    }

    public void OnDrawGizmos()
    {
        Vector2 velocity = GetComponent<Rigidbody2D>().velocity;

        Vector3 RayDirection = new Vector3(velocity.x, velocity.y, 0.0f);
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, RayDirection);

    }

}
