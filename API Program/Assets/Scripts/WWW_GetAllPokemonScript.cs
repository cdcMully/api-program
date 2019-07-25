using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WWW_GetAllPokemonScript : MonoBehaviour
{
    private const string URL = "https://pokeapi.co/api/v2/pokemon/?limit=964";
    private RootObject2 jsonResponse;

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

        foreach (Result result in jsonResponse.results)
        {
            pokemonList.Add(result);
        }

        foreach (Result pokemon in pokemonList)
        {
            GameObject listItem = Instantiate(buttonListItem, new Vector2(0, 0), Quaternion.identity);
            listItem.transform.SetParent(contentWindow.transform);
            listItem.name = pokemon.name;
        }
    }
}

[System.Serializable]
public class Result
{
    public string name;
    public string url;
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