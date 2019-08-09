using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class PlayerController : MonoBehaviour
{
	[Header("Player Info")]
	[Range(1, 4)]
	public int playerID;
	public Rigidbody playerBody;

	[Header("Player Speed")]
	public float speed;

	//camera public values
	[Header("Camera Controls")]
	public float XMinRotation;
	public float XMaxRotation;
	[Range(1.0f, 10.0f)]
	public float Xsensitivity;
	[Range(1.0f, 10.0f)]
	public float Ysensitivity;
	public Transform target;                        //player object to track
	public float smoothSpeed;                       //amount of smoothing for movement
	public float slerpAmount;
	public float offset;                            //forward camera offset amount

	[Header("Weapon Fields")]
	public float fireRate = 0.5f;                                        // Number in seconds which controls how often the player can fire
	public float weaponRange = 200f;                                        // Distance in Unity units over which the player can fire
	public Transform gunEnd;                                            // Holds a reference to the gun end object, marking the muzzle location of the gun
	public Animator weaponAnim;
	public GameObject flashLight;
	public ParticleSystem muzzle;
	public ParticleSystem burst;
	public ParticleSystem blood;
	public GameObject ammoBar;
	public Animator ammoAnim;

	public PlayerState _state
	{
		get;
		private set;
	}

	public CameraControllerBase _cameraController
	{
		get;
		private set;
	}
	public GunScriptBase _weaponSystems
	{
		get;
		private set;
	}

	public PlayerMoveBase _playerMover
	{
		get;
		private set;
	}

	// Start is called before the first frame update
	void Start()
    {
		_state = PlayerState.normalState;
		_weaponSystems = new GunScriptBase(this);
		_playerMover = new PlayerMoveBase(this);
		_cameraController = new CameraControllerBase(this);
	}

    // Update is called once per frame
    void Update()
    {
		_state.Update(this);
    }

	private void FixedUpdate()
	{
		_state.FixedUpdate(this);
	}

	private void LateUpdate()
	{
		_state.LateUpdate(this);
	}

	public void InstantiateBurst(Vector3 point)
	{
		Instantiate(burst, point, Quaternion.identity);
	}

	public void InstantiateBlood(Vector3 point)
	{
		Instantiate(blood, point, Quaternion.identity);
	}
}
