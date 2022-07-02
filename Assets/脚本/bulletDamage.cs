using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDamage : MonoBehaviour
{
    // Start is called before the first frame update
    public int damage;
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.transform.tag.Equals("Player"))
        {
            CharaCtr cc = obj.GetComponent<CharaCtr>();
            cc.healthChange(-1*damage);
        }
    }
}
