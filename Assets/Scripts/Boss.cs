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
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
        attackDuration = 8;
        attackStarted = false;
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
        if (GameManager.GetComponent<GameManager>().playerTurn == false && GameManager.GetComponent<GameManager>().turn == 1)
        {
            battleBox.SetActive(true);
            attackDuration -= Time.deltaTime;
            if (!attackStarted)
            {
                StartCoroutine(Spawning());
                attackStarted = true;
            }
            if (attackDuration < 0)
            {
                GameManager.GetComponent<GameManager>().playerTurn = true;
                attackDuration = 8;
                battleBox.SetActive(false);
                attackStarted = false;
            }
        }
    }
    public void TakeDamage(int damage)
    {
        health -= damage;
        damageNumber.SetActive(true);
        damageNumber.GetComponent<TextMesh>().text = damage.ToString();
        hit = true;
    }
    IEnumerator Spawning()
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
}
