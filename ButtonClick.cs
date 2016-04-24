using UnityEngine;

public class ButtonClick : MonoBehaviour {

    private Ray ray;
    private RaycastHit hit;
    private GameObject building;

    public void onClick2()
    {
        /*
         * System.Random rnd = new System.Random();
        Storage.koalas.Add(new Koala(NameDictionary.firstNames[rnd.Next(NameDictionary.firstNames.Length)] + " " + NameDictionary.lastNames[rnd.Next(NameDictionary.lastNames.Length)],
            0, rnd.Next(2) + 1, rnd.Next(2) + 1, rnd.Next(2) + 1, Storage.days, (int)Random.Range(1, 3)));
            */
    }

	public void onClick(string buildingToSpawn)
    {
        if (building == null)
        {
            /*if("Factory" == "Factory")
            {
                building = Instantiate(Resources.Load("Factory", typeof(GameObject)), new Vector3(0, -80, 0), Quaternion.Euler(270, 0, 0)) as GameObject;
            }*/
            if(buildingToSpawn == "Water Purification Facility")
            {
                building = Instantiate(Resources.Load("Water Purification Facility", typeof(GameObject)), new Vector3(0, -80, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            else
            {
                building = Instantiate(Resources.Load(buildingToSpawn, typeof(GameObject)), new Vector3(0, -80, 0), Quaternion.Euler(0, 0, 0)) as GameObject;
            }
            building.GetComponent<Collider>().enabled = false;
        }
        else
        {
            Destroy(building);
            building = null;
        }
    }

    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(building != null)
        {
            if(Physics.Raycast(ray, out hit))
            {
                Vector3 v = hit.point;
                v.y = hit.point.y;
                building.transform.position = v;

                GameObject[] go = GameObject.FindGameObjectsWithTag("inGame");
                bool b = true;
                building.GetComponent<Collider>().enabled = true;
                foreach (GameObject g in go)
                {
                    if (g.GetComponent<Collider>()
                        .bounds.Intersects(building.GetComponent<Collider>().bounds))
                    {
                        b = false;
                        break;
                    }
                }
                building.GetComponent<Collider>().enabled = false;
                if (b)
                {
                    if (Input.GetMouseButton(0))
                    {
                        //NEED TO MAKE DYNAMIC
                        building.SetActive(true);
                        building.tag = "inGame";
                        building.GetComponent<Collider>().enabled = true;
                        Debug.Log(building.name);
                        if(building.name.Equals("Training Facility(Clone)"))
                        {
                            TrainingFacilityFacility tff = building.GetComponent<TrainingFacilityFacility>();
                            tff.setTrainingFacility(new TrainingFacility(building.transform.position.x, building.transform.position.y, building.transform.position.z, 1.0f,
                                building.transform.eulerAngles.x, building.transform.eulerAngles.y, building.transform.eulerAngles.z));
                            Storage.allTF.Add(tff.getTrainingFacility());
                        }
                        else if(building.name.Equals("Observatory(Clone)"))
                        {
                            Storage.allTB.Add(new Facility(FacilityType.getFacilityByName(building.name.Substring(0, building.name.Length - 7)),
                                building.transform.position.x, building.transform.position.y, building.transform.position.z, 1.0f,
                            building.transform.eulerAngles.x, building.transform.eulerAngles.y, building.transform.eulerAngles.z));
                        }
                        else if(building.name.Equals("Moonbase"))
                        {

                        }
                        else if(building.name.Equals("Mining Facility(Clone)"))
                        {
                            Storage.allTB.Add(new Facility(FacilityType.MINING, building.transform.position.x, building.transform.position.y, building.transform.position.z,
                                1.0f, building.transform.eulerAngles.x, building.transform.eulerAngles.y, building.transform.eulerAngles.z));
                        }
                        else if(building.name.Equals("Storage(Clone)"))
                        {
                            Storage.pd.setMaxBuildSupply(Storage.pd.getBuildSupply() + 500);
                            Storage.pd.setMaxFoodSupply(Storage.pd.getMaxFoodSupply() + 500);
                            Storage.pd.setWaterSupply(Storage.pd.getMaxWaterSupply() + 500);
                        }
                        else if(building.name.Equals("Agriculture Facility(Clone)"))
                        {

                        }
                        else if(building.name.Equals("Water Purification Facility(Clone)"))
                        {
                            Debug.Log("Added purification");
                            Storage.allTB.Add(new Facility(FacilityType.WATER_PURIFICATION, building.transform.position.x, building.transform.position.y, building.transform.position.z,
                                1.0f, building.transform.eulerAngles.x, building.transform.eulerAngles.y, building.transform.eulerAngles.z));
                        }
                        else if(building.name.Equals("Living Facility"))
                        {
                            Storage.allTB.Add(new Facility(FacilityType.LIVING_FACILITY, building.transform.position.x, building.transform.position.y, building.transform.position.z,
                                1.0f, building.transform.eulerAngles.x, building.transform.eulerAngles.y, building.transform.eulerAngles.z));
                        }
                        building = null;
                    }
                }
                else
                {
                    //Show player that they may not build in this area
                }
            }
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                Destroy(building);
                building = null;
            }
        }
        else
        {
            if(Input.GetMouseButtonUp(0))
            {
                if(Physics.Raycast(ray, out hit))
                {
                    Debug.Log(hit.collider.name);
                    GameObject hitObj = hit.collider.gameObject;
                    if (hit.collider.name.Contains("Training"))
                    {
                        //hitObj.GetComponent<TrainingFacilityFacility>().createMenu();
                    }
                    else if(hit.collider.name.Contains("Mining"))
                    {
                        //hitObj.GetComponent<MiningFacility>().createMenu();
                    }
                    else if(hit.collider.name.Contains("Water"))
                    {
                        //hitObj.GetComponent<WaterFacility>().createMenu();
                    }
                    else if(hit.collider.name.Contains("Observatory"))
                    {
                        //hitObj.GetComponent<Observatory>().createMenu();
                    }
                }
            }
        }
    }
}
