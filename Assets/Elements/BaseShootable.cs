using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseShootable : MonoBehaviour
{
    enum ActivationType
    {
        Timed, Toggle
    }

    [SerializeField] ActivationType type;
    [SerializeField] float activationTimeOnShot = 2f;

    float currentTimeActivatedLeft = 0f;
    bool toggleActivated = false;

    protected abstract void OnActivated();
    protected abstract void OnDeactivated();

    public void OnShot()
    {
        if (type == ActivationType.Timed)
        {
            if (currentTimeActivatedLeft > 0)
            {
                OnDeactivated();
            }
            OnActivated();
            currentTimeActivatedLeft = activationTimeOnShot;
        }
        else
        {
            toggleActivated = !toggleActivated;
            if (toggleActivated)
            {
                OnActivated();
            }
            else
            {
                OnDeactivated();
            }
        }
    }

    private void Update()
    {
        if (currentTimeActivatedLeft <= 0f) return;

        currentTimeActivatedLeft -= Time.deltaTime;

        if (currentTimeActivatedLeft <= 0f)
        {
            currentTimeActivatedLeft = 0f;
            OnDeactivated();
        }
    }
}
