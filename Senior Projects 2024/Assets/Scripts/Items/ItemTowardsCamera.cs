using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Author: Fenn Edmonds

public class ItemTowardsCamera : MonoBehaviour
{

    [SerializeField]
    private Camera m_Camera;

    private void LateUpdate()
    {
        Vector3 cameraPosition = m_Camera.transform.position;
        cameraPosition.y = transform.position.y;
        transform.LookAt(cameraPosition);
        transform.Rotate(0f, 180f, 0f);
    }
}

