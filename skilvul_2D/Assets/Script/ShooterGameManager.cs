using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using TMPro;
using UnityEngine;

public class ShooterGameManager : MonoBehaviourPun
{
    [SerializeField] GameObject playerPrefab;

    void Start()
    {
        PhotonNetwork.Instantiate
        (
            playerPrefab.name,
            Vector3.zero,
            Quaternion.identity
        );
    }
}
