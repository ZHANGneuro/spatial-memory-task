using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class toolbox : MonoBehaviour {





//
//	void calculate_initial_position(){
//		float circle_x;
//		float circle_z;
//		int circle_radius = 15;
//		for (float degree_of_angle = 0f; degree_of_angle <360; degree_of_angle+=22.5f) {
//			circle_x = 250f + Mathf.Cos((degree_of_angle * Mathf.PI)/180f) * circle_radius;
//			circle_z = 250f + Mathf.Sin((degree_of_angle * Mathf.PI)/180f) * circle_radius;
//			Instantiate (balloon_base, new Vector3 (circle_x, -1, circle_z), Quaternion.Euler (0, fence_rotation, 0));
//		}
//	}
//
	//	void highlight_initial_position(){
//	float circle_x;
//	float circle_z;
//	int circle_radius = 20;
//	for (float degree_of_angle = 0f; degree_of_angle <360; degree_of_angle+=22.5f) {
//		circle_x = 250f + Mathf.Cos((degree_of_angle * Mathf.PI)/180f) * circle_radius;
//		circle_z = 250f + Mathf.Sin((degree_of_angle * Mathf.PI)/180f) * circle_radius;
//		Instantiate (balloon_base, new Vector3 (circle_x, -1, circle_z), Quaternion.Euler (0, fence_rotation, 0));
//	}
//}
//
//
//
//	void create_scrambled(int file_name){
//		int width = 0;
//		int height = 0;
//
//		facing_balloon_example_image = File.ReadAllBytes ("Assets/Resources/scambeled_image_fence.png");
//		width = 1024;
//		height = 768;
//
//		Texture2D tex = new Texture2D(width, height,TextureFormat.RGB24,false);
//		tex.LoadImage (facing_balloon_example_image);
//		List<Color[]> color_list = new List<Color[]>();
//		int square_size = 4;
//		int number_width = width / square_size; 
//		int number_height = height / square_size; 
//		for(int i=0; i<number_width;i++){
//			for(int j=0; j<number_height;j++){
//				Color[] each_square = tex.GetPixels(i*square_size, j*square_size, square_size, square_size);
//				color_list.Add (each_square);
//			}
//		}
//		color_list = ShuffleList (color_list);
//		for(int i=0; i<number_width;i++){
//			for(int j=0; j<number_height;j++){
//				tex.SetPixels(i*square_size, j*square_size, square_size, square_size,color_list.ElementAt(0));
//				color_list.RemoveAt (0);
//				tex.Apply();
//			}
//		}
//		byte[] bytes = tex.EncodeToPNG();
//		Destroy(tex);
//		var path = "Assets/Resources/noise_image2/" + file_name.ToString () + ".png";
//		File.WriteAllBytes(path, bytes);
//	}


//	void compute_rotation_between_enterDirection_and_facingDirection(){
//		script_enter_direction_person init_pool = wooden_fence.GetComponent<script_enter_direction_person>();
//		init_pool.create_initial_direction_pool_for_each_map ();
//
//		Vector3 person_location_1 = new Vector3(245+howClosePersonTocenter,0.02f,255-howClosePersonTocenter);
//		Vector3 person_location_2 = new Vector3(245+howClosePersonTocenter,0.02f,245+howClosePersonTocenter);
//		Vector3 person_location_3 = new Vector3(255-howClosePersonTocenter,0.02f,245+howClosePersonTocenter);
//		Vector3 person_location_4 = new Vector3(255-howClosePersonTocenter,0.02f,255-howClosePersonTocenter);
//		Vector3 facing_direction_1 = (person_location_1-subject_response_position).normalized;
//		Vector3 facing_direction_2 = (person_location_2-subject_response_position).normalized;
//		Vector3 facing_direction_3 = (person_location_3-subject_response_position).normalized;
//		Vector3 facing_direction_4 = (person_location_4-subject_response_position).normalized;
//		List<Vector3> facingDirection = new List<Vector3> ();
//		facingDirection.Add (facing_direction_1);
//		facingDirection.Add (facing_direction_2);
//		facingDirection.Add (facing_direction_3);
//		facingDirection.Add (facing_direction_4);
//
//		which_map = "3";
//		script_enter_direction_person get_direction_pool = wooden_fence.GetComponent<script_enter_direction_person>();
//		List<Vector3> direction_list = get_direction_pool.setup_enter_direction_pool ();
//
//		for(int i=1; i<8; i++){
//			for(int j=1; j<5; j++){
//				Debug.Log ("map3 d_" + i.ToString() + " "+ "facing_" + j.ToString() + " " +  Vector3.Angle( (subject_response_position - direction_list.ElementAt(i-1)).normalized ,    facingDirection.ElementAt(j-1))  );
//			}
//		}
//	}


}
