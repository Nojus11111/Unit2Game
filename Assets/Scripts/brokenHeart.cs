using UnityEngine;

public class brokenHeart : MonoBehaviour
{
    private AudioSource soundPlayer;
    public AudioClip breakSound;
    public AudioClip shatterSound;
    public float delay;
    private float timer;
    private bool played;
    void Start()
    {
        soundPlayer = GetComponent<AudioSource>();
        soundPlayer.clip = breakSound;
        soundPlayer.Play();
    }
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > delay && !played)
        {
            soundPlayer.clip = shatterSound;
            soundPlayer.Play();
            played = true;
            GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
