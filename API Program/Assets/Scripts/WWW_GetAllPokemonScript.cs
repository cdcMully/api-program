using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWW_GetAllPokemonScript : MonoBehaviour
{
    private const string URL = "https://pokeapi.co/api/v2/pokemon/?limit=964";
    private RootObject2 jsonResponse;
    private bool sorting = false;

    public List<Result> pokemonList;
    public GameObject buttonListItem;
    public GameObject contentWindow;    

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(GetAllPokemonAPICallCo(URL));
    }    

    private IEnumerator GetAllPokemonAPICallCo (string URL)
    {
        WWW request = new WWW(URL);

        yield return request;

        jsonResponse = RootObject2.CreateFromJSON(request.text);

        yield return jsonResponse;

        pokemonList = jsonResponse.results;        

        SortPokemonList();

        if (sorting)
        {
            yield return null;
        }

        foreach (Result pokemon in pokemonList)
        {
            GameObject listItem = Instantiate(buttonListItem, new Vector2(0, 0), Quaternion.identity);
            listItem.transform.SetParent(contentWindow.transform, false);            
            listItem.name = pokemon.name;
            listItem.GetComponent<ButtonListItemScript>().Setup(pokemon.name);
        }

        if (PlayerPrefs.HasKey("contentPosition"))
        {
            contentWindow.transform.position = new Vector2(contentWindow.transform.position.x, PlayerPrefs.GetFloat("contentPosition"));
        }        
    }

    private void SortPokemonList()
    {
        sorting = true;
        pokemonList.Sort();        
        sorting = false;
    }
}

[System.Serializable]
public class Result: System.IComparable<Result>
{
    public string name;
    public string url;

    public int CompareTo(Result compareResult)
    {
        if (compareResult == null)
        {
            return 1;
        } else
        {
            return this.name.CompareTo(compareResult.name);
        }
    }
}

[System.Serializable]
public class RootObject2
{
    public int count;
    public object next;
    public object previous;
    public List<Result> results;

    public static RootObject2 CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<RootObject2>(jsonString);
    }
}