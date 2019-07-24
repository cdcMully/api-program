using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class API : MonoBehaviour
{
    private const string URL = "https://pokeapi.co/api/v2/";
    private const string IMG_URL = "https://raw.githubusercontent.com/PokeAPI/sprites/master/sprites/pokemon/back/1.png";
    public Text responseText;
    public Image responseImage;

    public void Request()
    {
        StartCoroutine(getTextCo(URL));
        StartCoroutine(getImageCo(IMG_URL));
    }

    private IEnumerator getTextCo(string URL)
    {
        WWW req = new WWW(URL);

        yield return req;

        responseText.text = req.text;
    }

    private IEnumerator getImageCo(string URL)
    {
        WWW req = new WWW(URL);

        yield return req;        

        //responseText.text = req.text;

        responseImage.sprite = Sprite.Create(req.texture, new Rect(0, 0, req.texture.width, req.texture.height), new Vector2(0, 0));

    }
}
