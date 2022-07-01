using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulChange3 : MonoBehaviour
{
    private int timer;
    private int maxtime;
    private float direct;
    // Start is called before the first frame update
    void Start()
    {
        maxtime = 200;
        timer = 0;
        direct = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < maxtime)
        {
            timer++;
            this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + direct * 0.005f, this.transform.position.z);
        }
        else
        {
            timer = 0;
            direct = -1 * direct;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject obj = other.gameObject;
        if (obj.transform.tag.Equals("Player"))
        {
            CharaCtr cc = obj.GetComponent<CharaCtr>();
            cc.changeBul(3);
            GameObject.Destroy(this.gameObject);
        }
    }
}
