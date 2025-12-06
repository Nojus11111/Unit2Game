using UnityEngine;

public class GameManager : MonoBehaviour
{
    public bool playerTurn;
    public int turn;
    private void Start()
    {
        turn = 1;
    }
}
