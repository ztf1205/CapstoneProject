using Obi;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectInteraction : MonoBehaviour
{
    public GameObject particle1;
    public GameObject particle2;
    private ParticleSystem ps1;
    private ParticleSystem ps2;


    void Start()
    {
        ps1 = particle1.GetComponent<ParticleSystem>();
        ps2 = particle2.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnParticleCollision(GameObject other)
    {
        ps1.Stop();
        StartCoroutine(FireOff());
    }

    private IEnumerator FireOff()
    {
        yield return new WaitForSeconds(2f);
        ps2.Stop();
    }
}
