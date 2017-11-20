using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSpace : MonoBehaviour {

    // Use this for initialization
    public Transform spaceTransform;
    ParticleSystem particle;
	void Start () {
        particle = GetComponent<ParticleSystem>();
        spaceTransform = GameObject.Find("meteorSpace").transform;
        var main = particle.main;
        main.customSimulationSpace = spaceTransform;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
