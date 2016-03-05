using UnityEngine;
using System.Collections;

///<summary>
///The Controller for a Shot
///</summary>
public class ShotScript : MonoBehaviour
{
    public float force;     // Force of shot
    public GameObject AsteroidSmallPrefab;  // Prefab of small Asteroid
    public GameObject AsteroidBigPrefab;    // Prefab of big Asteroid

    GameController _game; //Reference to the GameController

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().AddForce(transform.up * force);
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
        _game = gameObject.GetComponent<GameController>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    ///<summary>
    ///Collision Detection for Shot
    ///</summary>
    ///<param name=”collision”>The collision object the shot has collided with</param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("asteroidBig"))
        {
            GameObject a1 = GameObject.Instantiate(AsteroidSmallPrefab) as GameObject;
            GameObject a2 = GameObject.Instantiate(AsteroidSmallPrefab) as GameObject;
            a1.transform.position = collision.gameObject.transform.position;
            a2.transform.position = collision.gameObject.transform.position;

            GetComponents<AudioSource>()[1].Play();
            Destroy(gameObject);
            Destroy(collision.gameObject);
            _game.AddScore(50);
        }

        else if (collision.gameObject.CompareTag("asteroidSmall"))
        {
            GetComponents<AudioSource>()[1].Play();
            Destroy(gameObject);
            Destroy(collision.gameObject);
            _game.AddScore(100);
        }
    }
}
