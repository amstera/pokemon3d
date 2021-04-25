using UnityEngine;

public class Sign : MonoBehaviour
{
    public string Dialog;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !string.IsNullOrEmpty(Dialog))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (Vector3.Distance(transform.position, player.transform.position) < 1.5f)
            {
                DialogBox.Instance.ShowDialog(Dialog);
            }
        }
    }
}
