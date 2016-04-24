using UnityEngine;
using System.Collections.Generic;

public class MiningFacility : MonoBehaviour
{

    private bool enbl = true;
    private Facility fac = null;

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
        if(enabled)
        {
            if(fac != null)
            {
                if(fac.getBuildStatus() >= 1.0f && fac.getHP() > 0f)
                {
                    int am = Random.Range(FacilityType.MINING.getMinPayout(), FacilityType.MINING.getMaxPayout()+(FacilityType.MINING.getMaxRewardModifier()*fac.getLevel()))/*/(fac.getKoalas().Count/FacilityType.MINING.getMaxWorkers())*/;
                    List<Koala> toRemove = new List<Koala>();
                    foreach(Koala k in fac.getKoalas())
                    {
                        if(!k.isAlive())
                        {
                            toRemove.Add(k);
                        }
                    }
                    foreach(Koala k in toRemove)
                    {
                        fac.removeKoala(k);
                    }
                    if (Storage.pd.getBuildSupply() + am > Storage.pd.getMaxBuildSupply())
                    {
                        Storage.pd.setBuildSupply(Storage.pd.getMaxBuildSupply());
                    }
                    else
                    {
                        Storage.pd.setBuildSupply(Storage.pd.getBuildSupply() + am);
                    }
                }
                else
                {
                    if((fac.getBuildStatus() + (.01 * fac.getEngineers().Count)) < 1.0f)
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
