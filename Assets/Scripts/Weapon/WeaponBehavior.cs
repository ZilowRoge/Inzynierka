using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Weapon {
public class WeaponBehavior : Inventory.Items.Item {
	public enum EFireType{
		Automatic,
		Semi_automatic,
		Burst
	}

	//Gun preferences
	public int max_rounds;
	public int current_rounds;
	public EFireType fire_type;

	public EAmmoType ammo_type;
	public float range;
	public float damage;
	public float fire_rate;

	public bool multiple_modes;


	//Audio
	public AudioClip shot_sound;
	public AudioClip out_of_ammo_sound;
	public AudioClip realoding_sound;

	public AudioClip mode_switch_sound;

	private AudioSource audio_source;

	//for raycasting
	public Transform shot_start;
	public GameObject bullet;

	public float force = 400;

	//private settings
	bool shot_fired = false;

	float last_shot = 0;



	// Use this for initialization
	void Start () {
		audio_source = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		update_fire_rate(Time.deltaTime);
	}
	
	private void fire_bullet()
	{
		if(current_rounds > 0){
				audio_source.PlayOneShot(shot_sound);
				create_bullet();
				current_rounds--;
		} else {
			audio_source.PlayOneShot(out_of_ammo_sound);
		}
	}
	public int get_ammo_type()
	{
		return (int)ammo_type;
	}
	public bool can_change_mode(){
		return multiple_modes;
	}

	public int reload(int ammo_to_use)
	{
		audio_source.PlayOneShot(realoding_sound);
		int missing_ammo = max_rounds - current_rounds;
		if(ammo_to_use >= missing_ammo){
			current_rounds += missing_ammo;
			return missing_ammo;
		}
		if(ammo_to_use < missing_ammo && ammo_to_use > 0){
			current_rounds += ammo_to_use;
			return ammo_to_use;
		}
		if(ammo_to_use <= 0){
			return 0;
		}
		return 0;	
		
	}

	private void update_fire_rate(float time)
	{
		if (last_shot < fire_rate) {
			last_shot += time;
		}
	}

	public void switch_mode()
	{
		audio_source.PlayOneShot(mode_switch_sound);
		if(fire_type == EFireType.Automatic) {
			fire_type = EFireType.Semi_automatic;
		} else {
			fire_type = EFireType.Automatic;
		}
		//fire_type = (EFireType)((((int)fire_type)+1) % 3);
	}
	public void fired()
	{
		shot_fired = false;
	}

	public void fire()
	{
		Debug.Log("Fire type = " + fire_type);
		switch(fire_type)
		{
			case EFireType.Semi_automatic:
				fire_semi_automatic();
			break;
			case EFireType.Automatic:
				fire_automatic();
			break;
		}
	}

	void fire_semi_automatic()
	{
		if(!shot_fired && last_shot > fire_rate){
			fire_bullet();
			shot_fired = true;
			last_shot = 0;
		}
	}


	void fire_automatic()
	{
		if(last_shot > fire_rate){
			fire_bullet();
			last_shot = 0;
		}
	}

	void fire_burst()
	{
		
	}

	void create_bullet()
	{
		GameObject obj;
		obj = Instantiate(bullet, shot_start.transform.position, new Quaternion(0,0,0,0));
		obj.GetComponent<BulletBehavior>().set_damage(damage);
		obj.GetComponent<Rigidbody>().AddForce(shot_start.transform.up * force);	
	}

}

}//namespace Weapon
