using UnityEngine;

public class Player : MonoBehaviour
{
    public bool attacking;
    private GameObject GameManager;
    public GameObject AttackUI;
    public bool canAttack = true;
    public int health;
    void Start()
    {
        GameManager = GameObject.FindWithTag("Manager");
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
    }
    public void disableAttack()
    {
        canAttack = false;
    }
    public void takeDamage(int damage)
    {
        health -= damage;
    }
}
