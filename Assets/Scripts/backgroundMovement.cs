using UnityEngine;

public class backgroundMovement : MonoBehaviour
{
    public Transform loopPoint;
    public float loopSpeed;
    private Vector3 position;
    private Vector3 start;
    void Start()
    {
        start = transform.position;
        position = transform.position;
    }
    void Update() // makes the background move to the left and loop back to it's start point when it's running out
    {
        transform.position = position;
        position = new Vector3(transform.position.x - 0.1f * loopSpeed, transform.position.y, transform.position.z);
        if (transform.position.x < loopPoint.position.x)
        {
            position = start;
        }
    }
}
