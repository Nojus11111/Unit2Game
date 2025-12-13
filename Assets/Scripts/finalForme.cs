using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class finalForme : MonoBehaviour
{
    public Transform spawnPos;
    public GameObject kromer;
    public Transform shootPos;
    public float spawnDelay;
    private float spawnTimer;
    public float maxHeight;
    public float minHeight;
    private float height;
    private bool part1 = true;
    private Vector2 spawnPosition;
    public float part1Duration;
    private float timer;
    public float shootDelay;
    public Transform upPoint;
    public Transform downPoint;
    private Transform targetPos;
    public GameObject bigShot;
    public float speed;
    public float part2Duration;
    private float timer2;
    public GameObject superBigShot;
    private bool part3;
    public Transform start;
    public float finalShotDelay;
    private float finalTimer;
    void Start()
    {
        targetPos = upPoint;
    }
    void Update()
    {
        if (part1 && !part3) // spawns money signs at random heights that get sucked into spamton's mouth
        {
            timer += Time.deltaTime;
            spawnTimer += Time.deltaTime;
            if (spawnTimer > spawnDelay)
            {
                height = Random.Range(minHeight, maxHeight);
                spawnPosition = new Vector2(spawnPos.position.x, height);
                Instantiate(kromer, spawnPosition, spawnPos.rotation);
                spawnTimer = 0;
            }
            if (timer > part1Duration)
            {
                part1 = false;
                timer = 0;
            }
        }
        if (!part1 && !part3) // shoots a bunch of big shots while moving up and down
        {
            timer2 += Time.deltaTime;
            transform.position = Vector2.MoveTowards(this.transform.position, targetPos.position, speed * Time.deltaTime);
            if (transform.position == upPoint.position)
            {
                targetPos = downPoint;
                Instantiate(bigShot, shootPos.position, shootPos.rotation);
            }
            if (transform.position == downPoint.position)
            {
                targetPos = upPoint;
                Instantiate(bigShot, shootPos.position, shootPos.rotation);
            }
            if (timer2 > part2Duration)
            {
                part3 = true;
            }
        }
        if (part3 && !part1) // shoots one final huge shot
        {
            transform.position = Vector2.MoveTowards(this.transform.position, start.position, speed * Time.deltaTime);
            if (transform.position == start.position)
            {
                finalTimer += Time.deltaTime;
                if (finalTimer > finalShotDelay)
                {
                    Instantiate(superBigShot, shootPos.position, shootPos.rotation);
                    part1 = true;
                }
            }
        }
    }
}
