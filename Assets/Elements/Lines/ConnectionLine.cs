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
    readonly Color ACTIVE = new Color(0, 1, 0, 0.4f);
    readonly Color INACTIVE = new Color(1, 0, 0, 0.4f);

    void Start()
    {
        lineParticles = Instantiate(lineParticlesPrefab);
        lineParticlesMain = lineParticles.GetComponent<ParticleSystem>().main;
        lineParticlesMain.startColor = INACTIVE;

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
        lineParticlesMain.startColor = ACTIVE;
    }

    void OnShootableDeactivated()
    {
        lineParticlesMain.startColor = INACTIVE;
    }
}
