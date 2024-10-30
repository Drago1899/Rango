using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject Flechita;
    [SerializeField] BoxCollider m_boxCollider;
    [SerializeField] private float velocity;
    PhotonView m_PV;
    [SerializeField] ParticleSystem m_PS;
    int m_life;

    Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        m_PS = GetComponent<ParticleSystem>();
        rb = GetComponent<Rigidbody>();
        m_PV = GetComponent<PhotonView>();
        m_boxCollider.enabled = false;
        m_life = 1;
        if (m_PV.IsMine) {
            Flechita.SetActive(true);


        }
        m_PS.Stop();
     


    }

    // Update is called once per frame
    void Update()
    {
        if (m_PV.IsMine)
        {
            Flechita.SetActive(true);
            float MoveHorizontal = Input.GetAxis("Horizontal");
            float MoveVertical = Input.GetAxis("Vertical");
            rb.velocity = new Vector3(MoveHorizontal, 0.0f , MoveVertical) * velocity;


        }
        if (Input.GetKey(KeyCode.E))
        {
            m_boxCollider.enabled = true;
        }
        else
        {
            m_boxCollider.enabled = false;
        }

    }
    private void OnTriggerStay(Collider other)
    {

        if (other.CompareTag("NPC"))
        {
            other.GetComponent<NPCBehaviour>().destroyNPC();
        }
        else if (other.CompareTag("Damage"))
        {
            m_PV.RPC("takingDamage", RpcTarget.All, 1);

        }

    }

    [PunRPC]
    void takingDamage(int p_damage)
    {
        print("Se murio");
        m_life -= p_damage;
        if (m_life <= 0)
        {
            StartCoroutine(waitForParticleSystem());
        }
    }

    IEnumerator waitForParticleSystem()
    {
        m_PS.Play();
        yield return new WaitForSeconds(m_PS.main.duration);
        PhotonNetwork.Destroy(gameObject);
    }
}
