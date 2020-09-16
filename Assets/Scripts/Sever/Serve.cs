using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Serve : MonoBehaviour
{

    static Vector3 BALL_VELOCITY = new Vector3(4.0f, 0.0f, 5.0f);

    PhotonView _pv;
    Rigidbody _rb;
    Transform _tr;

    public void HandleClick()
    {
        var ping = PhotonNetwork.GetPing();
        _pv.RPC("PutInPlay", RpcTarget.Others, ping);
        //_rb.velocity = BALL_VELOCITY;
    }

    [PunRPC]
    void PutInPlay(int remotePing)
    {
        var ping = PhotonNetwork.GetPing();
        var delay = (float)(ping / 2 + remotePing / 2);
        _tr.position += (BALL_VELOCITY * (delay / 1000));
        //_rb.velocity = BALL_VELOCITY;
    }

    void Start()
    {        
        _pv = GetComponent<PhotonView>();
        //_rb = GetComponent<Rigidbody>();
        _tr = GetComponent<Transform>();
    }
}