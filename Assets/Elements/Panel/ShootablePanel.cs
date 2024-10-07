using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootablePanel : BaseShootable
{
    [SerializeField] List<BaseActivatable> activatable;

    private void Start()
    {
        TextMeshPro mesh = transform.GetChild(0).GetComponent<TextMeshPro>();

        if (type == ActivationType.Timed)
        {
            mesh.text = activationTimeOnShot.ToString();
        }
        else
        {
            mesh.text = "T";
        }
    }

    protected override void OnActivated()
    {
        GetComponent<MeshRenderer>().material.color = Color.green;
        foreach(BaseActivatable activatable in activatable)
        {
            activatable.OnActivated();
        }
    }

    protected override void OnDeactivated()
    {
        GetComponent<MeshRenderer>().material.color = Color.yellow;
        foreach (BaseActivatable activatable in activatable)
        {
            activatable.RemoveActivation();
        }
    }
}
