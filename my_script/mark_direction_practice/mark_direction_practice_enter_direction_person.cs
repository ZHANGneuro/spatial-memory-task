
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class mark_direction_practice_enter_direction_person : MonoBehaviour
{



	//    Vector3 direction_0;
	Vector3 direction_22_5;
	Vector3 direction_45;
	Vector3 direction_67_5;
	//    Vector3 direction_90;
	Vector3 direction_112_5;
	Vector3 direction_135;
	Vector3 direction_157_5;
	//    Vector3 direction_180;
	Vector3 direction_202_5;
	Vector3 direction_225;
	Vector3 direction_247_5;
	//    Vector3 direction_270;
	Vector3 direction_292_5;
	Vector3 direction_315;
	Vector3 direction_337_5;

	string which_balloon;
	List<Vector3> map123;
	List<Vector3> map124;
	List<Vector3> map134;

	public void create_initial_direction_pool_for_each_map()
	{

		float positionY = 1f;
		//		direction_0 = new Vector3(250f, positionY, 270f);
		direction_22_5 = new Vector3(242.3463f, positionY, 268.4776f);
		//direction_45 = new Vector3(235.8579f, positionY, 264.1421f);
		direction_67_5 = new Vector3(231.5224f, positionY, 257.6537f);
		//		direction_90 = new Vector3(230f, positionY, 250f);
		direction_112_5 = new Vector3(231.5224f, positionY, 242.3463f);
		direction_135 = new Vector3(235.8579f, positionY, 235.8579f);
		direction_157_5 = new Vector3(242.3463f, positionY, 231.5224f);
		//		direction_180 = new Vector3(250f, positionY, 230f);
		direction_202_5 = new Vector3(257.6537f, positionY, 231.5224f);
		direction_225 = new Vector3(264.1422f, positionY, 235.8579f);
		direction_247_5 = new Vector3(268.4776f, positionY, 242.3463f);
		//		direction_270 = new Vector3(270f, positionY, 250f);
		direction_292_5 = new Vector3(268.4776f, positionY, 257.6537f);
		direction_315 = new Vector3(264.1421f, positionY, 264.1421f);
		direction_337_5 = new Vector3(257.6537f, positionY, 268.4776f);

		map123 = new List<Vector3>();
		map124 = new List<Vector3>();
		map134 = new List<Vector3>();

		if(string.Equals(mark_direction_practice.which_type, "7d")){
			map123.Add(direction_315);
			map123.Add(direction_22_5);
			map123.Add(direction_67_5);
			map123.Add(direction_112_5);
			map123.Add(direction_157_5);
			map123.Add(direction_202_5);
			map123.Add(direction_247_5);

			map124.Add(direction_225);
			map124.Add(direction_292_5);
			map124.Add(direction_337_5);
			map124.Add(direction_22_5);
			map124.Add(direction_67_5);
			map124.Add(direction_112_5);
			map124.Add(direction_157_5);

			map134.Add(direction_135);
			map134.Add(direction_202_5);
			map134.Add(direction_247_5);
			map134.Add(direction_292_5);
			map134.Add(direction_337_5);
			map134.Add(direction_22_5);
			map134.Add(direction_67_5);
		}
		if(string.Equals(mark_direction_practice.which_type, "6d")){
			map123.Add(direction_337_5);
			map123.Add(direction_67_5);
			map123.Add(direction_112_5);
			map123.Add(direction_157_5);
			map123.Add(direction_202_5);
			map123.Add(direction_292_5);

			map124.Add(direction_247_5);
			map124.Add(direction_337_5);
			map124.Add(direction_22_5);
			map124.Add(direction_67_5);
			map124.Add(direction_112_5);
			map124.Add(direction_202_5);

			map134.Add(direction_157_5);
			map134.Add(direction_247_5);
			map134.Add(direction_292_5);
			map134.Add(direction_337_5);
			map134.Add(direction_22_5);
			map134.Add(direction_112_5);
		}
			
	}



	public List<Vector3> setup_enter_direction_pool()
	{
		List<Vector3> initial_direction_pool = new List<Vector3>();
		if ((string.Equals(mark_direction_practice.which_subject_group, "1") & string.Equals(mark_direction_practice.which_map, "1")) |
			(string.Equals(mark_direction_practice.which_subject_group, "1") & string.Equals(mark_direction_practice.which_map, "2")) |
			(string.Equals(mark_direction_practice.which_subject_group, "2") & string.Equals(mark_direction_practice.which_map, "1")) |
			(string.Equals(mark_direction_practice.which_subject_group, "2") & string.Equals(mark_direction_practice.which_map, "2")) |
			(string.Equals(mark_direction_practice.which_subject_group, "3") & string.Equals(mark_direction_practice.which_map, "1")) |
			(string.Equals(mark_direction_practice.which_subject_group, "4") & string.Equals(mark_direction_practice.which_map, "1")))
		{
			initial_direction_pool = map123;
		}
		if ((string.Equals(mark_direction_practice.which_subject_group, "1") & string.Equals(mark_direction_practice.which_map, "3")) |
			(string.Equals(mark_direction_practice.which_subject_group, "2") & string.Equals(mark_direction_practice.which_map, "3")) |
			(string.Equals(mark_direction_practice.which_subject_group, "3") & string.Equals(mark_direction_practice.which_map, "2")) |
			(string.Equals(mark_direction_practice.which_subject_group, "4") & string.Equals(mark_direction_practice.which_map, "2")) |
			(string.Equals(mark_direction_practice.which_subject_group, "5") & string.Equals(mark_direction_practice.which_map, "1")) |
			(string.Equals(mark_direction_practice.which_subject_group, "6") & string.Equals(mark_direction_practice.which_map, "1")))
		{
			initial_direction_pool = map124;
		}
		if ((string.Equals(mark_direction_practice.which_subject_group, "3") & string.Equals(mark_direction_practice.which_map, "3")) |
			(string.Equals(mark_direction_practice.which_subject_group, "4") & string.Equals(mark_direction_practice.which_map, "3")) |
			(string.Equals(mark_direction_practice.which_subject_group, "5") & string.Equals(mark_direction_practice.which_map, "2")) |
			(string.Equals(mark_direction_practice.which_subject_group, "5") & string.Equals(mark_direction_practice.which_map, "3")) |
			(string.Equals(mark_direction_practice.which_subject_group, "6") & string.Equals(mark_direction_practice.which_map, "2")) |
			(string.Equals(mark_direction_practice.which_subject_group, "6") & string.Equals(mark_direction_practice.which_map, "3")))
		{
			initial_direction_pool = map134;
		}
		return initial_direction_pool;
	}
}
