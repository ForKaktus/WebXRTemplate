using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerReferences : MonoBehaviour
{
    public static PlayerReferences instance;

    public Transform rightHand;
    public Transform leftHand;

    private void Awake()
    {
        instance = this;
    }
}
