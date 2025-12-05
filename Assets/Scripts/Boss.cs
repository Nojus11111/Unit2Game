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
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
        attackDuration = 5;
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
            if (attackDuration < 0)
            {
                GameManager.GetComponent<GameManager>().playerTurn = true;
                attackDuration = 5;
                battleBox.SetActive(false);
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
}
