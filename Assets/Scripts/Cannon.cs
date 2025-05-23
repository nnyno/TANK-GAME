using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject effect;

    void Start()
    {
        GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 3000.0f);

        Destroy(this.gameObject, 3.0f);
    }


    void OnCollisionEnter(Collision coll)
    {
        var contact = coll.GetContact(0);
        var obj = Instantiate(effect, contact.point, Quaternion.LookRotation(-contact.normal));
        Destroy(obj, 2.0f);
        Destroy(this.gameObject);
    
    }

}