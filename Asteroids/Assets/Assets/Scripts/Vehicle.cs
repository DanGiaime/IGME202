using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// -----Class Header-----
// Vehicle class
// Placed on a tank prefab
// Moves the tank around the screen

public class Vehicle : MonoBehaviour 
{
	// -----Class fields-----
	//[SerializeField]   a private field will appear in the Inspector
	public Vector3 vehiclePosition;		// position of the vehicle, used for calculations
	//public float speed;					// scalar, speed per frame
	//public Vector3 speed;
	public Vector3 velocity;			// velocioty (change in X and Y)
	public Vector3 direction;			// normalized, helps for rotation

	// necessary for accelration / "speeding up"
	public Vector3 acceleration;		
	public float accelRate;				// constant rate of acceleration, 0.001f
	public float maxSpeed;				// 0.05f

	// Fields for movement
	public float anglePerFrame;			// 1f
	public float totalRotation;			// 0f

	public Camera myCamera;
	public float height;
	public float width;

	public GameObject sceneManager;

	public GameObject bullet;

	// Use this for initialization
	void Start () 
	{
		// Grab the position from the transform component
		vehiclePosition = transform.position;

		// Could also do this, though the above line is simpler:
		//vehiclePosition = gameObject.transform.position;
		//vehiclePosition = gameObject.GetComponent<Transform>().position;

		height = myCamera.orthographicSize * 2;    
		width = height * Screen.width / Screen.height;

		// Calculations assume map is position at the origin
	}
	
	// Update is called once per frame
	void Update () 
	{
		// Step 1: Add speed to the X and Y components of the vector
		// Add speed to X and Y values
		//vehiclePosition.x += speed;
		//vehiclePosition.y += speed;

		// Step 2:  Add speed vector to the vehicle position vector
		//vehiclePosition.x += speed.x;
		//vehiclePosition.y += speed.y;
		// OR you could do: 
		//vehiclePosition += speed;

		// CALCULATE DIRECTION CHANGE BEFORE CALCULATING VELOCITY
		// SEPARATE METHODS WOULD BE BENEFICIAL HERE
		// WE'RE STARTING TO ACCUMULATE A LOT OF CODE HERE IN THE UPDATE METHOD
		// ROTATEDIRECTION()   CALCULATEVELOCITY() UPDATETRANSFORM() ETC. 
		if(Input.GetKey(KeyCode.LeftArrow))
		{
			// rotate left, positive rotation
			totalRotation += anglePerFrame;
			direction = Quaternion.Euler (0, 0, anglePerFrame) * direction;
		}
		else if(Input.GetKey(KeyCode.RightArrow))
		{
			// rotate right, negative rotation
			totalRotation -= anglePerFrame;
			direction = Quaternion.Euler (0, 0, -anglePerFrame) * direction;
		}

		// Step 3: Calculate velocity and add to position
		//velocity = speed * direction;
		//vehiclePosition += velocity;

		// Step 4: CALCULATE ACCELERATION
		if (Input.GetKey (KeyCode.UpArrow)) {
			acceleration = accelRate * direction;
			velocity += acceleration;
		} else{
			velocity *= .999999f;
		}

		vehiclePosition.x = (vehiclePosition.x > 0)  ? vehiclePosition.x % width : width;
		vehiclePosition.y = (vehiclePosition.y > 0)  ? vehiclePosition.y % height : height;


		vehiclePosition += velocity * Time.deltaTime;
		velocity = Vector3.ClampMagnitude (velocity, maxSpeed);


		// Set the transform component to the vehicle's position vector
		transform.position = vehiclePosition;
		transform.rotation = Quaternion.Euler (0, 0, totalRotation);

		if(Input.GetKeyDown(KeyCode.Space))
		{
			// rotate left, positive rotation
			GameObject bulletObject = Instantiate(bullet, transform.position, transform.rotation);
			sceneManager.GetComponent<Collision> ().AddBullet(bulletObject);
		}
	}
}
