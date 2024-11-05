using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Augments change something about the players mechanics
    public enum Augment
    {
        None, // Nothing
        Detacher // The player's laser stays at the location and orientation where the shoot button was held down
    }

    // Notify anyone interested when the player's augment changes
    public UnityEvent<Augment> OnAugmentChanged;

    // Reset the level when below this y coordinate
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
