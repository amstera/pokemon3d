using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PokemonChoice : MonoBehaviour
{
    public Player Player;
    public GameObject Red;
    public PokemonSelection Selection;
    public GameObject Squirtle;
    public GameObject Bulbasaur;
    public GameObject Charmander;
    public List<GameObject> Pokeballs;
    public Text Number;
    public Text Name;
    public Text Description;
    public AudioSource AS;
    public AudioClip CharmanderCry;
    public AudioClip SquirtleCry;
    public AudioClip BulbasaurCry;

    void Update()
    {
        if (enabled && Input.GetKeyDown(KeyCode.Space))
        {
            Player.IsFrozen = false;
            gameObject.SetActive(false);
            if (Selection == PokemonSelection.Squirtle)
            {
                DialogBox.Instance.ShowChoiceDialog("So you want the water POKEMON, SQUIRTLE?", ChoosePokemon);
            }
            else if (Selection == PokemonSelection.Charmander)
            {
                DialogBox.Instance.ShowChoiceDialog("So you want the fire POKEMON, CHARMANDER?", ChoosePokemon);
            }
            else if (Selection == PokemonSelection.Bulbasaur)
            {
                DialogBox.Instance.ShowChoiceDialog("So you want the grass POKEMON, BULBASAUR?", ChoosePokemon);
            }
        }
    }

    void OnEnable()
    {
        Player.IsFrozen = true;
        Charmander.SetActive(false);
        Squirtle.SetActive(false);
        Bulbasaur.SetActive(false);

        if (Selection == PokemonSelection.Charmander)
        {
            Charmander.SetActive(true);
            AS.clip = CharmanderCry;
            AS.Play();
            Number.text = "No. 004";
            Name.text = "CHARMANDER\nLIZARD\nHT  2'00\"\nWT  19.0 lb";
            Description.text = "Obviously prefers hot places. When it rains, steam is said to spout from the tip of its tail.";
        }
        else if (Selection == PokemonSelection.Squirtle)
        {
            Squirtle.SetActive(true);
            AS.clip = SquirtleCry;
            AS.Play();
            Number.text = "No. 007";
            Name.text = "SQUIRTLE\nTINYTURTLE\nHT  1'08\"\nWT  20.0 lb";
            Description.text = "After birth, its back swells and hardens into a shell. Powerfully sprays foam from its mouth.";
        }
        else if (Selection == PokemonSelection.Bulbasaur)
        {
            Bulbasaur.SetActive(true);
            AS.clip = BulbasaurCry;
            AS.Play();
            Number.text = "No. 001";
            Name.text = "BULBASAUR\nSEED\nHT  2'04\"\nWT  15.0 lb";
            Description.text = "A strange seed was planted on its back at birth. The plant sprouts and grows with this POKEMON.";
        }
    }

    private void ChoosePokemon(bool chosen)
    {
        if (chosen)
        {
            DialogBox.Instance.ShowDialog("This POKEMON is really energetic!", ShowGainedPokemon, true);
        }
    }

    private void ShowGainedPokemon()
    {
        Destroy(Pokeballs[(int)Selection]);
        Player.ReceivePokemon(Selection);
        DialogBox.Instance.ShowDialog($"ASH received a {Selection.ToString().ToUpper()}!", RedSelectionWait, true);
    }

    private void RedSelectionWait()
    {
        Invoke("RedSelection", 1.5f);
    }

    private void RedSelection()
    {
        Red.transform.Rotate(new Vector3(0, 90, 0), Space.Self);
        DialogBox.Instance.ShowDialog($"GARY: I'll take this one, then!", RedTakePokemon, true);
    }

    private void RedTakePokemon()
    {
        PokemonSelection redSelection = PokemonSelection.None;
        if (Selection == PokemonSelection.Squirtle)
        {
            redSelection = PokemonSelection.Bulbasaur;
        }
        else if (Selection == PokemonSelection.Charmander)
        {
            redSelection = PokemonSelection.Squirtle;
        }
        else if (Selection == PokemonSelection.Bulbasaur)
        {
            redSelection = PokemonSelection.Charmander;
        }
        Destroy(Pokeballs[(int)redSelection]);
        DialogBox.Instance.ShowDialog($"GARY received a {redSelection.ToString().ToUpper()}!", null, true);
        Player.PlaySuccessChime();
    }
}

public enum PokemonSelection
{
    None = -1,
    Charmander = 0,
    Squirtle = 1,
    Bulbasaur = 2
}
