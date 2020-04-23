using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleGizmo : MonoBehaviour
{
    [SerializeField] Color color = Color.cyan;
    [SerializeField] bool isDouble;
    Vector3 offset = new Vector3(0.64f, -0.64f, 0);

	private void OnDrawGizmos()
    {
        Gizmos.color = Color.clear;
        Gizmos.DrawCube(transform.position + (offset * (isDouble ? 2f : 1)), (Vector3.one * 1.28f) * (isDouble ? 2f : 1));
        Gizmos.color = color;
        Gizmos.DrawWireCube(transform.position + (offset * (isDouble ? 2f : 1)) , (Vector3.one * 1.28f) * (isDouble ? 2f : 1));
        Gizmos.DrawLine(transform.position , transform.position + (offset * (isDouble ? 4f : 2)));
    }
}
