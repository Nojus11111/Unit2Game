using UnityEngine;

public class spamWisp : MonoBehaviour
{
    public float minSpeed;
    public float maxSpeed;
    private float speed;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = Random.Range(minSpeed, maxSpeed);
        rb.AddForce(transform.up * speed);
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
