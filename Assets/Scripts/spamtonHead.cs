using UnityEngine;

public class spamtonHead : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public float shootDelay;
    private float timer;
    public GameObject bullet;
    public Transform shootPos;
    public float minSpeed;
    public float slowRate;
    private GameObject Player;
    public int damage;
    public float speed2;
    public bool useSpeed2;
    private GameObject gameManager;
    private Animator animator;
    void Start()
    {
        gameManager = GameObject.FindWithTag("Manager");
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        if (gameManager.GetComponent<GameManager>().turn == 2) // changes speed on the second attack
        {
            useSpeed2 = true;
            GetComponent<despawner>().lifetime = 3;
            shootDelay += 0.35f;
        }
        else
        {
            useSpeed2 = false;
        }
        if (!useSpeed2)
        {
            rb.linearVelocityX = -speed;
        }
        else
        {
            rb.linearVelocityX = -speed2;
        }
        Player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        // shooting
        timer += Time.deltaTime;
        if (timer > shootDelay * 0.8)
        {
            animator.Play("shoot");
        }
        if (timer > shootDelay)
        {
            Instantiate(bullet, shootPos.position, shootPos.rotation);
            timer = 0;
        }
        
        if (!useSpeed2)
        {
            if (transform.position.x < 3 && transform.position.x > -1.5 && rb.linearVelocityX < -minSpeed) // slows down when near the box
            {
                rb.linearVelocityX += slowRate;
            }
            else if (rb.linearVelocityX > -speed) // speeds back up when leaving the designated area
            {
                rb.linearVelocityX -= slowRate;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(gameObject);
            if (collision.GetComponent<bullet>().bigShot == false)
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "Soul")
        {
            Player.GetComponent<Player>().takeDamage(damage);
        }
    }
}
