using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Cinemachine;

public class MoveCtrls : MonoBehaviour, IPunObservable
{
    private float h, v;
    private Rigidbody rb;
    private Transform tr;

    public float speed = 20.0f;
    public float rotSpeed = 60.0f;

    private PhotonView pv;

    private CinemachineVirtualCamera virtualCamera;

    private Vector3 receivePos;
    private Quaternion receiveRot;

    public float damping = 10.0f;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.centerOfMass = new Vector3(0, -1.5f, 0);

        tr = GetComponent<Transform>();

        pv = GetComponent<PhotonView>();
        virtualCamera = GameObject.FindObjectOfType<CinemachineVirtualCamera>();

        if(pv.IsMine)
        {
            virtualCamera.Follow = transform;
            virtualCamera.LookAt = transform;
        }
    }


    void Update()
    {
        if (pv.IsMine)
        {
            v = Input.GetAxis("Vertical");
            h = Input.GetAxis("Horizontal");

            this.tr.Translate(Vector3.forward * v * speed * Time.deltaTime);
            this.tr.Rotate(new Vector3(0.0f, h * rotSpeed * Time.deltaTime, 0.0f));
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position,receivePos, Time.deltaTime * damping);

            transform.rotation = Quaternion.Slerp(transform.rotation, receiveRot, Time.deltaTime * damping);
        }
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if(stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            receivePos = (Vector3)stream.ReceiveNext();
            receiveRot = (Quaternion)stream.ReceiveNext();
        }
    }
}
