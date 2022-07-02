using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class exporsion : MonoBehaviour
{
    public int maxtime;
    public int timer;
    public GameObject range;
    private bool ret=false;
    // Start is called before the first frame update
    void Start()
    {
        maxtime = 200;
        timer = 0; 
        this.GetComponent<exporsionDamage>().enabled = false;
    }
    private void Awake()
    {
        ret = false;
    }
    // Update is called once per frame
    void Update()
    {
        timer++;
        if(timer>maxtime)
        {
            range.SetActive(true);
        }
        if(timer>2*maxtime)
        {
            if (!ret)
            {
                ret = true;
                this.GetComponent<exporsionDamage>().enabled = true;
            }
            GameObject.Destroy(this.gameObject);
        }
    }
}
