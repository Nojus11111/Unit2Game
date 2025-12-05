using UnityEngine;

public class despawner : MonoBehaviour
{
    public float lifetime;
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
    }
}
