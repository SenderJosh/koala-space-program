using UnityEngine;

public class CreateGO : MonoBehaviour {

    internal static void create(GameObject gameObject, Vector3 vector3, Quaternion identity, float x, float y, float z, Facility fac)
    {
        GameObject g = (GameObject)Instantiate(gameObject, vector3, identity);
        Vector3 v = new Vector3(x, y, z);
        g.transform.eulerAngles = v;
        g.tag = "inGame";
        if (g.name.ToLower().Contains("water"))
        {
            g.AddComponent<WaterFacility>();
            g.GetComponent<WaterFacility>().setFacility(fac);
            Debug.Log("Set");
        }
        else if (g.name.ToLower().Contains("observatory"))
        {
            g.AddComponent<ObservatoryFacility>();
            g.GetComponent<ObservatoryFacility>().setFacility(fac);
        }
        else if (g.name.ToLower().Contains("mining"))
        {
            g.AddComponent<MiningFacility>();
            g.GetComponent<MiningFacility>().setFacility(fac);
        }
        else if (g.name.ToLower().Contains("storage"))
        {
            g.AddComponent<WaterFacility>();
            g.GetComponent<WaterFacility>().setFacility(fac);
        }
        else if (g.name.ToLower().Contains("rocket"))
        {
            g.AddComponent<WaterFacility>();
            g.GetComponent<Rocket>().setFacility(fac);
        }
    }

    internal static void create(GameObject gameObject, Vector3 vector3, Quaternion identity, float x, float y, float z, TrainingFacility fac)
    {
        GameObject g = (GameObject)Instantiate(gameObject, vector3, identity);
        Vector3 v = new Vector3(x, y, z);
        g.transform.eulerAngles = v;
        g.tag = "inGame";
        g.AddComponent<TrainingFacilityFacility>();
        g.GetComponent<TrainingFacilityFacility>().setTrainingFacility(fac);
    }
}
