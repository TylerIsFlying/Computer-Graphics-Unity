using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleShooter : MonoBehaviour
{
    public ParticleSystem particleSystem;
    private static ParticleSystem _particleSystem;
    private static List<ParticleCollisionEvent> collisionEvents;

    public static List<ParticleCollisionEvent> GetCollisionEvents() 
    {
        return collisionEvents;
    }
    public static ParticleSystem GetParticleSystem() 
    {
        return _particleSystem;
    }
    void Start()
    {
        _particleSystem = particleSystem;
        particleSystem.gameObject.SetActive(false);
        collisionEvents = new List<ParticleCollisionEvent>();
    }
    private void Update()
    {
        // shoots the particles
        if (Input.GetKey(KeyCode.R)) 
        {
            particleSystem.gameObject.SetActive(true);
        }else if (Input.GetKeyUp(KeyCode.R)) 
        {
            particleSystem.gameObject.SetActive(false);
        }
    }
}
