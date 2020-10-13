using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShowMarbles : MonoBehaviourPun
{
    public bool isBack;
    public float speed;
    public GameObject ring;
    public List<GameObject> iCon = new List<GameObject>();
    bool isGo, isOpen, isRight, isLeft;
    Vector2 mouse, drag;

    public GameObject start,wait;

    //旋轉的角度上限。
    float rotationleft = 45;
    //每 frame 旋轉速度。
    float rotationspeed = 45;

    public L_ l;

    public Material[] materials;
    public MeshRenderer mr;
    void Start()
    {
        l = GameObject.Find("S&L").GetComponent<L_>();
        wait.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        ShowAndRing();
        if (PhotonNetwork.IsConnected && l.loadShowAndFight != 0)
        {
            mr.material = materials[l.loadShowAndFight - 1];
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            isLeft = true;
        }

        if (isBack)
        {
            ring.SetActive(false);
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(0, 0, 40), 1);
            if (transform.position == new Vector3(0, 0, 40))
            {
                wait.SetActive(true);
                isOpen = false;
                isBack = false;
            }
        }

    }
    void ShowAndRing()
    {
        if (isGo)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(-30, -25, -20), 1);
            if (transform.position == new Vector3(-30, -25, -20))
            {
                ring.SetActive(true);
                isOpen = true;
                isGo = false;
            }
        }

        if (!isOpen)
        {
            transform.Rotate(0, speed * Time.deltaTime, 0);
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && !isLeft && !isRight)
            {
                mouse = Input.mousePosition;
            }
            if (Input.GetMouseButtonUp(0))
            {
                mouse = Vector2.zero;
                drag = Vector2.zero;
            }
            if (Input.GetMouseButton(0) && !isLeft && !isRight)
            {
                drag = Input.mousePosition;
                float Qy = System.Math.Abs(drag.y - mouse.y);
                if (((System.Math.Abs(drag.y) - System.Math.Abs(mouse.y)) < 0) && !isLeft && !isRight)
                {
                    if (Qy > 50 && !isLeft && !isRight)
                    {
                        isRight = true;
                        return;
                    }
                }
                else if (((System.Math.Abs(drag.y) - System.Math.Abs(mouse.y)) > 0 && mouse != Vector2.zero) && !isLeft && !isRight)
                {
                    if (Qy > 50 && !isLeft && !isRight)
                    {
                        isLeft = true;
                        return;
                    }
                }
            }
        }

        if (isLeft)
        {
            //設定每個 frame 的旋轉速度。
            float rotation = rotationspeed * Time.deltaTime;

            //若最大旋轉值大於每個 frame的旋轉速度，最大旋轉值 -10 度。
            if (rotationleft > rotation)
            {
                rotationleft -= rotation;
            }

            //若最大旋轉值等於=10 也就是等於每個 frame 的旋轉速度。
            else
            {
                //每個Farm的旋轉速度=10。
                rotation = rotationleft;

                //最大旋轉值設成0。
                rotationleft = 0;
            }

            //旋轉(每個 frame)。
            transform.Rotate(0, -rotation, 0);
            ring.transform.Rotate(0, -rotation, 0);
            if (rotation == 0)
            {
                rotationleft = 45;
                isLeft = false;
            }
        }
        if (isRight)
        {
            //設定每個 frame 的旋轉速度。
            float rotation = rotationspeed * Time.deltaTime;

            //若最大旋轉值大於每個 frame的旋轉速度，最大旋轉值 -10 度。
            if (rotationleft > rotation)
            {
                rotationleft -= rotation;
            }

            //若最大旋轉值等於=10 也就是等於每個 frame 的旋轉速度。
            else
            {
                //每個Farm的旋轉速度=10。
                rotation = rotationleft;

                //最大旋轉值設成0。
                rotationleft = 0;
            }

            //旋轉(每個 frame)。
            transform.Rotate(0, rotation, 0);
            ring.transform.Rotate(0, rotation, 0);
            if (rotation == 0)
            {
                rotationleft = 45;
                isRight = false;
            }
        }
    }

    private void OnMouseDown()
    {
        if (start.activeSelf || wait.activeSelf || isBack)             
            return;
        isGo = true;
    }
}
