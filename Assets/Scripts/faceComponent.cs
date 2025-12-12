using Unity.VisualScripting;
using UnityEngine;

public class faceComponent : MonoBehaviour
{
    public int health;
    public int shotDamage;
    public int bigShotDamage;
    private Animator animator;
    public Component attackScript;
    private Color ogColour;
    public float flashDuration;
    private float timer;
    private bool flashing;
    void Start()
    {
        animator = GetComponent<Animator>();
        ogColour = gameObject.GetComponent<SpriteRenderer>().color;
    }
    void Update()
    {
        if (flashing && health > 0) // damage flash when shot by player
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
            timer += Time.deltaTime;
            if (timer > flashDuration)
            {
                timer = 0;
                gameObject.GetComponent<SpriteRenderer>().color = ogColour;
                flashing = false;
            }
        }
        if (health <= 0)
        {
            animator.Play("Destroyed");
            gameObject.GetComponent<SpriteRenderer>().color = Color.white;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (health > 0)
        {
            if (collision.gameObject.tag == "bullet")
            {
                if (collision.GetComponent<bullet>().bigShot == false)
                {
                    health -= shotDamage;
                    flashing = true;
                }
                else
                {
                    health -= bigShotDamage;
                    flashing = true;
                }
                Destroy(collision.gameObject);
                if (health <= 0)
                {
                    gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    ((MonoBehaviour)attackScript).enabled = false; // disables attack script
                }
            }
        }
    }
}
