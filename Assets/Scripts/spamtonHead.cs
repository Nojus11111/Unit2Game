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
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = -speed;
        Player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootDelay)
        {
            Instantiate(bullet, shootPos.position, shootPos.rotation);
            timer = 0;
        }
        if (transform.position.x < 2 &&  transform.position.x > -1.5 && rb.linearVelocityX < -minSpeed)
        {
            rb.linearVelocityX += slowRate;
        }
        else if(rb.linearVelocityX > -speed)
        {
            rb.linearVelocityX -= slowRate;
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
