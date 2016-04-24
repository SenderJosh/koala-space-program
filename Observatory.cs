﻿using UnityEngine;

public class ObservatoryFacility : MonoBehaviour
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
        if (enabled)
        {
            if (fac != null)
            {
                if (fac.getBuildStatus() >= 1.0f && fac.getHP() > 0f)
                {
                    int am = Random.Range(FacilityType.OBSERVATORY.getMinPayout(), FacilityType.OBSERVATORY.getMaxPayout() + (FacilityType.OBSERVATORY.getMaxRewardModifier() * fac.getLevel())) /*/ (fac.getKoalas().Count / FacilityType.OBSERVATORY.getMaxWorkers())*/;
                    if (Storage.pd.getBuildSupply() + am > Storage.pd.getMaxBuildSupply())
                    {
                        Storage.pd.setResearchSupply(Storage.pd.getMaxBuildSupply());
                    }
                    else
                    {
                        Storage.pd.setResearchSupply(Storage.pd.getBuildSupply() + am);
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
