using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine.UI; 

public class UpdateFacilitiesValues : MonoBehaviour {

    /* NOTICE ME SENPAI !!!!! */
    /* MAKE SURE YOU TAG ALL OF THE TEXT !!!!!!!! */
    /* NOTICE ME SENPAI !!!!! */

    FacilityType facility;

	void Start () 
	{
        Text observatory_cap = GameObject.FindGameObjectWithTag("observatory_cap").GetComponent<Text>();
        Text agriculture_cap = GameObject.FindGameObjectWithTag("agriculture_cap").GetComponent<Text>();
        Text water_cap = GameObject.FindGameObjectWithTag("water_cap").GetComponent<Text>();
        Text mining_cap = GameObject.FindGameObjectWithTag("mining_cap").GetComponent<Text>();
        Text training_cap = GameObject.FindGameObjectWithTag("training_cap").GetComponent<Text>();
        Text launchpad_cap = GameObject.FindGameObjectWithTag("launchpad_cap").GetComponent<Text>();
        Text powerplant_cap = GameObject.FindGameObjectWithTag("powerplant_cap").GetComponent<Text>();
        Text living_cap = GameObject.FindGameObjectWithTag("living_cap").GetComponent<Text>();

        Text observatory_health = GameObject.FindGameObjectWithTag("observatory_health").GetComponent<Text>();
        Text agriculture_health = GameObject.FindGameObjectWithTag("agriculture_health").GetComponent<Text>();
        Text water_health = GameObject.FindGameObjectWithTag("water_health").GetComponent<Text>();
        Text mining_health = GameObject.FindGameObjectWithTag("mining_health").GetComponent<Text>();
        Text training_health = GameObject.FindGameObjectWithTag("training_health").GetComponent<Text>();
        Text launchpad_health = GameObject.FindGameObjectWithTag("launchpad_health").GetComponent<Text>();
        Text powerplant_health = GameObject.FindGameObjectWithTag("powerplant_health").GetComponent<Text>();
        Text living_health = GameObject.FindGameObjectWithTag("living_health").GetComponent<Text>();
        String[] facilityNameArray = new string[8]
		{
		"Observatory", 
		"Agriculture Facility", 
		"Water Purification Facility", 
		"Mining Facility", 
		"Training Facility", 
		"Launch Pad", 
		"Power Plant", 
		"Living Facility"
		};	 

		/* Capacity = (# of buildings) * (max per building) */ 
		/* Full = # koalas per building */
		/* Empty = capacity - full */  
		/* Health = average of each building's health */ 
		/* THERE ARE A TOTAL OF 8 TYPES OF BUILDINGS */ 

		int capacity = 0; 
		int full = 0; 
		int empty = 0; 
		int num_buildings = 0; 
		int facility_maxWorkers = 0; 
		float health = 0; 

		for (int i = 0; i < 8; i++) 
		{
			if(facilityNameArray[i].ToLower().Contains("training"))
            {
                facility_maxWorkers = 4;
            }
            else
            {
                facility = FacilityType.getFacilityByName(facilityNameArray[i]);
                facility_maxWorkers = facility.getMaxWorkers();
            }

            List<object> obs = new List<object>();
            foreach(Facility fac in Storage.allTB)
            {
                obs.Add(fac);
            }
            foreach(TrainingFacility tf in Storage.allTF)
            {
                obs.Add(tf);
            }
			foreach (object f in obs) 
			{
                if(f is Facility)
                {
                    Facility fac = (Facility)f;
                    // getFacilityType does not return String type 
                    if (fac.getFacilityType().Equals(facilityNameArray[i]) && !facilityNameArray[i].ToLower().Contains("training"))
                    {
                        num_buildings++;
                        full += fac.getKoalas().Count;
                        health += fac.getHP();
                    }
                }
                else
                {
                    TrainingFacility tf = (TrainingFacility)f;
                    num_buildings++;
                    full += tf.getTrainees().Count + tf.getTrainers().Count;
                    health += tf.getHP();
                }
			}

			capacity = facility_maxWorkers * num_buildings; 
			empty = capacity - full; 

			health = health / num_buildings; 

			/* String builder */ 
			switch (i)
			{
			case 0:
				observatory_cap.text = full + "F/" + empty + "E";
				observatory_health.text = health + "%"; 
				break; 
			case 1:
				agriculture_cap.text = full + "F/" + empty + "E";
				agriculture_health.text = health + "%"; 
				break; 
			case 2:
				water_cap.text = full + "F/" + empty + "E";
				water_health.text = health + "%"; 

				break; 
			case 3:
				mining_cap.text = full + "F/" + empty + "E";
				mining_health.text = health + "%"; 
				break; 
			case 4:
				training_cap.text = full + "F/" + empty + "E";
				training_health.text = health + "%"; 
				break; 
			case 5:
				launchpad_cap.text = full + "F/" + empty + "E";
				launchpad_health.text = health + "%"; 
				break; 
			case 6:
				powerplant_cap.text = full + "F/" + empty + "E";
				powerplant_health.text = health + "%"; 
				break; 
			case 7:
				living_cap.text = full + "F/" + empty + "E";
				living_health.text = health + "%"; 
				break; 
			}

			num_buildings = 0;
			full = 0; 
			health = 0;  
		}
        this.gameObject.SetActive(false);
	}

}
