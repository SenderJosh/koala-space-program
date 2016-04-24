using UnityEngine;
using System;


[Serializable][SerializeField]
public class Koala
{
    
    private bool health = true, employed = false, happy = true, exiled = false, hadKids = false;
    private string sick = "0:false"; //AmountOfUpdates:bool
    private string kName;
    private int age, food, water, maxFood = 100, maxWater = 100, dayBorn, size;
    private int intelligence, strength, dexterity;
    private Skill skill;

    public Koala(string kName, int age, int intelligence, int strength, int dexterity, int dayBorn, int size) //size 1-3; divider of how much food gives. Default food giver: 30. Every koala eats until full
    {
        this.kName = kName;
        this.age = age;
        this.intelligence = intelligence;
        this.strength = strength;
        this.dexterity = dexterity;
        this.food = 100;
        this.water = 100;
        this.dayBorn = dayBorn;
        this.size = size;
    }

    public void setHaveKid(bool hadKids)
    {
        this.hadKids = hadKids;
    }
    public bool hadKid()
    {
        return this.hadKids;
    }

    public void setExiled(bool exiled)
    {
        this.exiled = exiled;
    }
    public bool isExiled()
    {
        return this.exiled;
    }

    public void setSize(int size)
    {
        this.size = size;
    }
    public int getSize()
    {
        return this.size;
    }

    public int getDayBorn()
    {
        return this.dayBorn;
    }
    public void setHappy(bool happy)
    {
        this.happy = happy;
    }
    public bool isHappy()
    {
        return this.happy;
    }

    public void killKoala()
    {
        this.health = false;
        EventLogger.addLog(kName + " died");
    }
    public bool isAlive()
    {
        return this.health;
    }

    public void setName(string kName)
    {
        this.kName = kName;
    }
    public string getName()
    {
        return this.kName;
    }

    public void setAge(int age)
    {
        this.age = age;
    }
    public void incrementAge()
    {
        Debug.Log("Happy Birthday " + kName + "!");
        this.age++;
    }
    public int getAge()
    {
        return this.age;
    }

    public bool isSick()
    {
        bool b = false;
        if(sick.Split(':')[1].ToLower().Equals("true"))
        {
            b = true;
        }
        return b;
    }
    public void setSick(int it, bool b)
    {
        sick = string.Format("{0}:{1}", it, b);
    }
    public int getSickIteration()
    {
        return int.Parse(sick.Split(':')[0]);
    }

    public void setSkill(Skill s)
    {
        this.skill = s;
    }
    public Skill getSkill()
    {
        return this.skill;
    }

    public void setEmployed(bool b)
    {
        this.employed = b;
    }
    public bool isEmployed()
    {
        return this.employed;
    }

    public void setFood(int food)
    {
        this.food = food;
    }
    public int getFood()
    {
        return this.food;
    }

    public void setWater(int water)
    {
        this.water = water;
    }
    public int getWater()
    {
        return this.water;
    }

    public int getIntelligence()
    {
        return this.intelligence;
    }
    public int getDexterity()
    {
        return this.dexterity;
    }
    public int getStrength()
    {
        return this.strength;
    }

}
