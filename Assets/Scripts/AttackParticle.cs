using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Disable", 5f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}