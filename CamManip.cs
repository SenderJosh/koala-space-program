using UnityEngine;

public class CamManip : MonoBehaviour {

    private GameObject cm;
    private bool q = false, e = false;
    private int c = 0;

	void Start ()
    {
        Cursor.visible = true;
        cm = GameObject.Find("MainCamera");
	}
	
	void Update()
    {
        if(Input.GetAxis("Mouse ScrollWheel")>0)
        {
            if(cm.transform.position.y>12)
            {
                cm.transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * 4);
            }
        }
        else
        {
            cm.transform.Translate(Vector3.forward * Input.GetAxis("Mouse ScrollWheel") * 4);
        }
        if (Input.GetKeyUp(KeyCode.E) && !q)
        {
            e = true;
        }
        else if(Input.GetKeyUp(KeyCode.Q) && !e)
        {
            q = true;
        }
        if(Input.GetKey(KeyCode.W))
        {
            Vector3 v = new Vector3(cm.transform.position.x, cm.transform.position.y, cm.transform.position.z);
            cm.transform.Translate(Vector3.forward);
            v.Set(cm.transform.position.x, v.y, cm.transform.position.z);
            cm.transform.position = v;
        }
        if(Input.GetKey(KeyCode.A))
        {
            cm.transform.Translate(-1, 0, 0);
        }
        if(Input.GetKey(KeyCode.S))
        {
            Vector3 v = new Vector3(cm.transform.position.x, cm.transform.position.y, cm.transform.position.z);
            cm.transform.Translate(Vector3.back);
            v.Set(cm.transform.position.x, v.y, cm.transform.position.z);
            cm.transform.position = v;
        }
        if(Input.GetKey(KeyCode.D))
        {
            cm.transform.Translate(1, 0, 0);
        }
        if (q && c<60)
        {
            Vector3 v = cm.transform.rotation.eulerAngles;
            v.y = v.y + 5;
            cm.transform.eulerAngles = v;
            c = add(c, 5);
        }
        else
        {
            if(q)
            {
                c = 0;
            }
            q = false;
        }
        if (e && c < 60)
        {
            Vector3 v = cm.transform.rotation.eulerAngles;
            v.y = v.y - 5;
            cm.transform.eulerAngles = v;
            c = add(c, 5);
        }
        else
        {
            if(e)
            {
                c = 0;
            }
            e = false;
        }
    }

    private int add(int i, int z)
    {
        return i + z;
    }

}
