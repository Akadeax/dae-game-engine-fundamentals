using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BaseActivatable : MonoBehaviour
{
    [SerializeField] int numActivationsNeeded = 1;
    [SerializeField] bool allowMoreActivationsThanNeeded = true;
    int currentActivations;

    [SerializeField] bool permanent;

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

    protected abstract void Activate();
    protected abstract void Deactivate();
}
