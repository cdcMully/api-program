using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonListItemScript : MonoBehaviour
{

    public Text nameText;

    public void Setup(string name)
    {
        nameText.text = name;
    }
}
