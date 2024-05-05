using Unity.Netcode;
using UnityEngine;

public class TransformDublicator : NetworkBehaviour
{
    [SerializeField] private Transform target;

    private void OnConnectedToServer()
    {
        enabled = IsOwner;
    }

    private void Update()
    {
        if (IsOwner)
        {
            Dublicate();
        }
    }

    public void Dublicate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}