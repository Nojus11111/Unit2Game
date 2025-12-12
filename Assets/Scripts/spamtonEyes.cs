using System.Collections;
using UnityEngine;

public class spamtonEyes : MonoBehaviour
{
    public float shootDelay;
    public float shootGap;
    public int laserLength;
    public GameObject laser;
    public Transform shootPos;
    private float timer;
    private bool targeting = true;
    public Transform target;
    private GameObject soul;
    private void Start()
    {
        soul = GameObject.FindWithTag("Soul");
    }
    void Update()
    {
        if (targeting) // has a transform that follows the soul, the eyes start shooting the transform stays still so that the laser travels in a straight line
        {
            target.position = soul.transform.position;
        }
        timer += Time.deltaTime;
        if (timer > shootDelay)
        {
            StopCoroutine(shootLaser());
            StartCoroutine(shootLaser());
            timer = 0;
        }
    }
    IEnumerator shootLaser()
    {
        for (int i = 0; i < laserLength;)
        {
            targeting = false;
            Instantiate(laser, shootPos.position, shootPos.rotation);
            yield return new WaitForSeconds(shootGap);
            i++;
        }
        targeting = true;
    }
}
