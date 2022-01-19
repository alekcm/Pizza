using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game : MonoBehaviour
{
    public Text playerDisplay;
    public Text scoreDisplay;

    void Awake()
    {
        if (DBManager.username == null)
        {
            SceneManager.LoadScene(0);
        }
        playerDisplay.text = "Player: " + DBManager.username;
        scoreDisplay.text = "Score: " + DBManager.score;
    }

    public void CallSaveData()
    {
        StartCoroutine(SavePlayerData());
    }

    IEnumerator SavePlayerData()
    {
        WWWForm form = new WWWForm();
        form.AddField("name", DBManager.username);
        form.AddField("score", DBManager.score);

        WWW www = new WWW("http://localhost/sqlconnect/savedata.php", form);
        yield return www;
        if (www.text == "0")
        {
            Debug.Log("Game Saved.");
        }
        else
        {
            Debug.Log("Save failed. Error #" + www.text);
        }

        /*DBManager.LogOut();
        SceneManager.LoadScene(0);*/
    }

    public void IncreaseScore()
    {
        DBManager.score = PlayerPrefs.GetInt("Money");
        scoreDisplay.text = "Score: " + DBManager.score;
    }
}
