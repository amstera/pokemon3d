using UnityEngine;

public class Oak : MonoBehaviour
{
    public Player Player;
    public Animator Animator;
    public float Speed = 2f;
    public float StopTime = 1.5f;
    private bool _isWalking;
    private Vector3 _walkDirection;

    void Start()
    {
        _isWalking = true;
        _walkDirection = Vector3.left;
        Animator.SetBool("IsWalking", true);
        Invoke("StopWalking", StopTime);
    }

    void Update()
    {
        if (_isWalking)
        {
            transform.position += _walkDirection * Speed * Time.deltaTime;
        }
    }

    private void StopWalking()
    {
        _isWalking = false;
        Animator.SetBool("IsWalking", false);
        transform.Rotate(0, 0, -90, Space.Self);
        DialogBox.Instance.ShowDialog("Hey! Wait! Don't go out! It's unsafe!", ShowSecondMessage);
    }

    public void ShowSecondMessage()
    {
        DialogBox.Instance.ShowDialog("Wild POKEMON live in tall grass! You need your own POKEMON for your safety. I know! Here, come with me!", StartWalking, true);
    }

    private void StartWalking()
    {
        transform.Rotate(0, 0, -90, Space.Self);
        _walkDirection = Vector3.right;
        _isWalking = true;
        Animator.SetBool("IsWalking", true);
        Player.FollowProfessor();
        Invoke("GoRight", 5.5f);
    }

    private void GoRight()
    {
        transform.Rotate(0, 0, 90, Space.Self);
        _walkDirection = Vector3.forward;
        Invoke("EnterBuilding", 2f);
    }

    private void EnterBuilding()
    {
        Destroy(gameObject);
    }
}
