using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class practice_md_create_trial_table_person : MonoBehaviour {

	public void create_trial_table(){

        // dataset for training, training trials, read table: col 1.hm, 2.map, 3.direction, 4.correctness, 5.number of person with head movement
        TextAsset text_asset = (TextAsset)Resources.Load("dataset_training");
        List<string> DataSet = TextAssetToList(text_asset);

        string[][] reorganized_DataSet = new String[DataSet.Count][];
        for (int i = 0; i < DataSet.Count; i++)
        {
            string[] entries = DataSet[i].Split('\t');
            for (int j = 0; j < entries.Length; j++)
            {
                reorganized_DataSet[i] = entries;
            }
        }
        for (int i = 0; i < DataSet.Count; i++)
        {
            if (string.Equals(reorganized_DataSet[i][0], "0"))
            {
                reorganized_DataSet[i][4] = "0";
            }
            if (string.Equals(reorganized_DataSet[i][0], "1") &
                string.Equals(reorganized_DataSet[i][3], "1"))
            {
                float rand = UnityEngine.Random.value;
                if (rand < 0.778f)
                {
                    reorganized_DataSet[i][4] = "1";
                }
                if (rand >= 0.778f & rand < 0.982f)
                {
                    reorganized_DataSet[i][4] = "2";
                }
                if (rand >= 0.982f & rand <= 1)
                {
                    reorganized_DataSet[i][4] = "3";
                }
            }
            if (string.Equals(reorganized_DataSet[i][0], "1") &
                string.Equals(reorganized_DataSet[i][3], "0"))
            {
                float rand = UnityEngine.Random.value;
                if (rand < 0.778f)
                {
                    reorganized_DataSet[i][4] = "1";
                }
                if (rand >= 0.778f & rand < 1)
                {
                    reorganized_DataSet[i][4] = "2";
                }
            }
        }

        // practice stage 1 with head-nod
        practice_md.practice_training_stage1_DataSet = new String[4][];
        string[] stage1_trial1 = new String[5];
        stage1_trial1[0] = "1";
        stage1_trial1[1] = "1";
        stage1_trial1[2] = "1";
        stage1_trial1[3] = "1";
        stage1_trial1[4] = "1";
        practice_md.practice_training_stage1_DataSet[0] = stage1_trial1;

        string[] stage1_trial2 = new String[5];
        stage1_trial2[0] = "1";
        stage1_trial2[1] = "2";
        stage1_trial2[2] = "2";
        stage1_trial2[3] = "0";
        stage1_trial2[4] = "2";
        practice_md.practice_training_stage1_DataSet[1] = stage1_trial2;

        string[] stage1_trial3 = new String[5];
        stage1_trial3[0] = "1";
        stage1_trial3[1] = "3";
        stage1_trial3[2] = "3";
        stage1_trial3[3] = "1";
        stage1_trial3[4] = "3";
        practice_md.practice_training_stage1_DataSet[2] = stage1_trial3;

        string[] stage1_trial4 = new String[5];
        stage1_trial4[0] = "1";
        stage1_trial4[1] = "2";
        stage1_trial4[2] = "5";
        stage1_trial4[3] = "0";
        stage1_trial4[4] = "1";
        practice_md.practice_training_stage1_DataSet[3] = stage1_trial4;

        // practice stage 2 without head-nod
        practice_md.practice_training_stage2_DataSet = new String[4][];
        string[] stage2_trial1 = new String[5];
        stage2_trial1[0] = "0";
        stage2_trial1[1] = "3";
        stage2_trial1[2] = "6";
        stage2_trial1[3] = "0";
        stage2_trial1[4] = "0";
        practice_md.practice_training_stage2_DataSet[0] = stage2_trial1;

        string[] stage2_trial2 = new String[5];
        stage2_trial2[0] = "0";
        stage2_trial2[1] = "1";
        stage2_trial2[2] = "1";
        stage2_trial2[3] = "0";
        stage2_trial2[4] = "0";
        practice_md.practice_training_stage2_DataSet[1] = stage2_trial2;

        string[] stage2_trial3 = new String[5];
        stage2_trial3[0] = "0";
        stage2_trial3[1] = "2";
        stage2_trial3[2] = "4";
        stage2_trial3[3] = "0";
        stage2_trial3[4] = "0";
        practice_md.practice_training_stage2_DataSet[2] = stage2_trial3;

        string[] stage2_trial4 = new String[5];
        stage2_trial4[0] = "0";
        stage2_trial4[1] = "1";
        stage2_trial4[2] = "7";
        stage2_trial4[3] = "0";
        stage2_trial4[4] = "0";
        practice_md.practice_training_stage2_DataSet[3] = stage2_trial4;

        // practice stage 3 for training
        practice_md.practice_training_stage3_DataSet = new String[10][];
        List<int> Shuffle_pool3 = new List<int>();
        Shuffle_pool3 = Enumerable.Range(0, DataSet.Count).ToList();
        Shuffle_pool3 = ShuffleList(Shuffle_pool3);
        for (int i = 0; i < 10; i++)
        {
            string[] newA = new String[5];
            string[] oldA = reorganized_DataSet[Shuffle_pool3.ElementAt(i)];
            float rand = UnityEngine.Random.value;
            if (rand <= 0.6f)
            {
                newA[0] = "0";
                newA[3] = "0";
                newA[4] = "0";
            }
            if (rand > 0.6f)
            {
                newA[0] = "1";
                float rand_np = UnityEngine.Random.value;
                if (rand_np < 0.778f)
                {
                    newA[4] = "1";
                    float rand_cr = UnityEngine.Random.value;
                    if (rand_cr <= 0.5f)
                    {
                        newA[3] = "0";
                    }
                    if (rand_cr > 0.5f)
                    {
                        newA[3] = "1";
                    }
                }
                if (rand_np >= 0.778f & rand_np < 0.982f)
                {
                    newA[4] = "2";
                    float rand_cr = UnityEngine.Random.value;
                    if (rand_cr <= 0.5f)
                    {
                        newA[3] = "0";
                    }
                    if (rand_cr > 0.5f)
                    {
                        newA[3] = "1";
                    }
                }
                if (rand_np >= 0.982f & rand_np <= 1)
                {
                    newA[4] = "3";
                    newA[3] = "1";
                }
            }
            newA[1] = oldA[1];
            newA[2] = oldA[2];
            practice_md.practice_training_stage3_DataSet[i] = newA;
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
