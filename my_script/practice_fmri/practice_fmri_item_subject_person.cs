using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityStandardAssets.Characters.FirstPerson;

public class practice_fmri_item_subject_person : MonoBehaviour {

	public GameObject cartoon_person_1;
	public GameObject cartoon_person_2;
	public GameObject cartoon_person_3;
	GameObject person_1;
	GameObject person_2;
	GameObject person_3;
	GameObject person_1_object;
	GameObject person_2_object;
	GameObject person_3_object;

	Animator animator_person1;
	Animator animator_person2;
	Animator animator_person3;

	string idle_1;
	string idle_2;
	string idle_3;


	List<int> person_who_motivate;
	List<string> idle_list;
	static Vector3 person_location_1 = new Vector3(245+practice_fmri.howClosePersonTocenter,0,255-practice_fmri.howClosePersonTocenter);
	static Vector3 person_location_2 = new Vector3(245+practice_fmri.howClosePersonTocenter,0,245+practice_fmri.howClosePersonTocenter);
	static Vector3 person_location_3 = new Vector3(255-practice_fmri.howClosePersonTocenter,0,245+practice_fmri.howClosePersonTocenter);
	static Vector3 person_location_4 = new Vector3(255-practice_fmri.howClosePersonTocenter,0,255-practice_fmri.howClosePersonTocenter);
	static Vector3 facing_direction_1 = (person_location_1- new Vector3(250,0,250)).normalized;
	static Vector3 facing_direction_2 = (person_location_2- new Vector3(250,0,250)).normalized;
	static Vector3 facing_direction_3 = (person_location_3- new Vector3(250,0,250)).normalized;
	static Vector3 facing_direction_4 = (person_location_4- new Vector3(250,0,250)).normalized;

