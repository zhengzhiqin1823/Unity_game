using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    public Image bul;
    //�����ƶ�
    private bool move;//ǰ���ƶ�
    private bool movex;//�����ƶ�
    private bool direct;//�ƶ���ǰ����Ϊtrue;
    public float speed;
    private bool run;
    public float mousesense;
    //���
    public Camera main;
    public Vector3 offset;
    public Image type;
    
    //����ֵ
    private int maxhealth;
    public int health;
    private bool die;
    private bool ret;
    public Image hp;
    public int hpdelay;
    public int maxhpdelay;//�����޵�ʱ��
    private bool isinvicible;
    //�����ӵ�
    public GameObject bullet1;
    public GameObject bullet2;
    public GameObject bullet3;
    public GameObject bullet4;
    public int bulOp;//1-4����ӵ�1-4��
    public GameObject aim1;
    public int maxfiretime;
    public int firetime;//�����������ӵ�֮����ӳ�
    public float anglelimit = 20;
    void Start()
    {
        cc= GetComponent<CharacterController>();
        ani =GetComponent<Animator>();
        auto = true;
        shoot = false;
        shootDelayr = false;
        jump = false;
        maxjumptime = 30;
        jumptimer = 0;
        reload = false;
        reloadtimer = 0;
        maxreloadtime = 400;
        buttlenum = 800;
        maxbuttlenum = 800;
        speed = 4f;
        run = false;
        
        maxhealth = 100;
        health = maxhealth;
        die = false;
        ret = false;
        offset = new Vector3(-0.01159334f, 2.32391f, -1.147557f);
        main.transform.SetParent(this.transform);
        main.transform.position =  this.transform.position + offset;
        mousesense = 2f;
        maxfiretime = 70;
        firetime = 0;
        bulOp = 1;
        isinvicible = false;
    }

    // Update is called once per frame
    void Update()
    {
        bulletImgChange(5);
        hpImgChange();
        if (die||ret)
        {
            if(die)
            {
                ani.SetBool("die", die);
                die=false;
            }
            return;
        }
        typeChange();
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
            switch(bulOp)
            {
                case 1:
                    {
                        if (buttlenum % 10 == 0 && firetime > maxfiretime)
                        {
                            var b = Instantiate(bullet1);
                            b.GetComponent<bulletDamage>().damage = 10;
                            b.transform.position = aim1.transform.position;
                            b.transform.rotation = this.transform.rotation;
                            b.GetComponent<Rigidbody>().velocity = transform.forward * 50;
                        }
                        break;
                    }
                    case 2:
                    {
                        if (buttlenum % 40 == 0 && firetime > maxfiretime)
                        {
                            var b = Instantiate(bullet2);
                            b.GetComponent<bulletDamage>().damage = 5;
                            b.transform.position = aim1.transform.position;
                            b.transform.rotation = this.transform.rotation;
                            b.GetComponent<Rigidbody>().velocity = transform.forward * 50;
                        }

                        break;
                    }
                case 3:
                    {
                        if (buttlenum % 40 == 0 && firetime > maxfiretime)
                        {
                            var b = Instantiate(bullet3);
                            b.GetComponent<bulletDamage>().damage = 10;
                            b.transform.position = aim1.transform.position;
                            b.transform.rotation = this.transform.rotation;
                            b.GetComponent<Rigidbody>().velocity = transform.forward * 10;
                        }
                        break;
                    }
                case 4:
                    {
                        if (buttlenum % 100 == 0 && firetime > maxfiretime)
                        {
                            var b = Instantiate(bullet4);
                            b.transform.position = aim1.transform.position;
                            b.GetComponent<bulletDamage>().damage = 30;
                            b.transform.rotation = this.transform.rotation;
                            b.GetComponent<Rigidbody>().velocity = transform.forward * 10;
                        }
                        break;
                    }
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
        if(Input.GetMouseButton(0)||Input.GetMouseButton(1))
        this.transform.Rotate(new Vector3(0, Input.GetAxis("Mouse X") * mousesense, 0));
        /*
         * float MoveY = Input.GetAxis("Mouse Y");
        if (this.transform.rotation.eulerAngles.x < anglelimit || this.transform.rotation.eulerAngles.x > 360-anglelimit)
        {
            Vector3 v = new Vector3(MoveY, 0, 0);
            this.transform.Rotate(v, Space.Self);

            if (!(this.transform.rotation.eulerAngles.x < anglelimit || this.transform.rotation.eulerAngles.x > 360-anglelimit))
            {
                this.transform.Rotate(new Vector3(-MoveY, 0, 0), Space.Self);
            }
        }
         
         */

        ani.SetBool("move", move);
        ani.SetBool("movex", movex);
        ani.SetBool("run", run);
        if(isinvicible)
        {
            hpdelay++;
            if(hpdelay>maxhpdelay)
            {
                isinvicible = false;
            }
        }
    }
    private void listener()
    {
        if(health<=0&&!ret)
        {
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

    void bulletImgChange(int speed)
    {
        float radio = (float)buttlenum / maxbuttlenum;
        bul.fillAmount=Mathf.Lerp(bul.fillAmount,radio,Time.deltaTime*speed);
    }

    void hpImgChange()
    {
        float radio = (float)health / maxhealth;
        hp.fillAmount = Mathf.Lerp(hp.fillAmount, radio, Time.deltaTime * speed);
    }
    public void changeBul(int amount)
    {
        this.bulOp = amount;
    }
    private void typeChange()
    {
        string img1 = "img/Icon36";//��ɫ
        string img2 = "img/Icon35";//��ɫ
        string img3 = "img/Icon37";//��ɫ
        string img4 = "img/Icon29";//��ɫ
        switch (bulOp)
        {
            case 1:
                {
                    Sprite s=Resources.Load(img1, typeof(Sprite)) as Sprite;
                    type.sprite=s;
                    break;
                }
            case 2:
                {
                    Sprite s = Resources.Load(img2, typeof(Sprite)) as Sprite;
                    type.sprite = s;
                    break;
                }
            case 3:
                {
                    Sprite s = Resources.Load(img3, typeof(Sprite)) as Sprite;
                    type.sprite = s;
                    break;
                }
            case 4:
                {
                    Sprite s = Resources.Load(img4, typeof(Sprite)) as Sprite;
                    type.sprite = s;
                    break;
                }
        }
    }
    public void healthChange(int amount)
    {
        Debug.Log("ok");
        if (isinvicible) return;
        else
        {
            isinvicible = true;
            hpdelay = 0;
        }
        health=Mathf.Clamp(health+amount, 0, maxhealth);
        hpImgChange();
    }
}
