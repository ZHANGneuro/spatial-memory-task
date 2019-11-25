
using System;
using System.IO;
using System.Globalization;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityStandardAssets.Characters.FirstPerson;
using System.Diagnostics;

public class mark_direction_practice : MonoBehaviour{


	public static string subject_number = "";
	public static string which_subject_group = "";
	public static string which_type = "6d";
	bool noITI = false;

	//bool walking_finsih = false;
//	bool facing_finish = false;
//	bool target_finish = false;

	public static string[][] practice_formal_stage1_DataSet; 
	public static string[][] practice_formal_stage2_DataSet; 
	public static string[][] practice_formal_stage3_DataSet; 
	public static string which_map;
	public static string which_person;
	public static string facing_direction;
	string which_direction;


	public static int exp_stage=1;
	public static int trial_ith_formal_exp_stage1=0;
	public static int trial_ith_formal_exp_stage2=0;
	public static int trial_ith_formal_exp_stage3=0;
	public static int Number_of_person_move_head; 

	public static Vector3 subject_initial_position;
	public static Vector3 subject_response_position= new Vector3(250, 1f, 250);
	Vector3 moving_direction;
	Vector3 diff_betw_2Points;

	public static List<string> headmovement_order;
	public static List<Vector3> direction_list = new List<Vector3>();
	public static List<int> trial_order = new List<int>();
	public static List<int> trial_order_training_exp_stage3 = new List<int>();
	List<string> person_who_head_moved;
	List<string> person_who_head_NOT_moved;
	List<string> showed_head_profile;

	public GameObject balloon_base;
	public GameObject FPScontroller;
	public GameObject wooden_fence;
	GameObject canvasobject;
	GameObject canvasobject2;
	GameObject textObject;
	GameObject endpoint;

	Texture2D returned_person_image;
	Texture2D one_two_three;
	Texture2D one_three_two;
	Texture2D two_one_three;
	Texture2D two_three_one;
	Texture2D three_one_two;
	Texture2D three_two_one;
	Texture2D current_cue;
	Texture2D scrambled_image;
	Texture2D head_profile_image;

	bool input_exp_information = false;
	bool IsExploring = false;
	bool is_Motion_Detection_response_period = false;
	bool Is_navigation_response_period = false;
	bool motion_1_finished = false;
	bool motion_2_finished = false;
	bool motion_3_finished = false;
	bool timeRecordForTotal = false;
	bool timeRecordForWalking = false;
	bool timeRecordForRunning_constantSpeed = false;
	bool isOn_inst_1_screen = false;
	bool isOn_inst_2_screen = false;
	bool isOn_inst_3_screen = false;
    bool isOn_formal_exp_screen = false;
	bool isPausing_inst_fixation = false;
	bool isPausing_inst_walkingStart = false;
	bool isPausing_inst_walkingEnd = false;
	bool isPausing_inst_facingP = false;
	bool isPausing_inst_targetP = false;
	bool isIn_formal_exp_stage1 = false;
	bool isIn_formal_exp_stage2 = false;

	public static float howClosePersonTocenter = 2.6f;  
	float reference_coordinate_x = 225f;
	float reference_coordinate_z = 250f-2.25f;
	float new_coordinate_x;
	float new_coordinate_z;
	float fence_rotation=0;
	float RotateAngle_for_fence = 10.6f;
	float point_to_run;
	float distance_to_center_showing_motion_1 = 0f;
	float distance_to_center_showing_motion_2 = 0f;
	float distance_to_center_showing_motion_3 = 0f;
	float initial_speed;
	float speedup_duration;
	float Duration_for_total = 0f;
	float Duration_for_walk = 0f;
	float Duration_for_run_constantSpeed = 0f;
	float distance_to_center;
	float pure_duration;
	List<Texture2D> cue_list = new List<Texture2D>();

	Color32 backgroundColor = new Color32(68,68,68,255);
	Color fontColor = Color.white;
	Canvas canvas;
	Canvas canvas2;
	Image backgroundScreen;
	Image Scramble_Image;

	UnityEngine.KeyCode key_one;
	UnityEngine.KeyCode key_two;
	UnityEngine.KeyCode key_three;

