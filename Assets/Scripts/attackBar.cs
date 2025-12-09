using UnityEngine;

public class attackBar : MonoBehaviour
{
    public GameObject attackPoint;
    private Rigidbody2D rb;
    private GameObject player;
    public float speed;
    private GameObject enemy;
    public Transform startPoint;
    public float delay;
    private float timer;
    private bool canHit = false;
    private GameObject GameManager;
    public int weakDamage; // if you attack way too early
    public int strongDamage; // if you land it near the box
    public int perfectDamage; // if you land it in the box
    private int damage;
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        rb = GetComponent<Rigidbody2D>();
        GameManager = GameObject.FindWithTag("Manager");
        damage = weakDamage;
    }
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Player>().canAttack = false;
    }
    void Update()
    {
        if (!canHit) // prevents player from accidentally attacking immediately
        {
            timer += Time.deltaTime;
        }
        if (timer > delay)
        {
            timer = 0;
            canHit = true;
        }
        if (player.GetComponent<Player>().attacking)
        {
            rb.linearVelocityX = -speed;
        }
        if (Input.GetKey(KeyCode.Z) && canHit) // hit
        {
            enemy.GetComponent<Boss>().TakeDamage(damage);
            player.GetComponent<Player>().attacking = false;
            player.GetComponent<Player>().canAttack = true;
            transform.position = startPoint.position;
            canHit = false;
            GameManager.GetComponent<GameManager>().playerTurn = false;
            damage = weakDamage;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "strong attack")
        {
            damage = strongDamage;
        }
        else if (collision.gameObject == attackPoint)
        {
            damage = perfectDamage;
        }
        if (collision.gameObject.tag == "killZone") // miss if the player doesn't press anything
        {
            player.GetComponent<Player>().attacking = false;
            player.GetComponent<Player>().canAttack = true;
            transform.position = startPoint.position;
            canHit = false;
            GameManager.GetComponent<GameManager>().playerTurn = false;
            enemy.GetComponent<Boss>().TakeDamage(0); // calling the damage function with 0 damage will make it display "MISS"
            damage = weakDamage;
        }
    }
}
