using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

///<summary>
///The GameController of the Start Screen
///</summary>
public class StartScreen : MonoBehaviour
{
    public string sceneName;   //  Name of Start Scene
    public Text highscoreText;  // Reference to Highscore Text

    string _highscoreKey = "VALUE_HIGHSCORE";   //Stores the Highscore

    // Use this for initialization
    void Start()
    {
        if (PlayerPrefs.HasKey(_highscoreKey))
        {
            highscoreText.text = "HIGHSCORE: " + PlayerPrefs.GetInt(_highscoreKey);
        }
        else
        {
            PlayerPrefs.SetInt(_highscoreKey, 0);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Invoke("LoadScene", 3.0f);
        }
    }

    ///<summary>
    ///Loads the next scene
    ///</summary>
    void LoadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
