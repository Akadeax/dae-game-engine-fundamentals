using System.Collections;
using UnityEngine;

public class ActivatableDoor : BaseActivatable
{
    [SerializeField] bool inverted = false;

    float startingYScale = 0f;
    bool open = false;

    Coroutine toggleCoroutine;
    bool toggleCoroutineRunning = false;

    private void Start()
    {
        startingYScale = transform.localScale.y;

        if (inverted) Open();
    }

    protected override void Activate()
    {
        if (inverted)
        {
            Close();
            return;
        }

        Open();
    }

    protected override void Deactivate()
    {
        if (inverted)
        {
            Open();
            return;
        }

        Close();
    }

    void Open()
    {
        if (toggleCoroutineRunning)
        {
            StopCoroutine(toggleCoroutine);
        }
        open = false;
        toggleCoroutine = StartCoroutine(Toggle());
    }

    void Close()
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
