using System.Collections;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UIElements;

public class spamtonMouth : MonoBehaviour
{
    public int wispCount;
    public Transform shootPos;
    public GameObject wisp;
    public float shootGap;
    public float shootDelay;
    private float timer;
    public float minRotation;
    public float maxRotation;
    private float rotation;
    void Start()
    {
        
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootDelay)
        {
            StopCoroutine(shootWisps());
            StartCoroutine(shootWisps());
            timer = 0;
        }
    }
    IEnumerator shootWisps()
    {
        for (int i = 0; i < wispCount;)
        {
            rotation = UnityEngine.Random.Range(minRotation, maxRotation);
            shootPos.rotation = Quaternion.Euler(0, 0, rotation);
            Instantiate(wisp, shootPos.position, shootPos.rotation);
            yield return new WaitForSeconds(shootGap);
            i++;
        }
    }
}
