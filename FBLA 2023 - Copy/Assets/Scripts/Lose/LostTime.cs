using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LostTime : MonoBehaviour
{
    public TMP_Text text;

    private void Start()
    {
        text = GetComponent<TMP_Text>();
        text.text = PlayerPrefs.GetInt("LastCount").ToString() + " WORDS";
    }
}
