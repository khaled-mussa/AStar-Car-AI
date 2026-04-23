using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarAi : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float turnSpeed = 5f;
    public AStarAlgorithm pathfinding;

    
    List<Node> path;

    int targetIndex = 0;

    void Start()
    {
        if (pathfinding == null)
        {
            pathfinding = FindObjectOfType<AStarAlgorithm>();
        }
        if (target == null)
        {
            Debug.LogError("Target not assigned!");
            return;
        }

        path = pathfinding.FindPath(transform.position, target.position);
    }

    void Update()
    {
        if (path == null || path.Count == 0)
            return;

        Vector3 targetPosition = path[targetIndex].worldPosition;

        Vector3 direction = (targetPosition - transform.position).normalized;

        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Lerp(transform.rotation, lookRotation, turnSpeed * Time.deltaTime);

        transform.position += transform.forward * speed * Time.deltaTime;

        float distance = Vector3.Distance(transform.position, targetPosition);

        if (distance < 0.5f)
        {
            targetIndex++;

            if (targetIndex >= path.Count)
            {
                Debug.Log("Goal Reached");
                enabled = false;
            }
        }
    }
}
