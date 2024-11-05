using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using TMPro;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject Flechita;
    [SerializeField] BoxCollider m_boxCollider;
    [SerializeField] private float velocity;
    PhotonView m_PV;
    [SerializeField] ParticleSystem m_PS;
    int m_life;
    [SerializeField] TextMeshProUGUI m_currentText;

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
        GetNewGameplayRole(); ;




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
    protected void GetNewGameplayRole()
    {
        if (PhotonNetwork.LocalPlayer.CustomProperties.TryGetValue("Role", out object role))
        {
            string m_newPlayerRole = role.ToString();
            switch (role)
            {
                case "Innocent":
                    m_currentText.text = "Innocent";
                    m_currentText.color = Color.blue;
                    break;
                case "Traitor":
                    m_currentText.text = "Traitor";
                    m_currentText.color = Color.red;
                    break;
            }
        }
    }
}
