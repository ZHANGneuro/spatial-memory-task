
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
using System.Threading;

public class mark_direction_fmri : MonoBehaviour{

	public static string subject_number = "";
	public static string which_subject_group = "";
	public static string which_type = "7d"; //d7

	bool noITI = false;

    elconnect.ELmain eyelink;

    public static string[][] reorganized_DataSet; 

	public static string output_path;
	public static string output_Time_path;
	public static string KeyPress_Testing_path;
	public static string eyelink_fileName;
	public static string eyelink_output_path;
	public static string which_map;
	public static string which_person;
	public static string facing_direction;
	string which_direction;

	public static int run_ith=1;
	public static int trial_ith=0;
	public static int Number_of_person_move_head; 

	public static Vector3 subject_initial_position;
	public static Vector3 subject_response_position= new Vector3(250, 1f, 250);
	Vector3 moving_direction;
	Vector3 diff_betw_2Points;

	public static List<string> headmovement_order;
	public static List<Vector3> direction_list = new List<Vector3>();
	public static List<int> trial_order = new List<int>();
	List<string> person_who_head_moved;
	List<string> person_who_head_NOT_moved;
	List<string> showed_head_profile;
	List<string> output;
	List<string> output_time;
	List<string> keyPress_output;

	public GameObject FPScontroller;
	public GameObject wooden_fence;
    public GameObject balloon_base;
    GameObject canvasobject;
	GameObject canvasobject2;
	GameObject canvasobject3;
	GameObject textObject;
   
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
	Texture2D feedback;

	Stopwatch exp_time = new Stopwatch();

	bool input_exp_information = false;
    bool init_finish = false;
    bool IsExploring = false;
	bool is_Motion_Detection_response_period = false;
	bool Is_navigation_response_period = false;
	bool motion_1_finished = false;
	bool motion_2_finished = false;
	bool motion_3_finished = false;
	bool isOnBreakScreen = false;
	bool timeRecordForTotal = false;
	bool timeRecordForWalking = false;
	bool timeRecordForRunning_constantSpeed = false;
	bool correctness = false;
    bool isOn_formal_exp_screen = false;
	bool isOn_Question_screen = false;

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
	float subject_preformance_fee;
	float Duration_for_total = 0f;
	float Duration_for_walk = 0f;
	float Duration_for_run_constantSpeed = 0f;
	float distance_to_center;
	float pure1_onsetTime;
	float waitingTR_onsetTime;
	float waitingTR_endTime;
	float fixation_onsetTime;
	float walking_onsetTime;
	float noise1_onsetTime;
	float facing_onsetTime;
	float noise2_onsetTime;
	float target_onsetTime;
	float pure2_onsetTime;
	float Question_onsetTime;
	float Question_headNod_onsetTime;
	float PressButton_Time;
	float PressButton_HeadNod_Time;
	float ReactionTime;
	float pure_duration;
	int screenWidth;
	int screenHeight;
	List<Texture2D> cue_list = new List<Texture2D>();

	Color32 backgroundColor = new Color32(68,68,68,255);
	Color fontColor = Color.white;
	Canvas canvas;
	Canvas canvas2;
	Canvas canvas3;
	Image backgroundScreen;
	Image Scramble_Image;

	UnityEngine.KeyCode key_one;
	UnityEngine.KeyCode key_two;
	UnityEngine.KeyCode key_three;

