using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Pun2_LagOver : MonoBehaviourPunCallbacks, IPunObservable
{
    public float smooth;

    float inputX;
    int width;

    static float speed = 30.0f;
    float turnSpeed = 240.0f;
    Vector3 direction = new Vector3(0.0f, 0.0f, speed);

    Vector3 networkPosition;
    Quaternion networkRotation;


    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(transform.rotation);
        }
        else
        {
            networkPosition = (Vector3)stream.ReceiveNext();
            networkRotation = (Quaternion)stream.ReceiveNext();

            
        }
    }

    void Awake()
    {
        PhotonNetwork.SendRate = 45;
        PhotonNetwork.SerializationRate = 45;
        width = Screen.width / 2;
    }

    void Start()
    {
        
    }

    void Update()
    {
        if (!photonView.IsMine)
        {
            transform.position = Vector3.Lerp(transform.position, networkPosition, smooth * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, networkRotation, smooth * Time.deltaTime);
        }
    }
}