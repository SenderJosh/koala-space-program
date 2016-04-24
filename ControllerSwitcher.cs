using UnityEngine;

public class ControllerSwitcher : MonoBehaviour
{
    public GameObject mainCam, firstCam;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            if(mainCam != null)
            {
                if(mainCam.activeInHierarchy)
                {
                    mainCam.SetActive(false);
                    firstCam.SetActive(true);
                }
                else
                {
                    if(firstCam != null)
                    {
                        if (firstCam.activeInHierarchy)
                        {
                            firstCam.SetActive(false);
                            mainCam.SetActive(true);
                        }
                    }
                }
            }
        }
    }


}
