using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// A base class for anything that can be activated (doors, moving platforms, etc.)
/// </summary>
public abstract class BaseActivatable : MonoBehaviour
{
    // How many different activation sources does this need?
    [SerializeField] int numActivationsNeeded = 1;
    // If there is more activation sources than needed, should it still activate?
    [SerializeField] bool allowMoreActivationsThanNeeded = true;
    // If this is activated once, should it stay active permanently?
    [SerializeField] bool permanent;

    int currentActivations;

    public void OnActivated()
    {
        currentActivations++;
        ActivationsUpdated();
    }

    public void RemoveActivation() 
    { 
        currentActivations--;
        ActivationsUpdated();
    }

    void ActivationsUpdated()
    {
        if (
            (allowMoreActivationsThanNeeded && currentActivations >= numActivationsNeeded) ||
            (!allowMoreActivationsThanNeeded && currentActivations == numActivationsNeeded)
            )
        {
            Activate();
        }
        else if (!permanent)
        {
            Deactivate();
        }
    }

    // Overridables
    protected abstract void Activate();
    protected abstract void Deactivate();
}
