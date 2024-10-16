using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;
using Photon.Realtime;





public class Photon1 : MonoBehaviourPunCallbacks

{
    [SerializeField] TMP_InputField NickName;
    [SerializeField] TMP_Text NoNickName;
    [SerializeField] string Room;
    void Start()
    {
     
        NoNickName.text = "";
        
    }
    public override void OnJoinedRoom()
    {
        print("Se entr� al room");
        
        PhotonNetwork.LoadLevel("Juego");
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
