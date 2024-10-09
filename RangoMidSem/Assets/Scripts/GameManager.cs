using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Transform Spawner;

    // Start is called before the first frame update
    void Start()
    {
        Spawn();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Spawn() {
        Vector3 SpawnPoint = Spawner.position;
        PhotonNetwork.Instantiate("Avatar", SpawnPoint, Quaternion.identity);
      



    }
}
