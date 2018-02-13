using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour {

    public float scroolSpeed;
    public float tileSizeZ;

    private Vector3 startPosition;

    private void Start()
    {
        startPosition = transform.position;
        Debug.Log(startPosition);
    }

    private void Update()
    {
        float newPosition = Mathf.Repeat(Time.time * scroolSpeed, tileSizeZ);
        transform.position = startPosition + Vector3.forward * newPosition;
    }
}
