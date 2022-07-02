using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exporsionDamage : MonoBehaviour
{
    public bool ready;
    // Start is called before the first frame update
    private void Awake()
    {
        ready = true;
    }
    private void OnTriggerStay(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.transform.tag.Equals("Player")&&ready)
        {
            ready=false;
            Debug.Log("123");
            CharaCtr cc = obj.GetComponent<CharaCtr>();
            cc.healthChange(-1 * 30);
            this.GetComponent<exporsionDamage>().enabled = false;
        }
    }
}
