using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectionLine : MonoBehaviour
{
    [SerializeField] BaseShootable shootable;
    [SerializeField] GameObject source;
    [SerializeField] GameObject lineParticlesPrefab;

    GameObject lineParticles;
    ParticleSystem.MainModule lineParticlesMain;

    const float TRAVEL_SPEED = 2f;

    void Start()
    {
        lineParticles = Instantiate(lineParticlesPrefab);
        lineParticlesMain = lineParticles.GetComponent<ParticleSystem>().main;
        lineParticlesMain.startColor = Color.red;

        StartCoroutine(Move());

        shootable.Activation += OnShootableActivated;
        shootable.Deactivation += OnShootableDeactivated;
    }

    IEnumerator Move()
    {
        float value = 0f;

        while (value < 1f) 
        {
            value += Time.deltaTime * TRAVEL_SPEED;
            lineParticles.transform.position = Vector3.Lerp(source.transform.position, transform.position, value);
            yield return null;
        }

        StartCoroutine(Move());
    }

    void OnShootableActivated()
    {
        lineParticlesMain.startColor = Color.green;
    }

    void OnShootableDeactivated()
    {
        lineParticlesMain.startColor = Color.red;
    }
}
