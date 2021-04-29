using UnityEngine;
using UnityEngine.SceneManagement;

public class Red : MonoBehaviour
{
    public GameObject Transition;
    public AudioSource AS;

    public void EnterBattle()
    {
        Camera.main.GetComponent<AudioSource>().Stop();
        AS.Play();
        Transition.SetActive(true);
        Invoke("LoadBattle", 4f);
    }

    private void LoadBattle()
    {
        SceneManager.LoadScene("Battle");
    }
}
