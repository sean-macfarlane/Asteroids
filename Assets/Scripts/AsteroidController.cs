using UnityEngine;
using System.Collections;


///<summary>
///The Controller for an Asteroid: including movement, destruction.
///</summary>
public class AsteroidController : MonoBehaviour
{
    public float minTorque = 100f;  // Minimum Torque
    public float maxTorque = 100f;  // Maximum Torque
    public float respawn_x_min;     // Minimum X can travel
    public float respawn_x_max;     // Maximum X can travel
    public float respawn_y_min;     // Minimum Y can travel
    public float respawn_y_max;     // Maximum Y can travel

    GameController _game; //Reference to the GameController

    // Use this for initialization
    void Start()
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
        _game = gameObject.GetComponent<GameController>();
        float magnitude = Random.Range(_game.getMinForce(), _game.getMaxForce());
        float torque = Random.Range(minTorque, maxTorque);

        float force_x = Random.Range(-1f, 1f);
        float force_y = Random.Range(-1f, 1f);

        GetComponent<Rigidbody2D>().AddForce(magnitude * new Vector2(force_x, force_y));
        GetComponent<Rigidbody2D>().AddTorque(torque);
    }

    // Update is called once per frame
    void Update()
    {
        if(transform.position.x < respawn_x_min)
        {
            Destroy(gameObject);
        }
        else if (transform.position.x > respawn_x_max)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y < respawn_y_min)
        {
            Destroy(gameObject);
        }
        else if (transform.position.y > respawn_y_max)
        {
            Destroy(gameObject);
        }
    }

    ///<summary>
    ///Sets the force of an Asteroid
    ///</summary>
    ///<param name=”direction”>Determines the direction of the force</param>
    public void SetForce(int direction)
    {
        GameObject gameObject = GameObject.FindGameObjectWithTag("GameController");
        _game = gameObject.GetComponent<GameController>();
        float magnitude = Random.Range(_game.getMinForce(), _game.getMaxForce());
        float x = 0;
        float y = 0;

        if(direction == 0)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(-1f, 0);
            GetComponent<Rigidbody2D>().AddForce(magnitude * new Vector2(x, y));
        }
        else if (direction == 1)
        {
            x = Random.Range(-1f, 1f);
            y = Random.Range(0, 1f);
            GetComponent<Rigidbody2D>().AddForce(magnitude * new Vector2(x, y));
        }
        else if (direction == 2)
        {
            y = Random.Range(-1f, 1f);
            x = Random.Range(-1f, 0);
            GetComponent<Rigidbody2D>().AddForce(magnitude * new Vector2(x, y));
        }
        else if (direction == 3)
        {
            y = Random.Range(-1f, 1f);
            x = Random.Range(0, 1f);
            GetComponent<Rigidbody2D>().AddForce(magnitude * new Vector2(x, y));
        }     
    }

    ///<summary>
    ///Turns on the collider if on screen
    ///</summary>
    void OnBecameVisible()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = true;
    }

    ///<summary>
    ///Turns off the collider if off screen
    ///</summary>
    void OnBecameInvisible()
    {
        gameObject.GetComponent<PolygonCollider2D>().enabled = false;
    }
}
