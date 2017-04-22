using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour {

    public float FireDamage;

    void OnTriggerStay(Collider other)
    {
        other.gameObject.SendMessage("OnFire", FireDamage);
    }

    void OnTriggerExit(Collider other)
    {
        other.gameObject.SendMessage("StopFire");
    }

}
