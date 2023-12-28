using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Name : MonoBehaviour
{
    public TMP_InputField nameField;

    public void MakeName()
    {
        string NewName = nameField.text;
        if(!string.IsNullOrEmpty(NewName))
        {
            PlayerPrefs.SetString("CurrentName", NewName);
            SceneManager.LoadScene("Win");
        } else
        {
            Debug.LogError("NAME EMPTY!");
        }
    }
}
