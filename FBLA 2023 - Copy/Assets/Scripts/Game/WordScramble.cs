using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Word
{
    public string word;

    [Header("leave empty if randomized")]
    public string desiredRandom;

    public string GetString()
    {
        if (!string.IsNullOrEmpty(desiredRandom))
        {
            return desiredRandom;
        }

        string result = word;
  
        while (result == word){
            result = "";
            List<char> characters = new List<char>(word.ToCharArray());
            while (characters.Count > 0)
            {
                int indexChar = Random.Range(0, characters.Count - 1);
                result += characters[indexChar];

                characters.RemoveAt(indexChar);
            }  
        }

        return result;
    }
}

public static class IListExtensions
{
    /// <summary>
    /// Shuffles the element order of the specified list.
    /// </summary>
    public static void Shuffle<T>(this IList<T> ts)
    {
        var count = ts.Count;
        var last = count - 1;
        for (var i = 0; i < last; ++i)
        {
            var r = Random.Range(i, count);
            var tmp = ts[i];
            ts[i] = ts[r];
            ts[r] = tmp;
        }
    }
}

public class WordScramble : MonoBehaviour
{
    public Word[] words;

    [Header("UI REFERENCE")]
    public CharObject prefab;
    public Transform container;
    public float space;

    public TMP_Text CounterText;
    public int Counter;
    public float lerpSpeed;

    public GameObject GoodJobPrefab;
    public GameObject UICanvas;

    List<CharObject> charObjects = new List<CharObject>();
    CharObject firstSelected;

    public int currentWord;


    public static WordScramble main;
    public Timer timer;

    private void Awake()
    {
        words.Shuffle();
        main = this;

        Counter = 0;
    }

    private void Start()
    {
        ShowScramble(currentWord);
    }

    private void Update()
    {
        RepositionObject();
        if(Counter >= 2)
        {
            PlayerPrefs.SetFloat("LastTimer", timer.TimeLeft);
            SceneManager.LoadScene("SelectName");
        }
    }

    void RepositionObject()
    {
        if (charObjects.Count == 0)
        {
            return;
        }

        float center = (charObjects.Count - 1) / 2;
        for (int i = 0; i < charObjects.Count; i++){
            charObjects[i].rectTransform.anchoredPosition
                 = Vector2.Lerp(charObjects[i].rectTransform.anchoredPosition,
                 new Vector2((i - center) * space, 0), lerpSpeed * Time.deltaTime);
            charObjects[i].index = i;
        }
    }

    /// <summary>
    /// Show a random word to the screen
    /// </summary>
    public void ShowScramble()
    {
       
        ShowScramble(Random.Range(0, words.Length - 1));

    }

    public void ShowScramble(int index)
    {
        

        //Clear Letters
        charObjects.Clear();
        foreach(Transform child in container)
        {
            Destroy(child.gameObject);
        }

        if (index > words.Length - 1)
        {
     
            Debug.LogError("Indexout of range, please enter range between 0-" + (words.Length - 1).ToString());
            return;
        }

        char[] chars = words[index].GetString().ToCharArray();
        foreach(char c in chars)
        {
            CharObject clone = Instantiate(prefab.gameObject).GetComponent<CharObject>();
            clone.transform.SetParent(container);

            charObjects.Add(clone.Init(c));
        }

        currentWord = index;
    }

    public void Swap(int indexA, int indexB)
    {
        CharObject tmpA = charObjects[indexA];

        charObjects[indexA] = charObjects[indexB];
        charObjects[indexB] = tmpA;

        charObjects[indexA].transform.SetAsLastSibling();
        charObjects[indexB].transform.SetAsLastSibling();

        checkWord();
    }

    public void Select (CharObject charObject)
    {
        if (firstSelected)
        {
            Swap(firstSelected.index, charObject.index);

            //Unselect
            firstSelected.Select();
            charObject.Select();
        }
        else
        {
            firstSelected = charObject;
        }
    }

    public void UnSelect()
    {
        firstSelected = null;
    }
    public void checkWord()
    {
        StartCoroutine(CoCheckWord());
    }

    IEnumerator CoCheckWord()
    {
        yield return new WaitForSeconds(0.5f);
        string word = "";
        foreach (CharObject charObject in charObjects)
        {
            word += charObject.character;
        }

        //Words Correct
        if (word == words[currentWord].word)
        {
            currentWord++;
            ShowScramble(currentWord);
            //Update Counter
            Counter++;
            CounterText.text = Counter.ToString();

            //Good Job Animation

            Destroy(GameObject.Find("UI/Correct(Clone)"));
            Instantiate(GoodJobPrefab, UICanvas.transform);

        }

    }
}
