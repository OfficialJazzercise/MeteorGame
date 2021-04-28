using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryTransformList;
    private const int highscoreLimit = 10;

    private List<string> alphabet;
    private string inputName;
    private int counter = 3;

    private int currentLetter = 0;

    private IEnumerator coroutine;

    private const string defaultPref = "{\"highscoreEntryList\":[{\"score\":53488,\"name\":\"AAA\"},{\"score\":58888,\"name\":\"FML\"}]}";
 

    private void Awake()
    {
        entryContainer = transform.Find("highscoreEntryContainer");
        entryTemplate = entryContainer.Find("highscoreEntryTemplate");
        alphabet = new List<string>();
        makeAlphabet();

        entryTemplate.gameObject.SetActive(false);

        coroutine = AddP1Score();
        StartCoroutine(coroutine);
        
    }

    private void CreateHighscoreEntryTransform(HighscoreEntry highscoreEntry, Transform container, List<Transform> transformList)
    {
        float templateHeight = 65f;

        Transform entryTransform = Instantiate(entryTemplate, container);
        RectTransform entryRectTransform = entryTransform.GetComponent<RectTransform>();
        entryRectTransform.anchoredPosition = new Vector2(0, -templateHeight * transformList.Count);
        entryTransform.gameObject.SetActive(true);

        int rank = transformList.Count + 1;
        string rankString;

        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;

            case 2:
                rankString = "2ND";
                break;

            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }

        entryTransform.Find("posText").GetComponent<TextMeshProUGUI>().SetText(rankString);

        int score = highscoreEntry.score;

        entryTransform.Find("scoreText").GetComponent<TextMeshProUGUI>().SetText(score.ToString());

        string name = highscoreEntry.name;

        entryTransform.Find("nameText").GetComponent<TextMeshProUGUI>().SetText(name);

        transformList.Add(entryTransform);
    }

    private class Highscores
    {
        public List<HighscoreEntry> highscoreEntryList;
    }


    [System.Serializable]
    private class HighscoreEntry
    {
        public int score;
        public string name;
    }

    private void makeAlphabet()
    {
        alphabet.Add("A");
        alphabet.Add("B");
        alphabet.Add("C");
        alphabet.Add("D");
        alphabet.Add("E");
        alphabet.Add("F");
        alphabet.Add("G");
        alphabet.Add("H");
        alphabet.Add("I");
        alphabet.Add("J");
        alphabet.Add("K");
        alphabet.Add("L");
        alphabet.Add("M");
        alphabet.Add("N");
        alphabet.Add("O");
        alphabet.Add("P");
        alphabet.Add("Q");
        alphabet.Add("R");
        alphabet.Add("S");
        alphabet.Add("T");
        alphabet.Add("U");
        alphabet.Add("V");
        alphabet.Add("W");
        alphabet.Add("X");
        alphabet.Add("Y");
        alphabet.Add("Z");
    }

    public void scrollUp(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentLetter++;

            if (currentLetter >= 26)
            {
                currentLetter = 0;
            }
        }
    }

    public void scrollDown(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            currentLetter--;

            if (currentLetter < 0)
            {
                currentLetter = 25;
            }
        }
    }

    public void confirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            inputName += alphabet[currentLetter];
            currentLetter = 0;
            counter--;   
        }
    }

    private IEnumerator AddP1Score()
    {

        string jsonString = PlayerPrefs.GetString("highscoreTable", defaultPref);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        if (highscores.highscoreEntryList[highscores.highscoreEntryList.Count - 1].score < PlayerPrefs.GetInt("P1Score", 12000))
        {
            counter = 3;
            inputName = "";

            while (counter > 0)
            {
                yield return null;
            }
        }

        HighscoreEntry highscoreEntry = new HighscoreEntry { score = PlayerPrefs.GetInt("P1Score", 12000), name = inputName };

        highscores.highscoreEntryList.Add(highscoreEntry);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = 0; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }


        while (highscores.highscoreEntryList.Count > highscoreLimit)
        {
            highscores.highscoreEntryList.RemoveAt(highscores.highscoreEntryList.Count - 1);
        }

        string json = JsonUtility.ToJson(highscores);


        //Use this as a template to determine player scores and which scene to reset to
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        coroutine = AddP2Score();
        StartCoroutine(coroutine);
    }

    private IEnumerator AddP2Score()
    {

        string jsonString = PlayerPrefs.GetString("highscoreTable", defaultPref);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);

        if (highscores.highscoreEntryList[highscores.highscoreEntryList.Count - 1].score < PlayerPrefs.GetInt("P2Score", 12000))
        {
            counter = 3;
            inputName = "";

            while (counter > 0)
            {
                yield return null;
            }
        }

        HighscoreEntry highscoreEntry = new HighscoreEntry { score = PlayerPrefs.GetInt("P2Score", 12000), name = inputName };

        highscores.highscoreEntryList.Add(highscoreEntry);

        for (int i = 0; i < highscores.highscoreEntryList.Count; i++)
        {
            for (int j = 0; j < highscores.highscoreEntryList.Count; j++)
            {
                if (highscores.highscoreEntryList[j].score < highscores.highscoreEntryList[i].score)
                {
                    HighscoreEntry tmp = highscores.highscoreEntryList[i];
                    highscores.highscoreEntryList[i] = highscores.highscoreEntryList[j];
                    highscores.highscoreEntryList[j] = tmp;
                }
            }
        }


        while (highscores.highscoreEntryList.Count > highscoreLimit)
        {
            highscores.highscoreEntryList.RemoveAt(highscores.highscoreEntryList.Count - 1);
        }

        string json = JsonUtility.ToJson(highscores);


        //Use this as a template to determine player scores and which scene to reset to
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();

        makeScoreBoard();
    }

    private void makeScoreBoard()
    {
        string jsonString = PlayerPrefs.GetString("highscoreTable", defaultPref);
        Highscores highscores = JsonUtility.FromJson<Highscores>(jsonString);


        highscoreEntryTransformList = new List<Transform>();
        foreach (HighscoreEntry highscoreEntry in highscores.highscoreEntryList)
        {
            CreateHighscoreEntryTransform(highscoreEntry, entryContainer, highscoreEntryTransformList);
        }
    }
}
