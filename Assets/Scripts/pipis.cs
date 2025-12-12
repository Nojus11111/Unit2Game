using UnityEngine;

public class pipis : MonoBehaviour
{
    public float rotateSpeed;
    public float minForce;
    public float maxForce;
    private float force;
    private Rigidbody2D rb;
    public int hp;
    public Transform shootPos;
    public GameObject miniSpam;
    public int miniSpamCount;
    public float minSpread;
    public float maxSpread;
    private float spread;
    private Quaternion ogRotation;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        force = Random.Range(minForce, maxForce);
        rb.linearVelocity = transform.up * force;
    }
    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            ogRotation = shootPos.rotation;
            for (int i = 0; i < miniSpamCount;)
            {
                spread = Random.Range(minSpread, maxSpread);
                shootPos.rotation = shootPos.rotation * Quaternion.Euler(0, 0, spread);
                Instantiate(miniSpam, shootPos.position, shootPos.rotation);
                shootPos.rotation = ogRotation;
                i++;
            }
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            if (collision.gameObject.GetComponent<bullet>().bigShot == true)
            {
                Destroy(gameObject);
            }
            else
            {
                Destroy(collision.gameObject);
                hp -= 1;
                if (hp <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
