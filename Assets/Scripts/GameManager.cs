using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public bool playerTurn;
    public int turn;
    public Light2D globalLight;
    public float attackDim;
    private void Update()
    {
        if (!playerTurn) // dims the screen to put focus on the attacks (attacks use the unlit material so they won't be dimmed)
        {
            globalLight.intensity = attackDim;
        }
        else
        {
            globalLight.intensity = 1;
        }
    }
}
