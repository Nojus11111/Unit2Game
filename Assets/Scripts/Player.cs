using UnityEngine;

public class Player : MonoBehaviour
{
    public bool attacking;
    private GameObject GameManager;
    public GameObject AttackUI;
    public bool canAttack = true;
    public int health;
    public float iFrames;
    private float timer;
    private bool invincible;
    private int selected;
    public bool blocking;
    [HideInInspector] public float actionDelay;
    public int TP;
    private GameObject enemy;
    public GameObject fightIcon;
    public GameObject actIcon;
    public GameObject itemIcon;
    public GameObject defendIcon;
    public GameObject actionUI;
    public GameObject healthDisplay;
    private int maxHealth;
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
        invincible = false;
        enemy = GameObject.FindWithTag("Enemy");
        maxHealth = health;
    }
    void Update()
    {
        actionDelay += Time.deltaTime;
        if (actionDelay < 0.5)
        {
            return;
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == true)
        {
            healthDisplay.GetComponent<TextMesh>().text = health.ToString() + "/" + maxHealth.ToString();
            if (selected < 1)
            {
                selected = 1;
            }
            if (selected > 4)
            {
                selected = 4;
            }
            switch (selected)
            {
                case 1:
                    fightIcon.SetActive(true);
                    actIcon.SetActive(false);
                    itemIcon.SetActive(false);
                    defendIcon.SetActive(false);
                break;
                case 2:
                    fightIcon.SetActive(false);
                    actIcon.SetActive(true);
                    itemIcon.SetActive(false);
                    defendIcon.SetActive(false);
                break;
                case 3:
                    fightIcon.SetActive(false);
                    actIcon.SetActive(false);
                    itemIcon.SetActive(true);
                    defendIcon.SetActive(false);
                break;
                case 4:
                    fightIcon.SetActive(false);
                    actIcon.SetActive(false);
                    itemIcon.SetActive(false);
                    defendIcon.SetActive(true);
                break;
            }
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == true && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            selected -= 1;
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            selected += 1;
        }

        if (GameManager.GetComponent<GameManager>().playerTurn == true && Input.GetKey(KeyCode.Z))
        {
            switch (selected)
            {
                case 1: //fight
                    if (canAttack)
                    {
                        attacking = true;
                        AttackUI.SetActive(true);
                        canAttack = false;
                        actionUI.SetActive(false);
                    }
                break;
                case 2: //act
                    if (TP >= 30)
                    {
                        TP -= 30;
                        enemy.GetComponent<Boss>().TakeDamage(20);
                        GameManager.GetComponent<GameManager>().playerTurn = false;
                        actionUI.SetActive(false);
                    }
                break;
                case 3: //item
                    heal(20);
                    GameManager.GetComponent<GameManager>().playerTurn = false;
                    actionUI.SetActive(false);
                break;
                case 4: //defend
                    blocking = true;
                    TP += 16;
                    GameManager.GetComponent<GameManager>().playerTurn = false;
                    actionUI.SetActive(false);
                break;
            }
        }
        if (!attacking)
        {
            AttackUI.SetActive(false);
        }
        if (invincible)
        {
            timer += Time.deltaTime;
            if (timer > iFrames)
            {
                timer = 0;
                invincible = false;
            }
        }
    }
    public void disableAttack()
    {
        canAttack = false;
    }
    public void takeDamage(int damage)
    {
        if (!invincible)
        {
            if (blocking)
            {
                damage = damage / 2;
            }
            health -= damage;
            Debug.Log(damage);
            invincible = true;
        }
    }
    public void heal(int heal)
    {
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
}
