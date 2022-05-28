using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thingyCollider : MonoBehaviour
{
    [SerializeField] private Rigidbody rigidbody;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.rigidbody);
        collision.rigidbody.velocity = -this.rigidbody.velocity*2;
    }
}