	void Start(){

		Screen.SetResolution (1024, 768,true,60);
        Cursor.visible = true;
        key_one = KeyCode.W;
        key_two = KeyCode.O;
        key_three = KeyCode.P;
        input_exp_information = true;
	}

	void OnGUI(){
		if (input_exp_information){
			if(!canvasobject){
				showBackgroundScreen ();
			}
			GUI.Window(0, new Rect((Screen.width-250)/2, (Screen.height/10), 250, 250), ShowGUI, " ");
		}
	}

	void ShowGUI(int windowID){
		var Gui_subNum = GUI.skin.GetStyle("Label");
		Gui_subNum.alignment = TextAnchor.MiddleCenter;
		Gui_subNum.fontSize = 20;
		GUI.Label(new Rect(40, 60, 120, 30), "被试编号",Gui_subNum);
		subject_number = GUI.TextField(new Rect (170, 60, 40, 30), subject_number, 2);

		var Gui_subGroup = GUI.skin.GetStyle("Label");
		Gui_subGroup.alignment = TextAnchor.MiddleCenter;
		Gui_subGroup.fontSize = 20;
		GUI.Label(new Rect(40, 90, 120, 30), "被试组号",Gui_subGroup);
		which_subject_group = GUI.TextField(new Rect (170, 90, 40, 30), which_subject_group, 2);

		if (GUI.Button(new Rect( (250-100)/2, 200, 100, 30), "OK"))
		{
			input_exp_information = false;
			Destroy (canvasobject);

			mark_direction_practice_create_trial_table_person create_trial_table = wooden_fence.GetComponent<mark_direction_practice_create_trial_table_person> ();
			create_trial_table.create_trial_table ();

			initilzing ();

            if (exp_stage == 1) {	
                showcase_inst_1();
            } 
			else if (exp_stage == 2) {
				trial_ith_formal_exp_stage1 = 5;
                showcase_inst_2();
            } 
			else if (exp_stage == 3) {
				trial_ith_formal_exp_stage1 = 5;
				trial_ith_formal_exp_stage2 = 5;
                showcase_inst_3();
            }
		}
	}


	void pureScreen(){
		// setting

		Destroy (canvasobject);
		Destroy (canvasobject2);

		Destroy (endpoint);
		//walking_finsih = false;
//		facing_finish = false;
//		target_finish = false;
		if( (exp_stage==2 & trial_ith_formal_exp_stage2 == 1) | 
			(exp_stage==3 & trial_ith_formal_exp_stage3 == 1) |
			(exp_stage==3 & trial_ith_formal_exp_stage3 == 19)){
			endpoint = Instantiate(balloon_base, new Vector3(250, -0.98f, 250), Quaternion.Euler(0, 0, 0));
			endpoint.transform.localScale = new Vector3(0.8f, 1, 0.8f);
		}


		motion_1_finished = false;
		motion_2_finished = false;
		motion_3_finished = false;
		person_who_head_moved = new List<string> ();
		person_who_head_NOT_moved = new List<string> ();
		showed_head_profile = new List<string> ();
		mark_direction_practice_item_subject_person remove_persons = wooden_fence.GetComponent<mark_direction_practice_item_subject_person> ();
		remove_persons.remove_persons ();

		showBackgroundScreen ();

        pure_duration = UnityEngine.Random.Range(3.0f, 5.0f);

		if(noITI){
			Invoke ("fixationScreen",  0.1f);
		}else{
            Invoke("fixationScreen", pure_duration);
        }
	}

	void fixationScreen(){
		Destroy (canvasobject);
		Destroy (canvas);
		noise_image();
		if ((isIn_formal_exp_stage1 & trial_ith_formal_exp_stage1 <2)|
			(isIn_formal_exp_stage2 & trial_ith_formal_exp_stage2 <2)){
			isPausing_inst_fixation = true;
		} else {
			if (noITI) {
				Invoke ("prepare_to_walk", 0.1f);
			} else {
				Invoke ("prepare_to_walk", 2);
			}
		}
	}

