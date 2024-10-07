using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatableMovingPlatform : BaseActivatable
{
    [SerializeField] Transform targetPos;
    [SerializeField] float unitsPerSecSpeed = 10;

    Vector3 startingPos;
    bool atTarget = false;

    Coroutine toggleCoroutine;
    bool toggleCoroutineRunning = false;

    void Start()
    {
        startingPos = transform.position;
    }

    protected override void Activate()
    {
        if (toggleCoroutineRunning)
        {
            StopCoroutine(toggleCoroutine);
        }
        atTarget = false;
        toggleCoroutine = StartCoroutine(Toggle());
    }
    protected override void Deactivate()
    {
        if (toggleCoroutineRunning)
        {
            StopCoroutine(toggleCoroutine);
        }
        atTarget = true;
        toggleCoroutine = StartCoroutine(Toggle());
    }


    IEnumerator Toggle()
    {
        toggleCoroutineRunning = true;
        atTarget = !atTarget;

        if (atTarget)
        {
            while (Vector3.Distance(transform.position, targetPos.position) > 0.5f)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPos.position, Time.deltaTime * unitsPerSecSpeed);
                yield return null;
            }
        }
        else
        {
            while (transform.localScale.y > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, startingPos, Time.deltaTime * unitsPerSecSpeed);
                yield return null;
            }
        }

        toggleCoroutineRunning = false;
    }
}
