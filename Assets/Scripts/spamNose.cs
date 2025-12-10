using UnityEngine;

public class spamNose : MonoBehaviour
{
    private Rigidbody2D rb;
    public int direction;
    public float speed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(0, direction));
    }
    private void Update()
    {
        rb.linearVelocityX = -speed;
    }
}
