using UnityEngine;

public class Blocker : MonoBehaviour
{
    public Player Player;
    public GameObject Oak;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Player" && !Oak.activeSelf)
        {
            Player.IsFrozen = true;
            Oak.SetActive(true);
        }
    }
}
