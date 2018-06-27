using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;

public class JoinGame : MonoBehaviour {

    private NetworkManager networkManager;

    List<GameObject> roomList = new List<GameObject>();

    [SerializeField]
    private GameObject roomListItemPrefab;
    [SerializeField]
    private Transform roomListParent;

    private void Start()
    {
        networkManager = NetworkLobbyManager.singleton;
        if (networkManager.matchMaker == null)
        {
            networkManager.StartMatchMaker();
        }

        RefreshRoomList();
    }

    public void RefreshRoomList()
    {
        networkManager.matchMaker.ListMatches(0, 10, "", true, 0, 0, OnMatchList);

    }

    public void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
    {
        if(matches == null)
        {

        } else
        {

        }

        ClearRoomList();
        foreach(MatchInfoSnapshot match in matches)
        {
            GameObject _roomListItemGO = Instantiate(roomListItemPrefab);
            _roomListItemGO.transform.SetParent(roomListParent);

            RoomListItem _roomListItem = _roomListItemGO.GetComponent<RoomListItem>();

            if(_roomListItem != null)
            {
                _roomListItem.Setup(match, JoinRoom);
            }

            roomList.Add(_roomListItemGO);
        }

    }

    public void ClearRoomList()
    {
        for (int i = 0; i < roomList.Count; i++)
        {
            Destroy(roomList[i]);

        }

        roomList.Clear();
    }

    public void JoinRoom(MatchInfoSnapshot _match)
    {
        networkManager.matchMaker.JoinMatch(_match.networkId, "", "", "", 0, 0, networkManager.OnMatchJoined);
        ClearRoomList();
    }
}
