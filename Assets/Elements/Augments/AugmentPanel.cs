using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentPanel : BaseShootable
{
    [SerializeField] PlayerController.Augment augment;

    protected override void OnActivated()
    {
        var controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        controller.currentAugment = augment;
        controller.OnAugmentChanged.Invoke(augment);
    }

    protected override void OnDeactivated() 
    {
    }
}
