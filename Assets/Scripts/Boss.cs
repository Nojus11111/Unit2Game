using System.Collections;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public int health;
    public float damageDisplayTime;
    private float timer;
    private bool hit;
    public GameObject damageNumber;
    private float attackDuration;
    private GameObject GameManager;
    public GameObject battleBox;
    public GameObject spamHead;
    public Transform spawnPos;
    public float spawnDelay;
    public float spawnGap;
    private Vector3 position;
    private bool attackStarted;
    private GameObject player;
    public GameObject wall;
    public GameObject bomb;
    public GameObject soul;
    public float attackDelay;
    [HideInInspector] public float delayTimer;
    private Animator animator;
    public float shakeSpeed;
    public Transform minShake;
    public Transform maxShake;
    private Transform target;
    public GameObject faceAttack;
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
        attackDuration = 8;
        attackStarted = false;
        player = GameObject.FindWithTag("Player");
        animator = GetComponent<Animator>();
        target = maxShake;
    }
    void Update()
    {
        // disable damage number
        if (hit)
        {
            if (timer < damageDisplayTime / 2) // makes the boss shake when hit
            {
                transform.position = Vector2.MoveTowards(this.transform.position, target.position, shakeSpeed * Time.deltaTime);
                if (transform.position == maxShake.position)
                {
                    target = minShake;
                }
                if (transform.position == minShake.position)
                {
                    target = maxShake;
                }
            }
            animator.Play("Pause");
            timer += Time.deltaTime;
            if (timer > damageDisplayTime)
            {
                damageNumber.SetActive(false);
                timer = 0;
                hit = false;
            }
        }
        else
        {
            animator.Play("Idle");
        }
        // attacks
        if (GameManager.GetComponent<GameManager>().playerTurn == false)
        {
            delayTimer += Time.deltaTime;
            if (delayTimer < attackDelay)
            {
                return;
            }
            if (player.GetComponent<Player>().blocking == false)
            {
                player.GetComponent<Animator>().Play("Idle");
            }
            battleBox.SetActive(true);
            attackDuration -= Time.deltaTime;
            if (!attackStarted && GameManager.GetComponent<GameManager>().turn == 1)
            {
                StartCoroutine(Attack1());
                attackStarted = true;
            }
            if (!attackStarted && GameManager.GetComponent<GameManager>().turn == 2)
            {
                StartCoroutine(Attack2());
                attackStarted = true;
                attackDuration = 10;
            }
            if (!attackStarted && GameManager.GetComponent<GameManager>().turn == 3)
            {
                faceAttack.SetActive(true);
                attackStarted = true;
                attackDuration = 8;
            }
            if (attackDuration < 0)
            {
                // end enemy turn
                GameManager.GetComponent<GameManager>().playerTurn = true;
                soul.transform.position = battleBox.transform.position; // puts soul back in the middle of the battlebox
                battleBox.SetActive(false);
                attackStarted = false;
                player.GetComponent<Player>().blocking = false;
                player.GetComponent<Player>().actionDelay = 0;
                player.GetComponent<Player>().actionUI.SetActive(true);
                GameManager.GetComponent<GameManager>().turn += 1;
                delayTimer = 0;
                player.GetComponent<Animator>().Play("Idle");
                faceAttack.SetActive(false);
            }
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == true)
        {
            StopAllCoroutines();
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        damageNumber.SetActive(true); // display damage number
        if (damage == 0)
        {
            damageNumber.GetComponent<TextMesh>().text = "MISS";
        }
        else
        {
            damageNumber.GetComponent<TextMesh>().text = damage.ToString();
        }
        hit = true;
    }
    IEnumerator Attack1()
    {
        while (GameManager.GetComponent<GameManager>().playerTurn == false)
        {
            float randomNumber = Random.Range(-2.1f, 2.1f);
            for (int i = 0; i < 4;) // spawns spamton heads in groups of 4
            {
                position = new Vector3(spawnPos.position.x, spawnPos.position.y + randomNumber, spawnPos.position.z); // randomise Y coordinate for the spawn location
                Instantiate(spamHead, position, gameObject.transform.rotation);
                yield return new WaitForSeconds(spawnGap); // the gap between each spamton head
                i++;
            }
            yield return new WaitForSeconds(spawnDelay); // the gap between each wave of spamton heads
        }
    }
    IEnumerator Attack2()
    {
        for (int x = 0;x < 2;)
        {
            for (int i = 0; i < 3;) // creates 3 walls of mail with 2 gaps in each wall, one with a spamton head, one with a bomb
            {
                position = new Vector3(spawnPos.position.x, 1.59f, spawnPos.position.z); 
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                if (i == 1) // alternates the spawn locations of the bomb and spamton head
                {
                    Instantiate(bomb, position, transform.rotation);
                }
                else
                {
                    Instantiate(spamHead, position, transform.rotation);
                }
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                if (i == 1) // alternates the spawn locations of the spamton head and bomb
                {
                    Instantiate(spamHead, position, transform.rotation);
                }
                else
                {
                    Instantiate(bomb, position, transform.rotation);
                }
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                yield return new WaitForSeconds(spawnDelay - 0.5f); // the gap between each wall
                i++;
            }
            yield return new WaitForSeconds(0.25f); // wait a little before spawning the quad walls
            for (int i = 0; i < 4;) // spawns 4 compact walls with a big gap filled with spamton heads near the bottom
            {
                position = new Vector3(spawnPos.position.x, 1.59f, spawnPos.position.z);
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(spamHead, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(spamHead, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                yield return new WaitForSeconds(0.15f); // gap between each wall
                i++;
            }
            x++;
            yield return new WaitForSeconds(spawnDelay - 0.5f);
        }
    }
}
