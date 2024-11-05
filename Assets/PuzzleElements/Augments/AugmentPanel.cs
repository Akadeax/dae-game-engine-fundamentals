using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AugmentPanel : BaseShootable
{
    // which augment to give the player on shot
    [SerializeField] PlayerController.Augment augment;

    PlayerController controller;

    private void Awake()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    protected override void OnActivated()
    {
        controller.currentAugment = augment;
        controller.OnAugmentChanged.Invoke(augment);
    }

    protected override void OnDeactivated() 
    {
    }
}
