using UnityEngine;

public class spamBullet : MonoBehaviour
{
    private GameObject Player;
    private Rigidbody2D rb;
    public float speed;
    public int damage;
    public float rotateSpeed;
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.up * speed;
    }
    private void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soul")
        {
            Player.GetComponent<Player>().takeDamage(damage);
        }
    }
}
