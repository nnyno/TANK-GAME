using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damage : MonoBehaviour
{
    private Renderer[] renderers;

    private int initHp = 100;

    public int currHp = 100;


    void Awake()
    {
        renderers = GetComponentsInChildren<Renderer>();

        currHp = initHp;
    }


    void OnCollisionEnter(Collision coll)
    {
        if (currHp > 0 && coll.collider.CompareTag("Bullet"))
        {
            currHp -= 20;
            if (currHp <= 0)
            {
                StartCoroutine(PlayerDie());
            }
        }
    }

    IEnumerator PlayerDie()
    {

        SetPlayerVisible(false);

        yield return new WaitForSeconds(3.0f);


        Transform[] points = GameObject.Find("SpawnPointGroup").GetComponentsInChildren<Transform>();
        int idx = Random.Range(1, points.Length);
        transform.position = points[idx].position;

        yield return new WaitForSeconds(1.5f);

        currHp = 100;

        SetPlayerVisible(true);
    }

    void SetPlayerVisible(bool isVisible)
    {
        for(int i = 0; i < renderers.Length; i++)
        {
            renderers[i].enabled = isVisible;
        }
    }

}
