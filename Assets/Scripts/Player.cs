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
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
        invincible = false;
    }
    void Update()
    {
        if (GameManager.GetComponent<GameManager>().playerTurn == true && Input.GetKey(KeyCode.Z) && canAttack)
        {
            attacking = true;
            AttackUI.SetActive(true);
            canAttack = false;
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
            health -= damage;
            Debug.Log("hit");
            invincible = true;
        }
    }
}
