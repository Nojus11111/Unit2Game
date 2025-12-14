using UnityEngine;

public class wall : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;
    public bool isWall;
    public AudioSource soundPlayer;
    public AudioClip ding;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.linearVelocityX = -speed;
    }
    private void OnTriggerEnter2D(Collider2D collision) // destroys any bullets it touches
    {
        if (collision.gameObject.tag == "bullet")
        {
            Destroy(collision.gameObject);
            if (isWall)
            {
                soundPlayer.clip = ding;
                soundPlayer.Play();
            }
        }
    }
}
