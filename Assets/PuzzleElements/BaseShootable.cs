using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A base class for anything that can be shot, such as panels or augment stations.
/// This shooting can then trigger either Activatables or something else.
/// </summary>
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

    // Timed means this shootable stays activated for `activationTimeOnShot` seconds
    // Toggle means it is flipped active/inactive by shooting it
    [SerializeField] protected ActivationType type;
    [SerializeField] protected float activationTimeOnShot = 2f;

    protected float currentTimeActivatedLeft = 0f;
    bool toggleActivated = false;

    // Overridables
    protected abstract void OnActivated();
    protected abstract void OnDeactivated();

    public void OnShot()
    {
        if (type == ActivationType.Timed)
        {
            if (currentTimeActivatedLeft > 0)
            {
                OnDeactivated();
                Deactivation?.Invoke();
            }

            currentTimeActivatedLeft = activationTimeOnShot;

            OnActivated();
            Activation?.Invoke();

        }
        else
        {
            toggleActivated = !toggleActivated;
            if (toggleActivated)
            {
                OnActivated();
                Activation?.Invoke();
            }
            else
            {
                OnDeactivated();
                Deactivation?.Invoke();
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
            Deactivation?.Invoke();
        }
    }
}
