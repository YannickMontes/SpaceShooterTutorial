using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin;
    public float xMax;
    public float zMin;
    public float zMax;
}

public class PlayerController : MonoBehaviour {

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shotPrefab;
    public Transform shotSpawn;
    public float fireRate;
    private float nextFire = 0.0f;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time > nextFire)
        {
            nextFire = Time.time + fireRate;
            Shot();
        }
    }
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().velocity = new Vector3(moveHorizontal * speed, 0.0f, moveVertical * speed);

        StickToTheScreen();

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(0.0f, 0.0f, GetComponent<Rigidbody>().velocity.x * -tilt);
    }

    private void StickToTheScreen()
    {
        Vector3 position = GetComponent<Rigidbody>().position;
        GetComponent<Rigidbody>().position = new Vector3(
            Mathf.Clamp(position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(position.z, boundary.zMin, boundary.zMax));
    }

    private void Shot()
    {
        GameObject shot = GameObject.Instantiate(shotPrefab, shotSpawn.position, shotSpawn.rotation);
        GetComponent<AudioSource>().Play();
    }
}
