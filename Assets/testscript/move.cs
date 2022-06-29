using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class move : MonoBehaviourPun
{
    private int health;
    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        health = 100;
        body=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!photonView.IsMine && PhotonNetwork.IsConnected)
            return;
        Vector3 pos = this.transform.position;
        if(Input.GetKey(KeyCode.W))
        {
            pos.x += 0.5f;
        }
        if(Input.GetKey(KeyCode.S))
        {
            pos.x -= 0.5f;
        }
        body.MovePosition(pos);
    }
    private void OnCollisionEnter(Collision collision)
    {
        this.health -= 20;
        Debug.Log(health);
    }
}

