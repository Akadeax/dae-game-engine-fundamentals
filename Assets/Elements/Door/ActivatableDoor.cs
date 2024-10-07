using System.Collections;
using UnityEditor.UI;
using UnityEngine;

public class ActivatableDoor : BaseActivatable
{
    float startingYScale = 0f;
    bool open = false;

    Coroutine toggleCoroutine;
    bool toggleCoroutineRunning = false;

    private void Start()
    {
        startingYScale = transform.localScale.y;
    }

    protected override void Activate()
    {
        if (toggleCoroutineRunning)
        {
            StopCoroutine(toggleCoroutine);
        }
        open = false;
        toggleCoroutine = StartCoroutine(Toggle());
    }
    protected override void Deactivate()
    {
        if (toggleCoroutineRunning)
        {
            StopCoroutine(toggleCoroutine);
        }
        open = true;
        toggleCoroutine = StartCoroutine(Toggle());
    }

    IEnumerator Toggle()
    {
        toggleCoroutineRunning = true;
        open = !open;

        if (!open)
        {
            while (transform.localScale.y < startingYScale)
            {
                ChangeYScale(Time.deltaTime);
                yield return null;
            }
            SetYScale(startingYScale);
        }
        else
        {
            while (transform.localScale.y > 0)
            {
                ChangeYScale(-Time.deltaTime);
                yield return null;
            }
            SetYScale(0);
        }

        toggleCoroutineRunning = false;
    }

    void ChangeYScale(float delta)
    {
        SetYScale(transform.localScale.y + delta);
    }

    void SetYScale(float val)
    {
        Vector3 localScale = transform.localScale;
        localScale.y = val;
        transform.localScale = localScale;
    }
}
