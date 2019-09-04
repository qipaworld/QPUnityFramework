using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class RigidbodyParticleWind : MonoBehaviour
{
    public Transform obj;
    ParticleSystem particlesSystem;
    ParticleSystem.Particle[] particles;
    Rigidbody myRigidbody;

    void Start()
    {
        particlesSystem = gameObject.GetComponent<ParticleSystem>();
        particles = new ParticleSystem.Particle[1];
        SetupParticleSystem();
        myRigidbody = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        particlesSystem.GetParticles(particles);

        myRigidbody.velocity += particles[0].velocity;
        particles[0].position = myRigidbody.position;
        particles[0].velocity = Vector3.zero;

        particlesSystem.SetParticles(particles, 1);
    }

    void SetupParticleSystem()
    {
        particlesSystem.startLifetime = Mathf.Infinity;
        particlesSystem.startSpeed = 0;
        particlesSystem.simulationSpace = ParticleSystemSimulationSpace.World;
        particlesSystem.maxParticles = 1;
        particlesSystem.emissionRate = 1;
        //cant set the following with code so you need to do it manually
        //1 - Enable "External Forces"
        //2 - Disable "Renderer"

        //the below is to start the particle at the center
        particlesSystem.Emit(1);
        particlesSystem.GetParticles(particles);
        particles[0].position = Vector3.zero;
        particlesSystem.SetParticles(particles, 1);
    }
}