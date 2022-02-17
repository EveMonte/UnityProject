using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]

[AddComponentMenu("Control Script/FPS Input")]

public class S_Movement : MonoBehaviour
{
    private CharacterController _charController;

    public float speed;
    public float gravity;
    // Start is called before the first frame update
    void Start()
    {
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            float deltaZ = -Input.GetAxis("Horizontal") * speed;
            float deltaX = Input.GetAxis("Vertical") * speed;
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);
        }
        catch (Exception ex)
        {
            S_Logger.WriteLog(ex.Message);
        }
    }
}
