using UnityEngine;

public class TrainingFacilityFacility : MonoBehaviour {

    private GameObject go;
    private TrainingFacility tf;

	void Start()
    {
        go = this.gameObject;
    }

    void OnEnable()
    {
        Storage.dayPassed += increment;
    }

    void OnDisable()
    {
        Storage.dayPassed -= increment;
    }

    private void increment()
    {
        if(tf != null)
        {
            tf.incrementDayTrain();
        }
    }

    public void setTrainingFacility(TrainingFacility tf)
    {
        this.tf = tf;
    }

    public TrainingFacility getTrainingFacility()
    {
        return this.tf;
    }

    public void createMenu()
    {

    }

}
