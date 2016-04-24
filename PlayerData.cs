using UnityEngine;
using System;

[Serializable][SerializeField]
public class PlayerData{

    private int day, year, minute;
    private int foodSupply, waterSupply, buildSupply, researchSupply;
    private int maxFoodSupply, maxWaterSupply, maxBuildSupply, maxResearchSupply;
    private float happyLevel = .5f;

    public PlayerData(int foodSupply, int waterSupply, int buildSupply, int researchSupply)
    {
        this.foodSupply = foodSupply;
        this.waterSupply = waterSupply;
        this.buildSupply = buildSupply;
        this.researchSupply = researchSupply;
    }

    public int getDayCount()
    {
        return this.day;
    }
    public int getMinuteCount()
    {
        return this.minute;
    }
    public int getYearCount()
    {
        return this.year;
    }
    public void setDayCount(int day)
    {
        this.day = day;
    }
    public void setYearCount(int year)
    {
        this.year = year;
    }
    public void setMinuteCount(int minute)
    {
        this.minute = minute;
    }

    public void setMaxFoodSupply(int maxFoodSupply)
    {
        this.maxFoodSupply = maxFoodSupply;
    }
    public int getMaxFoodSupply()
    {
        return this.maxFoodSupply;
    }

    public void setMaxWaterSupply(int maxWaterSupply)
    {
        this.maxWaterSupply = maxWaterSupply;
    }
    public int getMaxWaterSupply()
    {
        return this.maxWaterSupply;
    }
    
    public void setMaxBuildSupply(int maxBuildSupply)
    {
        this.maxBuildSupply = maxBuildSupply;
    }
    public int getMaxBuildSupply()
    {
        return this.maxBuildSupply;
    }

    public void setHappyLevel(float happyLevel)
    {
        this.happyLevel = happyLevel;
    }
    public float getHappyLevel()
    {
        return this.happyLevel;
    }
    
    public void setFoodSupply(int foodSupply)
    {
        this.foodSupply = foodSupply;
    }
    public void setWaterSupply(int waterSupply)
    {
        this.waterSupply = waterSupply;
    }
    public void setBuildSupply(int buildSupply)
    {
        this.buildSupply = buildSupply;
    }
    public void setResearchSupply(int researchSupply)
    {
        this.researchSupply = researchSupply;
    }

    public int getFoodSupply()
    {
        return this.foodSupply;
    }
    public int getWaterSupply()
    {
        return this.waterSupply;
    }
    public int getBuildSupply()
    {
        return this.buildSupply;
    }
    public int getResearchSupply()
    {
        return this.researchSupply;
    }
	
}
