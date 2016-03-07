using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

///<summary>
///The Controller for a Player: including movement, handling collisions, and destruction.
///</summary>
public class PlayerController : MonoBehaviour
{
    public float speed;     // Speed of Player
    public float rotationAngle;     // Rotation Angle of Player
    public GameObject lifePrefab;   // Life object
    public int lives;       // The number of lives a Player starts with
    public string startSceneName;   //  Name of Start Scene
    public GameObject explosionPrefab;

    GameController _game; //Reference to the GameController
    Vector2 _lifePositon;    // Position of the previous life object instatiated
    GameObject[] _lifePrefabs;    //The Sprites for the Lives
    int _livesCount;        // The current amount of lives a Player has

    // Use this for initialization
    void Start()
    {
        _livesCount = lives;
        _lifePrefabs = new GameObject[lives];
        for (int i = 0; i < lives; i++)
        {
            GameObject life = GameObject.Instantiate(lifePrefab) as GameObject;
            if (i == 0)
            {
                _lifePositon = life.transform.position;
            }
            else
            {
                _lifePositon.x += 0.7f;
            }
            life.transform.position = _lifePositon;
            _lifePrefabs[i] = life;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Move forward
        if (Input.GetKey(KeyCode.UpArrow))
        {
            GetComponent<Rigidbody2D>().AddForce(transform.up * speed * Time.deltaTime);
        }

        // Rotate Left
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            Vector3 rotation = transform.localRotation.eulerAngles;
            rotation.z += rotationAngle * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(rotation);
        }
        // Rotate Right
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Vector3 rotation = transform.localRotation.eulerAngles;
            rotation.z -= rotationAngle * Time.deltaTime;
            transform.localRotation = Quaternion.Euler(rotation);
        }
    }

    ///<summary>
    ///Collision Detection for Player
    ///</summary>
    ///<param name=”collision”>The collision object the player has collided with</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("asteroidBig") || collision.gameObject.CompareTag("asteroidSmall"))
        {
            Destroy(collision.gameObject);
            if (_livesCount == 0)
            {
                AudioSource[] audios = FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
                foreach (AudioSource aud in audios)
                    aud.Stop();

                gameObject.GetComponent<SpriteRenderer>().enabled = false;
                gameObject.GetComponent<PolygonCollider2D>().enabled = false;
                GameObject ex = GameObject.Instantiate(explosionPrefab) as GameObject;
                ex.transform.position = transform.position;
                StartCoroutine(Explosion());

            }
            else
            {
                _livesCount--;
                Destroy(_lifePrefabs[_livesCount]);

                if (_livesCount == 2)
                {
                    GetComponents<AudioSource>()[0].Play();
                }
                else if (_livesCount == 1)
                {
                    GetComponents<AudioSource>()[1].Play();
                }
                else if (_livesCount == 0)
                {
                    GetComponents<AudioSource>()[2].Play();
                }
                StartCoroutine(Flash(0.2f, 0.05f));
            }
        }
    }

    ///<summary>
    ///Routine to flash Player when hit
    ///</summary>
    ///<param name=”time”>the length of flashing</param>
    ///<param name=”intervalTime”>the length of each flash</param>
    IEnumerator Flash(float time, float intervalTime)
    {
        float elapsedTime = 0f;
        int flag = 0;

        while (elapsedTime < time)
        {
            if (flag % 2 == 0)
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().enabled = true;
            }
            elapsedTime += Time.deltaTime;
            flag++;
            yield return new WaitForSeconds(intervalTime);
        }

        gameObject.GetComponent<SpriteRenderer>().enabled = true;
    }

    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2);

        GameObject gameController = GameObject.FindGameObjectWithTag("GameController");
        _game = gameController.GetComponent<GameController>();
        _game.UpdateHighscore();
        SceneManager.LoadScene(startSceneName);
    }
}
