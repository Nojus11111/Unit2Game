using UnityEngine;

public class whiteScreen : MonoBehaviour
{
    private SpriteRenderer sprite;
    public float fadeSpeed;
    private bool fading;
    private Color tmp;
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        tmp = sprite.color;
    }
    void Update()
    {
        if (fading) // screen fades to white
        {
            tmp = sprite.color;
            tmp.a += fadeSpeed;
            sprite.color = tmp;
        }
        else // screen is normal
        {
            tmp.a = 0;
            sprite.color = tmp;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision) // checks if the final bigshot is in the scene
    {
        if (collision.gameObject.tag == "Finish")
        {
            fading = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish")
        {
            fading = false;
        }
    }
}
