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
    public ModApplication modApp;

    public enum Weapons { Blaster, Grenade, Machine, Missile, Shotgun, Sniper };
    [Header("Weapon Fields")]
    public Weapons activeWeapon;
	public Animator weaponAnim;
	public ParticleSystem blood;

    [Header("Weapons (Set per Player!)")]
    public GameObject blaster;
    public Transform blasterEnd;
    public float blasterShotSpeed;
    public float blasterShotRotAdd;

    public GameObject grenadeLauncher;
    public Transform grenadeLauncherEnd;
    public float grenadeShotSpeed;
    public float grenadeShotRotAdd;

    public GameObject machineGun;
    public Transform machineGunEnd;
    public float machineShotSpeed;
    public float machineShotRotAdd;

    public GameObject missileLauncher;
    public Transform missileLauncherEnd;
    public float missileShotSpeed;
    public float missileShotRotAdd;

    public GameObject shotgunGun;
    public Transform shotgunEnd;
    public float shotgunShotSpeed;
    public float shotgunShotRotAdd;

    public GameObject sniperRifle;
    public Transform sniperRifleEnd;
    public float sniperShotSpeed;
    public float sniperShotRotAdd;

    /*
    public GameObject blasterCrosshairs;
    public GameObject grenadeCrosshairs;
    public GameObject machineCrosshairs;
    public GameObject missileCrosshairs;
    public GameObject shotgunCrosshairs;
    public GameObject sniperCrosshairs;
    */

    [Header("Weapon Projectiles")]
    public GameObject blasterProjectile;
    public GameObject grenadeProjectile;
    public GameObject machineProjectile;
    public GameObject missileProjectile;
    public GameObject shotgunProjectile;
    public GameObject sniperProjectile;

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

	/*
     * public void InstantiateBurst(Vector3 point)
	{
		Instantiate(burst, point, Quaternion.identity);
	}

	public void InstantiateBlood(Vector3 point)
	{
		Instantiate(blood, point, Quaternion.identity);
	}
    *
    */

	public void SetState(PlayerState state)
	{
		//_state.LeaveState(this);
		_state = state;
		//_state.EnterState(this);
	}

	public void Reload()
	{
		_state = PlayerState.reloadState;
	}
}
