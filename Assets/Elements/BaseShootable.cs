using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class BaseShootable : MonoBehaviour
{
    public delegate void ActivationEvent();
    public event ActivationEvent Activation;

    public delegate void DeactivationEvent();
    public event DeactivationEvent Deactivation;

    protected enum ActivationType
    {
        Timed, Toggle
    }

    [SerializeField] protected ActivationType type;
    [SerializeField] protected float activationTimeOnShot = 2f;

    protected float currentTimeActivatedLeft = 0f;
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
                Deactivation.Invoke();
            }

            currentTimeActivatedLeft = activationTimeOnShot;

            OnActivated();
            Activation.Invoke();

        }
        else
        {
            toggleActivated = !toggleActivated;
            if (toggleActivated)
            {
                OnActivated();
                Activation.Invoke();
            }
            else
            {
                OnDeactivated();
                Deactivation.Invoke();
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
            Deactivation.Invoke();
        }
    }
}
