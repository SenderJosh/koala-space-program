using UnityEngine;
using System.Collections.Generic;

public class Rocket : MonoBehaviour {

    private GameObject g;
    private List<Koala> launchingKoala = new List<Koala>();
    private int buildSuppliesToSendBack = 0;
    private int dayToLaunch = 3;
    private Facility fac;

    public void setFacility(Facility fac)
    {
        this.fac = fac;
    }
    public Facility getFacility()
    {
        return this.fac;
    }

    void Start()
    {
        this.g = this.gameObject;
    }

    void OnEnable()
    {
        Storage.dayPassed += increment;
    }

    void OnDisable()
    {
        Storage.dayPassed -= increment;
    }

    public void increment()
    {
        if(launchingKoala.Count > 0 || buildSuppliesToSendBack > 0)
        {
            if(dayToLaunch <= 0)
            {
                if(launchingKoala.Count > 0)
                {
                    string s = "";
                    foreach (Koala k in launchingKoala)
                    {
                        s += k.getName() + ", ";
                        k.killKoala();
                    }
                    s = s.Substring(0, s.Length - 2);
                    if (launchingKoala.Count > 1)
                    {
                        EventLogger.addLog(s + " were been exiled from Mars peacefully.");
                    }
                    else
                    {
                        EventLogger.addLog(s + " has been exiled from Mars peacefully.");
                    }
                    launchingKoala.Clear();
                }
                if(buildSuppliesToSendBack > 0)
                {
                    EventLogger.addLog(buildSuppliesToSendBack + " supplies has been sent back to Earth. In return, you receive: "
                        + (buildSuppliesToSendBack / 5) + " research!");
                    Storage.pd.setResearchSupply(Storage.pd.getResearchSupply() + (buildSuppliesToSendBack / 5));
                    buildSuppliesToSendBack = 0;
                }
                dayToLaunch = 3;
            }
            else
            {
                dayToLaunch--;
            }
        }
    }

    public void addSuppliesToLaunch(int supp)
    {
        buildSuppliesToSendBack += supp;
    }
}
