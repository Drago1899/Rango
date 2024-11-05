using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Hashtable = ExitGames.Client.Photon.Hashtable;
using System.Linq;


public class LevelManager : MonoBehaviourPunCallbacks
{
    public static LevelManager Instance;
    protected PhotonView m_PV;
    LevelManagerState m_currentState;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(Instance);
        }
    }
    public enum GameplayRoles
    {
        Innocent, Traitor,
    }
    public enum LevelManagerState
    {
        None, WaitForPlayers, Playing,
    }
    // Start is called before the first frame update
    void Start()
    {
        m_PV = GetComponent<PhotonView>();
        SetLevelManagerState(LevelManagerState.WaitForPlayers);
        if (PhotonNetwork.IsMasterClient)
        {
            //assignRole();
        }
    }
    public static void assignRole()
    {
        Player[] m_playersArray = PhotonNetwork.PlayerList;
        GameplayRoles[] m_gameplayRole = { GameplayRoles.Innocent, GameplayRoles.Traitor };

        m_gameplayRole = m_gameplayRole.OrderBy(x => Random.value).ToArray();
        for (int i = 0; i < m_gameplayRole.Length; i++)
        {
            Hashtable m_playerProps = new Hashtable(); 
            m_playerProps["Role"] = m_gameplayRole[i % m_gameplayRole.Length].ToString();
            m_playersArray[i].SetCustomProperties(m_playerProps);
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if (PhotonNetwork.CurrentRoom.PlayerCount >= 4)
        {
            SetLevelManagerState(LevelManagerState.Playing);
        }
    }
    public void SetLevelManagerState(LevelManagerState p_newState)
    {
        if (p_newState == m_currentState)
        {
            return;
        }
        m_currentState = p_newState;
        switch (m_currentState)
        {
            case LevelManagerState.None:

                break;
            case LevelManagerState.WaitForPlayers:

                break;
            case LevelManagerState.Playing:
                Playing();
                break;
        }
    }
    public LevelManagerState CurrentState
    {
        get { return m_currentState; }
    }
    void Playing()
    {
        assignRole();
    }
    // Update is called once per frame
    void Update()
    {

    }
}
