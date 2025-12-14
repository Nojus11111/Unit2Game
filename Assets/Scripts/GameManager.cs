using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GameManager : MonoBehaviour
{
    public bool playerTurn;
    public int turn;
    public Light2D globalLight;
    public float attackDim;
    private GameObject NEO;
    private bool finalAttacked = false;
    public int finalAttackThreshold;
    public AudioSource musicPlayer;
    public AudioClip music;
    private void Start()
    {
        NEO = GameObject.FindWithTag("Enemy");
        musicPlayer.clip = music;
        musicPlayer.Play();
    }
    private void Update()
    {
        if (!playerTurn && NEO.GetComponent<Boss>().delayTimer > NEO.GetComponent<Boss>().attackDelay) // dims the screen to put focus on the attacks (attacks use the unlit material so they won't be dimmed)
        {
            if (NEO.GetComponent<Boss>().finalAttack)
            {
                globalLight.intensity = 0;
            }
            else
            {
                globalLight.intensity = attackDim;
            }
        }
        else
        {
            globalLight.intensity = 1;
        }

        if (NEO.GetComponent<Boss>().health < finalAttackThreshold && !finalAttacked) // makes spamton do his final attack if he's below the health threshold
        {
            turn = 0;
            finalAttacked = true;
        }

        if (turn > 4) // loops attacks
        {
            turn = 1;
        }
    }
}
