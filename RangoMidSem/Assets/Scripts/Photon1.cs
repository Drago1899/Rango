using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;
using Unity.VisualScripting.FullSerializer;
using UnityEditor.PackageManager;

public class Photon1 : MonoBehaviourPunCallbacks

{
    private enum Anim
    {
        Anim, Loading
    }
    [SerializeField] TMP_InputField NickName;
    [SerializeField] TMP_Text NoNickName;
    [SerializeField] string Room;
    public Sprite Canva;
    [SerializeField] Animator m_loadingScreen;
    private Anim m_Anim;
   
    void Start()
    {
     
        NoNickName.text = "";
        PhotonNetwork.ConnectUsingSettings();
    }
    public override void OnConnectedToMaster() {
        print("Se ha conectado al master");
        PhotonNetwork.JoinLobby();
        

    }
    public override void OnJoinedLobby()
    {
        print(" ha entrado al lobyy ");
        m_loadingScreen.Play(Anim.Loading.ToString());
    }

    public override void OnJoinedRoom()
    {
        print("Se entró al room");
        
        PhotonNetwork.LoadLevel("Juego");
    }
    public override void OnJoinRoomFailed(short returnCode, string message) {
        base.OnJoinRoomFailed(returnCode, message);
        print("return code " + message);
    }


    RoomOptions newRoomInfo()
    {
        RoomOptions roomOptions = new RoomOptions();
        roomOptions.MaxPlayers = 4;
        roomOptions.IsOpen = true;
        roomOptions.IsVisible = true;
        

        return roomOptions;
    }
  

    public void createRoom()
    {
        if (NickName.text == "")
        {
            print("Necesitas un nombre");
            NoNickName.text = "Necesitas un nombre";
            return;
        }
        PhotonNetwork.NickName = NickName.text;
        PhotonNetwork.CreateRoom(Room, newRoomInfo(), null);
    }

    public void Quit()
    {
        Application.Quit();
        Debug.Log("Cerrando");
    }

}
