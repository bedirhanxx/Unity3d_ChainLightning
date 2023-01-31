using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class RandomMovement : MonoBehaviour
{
    public float Speed = 1f;
    public float X_Range = 10f;
    public float Z_Range = 10f;

    private Vector3 targetPos;

    private void Start()
    {
        targetPos = GetRandomPosition();
    }

    private void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPos, Speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, targetPos) < 0.1f)
        {
            targetPos = GetRandomPosition();
        }
    }

    private Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(-Z_Range, X_Range);
        float randomZ = Random.Range(-Z_Range, X_Range);

        return new Vector3(randomX, transform.position.y, randomZ);
    }
}
