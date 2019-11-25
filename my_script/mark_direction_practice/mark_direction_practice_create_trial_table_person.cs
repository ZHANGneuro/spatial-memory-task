using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class mark_direction_practice_create_trial_table_person : MonoBehaviour {

	public void create_trial_table(){

		TextAsset text_asset = null;
		if(string.Equals(mark_direction_practice.which_type, "7d")){
			text_asset = (TextAsset)Resources.Load ("7d");
		}
		if(string.Equals(mark_direction_practice.which_type, "6d")){
			text_asset = (TextAsset)Resources.Load ("6d");
		}

        List<string> DataSet = TextAssetToList(text_asset);
        int number_of_lure_trials = 7;
        int number_of_trials = DataSet.Count + number_of_lure_trials;
        string[][] reorganized_DataSet = new String[number_of_trials][];

        for (int i = 0; i < DataSet.Count; i++)
        {
            string[] entries = DataSet[i].Split('\t');
            for (int j = 0; j < entries.Length; j++)
            {
                reorganized_DataSet[i] = entries;
            }
        }

        // add lure trials, 1 represents head-nod trial
        List<int> lure_trials = new List<int>();
        lure_trials = Enumerable.Range(0, DataSet.Count).ToList();
        lure_trials = ShuffleList(lure_trials);

        for (int i = 0; i < number_of_lure_trials; i++)
        {
            string[] newA = new String[5];
            string[] oldA = reorganized_DataSet[lure_trials.ElementAt(i)];
            newA[0] = oldA[0];
            newA[1] = oldA[1];
            newA[2] = oldA[2];
            float rand = UnityEngine.Random.value;
            if (rand < 0.5f)
            {
                newA[3] = "0";
            }
            if (rand >= 0.5f & rand < 0.889f)
            {
                newA[3] = "1";
            }
            if (rand >= 0.889f & rand < 0.991f)
            {
                newA[3] = "2";
            }
            if (rand >= 0.991f & rand <= 1)
            {
                newA[3] = "3";
            }
            newA[4] = "1";
            reorganized_DataSet[DataSet.Count + i] = newA;
        }

        // practice stage 1
        mark_direction_practice.practice_formal_stage1_DataSet = new String[4][];
        string[] stage1_trial1 = new String[6];
        stage1_trial1[0] = "3";
        stage1_trial1[1] = "2";
        stage1_trial1[2] = "1"; //facing person
        stage1_trial1[3] = "2"; //number of head-nod
        stage1_trial1[4] = "1"; //lure
        stage1_trial1[5] = "0"; //correctness
        mark_direction_practice.practice_formal_stage1_DataSet[0] = stage1_trial1;

        string[] stage1_trial2 = new String[6];
        stage1_trial2[0] = "1";
        stage1_trial2[1] = "5";
        stage1_trial2[2] = "3"; //facing person
        stage1_trial2[3] = "0"; //number of head-nod
        stage1_trial2[4] = "1"; //lure
        stage1_trial2[5] = "0"; //correctness
        mark_direction_practice.practice_formal_stage1_DataSet[1] = stage1_trial2;

        string[] stage1_trial3 = new String[6];
        stage1_trial3[0] = "3";
        stage1_trial3[1] = "4";
        stage1_trial3[2] = "2"; //facing person
        stage1_trial3[3] = "0"; //number of head-nod
        stage1_trial3[4] = "1"; //lure
        stage1_trial3[5] = "0"; //correctness
        mark_direction_practice.practice_formal_stage1_DataSet[2] = stage1_trial3;

        string[] stage1_trial4 = new String[6];
        stage1_trial4[0] = "2";
		if(string.Equals(mark_direction_practice.which_type, "7d")){
			stage1_trial4[1] = "7";
		}
		if(string.Equals(mark_direction_practice.which_type, "6d")){
			stage1_trial4[1] = "6";
		}
        stage1_trial4[2] = "1"; //facing person
        stage1_trial4[3] = "3"; //number of head-nod
        stage1_trial4[4] = "1"; //lure
        stage1_trial4[5] = "1"; //correctness
        mark_direction_practice.practice_formal_stage1_DataSet[3] = stage1_trial4;

        // practice stage 2
        TextAsset formal_practie2_asset = (TextAsset)Resources.Load("dataset_formal_practice_stage2");
        List<string> formal_practie2_List = TextAssetToList(formal_practie2_asset);

        mark_direction_practice.practice_formal_stage2_DataSet = new String[formal_practie2_List.Count][];
        for (int i = 0; i < formal_practie2_List.Count; i++)
        {
            string[] entries = formal_practie2_List[i].Split('\t');
            for (int j = 0; j < entries.Length; j++)
            {
                mark_direction_practice.practice_formal_stage2_DataSet[i] = entries;
            }
        }

        // practice stage 3 for formal
		TextAsset formal_practie3_asset = null;
		if(string.Equals(mark_direction_practice.which_type, "7d")){
			 formal_practie3_asset = (TextAsset)Resources.Load("dataset_formal_practice_stage3");
		}
		if(string.Equals(mark_direction_practice.which_type, "6d")){
			 formal_practie3_asset = (TextAsset)Resources.Load("dataset_formal_practice_stage3_6d");
		}

        List<string> formal_practie3_List = TextAssetToList(formal_practie3_asset);

        mark_direction_practice.practice_formal_stage3_DataSet = new String[formal_practie3_List.Count][];
        for (int i = 0; i < formal_practie3_List.Count; i++)
        {
            string[] entries = formal_practie3_List[i].Split('\t');
            for (int j = 0; j < entries.Length; j++)
            {
                mark_direction_practice.practice_formal_stage3_DataSet[i] = entries;
            }
        }
    }


	private List<string> TextAssetToList(TextAsset ta) {
		return new List<string>(ta.text.Split('\n'));
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
