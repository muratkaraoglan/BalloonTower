using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject balloonPrefab;
    public Transform connectPoint;

    void Update()
    {
        if ( Input.GetMouseButtonDown(0) )
        {
            Balloon b = Instantiate(balloonPrefab, transform.position, Quaternion.identity).GetComponent<Balloon>();
            b.connectedPoint = connectPoint;
            b.startFly = true;
        }
    }
}