	void prepare_to_walk(){
		Destroy (canvasobject);
		Destroy (canvas);

        // get direction & subject init location
        if (exp_stage == 1)
        {
            which_map = practice_formal_stage1_DataSet[trial_ith_formal_exp_stage1][0];
            which_direction = practice_formal_stage1_DataSet[trial_ith_formal_exp_stage1][1];
            Number_of_person_move_head = int.Parse(practice_formal_stage1_DataSet[trial_ith_formal_exp_stage1][3]);
        }
        if (exp_stage == 2)
        {
            which_map = practice_formal_stage2_DataSet[trial_ith_formal_exp_stage2][0];
            which_direction = practice_formal_stage2_DataSet[trial_ith_formal_exp_stage2][1];
            which_person = practice_formal_stage2_DataSet[trial_ith_formal_exp_stage2][2];
            Number_of_person_move_head = int.Parse(practice_formal_stage2_DataSet[trial_ith_formal_exp_stage2][4]);
        }
        if (exp_stage == 3)
        { 
            which_map = practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][0];
            which_direction = practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][1];
            which_person = practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][2];

            Number_of_person_move_head = int.Parse(practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][4]);
        }




        // order for head move & distance to center for head move
        headmovement_order = new List<string>();
		headmovement_order.Add ("head_cartoon_person_1");
		headmovement_order.Add ("head_cartoon_person_2");
		headmovement_order.Add ("head_cartoon_person_3");
		headmovement_order = ShuffleList (headmovement_order);
		if(Number_of_person_move_head==0){
			distance_to_center_showing_motion_1 = 0; 
			distance_to_center_showing_motion_2 = 0; 
			distance_to_center_showing_motion_3 = 0; 
		}
		if(Number_of_person_move_head==1){
			distance_to_center_showing_motion_1 = UnityEngine.Random.Range (8,14f); 
			distance_to_center_showing_motion_2 = 0; 
			distance_to_center_showing_motion_3 = 0; 
		}
		if(Number_of_person_move_head==2){
			distance_to_center_showing_motion_1 = UnityEngine.Random.Range (11.25f,14f); 
			distance_to_center_showing_motion_2 = UnityEngine.Random.Range (8,11.25f); 
			distance_to_center_showing_motion_3 = 0; 
		}
		if(Number_of_person_move_head==3){
			distance_to_center_showing_motion_1 = UnityEngine.Random.Range (8,10.3f); 
			distance_to_center_showing_motion_2 = UnityEngine.Random.Range (10.3f,12.1f); 
			distance_to_center_showing_motion_3 = UnityEngine.Random.Range (12.1f,14f); 
		}

        // place map (person position)
		mark_direction_practice_item_subject_person place_person_map = wooden_fence.GetComponent<mark_direction_practice_item_subject_person>();
		place_person_map.place_person_map ();

        // set subject start position
		mark_direction_practice_enter_direction_person get_direcion_pool = wooden_fence.GetComponent<mark_direction_practice_enter_direction_person> ();
		direction_list = get_direcion_pool.setup_enter_direction_pool ();
		subject_initial_position = direction_list.ElementAt (int.Parse (which_direction) - 1);


        // start animation
		mark_direction_practice_item_subject_person start_idle = wooden_fence.GetComponent<mark_direction_practice_item_subject_person>();
		start_idle.start_idle ();
        // person facing to subject
		mark_direction_practice_item_subject_person person_face_to_initial_position = wooden_fence.GetComponent<mark_direction_practice_item_subject_person>();
		person_face_to_initial_position.person_face_to_initial_position ();
		// subject move and forward
		FirstPersonController.m_CharacterController.transform.position = subject_initial_position;
		FirstPersonController.m_Camera.transform.localRotation = Quaternion.Euler (0, 0, 0);

		FirstPersonController.m_Camera.fieldOfView = 90;

		FirstPersonController.m_CharacterController.transform.forward=(subject_response_position-subject_initial_position).normalized;

		if ((isIn_formal_exp_stage1 & trial_ith_formal_exp_stage1 <2)|
			(isIn_formal_exp_stage2 & trial_ith_formal_exp_stage2 <2)){
			isPausing_inst_walkingStart = true;
		} else {
			Invoke ("runStart_init", 0);
		}
	}

	void runStart_init(){
		FirstPersonController.NO_MOVE = false;			
		if(noITI){
			initial_speed = 50f;
		}else{
			initial_speed = 2.555f; //2.68f
		}
		IsExploring = true;
		speedup_duration = 0.8f;
		point_to_run = 8f; 
	}

	void FixedUpdate(){

		if(timeRecordForTotal){
			Duration_for_total += Time.deltaTime;
		}
		if(timeRecordForWalking){
			Duration_for_walk += Time.deltaTime;
		}
		if(timeRecordForRunning_constantSpeed){
			Duration_for_run_constantSpeed += Time.deltaTime;
		}

		if (IsExploring) {
			timeRecordForTotal = true;
			diff_betw_2Points = subject_response_position - FirstPersonController.m_CharacterController.transform.position;
			distance_to_center = Vector3.Distance (subject_response_position, FirstPersonController.m_CharacterController.transform.position);
			if (distance_to_center > 0.25f) { // give head motion
				if (distance_to_center <= distance_to_center_showing_motion_1 & !motion_1_finished) {
					motion_1_finished = true;
					mark_direction_practice_item_subject_person head_movement = wooden_fence.GetComponent<mark_direction_practice_item_subject_person> ();
					person_who_head_moved.Add (headmovement_order.ElementAt (0));
					head_movement.start_headMovement (headmovement_order.ElementAt (0));
				}
				if (distance_to_center <= distance_to_center_showing_motion_2 & !motion_2_finished) {
					motion_2_finished = true;
					mark_direction_practice_item_subject_person head_movement = wooden_fence.GetComponent<mark_direction_practice_item_subject_person> ();
					person_who_head_moved.Add (headmovement_order.ElementAt (0));
					head_movement.start_headMovement (headmovement_order.ElementAt (0));
				}
				if (distance_to_center <= distance_to_center_showing_motion_3 & !motion_3_finished) {
					motion_3_finished = true;
					mark_direction_practice_item_subject_person head_movement = wooden_fence.GetComponent<mark_direction_practice_item_subject_person> ();
					person_who_head_moved.Add (headmovement_order.ElementAt (0));
					head_movement.start_headMovement (headmovement_order.ElementAt (0));
				}

				// walking & run controller
				if(distance_to_center>point_to_run){
					timeRecordForWalking = true;
					moving_direction = new Vector3 (diff_betw_2Points.normalized.x, -0.1f, diff_betw_2Points.normalized.z);
					FirstPersonController.m_CharacterController.Move (moving_direction * initial_speed * Time.deltaTime);
				} else if (distance_to_center <= point_to_run){
	
					//if(!walking_finsih){
					//	walking_finsih = true;
					//	Application.CaptureScreenshot(trial_ith_formal_exp_stage3 + "_walking.png");
					//}
					timeRecordForWalking = false;
					//	UnityEngine.Debug.Log ("Duration_for_walk" + Duration_for_walk);
					speedup_duration -= Time.deltaTime;
					if(speedup_duration>0){
						initial_speed = initial_speed + 0.125f; //+ 0.121f
						moving_direction = new Vector3 (diff_betw_2Points.normalized.x, -0.1f, diff_betw_2Points.normalized.z);
						FirstPersonController.m_CharacterController.Move (moving_direction * initial_speed * Time.deltaTime);
					}
					if(speedup_duration<=0){
						timeRecordForRunning_constantSpeed = true;
						moving_direction = new Vector3 (diff_betw_2Points.normalized.x, -0.1f, diff_betw_2Points.normalized.z);
						FirstPersonController.m_CharacterController.Move (moving_direction * initial_speed * Time.deltaTime);
					}
				}
			} 
			else {

				timeRecordForRunning_constantSpeed = false;
				timeRecordForTotal = false;
				//				UnityEngine.Debug.Log ("Duration_for_run_constantSpeed" + Duration_for_run_constantSpeed);
				//				UnityEngine.Debug.Log ("Duration_for_total" + Duration_for_total);
				Duration_for_total = 0;
				Duration_for_walk = 0;
				Duration_for_run_constantSpeed = 0;

				// who did not move head? add to list
				if(headmovement_order.Count!=0){
					for(int i =0; i<headmovement_order.Count;i++){
						person_who_head_NOT_moved.Add (headmovement_order.ElementAt(i));
					}
				}

				IsExploring = false;  
				FirstPersonController.NO_MOVE = true; 
				if ((isIn_formal_exp_stage1 & trial_ith_formal_exp_stage1 <2)|
					(isIn_formal_exp_stage2 & trial_ith_formal_exp_stage2 <2)){
					isPausing_inst_walkingEnd = true;
				} else {
					walking_end ();
				}
			}
		}
	}

	void Update(){
		if (!IsExploring & Is_navigation_response_period) {
				if(Input.GetKeyDown(key_one) | Input.GetKeyDown(key_two) | Input.GetKeyDown(key_three))
                {
					Is_navigation_response_period = false;
					Invoke ("write_to_text", 0);
				} 
		}


		if (!IsExploring & is_Motion_Detection_response_period) { 
			if(Input.GetKeyDown(key_one) | Input.GetKeyDown(key_two))
            {
				is_Motion_Detection_response_period = false;
                Invoke ("draw_feedback", 0);
			}
		}

		if(isOn_inst_1_screen){
			if(Input.GetKeyDown(KeyCode.Tab)){
				isOn_inst_1_screen = false;
                isIn_formal_exp_stage1 = true;
                Invoke ("pureScreen",0);
			}
		}
		if(isOn_inst_2_screen){
			if(Input.GetKeyDown(KeyCode.Tab)){
				isOn_inst_2_screen = false;
                isIn_formal_exp_stage1 = false;
                isIn_formal_exp_stage2 = true;
                Invoke ("pureScreen",0);
			}
		}
		if(isOn_inst_3_screen){
			if(Input.GetKeyDown(KeyCode.Tab)){
				isOn_inst_3_screen = false;
                isIn_formal_exp_stage2 = false;
                Invoke ("pureScreen",0);
			}
		}
		if(isPausing_inst_fixation){
			if (Input.GetKeyDown (KeyCode.Tab)) {
				isPausing_inst_fixation = false;
				Invoke ("prepare_to_walk", 0);
			}
		}
		if(isPausing_inst_walkingStart){
			if (Input.GetKeyDown (KeyCode.Tab)) {
				isPausing_inst_walkingStart = false;
				Invoke ("runStart_init", 0);
			}
		}
		if(isPausing_inst_walkingEnd){
			if (Input.GetKeyDown (KeyCode.Tab)) {
				isPausing_inst_walkingEnd = false;
				Invoke ("walking_end", 0f);
			}
		}
		if (isPausing_inst_facingP) {
			if (Input.GetKeyDown (KeyCode.Tab)) {
				isPausing_inst_facingP = false;
				Invoke ("noise_image2", 0f);
			}
		}
		if(isPausing_inst_targetP){
			if (Input.GetKeyDown (KeyCode.Tab)) {
				isPausing_inst_targetP = false;
				Invoke ("noise4", 0f);
			}
		}

        if (isOn_formal_exp_screen)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isOn_formal_exp_screen = false;
                pureScreen();
            }
        }
    }

	void show_head_profile(){
		showBackgroundScreen();
		showText ("这个人点头了吗?", 250, 50);
		canvasobject2 = new GameObject ("Canvas");
		canvas2 = canvasobject2.AddComponent<Canvas> ();
		canvas2.renderMode = RenderMode.WorldSpace;
		var image_object = canvasobject2.AddComponent<Image> ();
		image_object.transform.SetParent(backgroundScreen.transform);
		image_object.rectTransform.sizeDelta = new Vector2 (200, 200);
		image_object.rectTransform.anchoredPosition = new Vector3(0,0,0);

        if (exp_stage == 1)
        {
            if (practice_formal_stage1_DataSet[trial_ith_formal_exp_stage1][5] == "1")
            { 
                head_profile_image = (Texture2D)Resources.Load(person_who_head_moved.ElementAt(0));
                showed_head_profile.Add(person_who_head_moved.ElementAt(0));
            }
            if (practice_formal_stage1_DataSet[trial_ith_formal_exp_stage1][5] == "0")
            {
                head_profile_image = (Texture2D)Resources.Load(person_who_head_NOT_moved.ElementAt(0));
                showed_head_profile.Add(person_who_head_NOT_moved.ElementAt(0));
            }
        }
        if (exp_stage == 3)
        {
            if (practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][6] == "1")
            {
                head_profile_image = (Texture2D)Resources.Load(person_who_head_moved.ElementAt(0));
                showed_head_profile.Add(person_who_head_moved.ElementAt(0));
            }
            if (practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][6] == "0")
            {
                head_profile_image = (Texture2D)Resources.Load(person_who_head_NOT_moved.ElementAt(0));
                showed_head_profile.Add(person_who_head_NOT_moved.ElementAt(0));
            }
        }

        Sprite icon = Sprite.Create(head_profile_image, new Rect(0.0f, 0.0f, 256, 256), new Vector2(0.5f, 0.5f));
		image_object.sprite = icon;

		is_Motion_Detection_response_period = true;
	}


	void draw_feedback(){
		if(textObject){
			Destroy (textObject);
            Destroy(canvas2);
            Destroy(canvasobject2);
        }

		if(noITI){
			Invoke ("write_to_text", 0f);
		}else{
			Invoke ("write_to_text", 0.5f);
		}
	}

	void write_to_text(){
		if (exp_stage == 1) {
			trial_ith_formal_exp_stage1 = trial_ith_formal_exp_stage1 + 1;	
			if (trial_ith_formal_exp_stage1 < 4) {
				Invoke ("pureScreen", 0);
			}
			if (trial_ith_formal_exp_stage1 == 4) {
				exp_stage = exp_stage + 1;
				Invoke ("showcase_inst_2", 0);
			}
		} else if (exp_stage == 2) {
			trial_ith_formal_exp_stage2 = trial_ith_formal_exp_stage2 + 1;	
			if (trial_ith_formal_exp_stage2 < 4) {
				Invoke ("pureScreen", 0);
			}
			if (trial_ith_formal_exp_stage2 == 4) {
				exp_stage = exp_stage + 1;
				Invoke ("showcase_inst_3", 0);
			}
		} else if (exp_stage == 3) {
			trial_ith_formal_exp_stage3 = trial_ith_formal_exp_stage3 + 1;	
			if (trial_ith_formal_exp_stage3 < 20) {
				Invoke ("pureScreen", 0);
			}
			if (trial_ith_formal_exp_stage3 == 20) {
				exp_stage = exp_stage + 1;
				Invoke ("showcase_formal", 0);
			}
		} 
	}

	void noise_image1(){
		noise_image ();
		if(noITI){
			Invoke ("reset_subject_facing_direction",0);
		} else{
			Invoke ("reset_subject_facing_direction",2);
		}
	}

	void reset_subject_facing_direction(){
		mark_direction_practice_item_subject_person set_subject_facing_direction = wooden_fence.GetComponent<mark_direction_practice_item_subject_person> ();
		set_subject_facing_direction.set_subject_facing_direction ();

		mark_direction_practice_item_subject_person rotate_person = wooden_fence.GetComponent<mark_direction_practice_item_subject_person> ();
		rotate_person.person_face_to_center ();

		show_facing_person ();
	}

	void show_facing_person(){
		Destroy (canvasobject);
		Destroy (canvas);
	
//		if(!facing_finish){
//			facing_finish = true;
//			Application.CaptureScreenshot(trial_ith_formal_exp_stage3 + "_facing.png");
//		}

		if ((isIn_formal_exp_stage1 & trial_ith_formal_exp_stage1 <2)|
			(isIn_formal_exp_stage2 & trial_ith_formal_exp_stage2 <2)){
			isPausing_inst_facingP = true;
		} else {
			if(noITI){
				noise_image2 ();
			} else{
				Invoke ("noise_image2", 2f);
			}
		}
	}

	void noise_image2(){
		noise_image ();
		if(noITI){
			show_goal_person_image ();
		} else{
			Invoke ("show_goal_person_image", 2);
		}
	}


	void show_goal_person_image(){
		canvasobject2 = new GameObject ("Canvas");
		canvas2 = canvasobject2.AddComponent<Canvas> ();
		canvas2.renderMode = RenderMode.WorldSpace;
		var image_object = canvasobject2.AddComponent<Image> ();
		image_object.transform.SetParent(Scramble_Image.transform);
		image_object.rectTransform.sizeDelta = new Vector2(300, 300);
		image_object.rectTransform.anchoredPosition = new Vector3(0, -95, 0);

        // get person image & create output_part list
		mark_direction_practice_goalTable_personImage TargetImage = wooden_fence.GetComponent<mark_direction_practice_goalTable_personImage> ();
		returned_person_image = TargetImage.set_person_image ();

		Sprite icon = Sprite.Create(returned_person_image, new Rect(0.0f, 0.0f, 512, 512), new Vector2(0.5f, 0.5f));
		image_object.sprite = icon;

//		if(!target_finish){
//			target_finish = true;
//			Application.CaptureScreenshot(trial_ith_formal_exp_stage3 + "_target.png");
//		}
	
		if ((isIn_formal_exp_stage1 & trial_ith_formal_exp_stage1 <2)|
			(isIn_formal_exp_stage2 & trial_ith_formal_exp_stage2 <2)){
			isPausing_inst_targetP = true;
		} else {
			if(noITI){
                noise4();
			} else{
				Invoke ("noise4", 2f);
			}
		}
	}

	void noise4(){
		Destroy (canvasobject2);
		if(noITI){
			show_question ();
		} else{
			Invoke ("show_question", 2f);
		}
	}

	void show_question(){
		Destroy(canvasobject);
		Destroy(canvas);
		showBackgroundScreen ();
		canvasobject2 = new GameObject ("Canvas");
		canvas2 = canvasobject2.AddComponent<Canvas> ();
		canvas2.renderMode = RenderMode.WorldSpace;
		var image_object = canvasobject2.AddComponent<Image> ();
		image_object.transform.SetParent(backgroundScreen.transform);

		cue_list = ShuffleList (cue_list);
		current_cue = cue_list.ElementAt (0);
		Sprite icon = Sprite.Create(current_cue, new Rect(0.0f, 0.0f, 500, 500), new Vector2(0.5f, 0.5f));
		image_object.sprite = icon;
		image_object.rectTransform.sizeDelta = new Vector2 (500,500);

		Is_navigation_response_period = true;
	}
		
	void showcase_inst_1(){
		showBackgroundScreen();
		showText ("练习-1\n\n共4个游戏，主试向你讲解前2个\n\n按 Tab 键开始",0,50);
		isOn_inst_1_screen = true;
	}
	void showcase_inst_2(){
		Destroy(canvasobject);
		showBackgroundScreen();
		showText ("练习-2\n\n共4个游戏，主试向你讲解前2个\n\n按 Tab 键开始",0,50);
		isOn_inst_2_screen = true;
	}
	void showcase_inst_3(){
		Destroy(canvasobject);
		showBackgroundScreen();
        showText("练习-3\n\n请完成20个游戏\n\n按 Tab 键开始", 0, 50);
        isOn_inst_3_screen = true;
	}

    void showcase_formal(){
		Destroy(canvasobject);
		showBackgroundScreen();
        showText("练习结束\n\n有任何问题请询问主试\n\n不要带着问题实验，谢谢！", 0, 50);
    }


	void showBackgroundScreen(){
		canvasobject = new GameObject ("Canvas");
		canvas = canvasobject.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		backgroundScreen = canvasobject.AddComponent<Image> ();
		backgroundScreen.rectTransform.sizeDelta = new Vector2 (Screen.width, Screen.height);
		backgroundScreen.rectTransform.anchoredPosition = Vector3.zero;
		backgroundScreen.color = backgroundColor;
	}

	void showText(string inputText, int inputYposition, int inputFontSize){
		textObject = new GameObject("Text");
		textObject.transform.SetParent(backgroundScreen.transform);
		var text = textObject.AddComponent<Text>();
		text.horizontalOverflow = HorizontalWrapMode.Overflow;
		text.alignment = TextAnchor.MiddleCenter;
		text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
		text.color = fontColor;
		text.text = inputText;
		RectTransform text_size = text.GetComponent<RectTransform>();
		text_size.sizeDelta = new Vector2(1024, 768);
		text_size.anchoredPosition = new Vector3(0,inputYposition,0);
		text.fontSize = inputFontSize;
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

	void initilzing(){

        // set enter direction pool
		mark_direction_practice_enter_direction_person create_initial_direction_pool = wooden_fence.GetComponent<mark_direction_practice_enter_direction_person> ();
		create_initial_direction_pool.create_initial_direction_pool_for_each_map ();

        // create_goal_table
		mark_direction_practice_goalTable_personImage create_goal_table = wooden_fence.GetComponent<mark_direction_practice_goalTable_personImage> ();
		create_goal_table.create_goal_table ();

        // load_person_image
		mark_direction_practice_goalTable_personImage load_person_image = wooden_fence.GetComponent<mark_direction_practice_goalTable_personImage> ();
		load_person_image.load_person_image ();


		// load response cue
		one_two_three = (Texture2D) Resources.Load("123");
		one_three_two = (Texture2D) Resources.Load("132");
		two_one_three = (Texture2D) Resources.Load("213");
		two_three_one = (Texture2D) Resources.Load("231");
		three_one_two = (Texture2D) Resources.Load("312");
		three_two_one = (Texture2D) Resources.Load("321");

		cue_list.Add (one_two_three);
		cue_list.Add (one_three_two);
		cue_list.Add (two_one_three);
		cue_list.Add (two_three_one);
		cue_list.Add (three_one_two);
		cue_list.Add (three_two_one);

		Instantiate (FPScontroller, Vector3.zero, Quaternion.Euler (0, 0, 0));	
		Instantiate (wooden_fence, new Vector3 (reference_coordinate_x, 0, reference_coordinate_z), Quaternion.Euler (0, 0, 0));

        for (int i = 0; i < 34; i++) {
			if (i == 0) {
				new_coordinate_x = reference_coordinate_x;
				new_coordinate_z = reference_coordinate_z;
				Instantiate (wooden_fence, new Vector3 (new_coordinate_x, 0.0f, new_coordinate_z), Quaternion.Euler (0, fence_rotation, 0));
			} 
			if (i != 0) { 
				fence_rotation = fence_rotation + RotateAngle_for_fence;
				if (i == 1) {
					new_coordinate_x = reference_coordinate_x;
					new_coordinate_z = reference_coordinate_z + 4.6f;
					Instantiate(wooden_fence, new Vector3(reference_coordinate_x, 0.0f, new_coordinate_z), Quaternion.Euler(0, fence_rotation, 0));
				}
				if (i > 1) {
					new_coordinate_x = new_coordinate_x + 4.6f * Mathf.Sin (((fence_rotation-RotateAngle_for_fence) * Mathf.PI)/180);
					new_coordinate_z = new_coordinate_z + 4.6f * Mathf.Cos (((fence_rotation-RotateAngle_for_fence) * Mathf.PI)/180);
					Instantiate(wooden_fence, new Vector3(new_coordinate_x, 0.0f, new_coordinate_z), Quaternion.Euler(0, fence_rotation, 0));
				}
			}
		}
	}

	void walking_end(){
        if (exp_stage == 1)
        {
            if (practice_formal_stage1_DataSet[trial_ith_formal_exp_stage1][4] == "1")
            {
                show_head_profile();
            }
            if (practice_formal_stage1_DataSet[trial_ith_formal_exp_stage1][4] == "0")
            {
                noise_image1();
            }
        }
        if (exp_stage == 2)
        {
            if (practice_formal_stage2_DataSet[trial_ith_formal_exp_stage2][5] == "1")
            {
                show_head_profile();
            }
            if (practice_formal_stage2_DataSet[trial_ith_formal_exp_stage2][5] == "0")
            {
                noise_image1();
            }
        }
        if (exp_stage == 3)
        {
            if (practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][5] == "1")
            {//trial type lure = 1
                show_head_profile();
            }
            if (practice_formal_stage3_DataSet[trial_ith_formal_exp_stage3][5] == "0")
            {
                noise_image1();
            }
        }
    }

	void noise_image(){
		canvasobject = new GameObject ("Canvas");
		canvas = canvasobject.AddComponent<Canvas> ();
		canvas.renderMode = RenderMode.ScreenSpaceOverlay;
		Scramble_Image = canvasobject.AddComponent<Image> ();
		Scramble_Image.transform.SetParent (canvas.transform);

		int picked = UnityEngine.Random.Range (0, 281);
		var temp_path = "noise_image/" + picked.ToString ();
		scrambled_image = (Texture2D)Resources.Load (temp_path);

		Sprite fragment_sprite = Sprite.Create (scrambled_image, new Rect (0.0f, 0.0f, Screen.width, Screen.height), new Vector2 (0.5f, 0.5f));
		Scramble_Image.sprite = fragment_sprite;
		Scramble_Image.rectTransform.sizeDelta = new Vector2 (Screen.width, Screen.height);
		Scramble_Image.rectTransform.anchoredPosition = new Vector3 (0, 0, 0);
	}


} 

