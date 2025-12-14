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
    private AudioSource soundPlayer;
    public AudioClip tick;
    public AudioClip deathSound;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindWithTag("Player");
        rb.linearVelocityX = -speed;
        soundPlayer = GetComponent<AudioSource>();
        soundPlayer.clip = tick;
    }
    void Update()
    {
        if (counting) // if the bomb gets shot, explode after a delay
        {
            if (!soundPlayer.isPlaying) // plays ticking sound until the bomb explodes
            {
                soundPlayer.Play();
            }
            timer += Time.deltaTime;
            if (timer >  explosionDelay)
            {
                GameObject.FindWithTag("Enemy").GetComponent<Boss>().playSound(deathSound);
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
            if (collision.GetComponent<bullet>().bigShot == false) // only destroys regular shots, allowing big shots to pierce
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
