using UnityEngine;

public class contactDamage : MonoBehaviour
{
    public int damage;
    private GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Soul")
        {
            player.GetComponent<Player>().takeDamage(damage);
        }
    }
}
