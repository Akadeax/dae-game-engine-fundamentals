using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ShootablePanel : BaseShootable
{
    [SerializeField] List<BaseActivatable> activatable;

    TextMeshPro text;
    MeshRenderer mesh;

    private void Start()
    {
        text = transform.GetChild(0).GetComponent<TextMeshPro>();
        mesh = transform.GetChild(1).GetComponent<MeshRenderer>();

        if (type == ActivationType.Timed)
        {
            text.text = activationTimeOnShot.ToString();
        }
        else
        {
            text.text = "T";
        }
    }

    protected override void OnActivated()
    {
       mesh.material.color = Color.green;
        foreach(BaseActivatable activatable in activatable)
        {
            if (activatable == null)
            {
                Debug.LogError("Null Activatable in Panel");
                continue;
            }

            activatable.OnActivated();
        }

        if (type == ActivationType.Timed)
        {
            StartCoroutine(TimeLeftUpdate());
        }
    }

    protected override void OnDeactivated()
    {
        mesh.material.color = Color.red;
        foreach (BaseActivatable activatable in activatable)
        {
            if (activatable == null)
            {
                Debug.LogError("Null Activatable in Panel");
                continue;
            }

            activatable.RemoveActivation();
        }
    }

    IEnumerator TimeLeftUpdate()
    {
        while (currentTimeActivatedLeft > 0)
        {
            text.text = currentTimeActivatedLeft.ToString("N1");
            yield return null;
        }

        text.text = activationTimeOnShot.ToString();
    }
}
