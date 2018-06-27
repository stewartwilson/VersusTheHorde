using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyPlayerController : MonoBehaviour {

    private bool _isMasterPlayer;
    public bool isMasterPlayer
    {
        get { return _isMasterPlayer; }
        set { _isMasterPlayer = value; }
    }
}
