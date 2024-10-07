using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public abstract class BaseActivatable : MonoBehaviour
{
    [SerializeField] int numActivationsNeeded = 1;
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
        if (currentActivations >= numActivationsNeeded)
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
