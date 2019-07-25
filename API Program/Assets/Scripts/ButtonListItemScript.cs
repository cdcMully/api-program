using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ButtonListItemScript : MonoBehaviour
{

    public Text nameText;

    private string pokemonName;    

    public void Setup(string name)
    {
        nameText.text = name;
        pokemonName = name;
    }

    public void OpenPokemonScreen()
    {
        PlayerPrefs.SetString("pokemon", pokemonName);
        PlayerPrefs.SetFloat("contentPosition", GameObject.Find("Content").transform.position.y);
        SceneManager.LoadScene("Individual Pokemon");        
    }   
}
