using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootablePanel : BaseShootable
{
    [SerializeField] BaseActivatable activatable;

    protected override void OnActivated()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        activatable.OnActivated();
    }

    protected override void OnDeactivated()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        activatable.RemoveActivation();
    }
}
