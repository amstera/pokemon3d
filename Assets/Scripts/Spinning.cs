using UnityEngine;

public class Spinning : MonoBehaviour
{
    public float Speed = -35;
    void Update()
    {
        transform.Rotate(new Vector3(0, Speed * Time.deltaTime, 0), Space.Self);
    }
}
