using UnityEngine;
using UnityEngine.EventSystems;

public class Soul : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public GameObject shot;
    public GameObject bigShot;
    public float chargeTime;
    private float chargeTimer;
    public float shootDelay;
    private float shootTimer;
    public GameObject chargeIndicator;
    public AudioSource soundPlayer;
    public AudioSource loopPlayer;
    public AudioClip shotSound;
    public AudioClip bigShotSound;
    public AudioClip chargeSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        loopPlayer.clip = chargeSound;
    }
    void Update()
    {
        // movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed); // rb.linearVelocity should be fine here since the only physical collision in this game is between the soul and the battlebox walls, eveything else uses triggers

        // shooting
        if (shootTimer <= shootDelay)
        {
            shootTimer += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Z) && shootTimer > shootDelay) // charge
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer > chargeTime)
            {
                chargeIndicator.SetActive(true);
                if (!loopPlayer.isPlaying)
                {
                    loopPlayer.Play();
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Z) && shootTimer > shootDelay) // release/shoot
        {
            loopPlayer.Stop();
            if (chargeTimer > chargeTime) // charge shot
            {
                Instantiate(bigShot, transform.position, transform.rotation);
                chargeIndicator.SetActive(false);
                soundPlayer.clip = bigShotSound;
                soundPlayer.Play();
            }
            else // regular shot
            {
                Instantiate(shot, transform.position, transform.rotation);
                soundPlayer.clip = shotSound;
                soundPlayer.Play();
            }
            chargeTimer = 0;
            shootTimer = 0;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // if the player was charging at the end of the previous attack, this will stop them from retaining that charge
    {
        if (collision.gameObject.tag == "white screen")
        {
            loopPlayer.Stop();
            soundPlayer.Stop();
            chargeTimer = 0;
            chargeIndicator.SetActive(false);
        }
    }
}
