using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

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
    public AudioSource soundPlayer;
    public AudioClip music;
    public AudioClip loseMusic;
    public AudioClip winMusic;
    public GameObject victoryScreen;
    public GameObject loseScreen;
    public GameObject brokenHeart;
    public float musicDelay;
    private float timer;
    public GameObject battleBox;
    public GameObject prompt;
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

        if (GameObject.FindWithTag("Player").GetComponent<Player>().health <= 0) // game over screen
        {
            if (!playerTurn)
            {
                Instantiate(brokenHeart, GameObject.FindWithTag("Soul").transform.position, GameObject.FindWithTag("Soul").transform.rotation);
                musicPlayer.Stop();
                battleBox.SetActive(false);
            }
            loseScreen.SetActive(true);
            GameObject.FindWithTag("Player").GetComponent<Player>().enabled = false;
            playerTurn = true;
            timer += Time.deltaTime;
            if (timer > musicDelay && !musicPlayer.isPlaying)
            {
                musicPlayer.clip = loseMusic;
                musicPlayer.Play();
                prompt.SetActive(true);
            }
            if (timer > musicDelay && Input.GetKey(KeyCode.Z)) // restarts the game by reloading the scene
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
        if (GameObject.FindWithTag("Enemy").GetComponent<Boss>().health <= 0) // victory screen
        {
            musicPlayer.Stop();
            if (!playerTurn)
            {
                soundPlayer.clip = winMusic;
                soundPlayer.Play();
                playerTurn = true;
            }
            GameObject.FindWithTag("Player").GetComponent<Player>().enabled = false;
            victoryScreen.SetActive(true);
            battleBox.SetActive(false);
            timer += Time.deltaTime;
            if (timer > musicDelay)
            {
                prompt.SetActive(true);
            }
            if (timer > musicDelay && Input.GetKey(KeyCode.Z))
            {
                SceneManager.LoadScene("SampleScene");
            }
        }
    }
}
