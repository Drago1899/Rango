using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Photon0 : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()

    {

        PhotonNetwork.ConnectUsingSettings();

    }

    public override void OnConnectedToMaster()
    {
        print("Se ha conectado al master");
        PhotonNetwork.JoinLobby();

    }

    public override void OnJoinedLobby()
    {
        PhotonNetwork.LoadLevel("Menu");
        Debug.Log("Lobby");


    }

}
