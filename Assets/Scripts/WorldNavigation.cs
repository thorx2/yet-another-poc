using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldNavigation : MonoBehaviour
{
    [SerializeField]
    private Camera mainCameraRef;


    [SerializeField]
    [Range(1, 5)]
    private float cameraPanSpeed;

    private float xDelta;
    private float yDelta;

    void Update()
    {
        if (mainCameraRef != null)
        {
            mainCameraRef.transform.position += new Vector3(xDelta * cameraPanSpeed * Time.deltaTime, 0, yDelta * cameraPanSpeed * Time.deltaTime);
        }
    }

    public void OnMapNavigation(InputValue value)
    {
        var v = value.Get<Vector2>();
        xDelta = v.x * -1;
        yDelta = v.y * -1;
    }
}
