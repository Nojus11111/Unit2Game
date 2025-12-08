using UnityEngine;

public class bomb : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameObject player;
    public float speed;
    public int damage;
    public GameObject explosion;
    private bool counting = false;
    private float timer;
    public float explosionDelay;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        rb.linearVelocityX = -speed;
    }
    void Update()
    {
        if (counting)
        {
            timer += Time.deltaTime;
            if (timer >  explosionDelay)
            {
                Instantiate(explosion, transform.position, transform.rotation);
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            counting = true;
            if (collision.GetComponent<bullet>().bigShot == false)
            {
                Destroy(collision.gameObject);
            }
        }
        if (collision.gameObject.tag == "Soul")
        {
            player.GetComponent<Player>().takeDamage(damage);
        }
    }
}
