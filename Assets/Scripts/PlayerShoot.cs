using UnityEngine;
using System.Collections;

///<summary>
///To handle a Player shooting
///</summary>
public class PlayerShoot : MonoBehaviour
{
    public GameObject ShotPrefab;   // Prefab of Shot

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Shoot 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject shot = GameObject.Instantiate(ShotPrefab, transform.position, transform.rotation) as GameObject;
            Destroy(shot, 2);
        }
    }
}
