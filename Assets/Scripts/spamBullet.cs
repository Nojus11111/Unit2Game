using UnityEngine;

public class spamBullet : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D rb;
    public float speed;
    public int damage;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soul")
        {
            Player.GetComponent<Player>().takeDamage(damage);
        }
    }
}
