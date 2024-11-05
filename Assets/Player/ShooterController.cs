using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] PlayerController playerController;

    [SerializeField] GameObject impactParticlesPrefab;
    ParticleSystem impactParticles;

    [SerializeField] PlayerInputActions inputActions;

    Vector3 origin = Vector3.zero;
    Vector3 direction = Vector3.zero;

    bool shootHeld = false;

    private void Start()
    {
        // re-use the same impact particles instance so we don't have to spawn some anytime we shoot
        GameObject go = Instantiate(impactParticlesPrefab);
        impactParticles = go.GetComponent<ParticleSystem>();
    }

    private void OnEnable()
    {
        inputActions = new PlayerInputActions();

        inputActions.Gameplay.Enable();
        inputActions.Gameplay.Shoot.started += Shoot_started;
        inputActions.Gameplay.Shoot.canceled += Shoot_canceled;
    }

    private void OnDisable()
    {
        inputActions.Gameplay.Disable();
    }


    private void Shoot_started(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        shootHeld = true;
        SetShotPosition();
    }

    private void Shoot_canceled(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        shootHeld = false;

        lineRenderer.enabled = false;
        Transform hitObj = DrawRecursiveRaycast(origin, direction, out var hit);
        lineRenderer.positionCount = 0;

        impactParticles.transform.position = hit.point;
        impactParticles.Play();

        impactParticles.transform.rotation = Quaternion.LookRotation(hit.normal, Vector3.up);

        if (hitObj != null && hitObj.TryGetComponent(out BaseShootable shootableObj))
        {
            shootableObj.OnShot();
        }
    }


    private void Update()
    {
        if (!shootHeld) return;

        if (playerController.currentAugment != PlayerController.Augment.Detacher)
        {
            SetShotPosition();
        }
        DrawShotLine();
    }

    void SetShotPosition()
    {
        direction = Camera.main.transform.forward;
        Vector3 right = Camera.main.transform.right * 0.15f;
        origin = Camera.main.transform.position + new Vector3(0, -0.15f, 0) + right;
    }

    void DrawShotLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 1;
        DrawRecursiveRaycast(origin, direction, out var hit);
    }

    /// <summary>
    /// Execute a linecast in the given rayDirection, reflecting off of mirrors
    /// </summary>
    /// <returns></returns>
    Transform DrawRecursiveRaycast(Vector3 start, Vector3 rayDirection, out RaycastHit hit)
    {
        int reflectionIndex = 0;

        while (true)
        {
            Ray ray = new(start, rayDirection);
            Physics.Raycast(ray, out var hitInfo);
            hit = hitInfo;

            // If nothing was hit (e.g. shooting into the sky), just shoot out a visible ray in the direction anyway
            if (hitInfo.point == Vector3.zero)
            {
                hitInfo.point = ray.origin + ray.direction * 5000f;
            }

            Vector3 startOffset = Vector3.zero;
            // First point gets offset to not clip into player
            if (reflectionIndex == 0)
            {
                startOffset = rayDirection;
            }

            if (lineRenderer.positionCount != 1)
            {
                lineRenderer.positionCount = 1;
            }

            // Set line renderer positions to display the raycast to the player
            lineRenderer.SetPosition(reflectionIndex, start + startOffset);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(reflectionIndex + 1, hitInfo.point);

            if (!hitInfo.transform || !hitInfo.transform.CompareTag("Mirror")) return hitInfo.transform;

            // Re-iterate loop if this is a mirror
            Vector3 mirrored = Vector3.Reflect(ray.direction, hitInfo.normal);
            ++reflectionIndex;
            start = hitInfo.point;
            rayDirection = mirrored;
        }
    }
}
