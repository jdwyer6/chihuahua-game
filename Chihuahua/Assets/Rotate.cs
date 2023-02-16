using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    public float rotationSpeed = 40;

    void FixedUpdate()
    {
        transform.Rotate(0, 0, rotationSpeed);
    }
}
