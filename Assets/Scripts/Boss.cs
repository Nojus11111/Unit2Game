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
    private float spawnTimer;
    private Vector3 position;
    private bool attackStarted;
    private GameObject player;
    public GameObject wall;
    public GameObject bomb;
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
        attackDuration = 8;
        attackStarted = false;
        player = GameObject.FindWithTag("Player");
    }
    void Update()
    {
        if (hit)
        {
            timer += Time.deltaTime;
            if (timer > damageDisplayTime)
            {
                damageNumber.SetActive(false);
                timer = 0;
                hit = false;
            }
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == false)
        {
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
            }
            if (attackDuration < 0)
            {
                GameManager.GetComponent<GameManager>().playerTurn = true;
                attackDuration = 10;
                battleBox.SetActive(false);
                attackStarted = false;
                player.GetComponent<Player>().blocking = false;
                player.GetComponent<Player>().actionDelay = 0;
                player.GetComponent<Player>().actionUI.SetActive(true);
                GameManager.GetComponent<GameManager>().turn += 1;
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
        damageNumber.SetActive(true);
        damageNumber.GetComponent<TextMesh>().text = damage.ToString();
        hit = true;
    }
    IEnumerator Attack1()
    {
        while (GameManager.GetComponent<GameManager>().playerTurn == false)
        {
            float randomNumber = Random.Range(-2.1f, 2.1f);
            for (int i = 0; i < 4;)
            {
                position = new Vector3(spawnPos.position.x, spawnPos.position.y + randomNumber, spawnPos.position.z);
                Instantiate(spamHead, position, gameObject.transform.rotation);
                yield return new WaitForSeconds(spawnGap);
                i++;
            }
            yield return new WaitForSeconds(spawnDelay);
        }
    }
    IEnumerator Attack2()
    {
        for (int x = 0;x < 2;)
        {
            for (int i = 0; i < 3;)
            {
                position = new Vector3(spawnPos.position.x, 1.59f, spawnPos.position.z);
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                Instantiate(wall, position, transform.rotation);
                position.y -= 0.79f;
                if (i == 1)
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
                if (i == 1)
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
                yield return new WaitForSeconds(spawnDelay - 0.5f);
                i++;
            }
            yield return new WaitForSeconds(0.25f);
            for (int i = 0; i < 4;)
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
                yield return new WaitForSeconds(0.15f);
                i++;
            }
            x++;
            yield return new WaitForSeconds(spawnDelay - 0.5f);
        }
    }
}
