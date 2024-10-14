using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;
    [SerializeField] PlayerController playerController;

    Vector3 origin = Vector3.zero;
    Vector3 direction = Vector3.zero;

    private void Update()
    {
        // Align shot
        if (Input.GetMouseButtonDown(0))
        {
            SetShotPosition();
        }
        // Keep on drawing shot while held
        else if (Input.GetMouseButton(0))
        {
            if (playerController.currentAugment != PlayerController.Augment.Detacher)
            {
                SetShotPosition();
            }
            DrawShotLine();
        }
        // Release shot
        else if (Input.GetMouseButtonUp(0))
        {
            lineRenderer.enabled = false;
            Transform obj = DrawRecursiveRaycast(0, origin, direction);
            lineRenderer.positionCount = 0;

            if (obj != null && obj.TryGetComponent(out BaseShootable shootableObj))
            {
                shootableObj.OnShot();
            }
        }
        // Redirect shot
        if (Input.GetMouseButtonDown(1))
        {
            SetShotPosition();
        }
    }

    void SetShotPosition()
    {
        origin = Camera.main.transform.position + new Vector3(0, -0.15f, 0);
        direction = Camera.main.transform.forward;
    }

    void DrawShotLine()
    {
        lineRenderer.enabled = true;
        lineRenderer.positionCount = 1;
        DrawRecursiveRaycast(0, origin, direction);
    }


    Transform DrawRecursiveRaycast(int index, Vector3 start, Vector3 direction)
    {
        while (true)
        {
            Ray ray = new(start, direction);
            Physics.Raycast(ray, out var hitInfo);

            // If nothing was hit, just shoot out a visible ray anyway
            if (hitInfo.point == Vector3.zero)
            {
                hitInfo.point = ray.origin + ray.direction * 5000f;
            }

            Vector3 startOffset = Vector3.zero;
            // First point gets offset to not clip into player
            if (index == 0)
            {
                startOffset = direction;
            }

            lineRenderer.SetPosition(index, start + startOffset);
            lineRenderer.positionCount++;
            lineRenderer.SetPosition(index + 1, hitInfo.point);

            if (!hitInfo.transform || !hitInfo.transform.CompareTag("Mirror")) return hitInfo.transform;

            Vector3 mirrored = Vector3.Reflect(ray.direction, hitInfo.normal);
            ++index;
            start = hitInfo.point;
            direction = mirrored;
        }
    }
}
