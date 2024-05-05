using System;
using System.Collections;
using UnityEngine;

public class HandsMovement : MonoBehaviour
{
    [SerializeField] private CharacterController player;
    [SerializeField] private Transform movementPoint;
    [SerializeField] private float speedMultiplier = 1;
    [SerializeField] private float moveThreshold = 0.01f;
    [SerializeField] private float jumpThreshold = 0.02f;
    [SerializeField] private float jumpHeight = 1;
    [SerializeField] private float maxSpeed = 3.33f;

    private XRIDefaultInputActions _actions;

    private HandMovementData _leftHandData;
    private HandMovementData _rightHandData;
    private float _speed;

    private Vector3 _jumpVelocity;

    private void Awake()
    {
        _actions = new XRIDefaultInputActions();
        _actions.Enable();
    }

    private void Update()
    {
        _speed -= _speed * _speed * Time.deltaTime;

        Vector3 cameraRotation = Camera.main.transform.rotation.eulerAngles;
        cameraRotation.x = 0;
        cameraRotation.z = 0;
        movementPoint.eulerAngles = cameraRotation;


        _leftHandData.position = _actions.XRILeftHand.Position.ReadValue<Vector3>();
        _rightHandData.position = _actions.XRIRightHand.Position.ReadValue<Vector3>();


        if (_leftHandData.DeltaPosition.y < -moveThreshold && _rightHandData.DeltaPosition.y > moveThreshold)
        {
            _speed += (Mathf.Abs(_leftHandData.DeltaPosition.y) + Mathf.Abs(_rightHandData.DeltaPosition.y)) * speedMultiplier;
        }
        else if (_leftHandData.DeltaPosition.y > moveThreshold && _rightHandData.DeltaPosition.y < -moveThreshold)
        {
            _speed += (Mathf.Abs(_leftHandData.DeltaPosition.y) + Mathf.Abs(_rightHandData.DeltaPosition.y)) * speedMultiplier;
        }
        else if ( _leftHandData.DeltaPosition.y > jumpThreshold && _rightHandData.DeltaPosition.y > jumpThreshold)
        {
            if(player.isGrounded)
            {
                StartCoroutine(Jump());
            }
        }

        _speed = Mathf.Clamp(_speed, 0f, maxSpeed);
        Vector3 velocity = movementPoint.forward * _speed;
        player.SimpleMove(velocity);
    }

    private void LateUpdate()
    {
       _leftHandData.lastPosition = _actions.XRILeftHand.Position.ReadValue<Vector3>();
       _rightHandData.lastPosition = _actions.XRIRightHand.Position.ReadValue<Vector3>();
    }

    private IEnumerator Jump()
    {
        _jumpVelocity = Vector3.up * jumpHeight;

        do
        {
            _jumpVelocity += Physics.gravity * Time.deltaTime;

            if(_jumpVelocity.y < 0) yield break;

            player.transform.position += _jumpVelocity * Time.deltaTime;
            yield return null;

        } while (!player.isGrounded || _jumpVelocity.y > 0);
    }
}

[Serializable]
public struct HandMovementData
{
     public Vector3 lastPosition;
     public Vector3 position;

     public Vector3 DeltaPosition => position - lastPosition;
}
