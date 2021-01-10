using UnityEngine;
using System.Collections;

public class RayCastShootComplete : MonoBehaviour {

	public int gunDamage = 1;										
	public float fireRate = 0.25f;									
	public float weaponRange = 50f;										
	public float hitForce = 100f;										
	public Transform gunEnd;											
    public GameObject targetRectifier;
    public GameObject virusBullet;
	public GameObject bacteriaBullet;


	private Camera fpsCam;												
	private WaitForSeconds shotDuration = new WaitForSeconds(0.07f);	
	private AudioSource gunAudio;										
	private LineRenderer laserLine;										
	private float nextFire;												


	void Start () 
	{
		laserLine = GetComponent<LineRenderer>();

		gunAudio = GetComponent<AudioSource>();

		fpsCam = GetComponentInParent<Camera>();
	}
	

	void FixedUpdate () 
	{
		if (Input.GetButtonDown("Fire1") && Time.time > nextFire) 
		{
            AudioManager.instance.PlaySound("shoot");
			GameObject bullet = null;

			if(GameManager.instance.mode == GameMode.Bacteria)
            {
				bullet = Instantiate(bacteriaBullet, gunEnd.position, Quaternion.identity) as GameObject;
			}
            else
            {
				bullet = Instantiate(virusBullet, gunEnd.position, Quaternion.identity) as GameObject;
			}

			Rigidbody r = bullet.GetComponent<Rigidbody>();
            r.AddForce(targetRectifier.transform.forward * 3000f * 60 * Time.deltaTime);


            Vector3 vel = r.velocity;
            float angle = Mathf.Atan2(vel.y, vel.x) * Mathf.Rad2Deg;

            bullet.transform.eulerAngles = new Vector3(0, 0, angle);

            nextFire = Time.time + fireRate;

            r.useGravity = true;
        }
	}


	private IEnumerator ShotEffect()
	{
		gunAudio.Play ();
		laserLine.enabled = true;
		yield return shotDuration;
		laserLine.enabled = false;
	}
}