using System;
using System.Collections.Generic;

[Serializable]
public class FacilityType {

    /*ProductionType, i, int HoursItWillTakeToProduceItem, int MinReward, int MaxReward, int UpgradeMax (Default Building is 0), int MaxRewardModifier, int UpgradeCost*/
    /*If the cost for an upgrade is defaultly 1000 and the current facility is on the second upgrade and has a third upgrade, the cost will be 1000*3= 3000*/
    public static readonly FacilityType OBSERVATORY = new FacilityType("Observatory", ProductionType.RESEARCH,
        2500, //Amount of engineering development it'll take to build
        500, //Amount of build supplies it'll cost to build one initially
        4, //Hours To Produce Items 
        5, //MinAmount
        10, //MaxAmount
        1, //UpgradeNumber (Start Should ALways be 0)
        5, //MaxRewardModifier
        100, //UpgradeCostResearch
        500, //UpgradeCostBuildSupplies
        6, //Max amount of workers
        Skill.SCIENTIST); //Skill for the type of job
    public static readonly FacilityType AGRICULTURE = new FacilityType("Agriculture Facility", ProductionType.FOOD,
        1500, //Amount of engineering development it'll take to build
        250, //Amount of build supplies it'll cost to build one initially
        24, //Hours To Produce Items 
        50, //MinAmount
        100, //MaxAmount
        0, //UpgradeNumber (Start Should Always be 0); if 0, there are no upgrades for this
        0, //MaxRewardModifier
        0, //UpgradeCostResearch
        0, //UpgradeCostBuildSupplies
        15, //Max amount of workers
        Skill.FARMER); //Skill for the type of job
    public static readonly FacilityType WATER_PURIFICATION = new FacilityType("Water Purification Facility", ProductionType.WATER,
        1500, //Amount of engineering development it'll take to build
        250, //Amount of build supplies it'll cost to build one initially
        48, //Hours To Produce Items 
        50, //MinAmount
        100, //MaxAmount
        0, //UpgradeNumber (Start Should Always be 0)
        0, //MaxRewardModifier
        0, //UpgradeCostResearch
        0, //UpgradeCostBuildSupplies
        15, //Max amount of workers
        Skill.LABORER); //Skill for the type of job
    public static readonly FacilityType MINING = new FacilityType("Mining Facility", ProductionType.BUILD_SUPPLIES,
        1500, //Amount of engineering development it'll take to build
        250, //Amount of build supplies it'll cost to build one initially
        48, //Hours To Produce Items 
        50, //MinAmount
        100, //MaxAmount
        0, //UpgradeNumber (Start Should Always be 0)
        0, //MaxRewardModifier
        0, //UpgradeCostResearch
        0, //UpgradeCostBuildSupplies
        5, //Max amount of workers
        Skill.LABORER); //Skill for the type of job
    public static readonly FacilityType LAUNCH_PAD = new FacilityType("Launch Pad", ProductionType.RESEARCH,
        15000, //Amount of engineering development it'll take to build
        50000, //Amount of build supplies it'll cost to build one initially
        180, //Hours To Produce Items 
        50, //MinAmount
        100, //MaxAmount
        0, //UpgradeNumber (Start Should Always be 0)
        0, //MaxRewardModifier
        0, //UpgradeCostResearch
        0, //UpgradeCostBuildSupplies
        5, //Max amount of workers
        Skill.SCIENTIST); //Skill for the type of job
    public static readonly FacilityType POWER_PLANT = new FacilityType("Power Plant", ProductionType.RESEARCH,
        1500, //Amount of engineering development it'll take to build
        5000, //Amount of build supplies it'll cost to build one initially
        48, //Hours To Produce Items 
        50, //MinAmount
        100, //MaxAmount
        0, //UpgradeNumber (Start Should Always be 0)
        0, //MaxRewardModifier
        0, //UpgradeCostResearch
        0, //UpgradeCostBuildSupplies
        5, //Max amount of workers
        Skill.ENGINEER); //Skill for the type of job
    public static readonly FacilityType LIVING_FACILITY = new FacilityType("Living Facility", ProductionType.BUILD_SUPPLIES,
        200, //Amount of engineering development it'll take to build
        1000, //Amount of build supplies it'll cost to build one initially
        20, //Hours To Produce Items 
        0, //MinAmount
        0, //MaxAmount
        0, //UpgradeNumber (Start Should Always be 0)
        0, //MaxRewardModifier
        0, //UpgradeCostResearch
        0, //UpgradeCostBuildSupplies
        5, //Max amount of workers
        Skill.LABORER); //Skill for the type of job

    public static IEnumerable<FacilityType> Values
    {
        get
        {
            yield return OBSERVATORY;
            yield return AGRICULTURE;
            yield return WATER_PURIFICATION;
            yield return MINING;
            yield return LAUNCH_PAD;
            yield return POWER_PLANT;
            yield return LIVING_FACILITY;
        }
    }

    private readonly string name;
    private readonly ProductionType pt;
    private readonly Skill skillRequired;
    private readonly int buildProg, buildSupInitial, hoursProduce, minPayout, maxPayout, upgrades, maxRewardModifier, upgradeCostResearch, upgradeCostBuild, maxWorkers;
    FacilityType(string name, ProductionType pt, int buildProg, int buildSupInitial, int hoursProduce, int minPayout, int maxPayout, int upgrades, int maxRewardModifier,
        int upgradeCostResearch, int upgradeCostBuild, int maxWorkers, Skill skillRequired)
    {
        this.name = name;
        this.pt = pt;
        this.skillRequired = skillRequired;
        this.buildProg = buildProg;
        this.buildSupInitial = buildSupInitial;
        this.hoursProduce = hoursProduce;
        this.minPayout = minPayout;
        this.maxPayout = maxPayout;
        this.upgrades = upgrades;
        this.maxRewardModifier = maxRewardModifier;
        this.upgradeCostResearch = upgradeCostResearch;
        this.upgradeCostBuild = upgradeCostBuild;
        this.maxWorkers = maxWorkers;
    }

    public string getName()
    {
        return this.name;
    }
    public ProductionType getProductType()
    {
        return this.pt;
    }
    public int getBuildProgress()
    {
        return this.buildProg;
    }
    public int getInitialBuildSupplyCost()
    {
        return this.buildSupInitial;
    }
    public int getHoursToProduce()
    {
        return this.hoursProduce;
    }
    public int getMinPayout()
    {
        return this.minPayout;
    }
    public int getMaxPayout()
    {
        return this.maxPayout;
    }
    public int getUpgrades()
    {
        return this.upgrades;
    }
    public int getMaxRewardModifier()
    {
        return this.maxRewardModifier;
    }
    public int getUpgradeCostResearch()
    {
        return this.upgradeCostResearch;
    }
    public int getUpgradeCostBuild()
    {
        return this.upgradeCostBuild;
    }
    public int getMaxWorkers()
    {
        return this.maxWorkers;
    }
    public Skill getSkillRequired()
    {
        return this.skillRequired;
    }

    public static FacilityType getFacilityByName(string name)
    {
        FacilityType fc = null;
        using(var it = FacilityType.Values.GetEnumerator())
        {
            while(it.MoveNext())
            {
                FacilityType ft = it.Current;
                if(ft.getName().Equals(name))
                {
                    fc = ft;
                    break;
                }
            }
        }
        return fc;
    }
}
