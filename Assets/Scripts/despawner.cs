using UnityEngine;

public class despawner : MonoBehaviour
{
    public float lifetime;
    private GameObject gameManager;
    public bool attack;
    private void Start()
    {
        gameManager = GameObject.FindWithTag("Manager");
    }
    void Update()
    {
        lifetime -= Time.deltaTime;
        if (lifetime < 0)
        {
            Destroy(gameObject);
        }
        if (attack && gameManager.GetComponent<GameManager>().playerTurn)
        {
            Destroy(gameObject);
        }
    }
}
