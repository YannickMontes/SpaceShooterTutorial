using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour
{
    public GameObject explosionPrefab;
    public GameObject playerExplosionPrefab;
    public int scoreValue;

    private void OnTriggerEnter(Collider other)
    {
        if (IsBoundary(other) || IsEnemy(other))
        {
            return;
        }
        if (IsPlayer(other))
        {
            GameController.GetInstance().GameOver();
            Instantiate(playerExplosionPrefab, other.transform.position, other.transform.rotation);
        }
        if(explosionPrefab != null)
            Instantiate(explosionPrefab, transform.position, transform.rotation);

        GameController.GetInstance().AddScore(scoreValue);

        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

    private bool IsBoundary(Collider other)
    {
        return other.CompareTag("Boundary");
    }

    private bool IsPlayer(Collider other)
    {
        return other.CompareTag("Player");
    }

    private bool IsEnemy(Collider other)
    {
        return other.CompareTag("Enemy");
    }
    private bool IsHazard(Collider other)
    {
        return other.CompareTag("Hazard");
    }
}
