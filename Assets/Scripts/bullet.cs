using UnityEngine;

public class bullet : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public bool bigShot;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocity = transform.right * speed;
    }
}
