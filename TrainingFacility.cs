using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class TrainingFacility{

    private List<Koala> trainers = new List<Koala>();
    private List<object[]> trainees = new List<object[]>();

    private float x, y, z, hp, ex, ey, ez;

    public TrainingFacility(float x, float y, float z, float hp, float ex, float ey, float ez)
    {
        this.x = x;
        this.y = y;
        this.z = z;
        this.hp = hp;
        this.ex = ex;
        this.ey = ey;
        this.ez = ez;
    }

    public List<Koala> getTrainees()
    {
        List<Koala> koalaTrainees = new List<Koala>();
        foreach(object[] o in trainees)
        {
            koalaTrainees.Add((Koala)o[0]);
        }
        return koalaTrainees;
    }
    public List<Koala> getTrainers()
    {
        return this.trainers;
    }

    public void setHP(float f)
    {
        this.hp = f;
    }
    public float getHP()
    {
        return this.hp;
    }

    public void create()
    {
        CreateGO.create((GameObject)Resources.Load("Training Facility", typeof(GameObject)), new Vector3(x, y, z), Quaternion.identity, ex, ey, ez, this);
    }

    public void addTrainer(Koala k)
    {
        trainers.Add(k);
    }
    public void removeTrainer(Koala k)
    {
        trainers.Remove(k);
    }

    public void addTrainee(Koala k, Skill s, int daysToTrain)
    {
        bool b = true;
        foreach(object[] ob in trainees)
        {
            Koala ko = (Koala)ob[0];
            if(ko.Equals(k))
            {
                b = false; 
            }
        }
        if(b)
        {
            object[] sl = new object[4];
            sl[0] = k;
            sl[1] = 0;
            sl[2] = s;
            sl[3] = daysToTrain;
            trainees.Add(sl);
        }
    }

    public void removeTrainee(Koala k)
    {
        foreach (object[] ob in trainees)
        {
            Koala ko = (Koala)ob[0];
            if (ko.Equals(k))
            {
                trainees.Remove(ob);
                break;
            }
        }
    }

    public void incrementDayTrain()
    {
        List<object[]> toRemove = new List<object[]>();
        List<object[]> toIncrement = new List<object[]>();
        foreach (object[] ob in trainees)
        {
            Koala k = (Koala)ob[0];
            if(k.isAlive())
            {
                if (((int)ob[1]) + 1 >= (int)ob[3])
                {
                    Skill s = (Skill)ob[2];
                    k.setSkill(s);
                    EventLogger.addLog(((Koala)ob[0]).getName() + " has finished training to be a " + s.ToString());
                    toRemove.Add(ob);
                }
                else
                {
                    toIncrement.Add(ob);
                }
            }
            else
            {
                toRemove.Add(ob);
            }
        }
        foreach(object[] ob in toRemove)
        {
            trainees.Remove(ob);
        }
        foreach(object[] ob in toIncrement)
        {
            trainees.Remove(ob);
            ob[1] = ((int)ob[1])+1;
            trainees.Add(ob);
        }
    }

}
