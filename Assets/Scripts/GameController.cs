using UnityEngine;
using UnityEngine.UI;
using System.Collections;

///<summary>
///The GameController of the game to handle Spawning Asteroids and Score
///</summary>
public class GameController : MonoBehaviour
{
    public GameObject AsteroidPrefab;   //Asteroids Prefab
    public int asteroidCount;   //How many asteroids to spawn
    public float min_X;         //Minimum X position of game
    public float max_X;         // Maximum X position of game
    public float min_Y;         // Minimum Y position of game
    public float max_Y;         // Maximum Y position of game
    public float spawnWait;     // Wait time between every asteroid Spawn
    public float startWait;     // Starting wait time for spawning
    public float waveWait;      // Wait time between waves of spawning
    public float minForce;      // Minimum asteroid Force
    public float maxForce;      // Maximum asteroid force
    public Text scoreText;      // Text object for Score

    string _highscoreKey = "VALUE_HIGHSCORE";   //Stores the Highscore
    int _score;     // Score value
    float _time = 1f;   // Elasped time value

    // Use this for initialization
    void Start()
    {
        _score = 0;
        UpdateScore();
        StartCoroutine(SpawnAsteroids());
    }

    // Update is called once per frame
    void Update()
    {
        _time += Time.deltaTime;
        if ((int)_time % 10 == 0)
        {
            _time++;
            minForce += 5;
            maxForce += 5;
        }
        if ((int)_time % 30 == 0)
        {
            _time++;
            asteroidCount++;
        }
    }

    ///<summary>
    ///Routine for Spawning Asteroids
    ///</summary>
    IEnumerator SpawnAsteroids()
    {
        yield return new WaitForSeconds(startWait);
        while (true)
        {
            for (int i = 0; i < asteroidCount; i++)
            {
                float x;
                float y;
                GameObject a = GameObject.Instantiate(AsteroidPrefab) as GameObject;

                if (Random.Range(0, 2) == 1)
                {
                    x = Random.Range(min_X, max_X);
                    if (Random.Range(0, 2) == 1)
                    {
                        y = max_Y + 3;
                        a.GetComponent<AsteroidController>().SetForce(0);
                    }
                    else
                    {
                        y = min_Y - 3;
                        a.GetComponent<AsteroidController>().SetForce(1);
                    }
                }
                else
                {
                    y = Random.Range(min_Y, max_Y);
                    // spawn on left (true) or right (false)
                    if (Random.Range(0, 2) == 1)
                    {
                        x = max_X + 3;
                        a.GetComponent<AsteroidController>().SetForce(2);
                    }
                    else
                    {
                        x = min_X - 3;
                        a.GetComponent<AsteroidController>().SetForce(3);
                    }
                }

                a.transform.position = new Vector2(x, y);
                yield return new WaitForSeconds(spawnWait);
            }
            yield return new WaitForSeconds(waveWait);
        }
    }

    ///<summary>
    ///Updates the scoreboard
    ///</summary>
    void UpdateScore()
    {
        scoreText.text = "SCORE: " + _score;
    }

    ///<summary>
    ///Adds points to the current score
    ///</summary>
    ///<param name=”points”>The amount of points to add to the score</param>
    public void AddScore(int points)
    {
        _score += points;
        UpdateScore();
    }

    ///<summary>
    ///Updates the highscore
    ///</summary>
    public void UpdateHighscore()
    {
        if (PlayerPrefs.GetInt(_highscoreKey) < _score)
        {
            PlayerPrefs.SetInt(_highscoreKey, _score);
        }
    }

    ///<summary>
    ///Getter for the Minimum Force for asteroids
    ///</summary>
    ///<return>the amount of minimum force</return>
    public float getMinForce()
    {
        return minForce;
    }

    ///<summary>
    ///Getter for the Maximum Force for asteroids
    ///</summary>
    ///<return>the amount of Maximum force</return>
    public float getMaxForce()
    {
        return maxForce;
    }
}
