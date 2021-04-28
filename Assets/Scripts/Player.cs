using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float Speed = 3.5f;
    public bool IsFrozen;
    public PokemonSelection SelectedPokemon = PokemonSelection.None;
    public Animator Animator;
    public AudioSource AS;

    private Direction _direction;
    private Direction _forcedDirection = Direction.None;

    void Update()
    {
        if (IsFrozen)
        {
            Animator.SetBool("IsWalking", false);
            return;
        }

        Direction newDirection = Direction.None;
        bool isWalking = false;
        if ((Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)) && _forcedDirection == Direction.None || _forcedDirection == Direction.East)
        {
            newDirection = Direction.East;
            transform.position += Vector3.forward * Speed * Time.deltaTime;
            isWalking = true;
        }
        else if ((Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)) && _forcedDirection == Direction.None || _forcedDirection == Direction.West)
        {
            newDirection = Direction.West;
            transform.position += Vector3.back * Speed * Time.deltaTime;
            isWalking = true;
        }
        else if ((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow)) && _forcedDirection == Direction.None || _forcedDirection == Direction.North)
        {
            newDirection = Direction.North;
            transform.position += Vector3.left * Speed * Time.deltaTime;
            isWalking = true;
        }
        else if ((Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)) && _forcedDirection == Direction.None || _forcedDirection == Direction.South)
        {
            newDirection = Direction.South;
            transform.position += Vector3.right * Speed * Time.deltaTime;
            isWalking = true;
        }

        if (Animator.GetBool("IsWalking") != isWalking)
        {
            Animator.SetBool("IsWalking", isWalking);
        }

        ChangeDirection(newDirection);
    }

    public void FollowProfessor()
    {
        _forcedDirection = Direction.South;
        Invoke("GoRight", 7.25f);
    }

    public void ReceivePokemon(PokemonSelection selection)
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        AS.Play();
        SelectedPokemon = selection;
        Invoke("ResumeMusic", 2.5f);
    }

    private void ResumeMusic()
    {
        Camera.main.GetComponent<AudioSource>().Play();
    }

    private void GoRight()
    {
        _forcedDirection = Direction.East;
        Invoke("EnterBuilding", 2f);
    }

    private void EnterBuilding()
    {
        SceneManager.LoadScene("Lab");
    }

    private void ChangeDirection(Direction newDirection)
    {
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
