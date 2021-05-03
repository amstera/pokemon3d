using UnityEngine;

public class Pokeball : MonoBehaviour
{
    public Player player;
    public PokemonChoice PC;
    public PokemonSelection Selection;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && PC.CanChoose)
        {
            if (!player.IsFrozen && player.SelectedPokemon == PokemonSelection.None && Vector3.Distance(transform.position, player.transform.position) <= 1.4f)
            {
                PC.Selection = Selection;
                PC.gameObject.SetActive(true);
            }
        }
    }
}
