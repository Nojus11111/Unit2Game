using UnityEngine;

public class dontRotate : MonoBehaviour
{
    public Transform pipis;
    void Update()
    {
        transform.position = new Vector3(pipis.position.x - 0.6f, pipis.position.y - 0.65f, pipis.position.z); // locks rotation and position of the sign relative to the pipis
        transform.rotation = Quaternion.Euler(0, 0, 0);
    }
}
