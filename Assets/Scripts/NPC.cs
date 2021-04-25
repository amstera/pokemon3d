using UnityEngine;

public class NPC : MonoBehaviour
{
    public bool ShouldRotate = true;
    public string Dialog;
    private float _timeSinceLastDirectionChange;
    private Direction _direction;

    void Update()
    {
        if (ShouldRotate && Random.Range(0, 50) == 1 && (Time.time - _timeSinceLastDirectionChange) >= 5)
        {
            int dir = Random.Range(0, 2) == 1 ? 1 : -1;
            Direction newDirection = (Direction)(((int)_direction + dir) % 4);
            int angleChange = ((int)newDirection - (int)_direction) * 90;
            transform.Rotate(0, 0, angleChange, Space.Self);
            _direction = newDirection;
            _timeSinceLastDirectionChange = Time.time;
        }

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
