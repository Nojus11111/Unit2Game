using UnityEngine;

public class faceTarget : MonoBehaviour
{
    public GameObject target;
    public bool soulIsTarget;
    void Start()
    {
        if (soulIsTarget)
        {
            target = GameObject.FindWithTag("Soul");
        }
    }
    void Update()
    {
        Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y - transform.position.y); // face toward the target
        transform.up = direction;
    }
}
