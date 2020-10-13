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

    //Lag compensation
    float currentTime = 0;
    double currentPacketTime = 0;
    double lastPacketTime = 0;
    Vector3 positionAtLastPacket = Vector3.zero;
    Quaternion rotationAtLastPacket = Quaternion.identity;

    private Rigidbody _Rigidbody;
    private Vector3 _RigPos;
    private Quaternion _RigRot;
    private Vector3 _Velocity;

    void Awake()
    {
        PhotonNetwork.SendRate = 30;
        PhotonNetwork.SerializationRate = 30;
        width = Screen.width / 2;
    }

    private void Start()
    {
        _Rigidbody = GetComponent<Rigidbody>();
        //if (!photonView.IsMine) _Rigidbody.isKinematic = true;
    }

    private void Update()
    {
        if (photonView.IsMine)
        {
            if (_Rigidbody.isKinematic) _Rigidbody.isKinematic = false;
            return;
        }
        if (!photonView.IsMine)
        {
            //Lag compensation
            //double timeToReachGoal = currentPacketTime - lastPacketTime;
            //currentTime += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, networkPosition, Time.deltaTime * 10.0f);
            transform.rotation = Quaternion.Slerp(transform.rotation, networkRotation, Time.deltaTime * 10.0f);
        }
    }

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
            //Lag compensation
            //currentTime = 0.0f;
            //lastPacketTime = currentPacketTime;
            //currentPacketTime = info.SentServerTime;
            //positionAtLastPacket = transform.position;
            //rotationAtLastPacket = transform.rotation;
        }
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause) PhotonNetwork.Disconnect();
    }
}