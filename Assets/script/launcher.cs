using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class launcher : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }

    // Update is called once per frame
    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();
        Debug.Log("welcome");

        PhotonNetwork.JoinOrCreateRoom("2", new Photon.Realtime.RoomOptions() { MaxPlayers = 2 }, default);
        Debug.Log(123);
    }
    public override void OnJoinedRoom()
    {
        Debug.Log(345);
        base.OnJoinedRoom();
        PhotonNetwork.Instantiate("Cube", new Vector3(1, 1, 0),Quaternion.identity,0);
        Debug.Log("Ok");
    }
}
