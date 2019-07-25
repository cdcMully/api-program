using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    private const string URL = "https://pokeapi.co/api/v2/pokemon/";
    private const string IMG_URL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/1.png";
    public Text responseText;
    public Image responseImage;
    private RootObject jsonResponse;
    public InputField mainInputField;

    public void Request()
    {        
        StartCoroutine(getTextCo(URL + mainInputField.text.ToString().ToLower()));        
    }

    private IEnumerator getTextCo(string URL)
    {
        WWW req = new WWW(URL);

        yield return req;
        
        jsonResponse = RootObject.CreateFromJSON(req.text);

        yield return jsonResponse;

        responseText.text = jsonResponse.name.ToUpper();

        StartCoroutine(getImageCo(jsonResponse.sprites.front_default));

        //Debug.Log("Number of moves: " + jsonResponse.moves.Count);

        //foreach (Move move in jsonResponse.moves)
        //{
        //    Debug.Log(move.move.name);
        //}
    }

    private IEnumerator getImageCo(string URL)
    {
        WWW req = new WWW(URL);

        yield return req;                

        responseImage.sprite = Sprite.Create(req.texture, new Rect(0, 0, req.texture.width, req.texture.height), new Vector2(0, 0));
    }
}

[System.Serializable]
public class Ability2
{
    public string name;
    public string url;
}

[System.Serializable]
public class Ability
{
    public Ability2 ability;
    public bool is_hidden;
    public int slot;
}

[System.Serializable]
public class Form
{
    public string name;
    public string url;
}

[System.Serializable]
public class Version
{
    public string name;
    public string url;
}

[System.Serializable]
public class GameIndice
{
    public int game_index;
    public Version version;
}

[System.Serializable]
public class Move2
{
    public string name;
    public string url;
}

[System.Serializable]
public class MoveLearnMethod
{
    public string name;
    public string url;
}

[System.Serializable]
public class VersionGroup
{
    public string name;
    public string url;
}

[System.Serializable]
public class VersionGroupDetail
{
    public int level_learned_at;
    public MoveLearnMethod move_learn_method;
    public VersionGroup version_group;
}

[System.Serializable]
public class Move
{
    public Move2 move;
    public List<VersionGroupDetail> version_group_details;
}

[System.Serializable]
public class Species
{
    public string name;
    public string url;
}

[System.Serializable]
public class Sprites
{
    public string back_default;
    public object back_female;
    public string back_shiny;
    public object back_shiny_female;
    public string front_default;
    public object front_female;
    public string front_shiny;
    public object front_shiny_female;
}

[System.Serializable]
public class Stat2
{
    public string name;
    public string url;
}

[System.Serializable]
public class Stat
{
    public int base_stat;
    public int effort;
    public Stat2 stat;
}

[System.Serializable]
public class Type2
{
    public string name;
    public string url;
}

[System.Serializable]
public class Type
{
    public int slot;
    public Type2 type;
}

[System.Serializable]
public class RootObject
{
    public List<Ability> abilities;
    public int base_experience;
    public List<Form> forms;
    public List<GameIndice> game_indices;
    public int height;
    public List<object> held_items;
    public int id;
    public bool is_default;
    public string location_area_encounters;
    public List<Move> moves;
    public string name;
    public int order;
    public Species species;
    public Sprites sprites;
    public List<Stat> stats;
    public List<Type> types;
    public int weight;

    public static RootObject CreateFromJSON(string jsonString)
    {
        return JsonUtility.FromJson<RootObject>(jsonString);
    }
}