	void Start(){

		Screen.SetResolution(1024, 768, true, 60);
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
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

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
            Destroy(canvasobject);
            input_exp_information = false;
            subject_preformance_fee = 10f;

			mark_direction_fmri_create_trialTable_person create_trial_table = wooden_fence.GetComponent<mark_direction_fmri_create_trialTable_person> ();
			create_trial_table.create_trial_table ();

			initilzing ();

            //calculate_initial_position(); // REMOVE SOON
			showcase_formal ();
		}
	}


	void pureScreen(){
		// setting

		Destroy (canvasobject);
		Destroy (canvasobject2);

		output = new List<string>();
		output_time = new List<string>(); 
		motion_1_finished = false;
		motion_2_finished = false;
		motion_3_finished = false;
		person_who_head_moved = new List<string> ();
		person_who_head_NOT_moved = new List<string> ();
		showed_head_profile = new List<string> ();
		mark_direction_fmri_item_subject_person remove_persons = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person> ();
		remove_persons.remove_persons ();

		output.Add (which_subject_group.ToString() + "\t"); 

		showBackgroundScreen ();

		pure_duration = UnityEngine.Random.Range (3.0f, 5.0f);
		output_time.Add (pure_duration.ToString()+ "\t");

		output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t"); 

		if(noITI){
			Invoke ("noise_screen1",  0.1f);
		}else{
			Invoke ("noise_screen1", pure_duration);
		}
	}
		

	void noise_screen1(){
		Destroy (canvasobject);
		Destroy (canvas);
		noise_method();
		output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t");

        if (noITI)
        {
            Invoke("prepare_to_walk", 0.1f);
        }
        else
        {
            Invoke("prepare_to_walk", 2);
        }
    }

	void prepare_to_walk(){
		Destroy (canvasobject);
		Destroy (canvas);

        // get direction & subject init location
        which_map = reorganized_DataSet[trial_order.ElementAt(trial_ith)][0];
        which_direction = reorganized_DataSet[trial_order.ElementAt(trial_ith)][1];
		which_direction = "5";

		List<string> person_pool = new List<string>();
		person_pool.Add ("1");
		person_pool.Add ("2");
		person_pool.Add ("3");
		person_pool = ShuffleList (person_pool);
		which_person = person_pool.ElementAt(0);
        Number_of_person_move_head = int.Parse(reorganized_DataSet[trial_order.ElementAt(trial_ith)][3]);

        // output
        output.Add(which_map + "\t");
        output.Add(which_direction + "\t");

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
		mark_direction_fmri_item_subject_person place_person_map = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person>();
		place_person_map.place_person_map ();

        // set subject start position
		mark_direction_fmri_enter_direction_person get_direcion_pool = wooden_fence.GetComponent<mark_direction_fmri_enter_direction_person> ();
		direction_list = get_direcion_pool.setup_enter_direction_pool ();
		subject_initial_position = direction_list.ElementAt (int.Parse (which_direction) - 1);


        // start animation
		mark_direction_fmri_item_subject_person start_idle = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person>();
		start_idle.start_idle ();
        // person facing to subject
		mark_direction_fmri_item_subject_person person_face_to_initial_position = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person>();
		person_face_to_initial_position.person_face_to_initial_position ();
		// subject move and forward
		FirstPersonController.m_CharacterController.transform.position = subject_initial_position;
		FirstPersonController.m_Camera.transform.localRotation = Quaternion.Euler (0, 0, 0);

		FirstPersonController.m_Camera.fieldOfView = 90;
		FirstPersonController.m_CharacterController.transform.forward=(subject_response_position-subject_initial_position).normalized;

        Invoke("runStart_init", 0);
    }

	void runStart_init(){
		FirstPersonController.NO_MOVE = false;			
		if(noITI){
			initial_speed = 50f;
		}else{
			initial_speed = 2.65f; //2.68f
		}
		IsExploring = true;
		output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t");

        speedup_duration = 0.4f;
		point_to_run = 8.5f; 
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
                    mark_direction_fmri_item_subject_person head_movement = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person> ();
					person_who_head_moved.Add (headmovement_order.ElementAt (0));
					head_movement.start_headMovement (headmovement_order.ElementAt (0));
				}
				if (distance_to_center <= distance_to_center_showing_motion_2 & !motion_2_finished) {
					motion_2_finished = true;
                    mark_direction_fmri_item_subject_person head_movement = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person> ();
					person_who_head_moved.Add (headmovement_order.ElementAt (0));
					head_movement.start_headMovement (headmovement_order.ElementAt (0));
				}
				if (distance_to_center <= distance_to_center_showing_motion_3 & !motion_3_finished) {
					motion_3_finished = true;
                    mark_direction_fmri_item_subject_person head_movement = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person> ();
					person_who_head_moved.Add (headmovement_order.ElementAt (0));
					head_movement.start_headMovement (headmovement_order.ElementAt (0));
				}

				// walking & run controller
				if(distance_to_center>point_to_run){
					timeRecordForWalking = true;
					moving_direction = new Vector3 (diff_betw_2Points.normalized.x, -0.1f, diff_betw_2Points.normalized.z);
					FirstPersonController.m_CharacterController.Move (moving_direction * initial_speed * Time.deltaTime);
				} else if (distance_to_center <= point_to_run){
//					UnityEngine.Debug.Log ("start"+initial_speed);
					timeRecordForWalking = false;
//					 UnityEngine.Debug.Log ("Duration_for_walk" + Duration_for_walk);
					speedup_duration -= Time.deltaTime;
					if(speedup_duration>0){
						initial_speed = initial_speed + 0.141f; //+ 0.121f
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
//				UnityEngine.Debug.Log ("final"+initial_speed);
                timeRecordForRunning_constantSpeed = false;
				timeRecordForTotal = false;
//				 UnityEngine.Debug.Log ("Duration_for_run_constantSpeed" + Duration_for_run_constantSpeed);
//				 UnityEngine.Debug.Log ("Duration_for_total" + Duration_for_total);
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
                walking_end();
            }
		}
	}

	void Update(){

        // write pressed key and its time
        if (init_finish)
        {
            foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(vKey))
                {
                    keyPress_output = new List<string>();
                    keyPress_output.Add(trial_ith.ToString() + " \t");
                    keyPress_output.Add(isOn_Question_screen.ToString() + " \t");
                    keyPress_output.Add(exp_time.ElapsedMilliseconds.ToString() + " \t");
                    keyPress_output.Add(vKey + Environment.NewLine);
                    using (StreamWriter writer = new StreamWriter(KeyPress_Testing_path, true))
                    {
                        for (int i = 0; i < keyPress_output.Count; i++)
                        {
                            writer.Write(keyPress_output.ElementAt(i));
                        }
                        writer.Close();
                    }
                }
            }
        }

        if (!IsExploring & Is_navigation_response_period) {
			if (trial_ith < reorganized_DataSet.Length) {
				if( (Input.GetKeyDown (key_one) & (current_cue.Equals(one_two_three) | current_cue.Equals(one_three_two))) |
					(Input.GetKeyDown (key_two) & (current_cue.Equals(two_one_three) | current_cue.Equals(two_three_one))) |
					(Input.GetKeyDown (key_three) & (current_cue.Equals(three_one_two) | current_cue.Equals(three_two_one)))){
					output.Add ("left" + Environment.NewLine); 
					Is_navigation_response_period = false;
					PressButton_Time = exp_time.ElapsedMilliseconds;
					output_time.Add (PressButton_Time.ToString() + Environment.NewLine);

                    ReactionTime = PressButton_Time - Question_onsetTime;
					Invoke ("write_to_text", 0);	
				}
				if( (Input.GetKeyDown (key_one) & (current_cue.Equals(two_one_three) | current_cue.Equals(three_one_two))) |
					(Input.GetKeyDown (key_two) & (current_cue.Equals(one_two_three) | current_cue.Equals(three_two_one))) |
					(Input.GetKeyDown (key_three) & (current_cue.Equals(one_three_two) | current_cue.Equals(two_three_one)))){
					output.Add ("back" + Environment.NewLine); 
					Is_navigation_response_period = false;
					PressButton_Time = exp_time.ElapsedMilliseconds;
					output_time.Add (PressButton_Time.ToString() + Environment.NewLine);

                    ReactionTime = PressButton_Time - Question_onsetTime;
					Invoke ("write_to_text", 0);	
				}
				if( (Input.GetKeyDown (key_one) & (current_cue.Equals(three_two_one) | current_cue.Equals(two_three_one))) |
					(Input.GetKeyDown (key_two) & (current_cue.Equals(one_three_two) | current_cue.Equals(three_one_two))) |
					(Input.GetKeyDown (key_three) & (current_cue.Equals(two_one_three) | current_cue.Equals(one_two_three)))){
					output.Add ("right" + Environment.NewLine); 
					Is_navigation_response_period = false;
					PressButton_Time = exp_time.ElapsedMilliseconds;
					output_time.Add (PressButton_Time.ToString() + Environment.NewLine);

                    ReactionTime = PressButton_Time - Question_onsetTime;
					Invoke ("write_to_text", 0);	
				}
			}
		}


		if (!IsExploring & is_Motion_Detection_response_period) {
			if(Input.GetKeyDown(key_one)){
				is_Motion_Detection_response_period = false;
                PressButton_HeadNod_Time = exp_time.ElapsedMilliseconds;
                output_time.Add(PressButton_HeadNod_Time.ToString() + "\t");

                ReactionTime = PressButton_HeadNod_Time - Question_headNod_onsetTime;
                if (person_who_head_moved.Contains(showed_head_profile.ElementAt(0)))
                {
                    correctness = true;
                    output.Add("Q1_left \t");
                    output.Add("Correct \t");
                }
                if (!person_who_head_moved.Contains(showed_head_profile.ElementAt(0)))
                {
                    correctness = false;
                    output.Add("Q1_left \t");
                    output.Add("Incorrect \t");
                }
                Invoke ("draw_feedback", 0);
			}
			if(Input.GetKeyDown(key_two)){
				is_Motion_Detection_response_period = false;
                PressButton_HeadNod_Time = exp_time.ElapsedMilliseconds;
                output_time.Add(PressButton_HeadNod_Time.ToString() + "\t");

                ReactionTime = PressButton_HeadNod_Time - Question_headNod_onsetTime;
                if (person_who_head_moved.Contains(showed_head_profile.ElementAt(0)))
                {
                    correctness = false;
                    output.Add("Q1_right \t");
                    output.Add("Incorrect \t");
                }
                if (!person_who_head_moved.Contains(showed_head_profile.ElementAt(0)))
                {
                    correctness = true;
                    output.Add("Q1_right \t");
                    output.Add("Correct \t");
                }
                Invoke ("draw_feedback", 0);
			}
		}

		if (isOnBreakScreen & Input.GetKeyDown(KeyCode.E))
        {
            isOnBreakScreen = false;
			showcase_formal ();
        }

        if (isOn_formal_exp_screen)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                isOn_formal_exp_screen = false;
                Screen.SetResolution(1024, 768, true, 60);

                if (run_ith==1)
                {
                    exp_time.Reset();
                    exp_time.Start();
                    pureScreen();
                }
                if (run_ith==2)
                {
                    isOnBreakScreen = false;
                    trial_ith = 0;
                    List<int> temp_list = new List<int>();
                    temp_list = Enumerable.Range(0, reorganized_DataSet.Length).ToList();
                    trial_order = ShuffleList(temp_list);
                    exp_time.Reset();
                    exp_time.Start();
					var aaa = Instantiate(balloon_base, new Vector3(250, -0.98f, 250), Quaternion.Euler(0, 0, 0)) as GameObject;
					aaa.transform.localScale = new Vector3(0.8f, 1, 0.8f);
                    pureScreen();
                }
            }
        }
		if (Input.GetKeyDown(KeyCode.Escape)) {
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
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

        if (Number_of_person_move_head == 1 | Number_of_person_move_head == 2)
        {
            float rand = UnityEngine.Random.value;
            if (rand < 0.5)
            {
                head_profile_image = (Texture2D)Resources.Load(person_who_head_moved.ElementAt(0));
                showed_head_profile.Add(person_who_head_moved.ElementAt(0));
            }
            else
            {
                head_profile_image = (Texture2D)Resources.Load(person_who_head_NOT_moved.ElementAt(0));
                showed_head_profile.Add(person_who_head_NOT_moved.ElementAt(0));
            }
        }
        if (Number_of_person_move_head == 3)
        {
            head_profile_image = (Texture2D)Resources.Load(person_who_head_moved.ElementAt(0));
            showed_head_profile.Add(person_who_head_moved.ElementAt(0));
        }
        if (Number_of_person_move_head == 0)
        {
            head_profile_image = (Texture2D)Resources.Load(person_who_head_NOT_moved.ElementAt(0));
            showed_head_profile.Add(person_who_head_NOT_moved.ElementAt(0));
        }

        Sprite icon = Sprite.Create(head_profile_image, new Rect(0.0f, 0.0f, 256, 256), new Vector2(0.5f, 0.5f));
		image_object.sprite = icon;

		Question_headNod_onsetTime = exp_time.ElapsedMilliseconds;
		output_time.Add (Question_headNod_onsetTime.ToString()+ "\t");

        is_Motion_Detection_response_period = true;
		isOn_Question_screen = true;
	}


	void draw_feedback(){
		if(textObject){
			Destroy (textObject);
        }
        //Destroy(canvasobject);
        Destroy(canvasobject2);
        if (correctness)
        {
            subject_preformance_fee = subject_preformance_fee + 5;
        }
        //output
        output.Add(subject_preformance_fee + " \t");
        output.Add("NA \t"); // facing person
        output.Add("NA \t"); // facing direction
        output.Add("NA \t"); // egocentric location
        output.Add("NA \t"); // target person
        output.Add("NA \t"); // cue type
        output.Add("NA" + Environment.NewLine); // Q2_choice

        output_time.Add("NA \t");
        output_time.Add("NA \t");
        output_time.Add("NA \t");
        output_time.Add("NA \t");
        output_time.Add("NA" + Environment.NewLine);
        Invoke("write_to_text", 0f);
    }

	void write_to_text(){
		isOn_Question_screen = false;
        using (StreamWriter writer = new StreamWriter(output_path, true))
        {
            for (int i = 0; i < output.Count; i++)
            {
                writer.Write(output.ElementAt(i));
            }
            writer.Close();
        }
        using (StreamWriter writer = new StreamWriter(output_Time_path, true))
        {
            for (int i = 0; i < output_time.Count; i++)
            {
                writer.Write(output_time.ElementAt(i));
            }
            writer.Close();
        }
        trial_ith = trial_ith + 1;

        if (trial_ith < reorganized_DataSet.Length)
        {
            Invoke("pureScreen", 0);
        }
        else if (trial_ith == reorganized_DataSet.Length)
        {
            run_ith = run_ith + 1;
            trial_ith = reorganized_DataSet.Length - 1;
            if (run_ith < 3)
            {
                Invoke("break_session", 0);
            }
            else if (run_ith == 3)
            {
                thank_you();
            }
        }

//        if (trial_ith < 2)
//        {
//            Invoke("pureScreen", 0);
//        }
//        else if (trial_ith == 2)
//        {
//            run_ith = run_ith + 1;
//            if (run_ith < 3)
//            {
//                Invoke("break_session", 0);
//            }
//            else if (run_ith == 3)
//            {
//                thank_you();
//            }
//        }
    }

    void noise_after_walking(){
		noise_method ();
		output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t");

        if (noITI){
			Invoke ("reset_subject_facing_direction",0);
		} else{
			Invoke ("reset_subject_facing_direction",2);
		}
	}

	void reset_subject_facing_direction(){
        mark_direction_fmri_item_subject_person set_subject_facing_direction = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person> ();
		set_subject_facing_direction.set_subject_facing_direction ();
		//output
		output.Add (which_person + " \t"); // facing person
		output.Add (facing_direction + " \t"); // facing direction
        mark_direction_fmri_item_subject_person rotate_person = wooden_fence.GetComponent<mark_direction_fmri_item_subject_person> ();
		rotate_person.person_face_to_center ();
		show_facing_person ();
	}

	void show_facing_person(){
		Destroy (canvasobject);
		Destroy (canvas);
		output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t");

        //		Application.CaptureScreenshot("facing.png");

        if (noITI)
        {
            noise_after_facing();
        }
        else
        {
			Invoke("noise_after_facing", 2f);
        }
    }

	void noise_after_facing(){
		noise_method ();
		output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t");
        if (noITI){
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
		mark_direction_fmri_goalTable_personImage TargetImage = wooden_fence.GetComponent<mark_direction_fmri_goalTable_personImage> ();
		returned_person_image = TargetImage.set_person_image ();

		mark_direction_fmri_goalTable_personImage egoL_targetP = wooden_fence.GetComponent<mark_direction_fmri_goalTable_personImage> ();
		output.Add (egoL_targetP.return_egocentric_location() + " \t"); // egocentric location
		output.Add (egoL_targetP.return_target_person() + " \t"); // target person
		Sprite icon = Sprite.Create(returned_person_image, new Rect(0.0f, 0.0f, 512, 512), new Vector2(0.5f, 0.5f));
		image_object.sprite = icon;
        //output
        output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t");

        //		Application.CaptureScreenshot("goal.png");
        if (noITI)
        {
            fixation4();
        }
        else
        {
            Invoke("fixation4", 2f);
        }
    }

	void fixation4(){
		Destroy (canvasobject2);
		output_time.Add (exp_time.ElapsedMilliseconds.ToString()+ "\t");
        if (noITI){
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
		//output
		output.Add(current_cue.name + "\t"); // number of person who move head
		Sprite icon = Sprite.Create(current_cue, new Rect(0.0f, 0.0f, 500, 500), new Vector2(0.5f, 0.5f));
		image_object.sprite = icon;
		image_object.rectTransform.sizeDelta = new Vector2 (500,500);

		Question_onsetTime = exp_time.ElapsedMilliseconds;
		output_time.Add (Question_onsetTime.ToString () + "\t");
        Is_navigation_response_period = true;
		isOn_Question_screen = true;
	}
		

	void break_session(){
		Destroy (canvasobject2);
		isOnBreakScreen = true;
        showText ("休息时间\n\n休息好后持续按‘1’键示意",0,50);
	}
	void thank_you(){
		Destroy (canvasobject);
		showBackgroundScreen ();
        showText ("实验结束\n\n" + "你的被试费是 " + subject_preformance_fee.ToString () + " 元",0,50);
		exp_time.Stop ();

	}

    void showcase_formal(){
		Destroy(canvasobject);
        showBackgroundScreen();
        showText("实验即将开始\n\n", 0, 50);
        isOn_formal_exp_screen = true;
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

        output_path = Application.dataPath + "/sub_" + subject_number + "_formal_rawdata.txt";
        KeyPress_Testing_path = Application.dataPath + "/sub_" + subject_number + "_formal_keyPress_record.txt";
        output_Time_path = Application.dataPath + "/sub_" + subject_number + "_formal_Time_record.txt";

		List<int> temp_list = new List<int> ();
		temp_list = Enumerable.Range (0, reorganized_DataSet.Length).ToList ();
		trial_order = ShuffleList (temp_list);

        // set enter direction pool
		mark_direction_fmri_enter_direction_person create_initial_direction_pool = wooden_fence.GetComponent<mark_direction_fmri_enter_direction_person> ();
		create_initial_direction_pool.create_initial_direction_pool_for_each_map ();

        // create_goal_table
		mark_direction_fmri_goalTable_personImage create_goal_table = wooden_fence.GetComponent<mark_direction_fmri_goalTable_personImage> ();
		create_goal_table.create_goal_table ();

        // load_person_image
		mark_direction_fmri_goalTable_personImage load_person_image = wooden_fence.GetComponent<mark_direction_fmri_goalTable_personImage> ();
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
        init_finish = true;
    }

	void walking_end(){
        if (reorganized_DataSet[trial_order.ElementAt(trial_ith)][4] == "0")
        {
            output.Add(Number_of_person_move_head + "\t"); // number of person who move head
            output.Add("NA \t"); // Q1_choice
            output.Add("NA \t"); // correctness
            output.Add(subject_preformance_fee + " \t"); // sub_fee
			noise_after_walking();
        }
        if (reorganized_DataSet[trial_order.ElementAt(trial_ith)][4] == "1")
        {
            output.Add(Number_of_person_move_head + "\t"); // number of person who move head
            show_head_profile();
        }
    }

	void noise_method(){
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


    void calculate_initial_position()
    {
        float circle_x;
        float circle_z;
        int circle_radius = 20;
        for (float degree_of_angle = 0f; degree_of_angle < 360; degree_of_angle += 22.5f)
        {
            circle_x = 250f + Mathf.Cos((degree_of_angle * Mathf.PI) / 180f) * circle_radius;
            circle_z = 250f + Mathf.Sin((degree_of_angle * Mathf.PI) / 180f) * circle_radius;
            Instantiate(balloon_base, new Vector3(circle_x, -0.8f, circle_z), Quaternion.Euler(0, fence_rotation, 0));
        }
    }
		
}

