using System.Collections.Generic;
using UnityEngine;
using Invector.vCharacterController;

public class CameraController : MonoBehaviour
{
    public Transform target;
    public List<Transform> targets = new List<Transform>();
    public float distance = 10.0f;
    public float xSpeed = 250.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float distanceMin = 3f;
    public float distanceMax = 15f;

    private int currentTargetIndex = 0;
    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        if (targets.Count > 0)
        {
            target = targets[currentTargetIndex];
        }
    }

    void Update()
    {
        if (targets.Count == 0) return;

        // Cycle through targets with arrow keys
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            currentTargetIndex = (currentTargetIndex - 1 + targets.Count) % targets.Count;
            target = targets[currentTargetIndex];
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            currentTargetIndex = (currentTargetIndex + 1) % targets.Count;
            target = targets[currentTargetIndex];
        }

        // Check if the target is dead and switch to the closest alive AI
        if (target == null || (target.GetComponent<vCharacter>() != null && target.GetComponent<vCharacter>().isDead))
        {
            targets.Remove(target);
            if (targets.Count > 0)
            {
                target = FindClosestTarget();
                currentTargetIndex = targets.IndexOf(target);
            }
            else
            {
                target = null;
            }
        }

        // Rotate around the target with mouse
        if (target && Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * distance * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = ClampAngle(y, yMinLimit, yMaxLimit);
        }

        // Zoom in and out with the mouse wheel
        distance = Mathf.Clamp(distance - Input.GetAxis("Mouse ScrollWheel") * 5, distanceMin, distanceMax);
    }

    void LateUpdate()
    {
        if (target)
        {
            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 position = rotation * new Vector3(0.0f, 0.0f, -distance) + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }

    Transform FindClosestTarget()
    {
        Transform closest = null;
        float closestDistance = Mathf.Infinity;
        Vector3 currentPosition = transform.position;
        foreach (Transform potentialTarget in targets)
        {
            if (potentialTarget == null || (potentialTarget.GetComponent<vCharacter>() != null && potentialTarget.GetComponent<vCharacter>().isDead)) continue;

            float distanceToTarget = Vector3.Distance(potentialTarget.position, currentPosition);
            if (distanceToTarget < closestDistance)
            {
                closest = potentialTarget;
                closestDistance = distanceToTarget;
            }
        }
        return closest;
    }

    static float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360F)
            angle += 360F;
        if (angle > 360F)
            angle -= 360F;
        return Mathf.Clamp(angle, min, max);
    }

    public void AddTarget(Transform newTarget)
    {
        if (newTarget != null && !targets.Contains(newTarget))
        {
            targets.Add(newTarget);
            Debug.Log("Target added: " + newTarget.name);
            if (target == null)
            {
                target = newTarget;
                currentTargetIndex = targets.IndexOf(target);
            }
        }
    }

    public void ClearTargets()
    {
       
        target = null;
        currentTargetIndex = 0;
        Debug.Log("Targets cleared.");
    }
}
