using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[Serializable][SerializeField]
public class Facility{
    
    /*
     * Efficiency of any facility will be determined by it's (workers/totalWorkers)*health
     */
    private string name;
    private int level;
    private float x, y, z, ex, ey, ez;
    private float hp, buildStatus;
    private List<Koala> koalas = new List<Koala>();
    private List<Koala> engineers = new List<Koala>();
    private FacilityType facType;

    public Facility(FacilityType facType, float x, float y, float z, float hp, float ex, float ey, float ez)
    {
        this.name = facType.getName();
        this.level = 0;
        this.x = x;
        this.y = y;
        this.z = z;
        this.ex = ex;
        this.ey = ey;
        this.ez = ez;
        this.hp = hp;
        this.facType = facType;
        buildStatus = 1;
    }

    public void upgrade()
    {
        buildStatus = 0;
        level++;
    }

    public bool isMaximumUpgrade()
    {
        return (level >= this.facType.getUpgrades()) ? true : false;
    }

    public bool canUpgrade()
    {
        return (this.facType.getUpgrades() > 0) ? true : false;
    }

    public List<Koala> getEngineers()
    {
        return this.engineers;
    }
    public void addEngineer(Koala k)
    {
        this.engineers.Add(k);
    }
    public void clearEngineers()
    {
        this.engineers.Clear();
    }

    public int getLevel()
    {
        return this.level;
    }
    public void setlevel(int level)
    {
        this.level = level;
    }

    public void setBuildStatus(float buildStatus)
    {
        this.buildStatus = buildStatus;
    }
    public float getBuildStatus()
    {
        return this.buildStatus;
    }

    public void create()
    {
        CreateGO.create((GameObject) Resources.Load(name, typeof(GameObject)), new Vector3(x, y, z), Quaternion.identity, ex, ey, ez, this);
    }

    public void addKoala(Koala k)
    {
        this.koalas.Add(k);
    }
    public void removeKoala(Koala k)
    {
        this.koalas.Remove(k);
    }
    public void clearKoala()
    {
        this.koalas.Clear();
    }

    public void setHP(float hp)
    {
        this.hp = hp;
    }
    
    public float getHP()
    {
        return this.hp;
    }

    public List<Koala> getKoalas()
    {
        return this.koalas;
    }
    public string getName()
    {
        return this.name;
    }
    public float getX()
    {
        return this.x;
    }
    public float getY()
    {
        return this.y;
    }
    public float getZ()
    {
        return this.z;
    }

    public FacilityType getFacilityType()
    {
        return this.facType;
    }
    public void setFacilityType(FacilityType fac)
    {
        this.facType = fac;
    }
}
