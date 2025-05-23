using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class RoomData : MonoBehaviour
{
    private RoomInfo _roomInfo;

    private Text roomInfoText;

    private PhotonManager photonManager;

    public RoomInfo RoomInfo
    {
        get
        {
            return _roomInfo;
        }
        set
        {
            _roomInfo = value;

            roomInfoText.text = $"{_roomInfo.Name} ({_roomInfo.PlayerCount}/{_roomInfo.MaxPlayers})";

            GetComponent<UnityEngine.UI.Button>().onClick.AddListener(() => OnEnterRoom(_roomInfo.Name));
        }
    }

    void Awake()
    {
        roomInfoText = GetComponentInChildren<Text>();
        photonManager = GameObject.Find("PhotonManager").GetComponent<PhotonManager>();
    }

    void OnEnterRoom(string roomName)
    {
        photonManager.SetUserId();

        PhotonNetwork.JoinRoom(roomName);
    }
}
