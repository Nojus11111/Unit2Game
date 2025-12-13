using UnityEngine;

public class moveTowardsTarget : MonoBehaviour
{
    public Transform target;
    public float speed;
    public bool finalAttack;
    void Start()
    {
        if (finalAttack)
        {
            target = GameObject.FindWithTag("Kromer").GetComponent<Transform>();
        }
    }
    void Update()
    {
        transform.position = Vector2.MoveTowards(this.transform.position, target.transform.position, speed * Time.deltaTime);
        if (finalAttack && transform.position == target.position)
        {
            Destroy(gameObject);
        }
    }
}
