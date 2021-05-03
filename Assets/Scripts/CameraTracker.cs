using UnityEngine;

public class CameraTracker : MonoBehaviour
{
    public GameObject Target;
    private Vector3 _offset;

    void Start()
    {
        _offset = transform.position - Target.transform.position;
    }

    void LateUpdate()
    {
        transform.position = Target.transform.position + _offset;
    }
}
