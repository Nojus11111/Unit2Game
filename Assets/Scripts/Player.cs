using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public bool attacking;
    private GameObject GameManager;
    public GameObject AttackUI;
    public bool canAttack = true;
    public int health;
    public float iFrames;
    private float timer;
    private bool invincible;
    private int selected;
    [HideInInspector] public bool blocking;
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
    private Animator animator;
    public int actDamage;
    public GameObject damageNumber;
    private bool hit;
    public float damageDisplayTime;
    private float damageTimer;
    public int healAmount;
    public GameObject tpDisplay;
    public AudioSource soundPlayer;
    public AudioClip hurtSound;
    public AudioClip healSound;
    public AudioClip pickSound;
    public AudioClip confirmSound;
    public AudioClip strongSlashSound;
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
        invincible = false;
        enemy = GameObject.FindWithTag("Enemy");
        maxHealth = health;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        if (hit)
        {
            damageTimer += Time.deltaTime;
            if (damageTimer > damageDisplayTime)
            {
                damageNumber.SetActive(false);
                damageTimer = 0;
                hit = false;
            }
        }
        actionDelay += Time.deltaTime;
        if (actionDelay < 0.5) // prevents the player from accidentally picking an option immediately as the turn starts by adding a delay before they can pick anything
        {
            return;
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == true)
        {
            healthDisplay.GetComponent<TextMesh>().text = health.ToString() + "/" + maxHealth.ToString(); // display health
            tpDisplay.GetComponent<TextMesh>().text = TP.ToString() + "/100"; // display TP
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
                case 1: // highlight FIGHT
                    fightIcon.SetActive(true);
                    actIcon.SetActive(false);
                    itemIcon.SetActive(false);
                    defendIcon.SetActive(false);
                break;
                case 2: // highlight ACT
                    fightIcon.SetActive(false);
                    actIcon.SetActive(true);
                    itemIcon.SetActive(false);
                    defendIcon.SetActive(false);
                break;
                case 3: // highlight ITEM
                    fightIcon.SetActive(false);
                    actIcon.SetActive(false);
                    itemIcon.SetActive(true);
                    defendIcon.SetActive(false);
                break;
                case 4: // highlight DEFEND
                    fightIcon.SetActive(false);
                    actIcon.SetActive(false);
                    itemIcon.SetActive(false);
                    defendIcon.SetActive(true);
                break;
            }
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == true && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            soundPlayer.clip = pickSound;
            soundPlayer.Play();
            selected -= 1;
        }
        if (GameManager.GetComponent<GameManager>().playerTurn == true && Input.GetKeyDown(KeyCode.RightArrow))
        {
            soundPlayer.clip = pickSound;
            soundPlayer.Play();
            selected += 1;
        }

        if (GameManager.GetComponent<GameManager>().playerTurn == true && Input.GetKey(KeyCode.Z))
        {
            switch (selected)
            {
                case 1: // FIGHT
                    if (canAttack)
                    {
                        animator.Play("Attack Ready");
                        attacking = true;
                        AttackUI.SetActive(true);
                        canAttack = false;
                        actionUI.SetActive(false);
                        soundPlayer.clip = confirmSound;
                        soundPlayer.Play();
                    }
                break;
                case 2: // ACT
                    if (TP >= 30)
                    {
                        animator.Play("Slash");
                        TP -= 30;
                        enemy.GetComponent<Boss>().TakeDamage(actDamage);
                        GameManager.GetComponent<GameManager>().playerTurn = false;
                        actionUI.SetActive(false);
                        soundPlayer.clip = strongSlashSound;
                        soundPlayer.Play();
                    }
                break;
                case 3: // ITEM
                    heal(healAmount);
                    GameManager.GetComponent<GameManager>().playerTurn = false;
                    actionUI.SetActive(false);
                    break;
                case 4: // DEFEND
                    animator.Play("Defend");
                    blocking = true;
                    TP += 16;
                    GameManager.GetComponent<GameManager>().playerTurn = false;
                    actionUI.SetActive(false);
                    soundPlayer.clip = confirmSound;
                    soundPlayer.Play();
                break;
            }
        }
        if (!attacking)
        {
            AttackUI.SetActive(false);
        }
        if (invincible) // i frames
        {
            timer += Time.deltaTime;
            if (timer > iFrames)
            {
                timer = 0;
                invincible = false;
            }
        }
    }
    public void disableAttack() // prevents the attack code from running multiple times
    {
        canAttack = false;
    }
    public void takeDamage(int damage)
    {
        if (!invincible)
        {
            soundPlayer.clip = hurtSound;
            soundPlayer.Play();
            if (blocking) // reduce damage if blocking
            {
                damage = damage / 2;
            }
            health -= damage;
            damageNumber.SetActive(true);
            damageNumber.GetComponent<TextMesh>().text = damage.ToString();
            hit = true;
            invincible = true;
        }
    }
    public void heal(int heal)
    {
        soundPlayer.clip = healSound;
        soundPlayer.Play();
        health += heal;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
    }
    public void playSlash(AudioClip slash)
    {
        soundPlayer.clip = slash;
        soundPlayer.Play();
    }
}
