using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Events;

public class NetworkPlayer : NetworkBehaviour
{
    [SerializeField] private Transform rightHand;
    [SerializeField] private Transform leftHand;

    private void Start()
    {
        if (IsOwner)
        {
            rightHand.GetChild(0).gameObject.SetActive(false);
            leftHand.GetChild(0).gameObject.SetActive(false);
        }
    }

    private void Update()
    {
        if (IsOwner)
        {
            UpdateTransform();
        }
    }

    public void UpdateTransform()
    {
        rightHand.position = PlayerReferences.instance.rightHand.position;
        rightHand.rotation = PlayerReferences.instance.rightHand.rotation;

        leftHand.position = PlayerReferences.instance.leftHand.position;
        leftHand.rotation = PlayerReferences.instance.leftHand.rotation;
    }
}
