using UnityEngine;
using System.Collections.Generic;

public class AgricultureFacility : MonoBehaviour
{

    private bool enbl = true;
    private Facility fac = null;
    // On enable
    void OnEnable()
    {
        Storage.dayPassed += newDay;
    }

    void OnDisable()
    {
        Storage.dayPassed -= newDay;
    }

    public void setFacility(Facility fac)
    {
        this.fac = fac;
    }
    public Facility getFacility()
    {
        return this.fac;
    }

    private void newDay()
    {
        if (enabled)
        {
            if (fac != null)
            {
                if (fac.getBuildStatus() >= 1.0f && fac.getHP() > 0f)
                {
                    int am = Random.Range(FacilityType.AGRICULTURE.getMinPayout(), FacilityType.AGRICULTURE.getMaxPayout() + (FacilityType.AGRICULTURE.getMaxRewardModifier() * fac.getLevel()))/*/(fac.getKoalas().Count/FacilityType.AGRICULTURE.getMaxWorkers())*/;
                    List<Koala> toRemove = new List<Koala>();
                    foreach (Koala k in fac.getKoalas())
                    {
                        if (!k.isAlive())
                        {
                            toRemove.Add(k);
                        }
                    }
                    foreach (Koala k in toRemove)
                    {
                        fac.removeKoala(k);
                    }
                    if (Storage.pd.getFoodSupply() + am > Storage.pd.getMaxFoodSupply())
                    {
                        Storage.pd.setFoodSupply(Storage.pd.getMaxFoodSupply());
                    }
                    else
                    {
                        Storage.pd.setFoodSupply(Storage.pd.getFoodSupply() + am);
                    }
                }
                else
                {
                    if ((fac.getBuildStatus() + (.01 * fac.getEngineers().Count)) < 1.0f)
                    {
                        fac.setBuildStatus(fac.getBuildStatus() + (.01f * fac.getEngineers().Count));
                        fac.clearEngineers();
                        EventLogger.addLog(fac.getName() + " has finished being built!");
                    }
                    else
                    {
                        fac.setBuildStatus(1.0f);
                        fac.clearEngineers();
                        EventLogger.addLog(fac.getName() + " has finished being built!");
                    }
                }
            }
        }
    }

    public void setEnabled(bool enbl)
    {
        this.enbl = enbl;
    }

    public bool isEnabled()
    {
        return this.enbl;
    }
    public void createMenu()
    {

    }
}
