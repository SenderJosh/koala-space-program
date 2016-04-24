using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpdateTopHorizontalBar : MonoBehaviour {

    Text food;
    Text water;
    Text materials;
    Text koalas;
    Text research;
    Text happiness;
    Text weather;
    Text days;
    Text time;

    Image dayIcon;
    Image nightIcon;
    Image solarFlareIcon;
    Image dustStormIcon;

    void Start()
    {
        food = GameObject.FindGameObjectWithTag("food_text").GetComponent<Text>();
        water = GameObject.FindGameObjectWithTag("water_text").GetComponent<Text>();
        materials = GameObject.FindGameObjectWithTag("materials_text").GetComponent<Text>();
        koalas = GameObject.FindGameObjectWithTag("koalas_text").GetComponent<Text>();
        research = GameObject.FindGameObjectWithTag("research_text").GetComponent<Text>();
        happiness = GameObject.FindGameObjectWithTag("happiness_text").GetComponent<Text>();
        weather = GameObject.FindGameObjectWithTag("weather_text").GetComponent<Text>();
        days = GameObject.FindGameObjectWithTag("days_text").GetComponent<Text>();
        time = GameObject.FindGameObjectWithTag("time_text").GetComponent<Text>();

        dayIcon = GameObject.FindGameObjectWithTag("day_icon").GetComponent<Image>();
        nightIcon = GameObject.FindGameObjectWithTag("night_icon").GetComponent<Image>();
        solarFlareIcon = GameObject.FindGameObjectWithTag("solar_flare_icon").GetComponent<Image>();
        dustStormIcon = GameObject.FindGameObjectWithTag("dust_storm_icon").GetComponent<Image>();
    }

	void Update ()
	{
		food.text = Storage.pd.getFoodSupply () + ""; 
		water.text = Storage.pd.getWaterSupply () + "";
		materials.text = Storage.pd.getBuildSupply () + ""; 
		koalas.text = Storage.koalas.Count + ""; 
		research.text = Storage.pd.getResearchSupply () + "";
		happiness.text = Storage.pd.getHappyLevel ()*100 + "%"; 

		if (Storage.events.ToArray().GetValue (Storage.events.Count - 1).Equals (Events.SOLAR_FLARE)) {
			weather.text = "Solar Flare"; 
		} else if (Storage.events.ToArray().GetValue (Storage.events.Count - 1).Equals (Events.DUST_STORM)) {
			weather.text = "Dust Storm"; 
		} else {
			weather.text = "Normal"; 
		}

		/* Days passed = (years * 387) + days */
		days.text = (Storage.years * 387) + Storage.days + "Days Elapsed"; 
		time.text = Storage.hours + ":" + Storage.minutes; 

		/* ===== Show one weather icon at a time (all 4 are set active) ===== */ 
		if (weather.text == "Solar Flare") 
		{
			dayIcon.enabled = false;
			nightIcon.enabled = false;
			dustStormIcon.enabled = false;
			solarFlareIcon.enabled = true; 
		}
		else if (weather.text == "Dust Storm")
		{
			dayIcon.enabled = false;
			nightIcon.enabled = false;
			dustStormIcon.enabled = true;
			solarFlareIcon.enabled = false; 
		}
		/* Day time at 05:00 to 17:59. 25 hours in a day */ 
		else if (Storage.hours > 5 && Storage.hours < 18)
		{
			dayIcon.enabled = true;
			nightIcon.enabled = false;
			dustStormIcon.enabled = false;
			solarFlareIcon.enabled = false; 			
		}
		/* Night time at 18:00 to 04:59. 25 hours in a day */
		else if (Storage.hours < 5 || Storage.hours > 17)
		{
			dayIcon.enabled = false;
			nightIcon.enabled = true;
			dustStormIcon.enabled = false;
			solarFlareIcon.enabled = false; 			
		}
	}

}
