using Unity.VisualScripting;
using UnityEngine;

public class faceComponent : MonoBehaviour
{
    public int health;
    public int shotDamage;
    public int bigShotDamage;
    private Animator animator;
    public Component attackScript;
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "bullet")
        {
            if (collision.GetComponent<bullet>().bigShot == false)
            {
                health -= shotDamage;
            }
            else
            {
                health -= bigShotDamage;
            }
            Destroy(collision.gameObject);
            if (health <= 0)
            {
                gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                animator.Play("Destroyed");
                ((MonoBehaviour)attackScript).enabled = false;
            }
        }
    }
}
