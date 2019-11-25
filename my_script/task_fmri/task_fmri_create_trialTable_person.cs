using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class task_fmri_create_trialTable_person : MonoBehaviour {

	public void create_trial_table(){

        TextAsset text_asset = (TextAsset)Resources.Load("dataset");
        List<string> DataSet = TextAssetToList(text_asset);
        int number_of_lure_trials = 17;
        int number_of_trials = DataSet.Count + number_of_lure_trials;
        task_fmri.reorganized_DataSet = new String[number_of_trials][];

        for (int i = 0; i < DataSet.Count; i++)
        {
            string[] entries = DataSet[i].Split('\t');
            for (int j = 0; j < entries.Length; j++)
            {
                task_fmri.reorganized_DataSet[i] = entries;
            }
        }

        // add lure trials, 1 represents head-nod trial
        List<int> lure_trials = new List<int>();
        lure_trials = Enumerable.Range(0, DataSet.Count).ToList();
        lure_trials = ShuffleList(lure_trials);

        for (int i = 0; i < number_of_lure_trials; i++)
        {
            string[] newA = new String[5];
            string[] oldA = task_fmri.reorganized_DataSet[lure_trials.ElementAt(i)];
            newA[0] = oldA[0];
            newA[1] = oldA[1];
            newA[2] = oldA[2];
            float rand = UnityEngine.Random.value;
            if (rand < 0.5f)  //if (rand < 0.5f)
            {
                newA[3] = "0";
            }
            if (rand >= 0.5f & rand < 0.889f) //if (rand >= 0.5f & rand < 0.889f)
            {
                newA[3] = "1";
            }
            if (rand >= 0.889f & rand < 0.991f) //if (rand >= 0.889f & rand < 0.991f)
            {
                newA[3] = "2";
            }
            if (rand >= 0.991f & rand <= 1) //if (rand >= 0.991f & rand <= 1)
            {
                newA[3] = "3";
            }
            newA[4] = "1";
            task_fmri.reorganized_DataSet[DataSet.Count + i] = newA;
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
