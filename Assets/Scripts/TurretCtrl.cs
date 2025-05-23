using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretCtrl : MonoBehaviour
{
    public float rotTurret;
    public GameObject Turret;

    public float move;
    public int moveSpeed;

    public float rotate;
    public int rotSpeed;

    void Start()
    {
        moveSpeed = 10;
        rotSpeed = 120;
    }

        void Update()
    {
        move = moveSpeed * Time.deltaTime;
        rotate = rotSpeed *Time.deltaTime;

        rotTurret = Input.GetAxis("Window Shake X");

        Turret.transform.Rotate(Vector3.up * rotTurret * rotate);

    }
}
