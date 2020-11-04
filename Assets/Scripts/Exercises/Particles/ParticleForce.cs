using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleForce : MonoBehaviour
{
    public Rigidbody rb;
    private void OnParticleCollision(GameObject other)
    {
        int num = ParticlePhysicsExtensions.GetCollisionEvents(ParticleShooter.GetParticleSystem(), gameObject, ParticleShooter.GetCollisionEvents());
        rb.AddForce(new Vector3(2, 2, 0), ForceMode.Impulse);
    }
}
