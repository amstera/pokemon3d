using UnityEngine;

public class Blocker : MonoBehaviour
{
    public Player Player;
    public GameObject Oak;
    public AudioClip OakAppears;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && !Oak.activeSelf)
        {
            Player.IsFrozen = true;
            Oak.SetActive(true);
            AudioSource audioSource = Camera.main.GetComponent<AudioSource>();
            audioSource.clip = OakAppears;
            audioSource.Play();
        }
    }
}
