using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDie : MonoBehaviour
{

    // kills a object when hit by any particles
    private void OnParticleCollision(GameObject other)
    {
        Destroy(this.gameObject);
    }
}
