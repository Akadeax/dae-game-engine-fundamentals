using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerController : MonoBehaviour
{
    public  UnityEvent<Augment> OnAugmentChanged;
    public enum Augment
    {
        None,
        Detacher
    }

    public Augment currentAugment;

    private void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}