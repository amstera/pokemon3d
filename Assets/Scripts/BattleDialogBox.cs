using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleDialogBox : MonoBehaviour
{
    public GameObject Player;
    public GameObject Red;
    public GameObject SendPokemonPlayer;
    public GameObject SendPokemonOpponent;
    public List<GameObject> PlayerPokemon;
    public List<GameObject> OpponentPokemon;
    public RawImage PokemonRemaining;
    public RawImage OpponentPokemonRemaining;
    public Text Text;
    public RawImage Box;
    public RawImage Arrow;
    public int MaxCharLengthPerBox = 80;
    public List<AudioClip> PokemonCries;
    public AudioClip PokeballSound;
    public AudioSource Press;
    public AudioSource SoundEffectsAS;

    private char[] _currentDialog;
    private int _dialogIndex;
    private bool _isPrinting;
    private bool _canFinish;
    private bool _isFinished = true;
    private bool _redStartMoving;
    private bool _trainerStartMoving;
    private Action _callback;
    private PokemonSelection _playerPokemon = PokemonSelection.Charmander;
    private PokemonSelection _opponentPokemon;

    void Start()
    {
        if (PlayerPrefs.HasKey("PokemonSelection"))
        {
            _playerPokemon = (PokemonSelection)PlayerPrefs.GetInt("PokemonSelection");
        }
        if (_playerPokemon == PokemonSelection.Charmander)
        {
            _opponentPokemon = PokemonSelection.Squirtle;
        }
        else if (_playerPokemon == PokemonSelection.Squirtle)
        {
            _opponentPokemon = PokemonSelection.Bulbasaur;
        }
        else if (_playerPokemon == PokemonSelection.Bulbasaur)
        {
            _opponentPokemon = PokemonSelection.Charmander;
        }
        ShowDialog("GARY wants to fight!", ShowOpponentPokemon);
    }

    void Update()
    {
        if (_isPrinting)
        {
            return;
        }
        if (_trainerStartMoving && Player != null)
        {
            Player.transform.position += Vector3.left * 10 * Time.deltaTime;
        }
        else if (_redStartMoving && Red != null)
        {
            Red.transform.position += Vector3.right * 10 * Time.deltaTime;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (_canFinish)
            {
                ResetDialog();
            }
            else if (!_isFinished)
            {
                Press.Play();
                StartCoroutine(PrintText());
            }
        }
    }

    public void ShowDialog(string text, Action callback = null)
    {
        _isFinished = false;
        Arrow.enabled = false;
        _dialogIndex = 0;
        _callback = callback;
        _currentDialog = text.ToCharArray();
        StartCoroutine(PrintText());
    }

    private void ResetDialog()
    {
        Press.Play();
        Arrow.enabled = false;
        Text.text = string.Empty;
        _canFinish = false;
        _isFinished = true;
        _callback?.Invoke();
    }

    private void ShowOpponentPokemon()
    {
        OpponentPokemonRemaining.enabled = false;
        _redStartMoving = true;
        Destroy(Red, 2);
        Invoke("BeginShowingPokemonOpponent", 0.5f);
    }

    private void BeginShowingPokemonOpponent()
    {
        SoundEffectsAS.clip = PokeballSound;
        SoundEffectsAS.Play();
        SendPokemonOpponent.SetActive(true);
        ShowDialog($"GARY sent out {_opponentPokemon.ToString().ToUpper()}!", ShowPlayerPokemon);
        Invoke("ActivateOpponentPokemon", 0.35f);
    }

    private void ActivateOpponentPokemon()
    {
        SoundEffectsAS.clip = PokemonCries[(int)_opponentPokemon];
        SoundEffectsAS.Play();
        OpponentPokemon[(int)_opponentPokemon].SetActive(true);
    }

    private void ShowPlayerPokemon()
    {
        PokemonRemaining.enabled = false;
        _trainerStartMoving = true;
        Destroy(Player, 2);
        Invoke("BeginShowingPokemon", 0.5f);
    }

    private void BeginShowingPokemon()
    {
        SoundEffectsAS.clip = PokeballSound;
        SoundEffectsAS.Play();
        SendPokemonPlayer.SetActive(true);
        ShowDialog($"GO {_playerPokemon.ToString().ToUpper()}!");
        Invoke("ActivatePokemon", 0.35f);
    }

    private void ActivatePokemon()
    {
        SoundEffectsAS.clip = PokemonCries[(int)_playerPokemon];
        SoundEffectsAS.Play();
        PlayerPokemon[(int)_playerPokemon].SetActive(true);
    }

    private IEnumerator PrintText()
    {
        Text.text = string.Empty;
        if (_currentDialog.Length < (_dialogIndex + 1) * MaxCharLengthPerBox)
        {
            Arrow.enabled = false;
            _isPrinting = true;
            for (int i = _dialogIndex * MaxCharLengthPerBox; i < _currentDialog.Length; i++)
            {
                Text.text += _currentDialog[i];
                yield return new WaitForSeconds(0.01f);
            }
            _isPrinting = false;
            _canFinish = true;
        }
        else
        {
            Arrow.enabled = true;
            _isPrinting = true;
            for (int i = _dialogIndex * MaxCharLengthPerBox; i < (_dialogIndex + 1) * MaxCharLengthPerBox; i++)
            {
                Text.text += _currentDialog[i];
                yield return new WaitForSeconds(0.01f);
            }
            _isPrinting = false;
            _dialogIndex++;
        }
    }
}