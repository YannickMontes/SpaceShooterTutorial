using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject asteroidExplosionPrefab;
    public GameObject playerExplosionPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (IsBoundary(other.tag))
        {
            return;
        }
        if (IsPlayer(other.tag))
        {
            Instantiate(playerExplosionPrefab, other.transform.position, other.transform.rotation);
        }
        Instantiate(asteroidExplosionPrefab, transform.position, transform.rotation);
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

    private bool IsBoundary(string tag)
    {
        return tag == "Boundary";
    }

    private bool IsPlayer(string tag)
    {
        return tag == "Player";
    }
}
