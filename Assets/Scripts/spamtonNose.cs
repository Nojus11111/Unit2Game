using System.Collections;
using UnityEngine;

public class spamtonNose : MonoBehaviour
{
    public float shootDelay;
    public float shootGap;
    public GameObject nose;
    public GameObject noseUp;
    public GameObject noseDown;
    public Transform shootPos;
    private float timer;
    private GameObject soul;
    private void Start()
    {
        soul = GameObject.FindWithTag("Soul");
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > shootDelay)
        {
            StopCoroutine(shootNose());
            StartCoroutine(shootNose());
            timer = 0;
        }
    }
    IEnumerator shootNose()
    {
        for (int i = 0; i < 3;)
        {
            Instantiate(nose, shootPos.position, shootPos.rotation);
            Instantiate(noseUp, shootPos.position, shootPos.rotation);
            Instantiate(noseDown, shootPos.position, shootPos.rotation);
            yield return new WaitForSeconds(shootGap);
            i++;
        }
    }
}
