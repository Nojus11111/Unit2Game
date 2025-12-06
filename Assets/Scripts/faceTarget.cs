using UnityEngine;

public class faceTarget : MonoBehaviour
{
    private GameObject soul;
    void Start()
    {
        soul = GameObject.FindWithTag("Soul");
    }
    void Update()
    {
        Vector2 direction = new Vector2(soul.transform.position.x - transform.position.x, soul.transform.position.y - transform.position.y);
        transform.up = direction;
    }
}