	public void place_person_map(){

		int angle_rotation = 0;

			person_1 = cartoon_person_1;
			person_2 = cartoon_person_2;
			person_3 = cartoon_person_3;

		if(string.Equals ( practice_fmri.which_subject_group, "1")){
			if (string.Equals ( practice_fmri.which_map, "1")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "2")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "3")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
			}
		}
		if(string.Equals ( practice_fmri.which_subject_group, "2")){
			if (string.Equals ( practice_fmri.which_map, "1")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "2")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "3")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
			}
		}
		if(string.Equals ( practice_fmri.which_subject_group, "3")){
			if (string.Equals ( practice_fmri.which_map, "1")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "2")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "3")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
			}
		}
		if(string.Equals ( practice_fmri.which_subject_group, "4")){
			if (string.Equals ( practice_fmri.which_map, "1")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "2")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "3")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
			}
		}
		if(string.Equals ( practice_fmri.which_subject_group, "5")){
			if (string.Equals ( practice_fmri.which_map, "1")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "2")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "3")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
			}
		}
		if(string.Equals ( practice_fmri.which_subject_group, "6")){
			if (string.Equals ( practice_fmri.which_map, "1")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_2, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "2")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
			}
			if (string.Equals ( practice_fmri.which_map, "3")){
				person_1_object = Instantiate(person_1, person_location_1, Quaternion.Euler(0, angle_rotation, 0));
				person_2_object = Instantiate(person_2, person_location_4, Quaternion.Euler(0, angle_rotation, 0));
				person_3_object = Instantiate(person_3, person_location_3, Quaternion.Euler(0, angle_rotation, 0));
			}
		}
	}
		

	public void start_idle(){
		idle_1 = "idle_lr_1";
		idle_2 = "idle_lr_2";
		idle_3 = "idle_lr_3";

		idle_list = new List<string>();
		idle_list.Add (idle_1);
		idle_list.Add (idle_2);
		idle_list.Add (idle_3);
		idle_list = ShuffleList (idle_list);

		animator_person1 = person_1_object.GetComponent<Animator> ();
		animator_person1.runtimeAnimatorController = Instantiate(Resources.Load("animation_headmotion")) as RuntimeAnimatorController;
		animator_person1.SetTrigger (idle_list.ElementAt(0));
		animator_person2 = person_2_object.GetComponent<Animator> ();
		animator_person2.runtimeAnimatorController = Instantiate(Resources.Load("animation_headmotion")) as RuntimeAnimatorController;
		animator_person2.SetTrigger (idle_list.ElementAt(1));
		animator_person3 = person_3_object.GetComponent<Animator> ();
		animator_person3.runtimeAnimatorController = Instantiate(Resources.Load("animation_headmotion")) as RuntimeAnimatorController;
		animator_person3.SetTrigger (idle_list.ElementAt(2));
	}

	public void remove_persons(){
		Destroy (animator_person1);
		Destroy (animator_person2);
		Destroy (animator_person3);
		Destroy (person_3_object);
		Destroy (person_2_object);
		Destroy (person_1_object);
	}
		
	public void start_headMovement(string which_person_moveHead){

		string idle1_aniCondition = "idle1_03";
		string idle2_aniCondition = "idle2_03";
		string idle3_aniCondition = "idle3_03";

		List<string> idle1_motion = new List<string>(); 
		idle1_motion.Add (idle1_aniCondition);  
//		idle1_motion.Add ("idle1_big");  
		idle1_motion = ShuffleList (idle1_motion);

		List<string> idle2_motion = new List<string>();
		idle2_motion.Add (idle2_aniCondition);
//		idle2_motion.Add ("idle2_big");
		idle2_motion = ShuffleList (idle2_motion);

		List<string> idle3_motion = new List<string>();
		idle3_motion.Add (idle3_aniCondition);
//		idle3_motion.Add ("idle3_big");
		idle3_motion = ShuffleList (idle3_motion);

		if(string.Equals(which_person_moveHead,"head_cartoon_person_1")){
			if(idle_list.ElementAt(0) == idle_1){
				animator_person1.SetTrigger (idle1_motion.ElementAt(0));
				if(string.Equals(idle1_motion.ElementAt(0),"idle1_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle1_motion.ElementAt(0),idle1_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
			if(idle_list.ElementAt(0) == idle_2){
				animator_person1.SetTrigger (idle2_motion.ElementAt(0));
				if(string.Equals(idle2_motion.ElementAt(0),"idle2_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle2_motion.ElementAt(0),idle2_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
			if(idle_list.ElementAt(0) == idle_3){
				animator_person1.SetTrigger (idle3_motion.ElementAt(0));
				if(string.Equals(idle3_motion.ElementAt(0),"idle3_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle3_motion.ElementAt(0),idle3_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
		}
		if(string.Equals(which_person_moveHead,"head_cartoon_person_2")){
			if(idle_list.ElementAt(1) == idle_1){
				animator_person2.SetTrigger (idle1_motion.ElementAt(0));
				if(string.Equals(idle1_motion.ElementAt(0),"idle1_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle1_motion.ElementAt(0),idle1_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
			if(idle_list.ElementAt(1) == idle_2){
				animator_person2.SetTrigger (idle2_motion.ElementAt(0));
				if(string.Equals(idle2_motion.ElementAt(0),"idle2_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle2_motion.ElementAt(0),idle2_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
			if(idle_list.ElementAt(1) == idle_3){
				animator_person2.SetTrigger (idle3_motion.ElementAt(0));
				if(string.Equals(idle3_motion.ElementAt(0),"idle3_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle3_motion.ElementAt(0),idle3_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
		}
		if(string.Equals(which_person_moveHead,"head_cartoon_person_3")){
			if(idle_list.ElementAt(2) == idle_1){
				animator_person3.SetTrigger (idle1_motion.ElementAt(0));
				if(string.Equals(idle1_motion.ElementAt(0),"idle1_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle1_motion.ElementAt(0),idle1_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
			if(idle_list.ElementAt(2) == idle_2){
				animator_person3.SetTrigger (idle2_motion.ElementAt(0));
				if(string.Equals(idle2_motion.ElementAt(0),"idle2_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle2_motion.ElementAt(0),idle2_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
			if(idle_list.ElementAt(2) == idle_3){
				animator_person3.SetTrigger (idle3_motion.ElementAt(0));
				if(string.Equals(idle3_motion.ElementAt(0),"idle3_small")){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
				if(string.Equals(idle3_motion.ElementAt(0),idle3_aniCondition)){
					practice_fmri.headmovement_order.RemoveAt (0);
				}
			}
		}
	}
		
	public void person_face_to_initial_position(){
		person_1_object.transform.forward = (practice_fmri.subject_initial_position - person_1_object.transform.position).normalized;
		person_2_object.transform.forward = (practice_fmri.subject_initial_position - person_2_object.transform.position).normalized;
		person_3_object.transform.forward = (practice_fmri.subject_initial_position - person_3_object.transform.position).normalized;
	}
	public void person_face_to_center(){
		person_1_object.transform.forward = (new Vector3(250,0,250) - person_1_object.transform.position).normalized;
		person_2_object.transform.forward = (new Vector3(250,0,250) - person_2_object.transform.position).normalized;
		person_3_object.transform.forward = (new Vector3(250,0,250) - person_3_object.transform.position).normalized;
	}

		
	public void set_subject_facing_direction(){

		if(string.Equals (practice_fmri.which_person, "1")){
			FirstPersonController.m_CharacterController.transform.forward = facing_direction_1;
			practice_fmri.facing_direction = "facing_direction_1";
		}
		if ((string.Equals ( practice_fmri.which_subject_group, "1") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "1") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "1") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "2") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "2") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "2") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "3") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "3") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "4") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "4") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "5") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "6") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "2"))) {
			FirstPersonController.m_CharacterController.transform.forward = facing_direction_2;
			practice_fmri.facing_direction = "facing_direction_2";
		}
		if ((string.Equals ( practice_fmri.which_subject_group, "1") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "1") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "2") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "2") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "3") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "3") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "4") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "4") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "5") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "5") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "6") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "6") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "3"))) {
			FirstPersonController.m_CharacterController.transform.forward = facing_direction_3;
			practice_fmri.facing_direction = "facing_direction_3";
		}
		if ((string.Equals ( practice_fmri.which_subject_group, "1") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "2") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "3") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "3") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "4") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "4") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "5") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "5") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "5") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "2"))|
			(string.Equals ( practice_fmri.which_subject_group, "6") & string.Equals ( practice_fmri.which_map, "1") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "6") & string.Equals ( practice_fmri.which_map, "2") & string.Equals (practice_fmri.which_person, "3"))|
			(string.Equals ( practice_fmri.which_subject_group, "6") & string.Equals ( practice_fmri.which_map, "3") & string.Equals (practice_fmri.which_person, "2"))) {
			FirstPersonController.m_CharacterController.transform.forward = facing_direction_4;
			practice_fmri.facing_direction = "facing_direction_4";
		}
		FirstPersonController.m_CharacterController.transform.position = new Vector3 (
			250,
			FirstPersonController.m_CharacterController.transform.position.y,
			250);
		FirstPersonController.m_Camera.transform.localRotation = Quaternion.Euler (0, 0, 0); 
	}
		

	List<E> ShuffleList<E>(List<E> inputList){
		List<E> randomList = new List<E>();
		System.Random r = new System.Random();
		int randomIndex = 0;
		while (inputList.Count > 0){
			randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
			randomList.Add(inputList[randomIndex]); //add it to the new, random list
			inputList.RemoveAt(randomIndex); //remove to avoid duplicates
		}
		return randomList; //return the new random list
	}
}
