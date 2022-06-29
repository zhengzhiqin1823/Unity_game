using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullectdestroy : MonoBehaviour
{
    // Start is called before the first frame update
    private int max;
    private int timer;
    private void Awake()
    {
        max = 200;
        timer = 0; 
    }

    // Update is called once per frame
    void Update()
    {
        timer++;
        if(timer > max)
        {
            GameObject.Destroy(this.gameObject);
        }
    }

}
