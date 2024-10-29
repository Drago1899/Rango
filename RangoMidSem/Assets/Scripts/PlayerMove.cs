using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject Flechita;
    [SerializeField] private Transform SpawnFlecha;

    [SerializeField] private float velocity;
    PhotonView m_PV;
    ParticleSystem m_PS;


    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        m_PS = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        m_PV = GetComponent<PhotonView>();
        if (m_PV.IsMine) {
            Flechita.SetActive(true);


        }
        m_PS.Stop();
        m_PS.Play();


    }

    // Update is called once per frame
    void Update()
    {
        float MoveHorizontal = Input.GetAxis("Horizontal");
        float MoveVertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector3(MoveHorizontal, 0.0f , MoveVertical) * velocity;
        

        
        
    }
}
