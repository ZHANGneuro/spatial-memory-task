using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;


public class task_md_goalTable_personImage : MonoBehaviour {

	List<string> map123_b1;
	List<string> map123_b2;
	List<string> map123_b3;

	List<string> map132_b1;
	List<string> map132_b2;
	List<string> map132_b3;

	List<string> map124_b1;
	List<string> map124_b2;
	List<string> map124_b3;

	List<string> map142_b1;
	List<string> map142_b2;
	List<string> map142_b3;

	List<string> map134_b1;
	List<string> map134_b2;
	List<string> map134_b3;

	List<string> map143_b1;
	List<string> map143_b2;
	List<string> map143_b3;


	Texture2D person_image_1;
	Texture2D person_image_2;
	Texture2D person_image_3;
	string egocentric_location;
	string target_person;

		
	public void create_goal_table(){
		map123_b1 = new List<string>(){"left","left","left","left","left","left","left","back","back","back","back","back","back","back"};
		map123_b2 = new List<string>(){"left","left","left","left","left","left","left","right","right","right","right","right","right","right"};
		map123_b3 = new List<string>(){"right","right","right","right","right","right","right","back","back","back","back","back","back","back"};

		map132_b1 = new List<string>(){"left","left","left","left","left","left","left","back","back","back","back","back","back","back"};
		map132_b2 = new List<string>(){"right","right","right","right","right","right","right","back","back","back","back","back","back","back"};
		map132_b3 = new List<string>(){"left","left","left","left","left","left","left","right","right","right","right","right","right","right"};

		map124_b1 = new List<string>(){"left","left","left","left","left","left","left","right","right","right","right","right","right","right"};
		map124_b2 = new List<string>(){"right","right","right","right","right","right","right","back","back","back","back","back","back","back"};
		map124_b3 = new List<string>(){"left","left","left","left","left","left","left","back","back","back","back","back","back","back"};

		map142_b1 = new List<string>(){"left","left","left","left","left","left","left","right","right","right","right","right","right","right"};
		map142_b2 = new List<string>(){"left","left","left","left","left","left","left","back","back","back","back","back","back","back"};
		map142_b3 = new List<string>(){"right","right","right","right","right","right","right","back","back","back","back","back","back","back"};

		map134_b1 = new List<string>(){"right","right","right","right","right","right","right","back","back","back","back","back","back","back"};
		map134_b2 = new List<string>(){"left","left","left","left","left","left","left","back","back","back","back","back","back","back"};
		map134_b3 = new List<string>(){"left","left","left","left","left","left","left","right","right","right","right","right","right","right"};

		map143_b1 = new List<string>(){"right","right","right","right","right","right","right","back","back","back","back","back","back","back"};
		map143_b2 = new List<string>(){"left","left","left","left","left","left","left","right","right","right","right","right","right","right"};
		map143_b3 = new List<string>(){"left","left","left","left","left","left","left","back","back","back","back","back","back","back"};

		map123_b1 = ShuffleList (map123_b1);
		map123_b2 = ShuffleList (map123_b2);
		map123_b3 = ShuffleList (map123_b3);

		map132_b1 = ShuffleList (map132_b1);
		map132_b2 = ShuffleList (map132_b2);
		map132_b3 = ShuffleList (map132_b3);

		map124_b1 = ShuffleList (map124_b1);
		map124_b2 = ShuffleList (map124_b2);
		map124_b3 = ShuffleList (map124_b3);

		map142_b1 = ShuffleList (map142_b1);
		map142_b2 = ShuffleList (map142_b2);
		map142_b3 = ShuffleList (map142_b3);

		map134_b1 = ShuffleList (map134_b1);
		map134_b2 = ShuffleList (map134_b2);
		map134_b3 = ShuffleList (map134_b3);

		map143_b1 = ShuffleList (map143_b1);
		map143_b2 = ShuffleList (map143_b2);
		map143_b3 = ShuffleList (map143_b3);
	}


	public void load_person_image(){
			person_image_1 = (Texture2D) Resources.Load("cartoon_person_1");  
			person_image_2 = (Texture2D) Resources.Load("cartoon_person_2");  
			person_image_3 = (Texture2D) Resources.Load("cartoon_person_3");  
	}
		

