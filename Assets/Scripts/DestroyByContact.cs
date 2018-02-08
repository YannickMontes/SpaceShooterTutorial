using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {

    private void OnTriggerEnter(Collider other)
    {
        if (IsBoundary(other.tag))
        {
            return;
        }
        Destroy(other.gameObject);
        Destroy(this.gameObject);
    }

    private bool IsBoundary(string tag)
    {
        return tag == "Boundary";
    }
}
