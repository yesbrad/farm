using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float speed = 5;
    public Transform playerTransform;

    void Update () {
        transform.position = playerTransform.position + -Vector3.forward;// Vector3.MoveTowards(transform.position , playerTransform.position + -Vector3.forward , speed * Time.deltaTime);
    }
}
