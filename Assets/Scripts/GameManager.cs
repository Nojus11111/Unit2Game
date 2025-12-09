using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public bool playerTurn;
    public int turn;
    public Light2D globalLight;
    public float attackDim;
    private GameObject NEO;
    private void Start()
    {
        NEO = GameObject.FindWithTag("Enemy");
    }
    private void Update()
    {
        if (!playerTurn && NEO.GetComponent<Boss>().delayTimer > NEO.GetComponent<Boss>().attackDelay) // dims the screen to put focus on the attacks (attacks use the unlit material so they won't be dimmed)
        {
            globalLight.intensity = attackDim;
        }
        else
        {
            globalLight.intensity = 1;
        }
    }
}
