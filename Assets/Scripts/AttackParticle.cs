using UnityEngine;

public class AttackParticle : MonoBehaviour
{
    void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}