using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class FireCannon : MonoBehaviour
{
    public Transform firePos;
    public GameObject cannon;

    private PhotonView pv;

    private bool isMouseClick => Input.GetMouseButtonDown(0);

    void Start()
    {
        pv = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pv.IsMine && isMouseClick)
        {
            FireBullet();
            pv.RPC("FireBullet", RpcTarget.Others, null);
        }
    }

    [PunRPC]
    void FireBullet()
    {
        GameObject bullet = Instantiate(cannon, firePos.position, firePos.rotation);
    }
}
