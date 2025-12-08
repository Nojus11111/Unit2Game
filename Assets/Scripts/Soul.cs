using UnityEngine;
using UnityEngine.EventSystems;

public class Soul : MonoBehaviour
{
    public float moveSpeed;
    private Rigidbody2D rb;
    private Vector2 moveDirection;
    public GameObject shot;
    public GameObject bigShot;
    public float chargeTime;
    private float chargeTimer;
    public float shootDelay;
    private float shootTimer;
    public GameObject chargeIndicator;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        // movement
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");
        moveDirection = new Vector2(moveX, moveY).normalized;
        rb.linearVelocity = new Vector2(moveX * moveSpeed, moveY * moveSpeed); // rb.linearVelocity should be fine here since the only physical collision in this game is between the soul and the battlebox walls, eveything else uses triggers

        // shooting
        if (shootTimer <= shootDelay)
        {
            shootTimer += Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.Z) && shootTimer > shootDelay) // charge
        {
            chargeTimer += Time.deltaTime;
            if (chargeTimer > chargeTime)
            {
                chargeIndicator.SetActive(true);
            }
        }
        if (Input.GetKeyUp(KeyCode.Z) && shootTimer > shootDelay) // release/shoot
        {
            if (chargeTimer > chargeTime)
            {
                Instantiate(bigShot, transform.position, transform.rotation);
                chargeIndicator.SetActive(false);
            }
            else
            {
                Instantiate(shot, transform.position, transform.rotation);
            }
            chargeTimer = 0;
            shootTimer = 0;
        }
    }
}
