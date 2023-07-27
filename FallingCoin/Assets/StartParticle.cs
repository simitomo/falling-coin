using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartParticle : MonoBehaviour
{
    public ParticleSystem particle;

    ParticleSystem newParticle;

    void OnCollisionEnter2D(Collision2D collision)
    {
        newParticle = Instantiate(particle);

        newParticle.transform.position = this.transform.position;

        newParticle.Play();

        Destroy(newParticle.gameObject, 5.0f);
    }
}
