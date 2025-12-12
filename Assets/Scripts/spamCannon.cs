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
    private int doubleShot;
    void Start()
    {
        randomRotation = transform.rotation.z;
        timer = 1;
    }
    void Update()
    {
        timer += Time.deltaTime;
        float currentRotation = rotatePoint.eulerAngles.z;
        float newRotation = Mathf.MoveTowardsAngle(currentRotation, randomRotation, rotateSpeed * Time.deltaTime);
        rotatePoint.rotation = Quaternion.Euler(0f, 0f, newRotation);
        if (timer > shootDelay)
        {
            doubleShot = Random.Range(0, doubleShotChance);
            if (doubleShot == 0)
            {
                Instantiate(pipis, shootPos.position, shootPos.rotation);
            }
            randomRotation = Random.Range(minRotation, maxRotation);
            Instantiate(pipis, shootPos.position, shootPos.rotation);
            timer = 0;
        }
    }
}
