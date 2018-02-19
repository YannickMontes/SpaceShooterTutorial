using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Limits
{
    public float min;
    public float max;
}

public class EvasiveBehaviour : MonoBehaviour {

    public float dodge;
    public float smoothing;
    public float tilt;
    public Limits startWait;
    public Limits maneuverTime;
    public Limits maneuverWait;
    public Boundary boundary;

    private float currentSpeed;
    private float target;
    private Rigidbody rb;

    private void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        currentSpeed = rb.velocity.z;
        StartCoroutine(Evade());
    }

    private IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startWait.min, startWait.max));

        while (true)
        {
            target = Random.Range(1, dodge) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.min, maneuverTime.max));
            target = 0;
            yield return new WaitForSeconds(Random.Range(maneuverWait.min, maneuverWait.max));
        }
    }

    private void FixedUpdate()
    {
        float newManeuveur = Mathf.MoveTowards(rb.velocity.x, target, Time.deltaTime * smoothing);
        this.rb.velocity = new Vector3(newManeuveur, 0f, currentSpeed);
        this.rb.position = new Vector3(
            Mathf.Clamp(rb.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rb.position.z, boundary.zMin, boundary.zMax));

        this.rb.rotation = Quaternion.Euler(0.0f, 0.0f, rb.velocity.x * -tilt);
    }
}
