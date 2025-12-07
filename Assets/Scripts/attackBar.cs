using UnityEngine;

public class attackBar : MonoBehaviour
{
    public GameObject attackPoint;
    private Rigidbody2D rb;
    private GameObject player;
    public float speed;
    bool damaging;
    private GameObject enemy;
    public Transform startPoint;
    public float delay;
    private float timer;
    private bool canHit = false;
    private GameObject GameManager;
    void Start()
    {
        enemy = GameObject.FindWithTag("Enemy");
        rb = GetComponent<Rigidbody2D>();
        GameManager = GameObject.FindWithTag("Manager");
    }
    private void OnEnable()
    {
        player = GameObject.FindWithTag("Player");
        player.GetComponent<Player>().canAttack = false;
    }
    void Update()
    {
        if (!canHit)
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
        if (Input.GetKey(KeyCode.Z) && damaging && canHit)
        {
            enemy.GetComponent<Boss>().TakeDamage(5);
            player.GetComponent<Player>().attacking = false;
            player.GetComponent<Player>().canAttack = true;
            transform.position = startPoint.position;
            canHit = false;
            GameManager.GetComponent<GameManager>().playerTurn = false;
        }
        if (Input.GetKey(KeyCode.Z) && !damaging && canHit)
        {
            player.GetComponent<Player>().attacking = false;
            player.GetComponent<Player>().canAttack = true;
            transform.position = startPoint.position;
            canHit = false;
            GameManager.GetComponent<GameManager>().playerTurn = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == attackPoint)
        {
            damaging = true;
        }
        if (collision.gameObject.tag == "killZone")
        {
            player.GetComponent<Player>().attacking = false;
            player.GetComponent<Player>().canAttack = true;
            transform.position = startPoint.position;
            canHit = false;
            GameManager.GetComponent<GameManager>().playerTurn = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == attackPoint)
        {
            damaging = false;
        }
    }
}
