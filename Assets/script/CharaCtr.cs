using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharaCtr : MonoBehaviour
{
    private Animator ani;
    private CharacterController cc;
    //trueΪȫ�Զ����
    private bool auto;
    private bool shoot;
    private bool shootDelayr;
    //������Ծ
    private bool jump;
    public int jumptimer;
    public int maxjumptime;
    private bool gronded;//�Ƿ��ڵ�����
    //����װ��
    private bool reload;
    public int reloadtimer;
    public int maxreloadtime;
    public int buttlenum;
    public int maxbuttlenum;
    //�����ƶ�
    private bool move;//ǰ���ƶ�
    private bool movex;//�����ƶ�
    private bool direct;//�ƶ���ǰ����Ϊtrue;
    public float speed;
    private bool run;
    public float mousesense;
    //���
    private Camera main;
    public Vector3 offset;
    //����ֵ
    private int maxhealth;
    public int health;
    private bool die;
    private bool ret;
    //�����ӵ�
    public GameObject bullet;
    public GameObject aim1;
    public int maxfiretime;
    public int firetime;//�����������ӵ�֮����ӳ�
    void Start()
    {
        cc= GetComponent<CharacterController>();
        ani =GetComponent<Animator>();
        auto = true;
        shoot = false;
        shootDelayr = false;
        jump = false;
        maxjumptime = 220;
        jumptimer = 0;
        reload = false;
        reloadtimer = 0;
        maxreloadtime = 400;
        buttlenum = 800;
        maxbuttlenum = 800;
        speed = 2f;
        run = false;
        main = Camera.main;
        maxhealth = 100;
        health = maxhealth;
        die = false;
        ret = false;
        offset = new Vector3(-0.01159334f, 2.32391f, -1.147557f);
        main.transform.SetParent(this.transform);
        main.transform.position = this.transform.position + offset;
        mousesense = 2f;
        maxfiretime = 100;
        firetime = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(die||ret)
        {
            if(die)
            {
                ani.SetBool("die", die);
                die=false;
            }
            return;
        }
        listener();
        ani.SetBool("jump", jump);
        if (jump)
        {
            jumptimer++;
            if (jumptimer > maxjumptime)
            {
                jump = false;
            }
            else
            {
                Debug.Log("jump");
                cc.Move(transform.up*Time.deltaTime);
            }
        }
        if(!cc.isGrounded)
        {
            cc.Move(transform.up * -1 * Time.deltaTime);
        }
        ani.SetBool("isauto", auto);
        ani.SetBool("shoot", shoot);
        ani.SetBool("reload", reload);
        ani.SetBool("direct", direct);
        if (shoot&&!reload)
        {
            firetime++;
            if(buttlenum%12==0&&firetime>maxfiretime)
            {
                var b = Instantiate(bullet);
                b.transform.position=aim1.transform.position;
                b.GetComponent<Rigidbody>().velocity = transform.forward*30;
            }
            buttlenum--;
        }
        if(reload)
        {
            reloadtimer++;
            if(reloadtimer>=maxreloadtime)
            {
                buttlenum = maxbuttlenum;
                firetime = 0;
                reload = false;
            }
        }
        cc.Move(transform.forward * Time.deltaTime * Input.GetAxis("Vertical") * speed);
        cc.Move(transform.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed);
        if (Input.GetMouseButton(1))
        {
            this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X")*mousesense, 0));
        }
        ani.SetBool("move", move);
        ani.SetBool("movex", movex);
        ani.SetBool("run", run);
    }
    private void listener()
    {
        if(health<=0&&!ret)
        {
            Debug.Log("into");
            die = true;
            ret = true;
        }
        if(!move)
        {
            run = false;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            auto = !auto;
        }
        if (Input.GetKeyDown(KeyCode.Space) && !jump)
        {
            jump = true;
            jumptimer = 0;
        }
        if (auto)//ȫ�Զ��£����¾Ϳ���
        {
            if (Input.GetMouseButton(0))
            {
                shoot = true;
            }
            else if(shoot)
            {
                shoot = false;
            }
        }
        else//����ģʽ����Ҫ���
        {
            if (Input.GetMouseButtonDown(0))
            {
                shoot = true;
                shootDelayr = false;
            }
            else if(shoot&&!shootDelayr)
            {
                StartCoroutine("shootDelay");
            }
        }
        if((Input.GetKeyDown(KeyCode.R)||buttlenum==0)&&!reload)
        {
            if (buttlenum == maxbuttlenum) return;
            reload = true;
            reloadtimer = 0;
        }
        move = false;
        movex = false;
        if(Input.GetKey(KeyCode.W))
        {
            move = true;
            direct = true;
            if (Input.GetKey(KeyCode.LeftShift))
                run = true;
            else
                run = false;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move = true;
            direct = false;
        }
        if (Input.GetKey(KeyCode.D))
        {
            movex = true;
            direct = false;
        }
        if (Input.GetKey(KeyCode.A))
        {
            movex = true;
            direct = false;
        }
    }
    private void OnCollisionExit(Collision collision)
    {
        GameObject gobj = collision.gameObject;
        if(gobj.transform.tag.Equals("evm"))
        {
            gronded = false;
        }
    }
    private void OnCollisionStay(Collision collision)
    {
        GameObject gobj = collision.gameObject;
        if (gobj.transform.tag.Equals("evm"))
        {
            gronded = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        GameObject gobj = collision.gameObject;
        if (gobj.transform.tag.Equals("evm"))
        {
            gronded = true;
        }
    }
    IEnumerator shootDelay()
    {
        shootDelayr = true; 
        yield return new WaitForSeconds(0.7f);
        shoot = false; 
    }

}
