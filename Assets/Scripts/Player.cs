using UnityEngine;

public class Player : MonoBehaviour
{
    public float Speed = 3.5f;
    public bool IsFrozen;
    private Direction _direction;
    public Animator Animator;

    void Update()
    {
        if (IsFrozen)
        {
            return;
        }

        Direction newDirection = Direction.None;
        bool isWalking = false;
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            newDirection = Direction.East;
            transform.position += Vector3.forward * Speed * Time.deltaTime;
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            newDirection = Direction.West;
            transform.position += Vector3.back * Speed * Time.deltaTime;
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            newDirection = Direction.North;
            transform.position += Vector3.left * Speed * Time.deltaTime;
            isWalking = true;
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            newDirection = Direction.South;
            transform.position += Vector3.right * Speed * Time.deltaTime;
            isWalking = true;
        }

        if (Animator.GetBool("IsWalking") != isWalking)
        {
            Animator.SetBool("IsWalking", isWalking);
        }

        if (_direction != newDirection && newDirection != Direction.None)
        {
            int angleChange = ((int)newDirection - (int)_direction) * 90;
            transform.Rotate(0, 0, angleChange, Space.Self);
            _direction = newDirection;
        }
    }
}

enum Direction
{
    None = -1,
    North = 0,
    West = 1,
    South = 2,
    East = 3
}
