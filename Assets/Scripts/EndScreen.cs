using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndScreen : MonoBehaviour
{
    public List<GameObject> Pokemon;
    public Text WinnerText;
    private PokemonSelection _playerPokemon;
    private bool _isWinner;

    void Start()
    {
        if (PlayerPrefs.HasKey("PokemonSelection"))
        {
            _playerPokemon = (PokemonSelection)PlayerPrefs.GetInt("PokemonSelection");
        }
        if (PlayerPrefs.HasKey("IsWinner"))
        {
            _isWinner = PlayerPrefs.GetInt("IsWinner") == 1;
        }

        Pokemon[(int)_playerPokemon].SetActive(true);
        WinnerText.text = _isWinner ? "You won! Press space to play again!" : "You lost! Press space to try again!";
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("Game");
        }
    }
}
