using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRotator : MonoBehaviour
{
    private const float yValue = 1f;

    void Update()
    {
        transform.Rotate(0f, yValue, 0f);
    }
}
