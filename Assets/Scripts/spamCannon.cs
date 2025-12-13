using UnityEngine;

public class spamCannon : MonoBehaviour
{
    public float minRotation;
    public float maxRotation;
    public Transform shootPos;
    public GameObject pipis;
    public Transform rotatePoint;
    public float shootDelay;
    private float timer;
    public float rotateSpeed;
    private float randomRotation;
    public int doubleShotChance;
    private int doubleShot = 1;
    private Animator animator;
    private bool firstShot;
    public GameObject firstPipis;
    void Start()
    {
        randomRotation = transform.rotation.z;
        timer = 1;
        animator = GetComponent<Animator>();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootDelay / 2)
        {
            animator.Play("Charge");
        }
        // rotates the cannon over time to randomRotation
        float currentRotation = rotatePoint.eulerAngles.z;
        float newRotation = Mathf.MoveTowardsAngle(currentRotation, randomRotation, rotateSpeed * Time.deltaTime);
        rotatePoint.rotation = Quaternion.Euler(0f, 0f, newRotation);
        if (timer > shootDelay)
        {
            if (doubleShot == 0)
            {
                Instantiate(pipis, shootPos.position, shootPos.rotation);
            }
            doubleShot = Random.Range(0, doubleShotChance); // has a chance to shoot 2 projectiles 
            randomRotation = Random.Range(minRotation, maxRotation);
            if (!firstShot)
            {
                Instantiate(firstPipis, shootPos.position, shootPos.rotation);
                firstShot = true;
            }
            else
            {
                Instantiate(pipis, shootPos.position, shootPos.rotation);
            }
            timer = 0;
            animator.Play("default");
        }
    }
}
