using UnityEngine;

public class NPC : MonoBehaviour
{
    private float _timeSinceLastDirectionChange;
    private Direction _direction;

    void Update()
    {
        if (Random.Range(0, 50) == 1 && (Time.time - _timeSinceLastDirectionChange) >= 5)
        {
            Direction[] directions = { Direction.North, Direction.East, Direction.South, Direction.West };
            Direction newDirection = directions[Random.Range(0, directions.Length)];
            int angleChange = ((int)newDirection - (int)_direction) * 90;
            transform.Rotate(0, 0, angleChange, Space.Self);
            _direction = newDirection;
            _timeSinceLastDirectionChange = Time.time;
        }
    }
}