	public Texture2D set_person_image(){
		int group = int.Parse(task_md.which_subject_group);

		//map123
		if ((group == 1 & string.Equals (task_md.which_map, "1"))|
			(group == 2 & string.Equals (task_md.which_map, "1"))|
			(group == 3 & string.Equals (task_md.which_map, "1"))){
			if(string.Equals (task_md.which_person, "1")){
				string option = " ";
                option = map123_b1.ElementAt(0);
                map123_b1.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "2";
					return person_image_2;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "3";
					return person_image_3;
				}
			}
			if(string.Equals (task_md.which_person, "2")){
				string option = " ";;
                option = map123_b2.ElementAt(0);
                map123_b2.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "3";
					return person_image_3;
				}
				if(string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "1";
					return person_image_1;
				}
			}
			if(string.Equals (task_md.which_person, "3")){
				string option = " ";;
                option = map123_b3.ElementAt(0);
                map123_b3.RemoveAt(0);
                if (string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "2";
					return person_image_2;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "1";
					return person_image_1;
				}
			}
		}
		//map132
		if ((group == 1 & string.Equals (task_md.which_map, "2"))|
			(group == 2 & string.Equals (task_md.which_map, "2"))|
			(group == 4 & string.Equals (task_md.which_map, "1"))){
			if(string.Equals (task_md.which_person, "1")){
				string option = " ";;
                option = map132_b1.ElementAt(0);
                map132_b1.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "3";
					return person_image_3;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "2";
					return person_image_2;
				}
			}
			if(string.Equals (task_md.which_person, "2")){
				string option = " ";;
                option = map132_b2.ElementAt(0);
                map132_b2.RemoveAt(0);
                if (string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "3";
					return person_image_3;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "1";
					return person_image_1;
				}
			}
			if(string.Equals (task_md.which_person, "3")){
				string option = " ";;
                option = map132_b3.ElementAt(0);
                map132_b3.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "2";
					return person_image_2;
				}
				if(string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "1";
					return person_image_1;
				}
			}
		}
		//map142
		if ((group == 1 & string.Equals (task_md.which_map, "3"))|
			(group == 3 & string.Equals (task_md.which_map, "2"))|
			(group == 5 & string.Equals (task_md.which_map, "1"))){
			if(string.Equals (task_md.which_person, "1")){
				string option = " ";;
                option = map142_b1.ElementAt(0);
                map142_b1.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "3";
					return person_image_3;
				}
				if(string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "2";
					return person_image_2;
				}
			}
			if(string.Equals (task_md.which_person, "2")){
				string option = " ";;
                option = map142_b2.ElementAt(0);
                map142_b2.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "1";
					return person_image_1;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "3";
					return person_image_3;
				}
			}
			if(string.Equals (task_md.which_person, "3")){
				string option = " ";;
                option = map142_b3.ElementAt(0);
                map142_b3.RemoveAt(0);
                if (string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "1";
					return person_image_1;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "2";
					return person_image_2;
				}
			}
		}
		//map124
		if ((group == 2 & string.Equals (task_md.which_map, "3"))|
			(group == 4 & string.Equals (task_md.which_map, "2"))|
			(group == 6 & string.Equals (task_md.which_map, "1"))){
			if(string.Equals (task_md.which_person, "1")){
				string option = " ";;
                option = map124_b1.ElementAt(0);
                map124_b1.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "2";
					return person_image_2;
				}
				if(string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "3";
					return person_image_3;
				}
			}
			if(string.Equals (task_md.which_person, "2")){
				string option = " ";;
                option = map124_b2.ElementAt(0);
                map124_b2.RemoveAt(0);
                if (string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "1";
					return person_image_1;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "3";
					return person_image_3;
				}
			}
			if(string.Equals (task_md.which_person, "3")){
				string option = " ";;
                option = map124_b3.ElementAt(0);
                map124_b3.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "1";
					return person_image_1;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "2";
					return person_image_2;
				}
			}
		}
		//map134
		if ((group == 3 & string.Equals (task_md.which_map, "3"))|
			(group == 5 & string.Equals (task_md.which_map, "2"))|
			(group == 6 & string.Equals (task_md.which_map, "2"))){
			if(string.Equals (task_md.which_person, "1")){
				string option = " ";;
                option = map134_b1.ElementAt(0);
                map134_b1.RemoveAt(0);
                if (string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "3";
					return person_image_3;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "2";
					return person_image_2;
				}
			}
			if(string.Equals (task_md.which_person, "2")){
				string option = " ";;
                option = map134_b2.ElementAt(0);
                map134_b2.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "3";
					return person_image_3;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "1";
					return person_image_1;
				}
			}
			if(string.Equals (task_md.which_person, "3")){
				string option = " ";;
                option = map134_b3.ElementAt(0);
                map134_b3.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "1";
					return person_image_1;
				}
				if(string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "2";
					return person_image_2;
				}
			}
		}
		//map143
		if ((group == 4 & string.Equals (task_md.which_map, "3"))|
			(group == 5 & string.Equals (task_md.which_map, "3"))|
			(group == 6 & string.Equals (task_md.which_map, "3"))){
			if(string.Equals (task_md.which_person, "1")){
				string option = " ";;
                option = map143_b1.ElementAt(0);
                map143_b1.RemoveAt(0);
                if (string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "2";
					return person_image_2;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "3";
					return person_image_3;
				}
			}
			if(string.Equals (task_md.which_person, "2")){
				string option = " ";;
                option = map143_b2.ElementAt(0);
                map143_b2.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "1";
					return person_image_1;
				}
				if(string.Equals (option, "right")){
					egocentric_location = "right";
					target_person = "3";
					return person_image_3;
				}
			}
			if(string.Equals (task_md.which_person, "3")){
				string option = " ";;
                option = map143_b3.ElementAt(0);
                map143_b3.RemoveAt(0);
                if (string.Equals (option, "left")){
					egocentric_location = "left";
					target_person = "2";
					return person_image_2;
				}
				if(string.Equals (option, "back")){
					egocentric_location = "back";
					target_person = "1";
					return person_image_1;
				}
			}
		}
		return null;
	}


	public string return_egocentric_location (){
		return  egocentric_location;
	}
	public string return_target_person (){
		return  target_person;
	}

	List<E> ShuffleList<E>(List<E> inputList){
		List<E> randomList = new List<E>();
		System.Random r = new System.Random();
		int randomIndex = 0;
		while (inputList.Count > 0)
		{
			randomIndex = r.Next(0, inputList.Count); //Choose a random object in the list
			randomList.Add(inputList[randomIndex]); //add it to the new, random list
			inputList.RemoveAt(randomIndex); //remove to avoid duplicates
		}
		return randomList; //return the new random list
	}


}
