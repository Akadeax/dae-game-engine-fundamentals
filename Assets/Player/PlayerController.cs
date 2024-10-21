using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public  UnityEvent<Augment> OnAugmentChanged;
    public enum Augment
    {
        None,
        Detacher
    }

    [SerializeField] float killYThreshold = -100f;

    public Augment currentAugment;

    private void Start()
    {
        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (transform.position.y <= killYThreshold)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}