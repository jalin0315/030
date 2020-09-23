using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// store a public reference to the Player game object, so we can refer to it's Transform
	public GameObject player;

	// Store a Vector3 offset from the player (a distance to place the camera from the player at all times)
	bool isOpen;
	// At the start of the game..
	public bool Qoo = true;
	public List<GameObject> ball = new List<GameObject>();	
	private Camera cm;

	private float xSpeed = 200;//x軸的旋轉速度
	private float ySpeed = 150;//x軸的旋轉速度

	//public bool needDamping = true; //是否需要的阻尼
	//private float damping = 5;//阻尼 
	public float x, y, speed, distance;

	public float xx, yy, xxSpeed = 1, yySpeed = 1,move;
	private Quaternion rot;
	Vector3 offset;
	bool up, down, left, right;
	void Start ()
	{
		offset = transform.position - player.transform.position;
		x = -157.377f;
		y = 37.231f;
		cm = GetComponent<Camera>();
				
		// Create an offset by subtracting the Camera's position from the player's position
	}

    // After the standard 'Update()' loop runs, and just before each frame is rendered..
    private void Update()
    {
		x = transform.eulerAngles.y;
		y = transform.eulerAngles.x;
		xx = transform.eulerAngles.y;
		yy = transform.eulerAngles.x;
	}

	void LateUpdate()
	{
		if (Qoo)
		{
			transform.position = player.transform.position + offset;
		}
		else
		{

			// Set the position of the Camera (the game object this script is attached to)
			// to the player's position, plus the offset amount
			//transform.position = player.transform.position + offset;

			if (isOpen)
			{
				//運算攝影機座標、旋轉
				Quaternion rotationEuler = Quaternion.Euler(y, x, 0);
				Vector3 cameraPosition = rotationEuler * new Vector3(0, 0, -distance) + (player.transform.position + new Vector3(0, 1, 0));

				//應用
				transform.rotation = rotationEuler;
				transform.position = cameraPosition;

				xx += Input.GetAxis("Mouse X") * xxSpeed * Time.deltaTime;
				yy -= Input.GetAxis("Mouse Y") * yySpeed * Time.deltaTime;
				if (x > 360) { x -= 360; }
				else if (x < 0) { x += 360; }
				rot = Quaternion.Euler(yy, xx, 0);
				transform.rotation = rot;
				//transform.rotation = Quaternion.Lerp(transform.rotation, rot, 5 * Time.deltaTime);
			}

			if (Input.GetAxis("Mouse ScrollWheel") > 0 && !isOpen)
			{
				transform.Translate(Vector3.forward * 500 * Time.deltaTime, transform);
			}
			else if (Input.GetAxis("Mouse ScrollWheel") > 0 && isOpen && distance > 5)
			{
				distance -= 10;

			}
			if (Input.GetAxis("Mouse ScrollWheel") < 0 && !isOpen)
			{
				transform.Translate(-Vector3.forward * 500 * Time.deltaTime, transform);
			}
			else if (Input.GetAxis("Mouse ScrollWheel") < 0 && isOpen && distance < 95)
			{
				distance += 10;
			}


			if (Input.GetMouseButton(1) && !isOpen)
			{
				x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
				y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
				Quaternion rotation = Quaternion.Euler(y, x, 0.0f);
				transform.rotation = rotation;
				//if (needDamping)
				//{
				//	transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * damping);
				//}
				//else
				//{
				//	transform.rotation = rotation;
				//}
			}

			if (up) { transform.Translate(0, move * Time.deltaTime, 0); }
			if (down) { transform.Translate(0, -move * Time.deltaTime, 0); }
			if (left) { transform.Translate(-move * Time.deltaTime, 0, 0); }
			if (right) { transform.Translate(move * Time.deltaTime, 0, 0); }
		}
	}
	public void 左()
    {
        for (int i = 0; i < ball.Count; i++)
        {
			if (player == ball[i] && i > 0) 
            {
				player = ball[i - 1];
            }else if (player == ball[i] && i == 0)
            {
				player = ball[9];
            }

		}
    }
	public void 右()
    {
		for (int i = 0; i < ball.Count; i++)
		{
			if (player == ball[i] && i < 9)
			{
				player = ball[i + 1];
			}
			else if (player == ball[i] && i == 9)
			{
				player = ball[0];
			}

		}
	}

	public void 鎖定()
	{		
		isOpen = !isOpen;		
	}

	public void 上上()
	{
		if (!isOpen)
		{
			up = !up;
		}
	}
	public void 下下()
	{
		if (!isOpen)
		{
			down = !down;
		}
	}
	public void 左左()
	{
		if (!isOpen)
		{
			left = !left;
		}
	}
	public void 右右()
	{
		if (!isOpen)
		{
			right = !right;
		}
	}
}