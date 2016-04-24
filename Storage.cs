using UnityEngine;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class Storage : MonoBehaviour {

    public static readonly int DEFAULT_ADDFOOD_PER_UNIT = 30, DEFAULT_ADDWATER_PER_UNIT = 30;

    public static List<Koala> koalas = new List<Koala>();
    public static List<object[]> koalaToJob = new List<object[]>();
    public static List<Facility> allTB = new List<Facility>();
    public static List<TrainingFacility> allTF = new List<TrainingFacility>();
    public static PlayerData pd;
    public static float timeScale = 1;
    public static int years, days, hours, minutes;
    public static int REDay = 0, RAND_Day = 0, hoursToStopEvent = 0;
    public static List<Events> events = new List<Events>();
    public static GameObject catastrophy, terrain;
    public static Events currEvent = null;

    public delegate void HourPassed();
    public static event HourPassed hourPassed;

    public delegate void DayPassed();
    public static event DayPassed dayPassed;

    public delegate void YearPassed();
    public static event YearPassed yearPassed;
    
    public static void addKoalaJob(Facility fac, Koala k)
    {
        bool b = true;
        foreach (object[] ob in koalaToJob)
        {
            if (((Koala)ob[0]).Equals(k))
            {
                b = false;
            }
        }
        if (b)
        {
            object[] ob = new object[2];
            ob[0] = k;
            ob[1] = fac;
            koalaToJob.Add(ob);
        }
    }
    public static void addKoalaJob(TrainingFacility fac, Koala k)
    {
        bool b = true;
        foreach (object[] ob in koalaToJob)
        {
            if (((Koala)ob[0]).Equals(k))
            {
                b = false;
            }
        }
        if (b)
        {
            object[] ob = new object[2];
            ob[0] = k;
            ob[1] = fac;
            koalaToJob.Add(ob);
        }
    }
    public static void removeKoalaJob(Koala k)
    {
        bool b = false;
        object[] o = new object[2];
        foreach(object[] ob in koalaToJob)
        {
            if(((Koala)ob[0]).Equals(k))
            {
                b = true;
                o = ob;
            }
        }
        if(b)
        {
            koalaToJob.Remove(o);
        }
    }


    //For time related stuffies, use FixedUpdate 
    void Start ()
    {
        terrain = GameObject.FindGameObjectWithTag("terrain");
        RAND_Day = Random.Range(2, 6);
        events.Add(Events.DUST_STORM);
        events.Add(Events.EARTHQUAKE);
        events.Add(Events.METEOR);
        events.Add(Events.SOLAR_FLARE);
        timeScale = PlayerPrefs.GetInt("TimeScale");
        Time.timeScale = .1f;
        DirectoryInfo d = new DirectoryInfo(@Application.dataPath);
        IFormatter formatter = new BinaryFormatter();

        FileInfo[] pdFiles = d.Parent.GetFiles("Assetspd.bin");
        foreach (FileInfo f in pdFiles)
        {
            Stream stream = new FileStream(f.Name,
                          FileMode.Open,
                          FileAccess.Read,
                          FileShare.Read);
            pd = (PlayerData)formatter.Deserialize(stream);
            stream.Close();
            days = pd.getDayCount();
            years = pd.getYearCount();
            minutes = pd.getMinuteCount();
            Debug.Log(pd.getFoodSupply());
            Debug.Log(pd.getWaterSupply());
        }

        if (pd == null)
        {
            pd = new PlayerData(1500, 1500, 1000, 500);
            FileInfo[] allBinFiles = d.Parent.GetFiles("*.bin");
            foreach(FileInfo f in allBinFiles)
            {
                f.Delete();
            }
            for (int i = 0; i < 10; i++)
            {
                System.Random rnd = new System.Random();
                koalas.Add(new Koala(NameDictionary.firstNames[rnd.Next(NameDictionary.firstNames.Length)] + " " + NameDictionary.lastNames[rnd.Next(NameDictionary.lastNames.Length)],
                                0, rnd.Next(2) + 1, rnd.Next(2) + 1, rnd.Next(2) + 1, Storage.days, Random.Range(1, 3)));
            }
            Debug.Log("New game");
        }
        else
        {
            FileInfo[] tbFiles = d.Parent.GetFiles("*tb.bin");
            foreach (FileInfo f in tbFiles)
            {
                Stream stream = new FileStream(f.Name,
                              FileMode.Open,
                              FileAccess.Read,
                              FileShare.Read);
                Facility fac = (Facility)formatter.Deserialize(stream);
                Storage.allTB.Add(fac);
                fac.create();
                stream.Close();
                Debug.Log(f.Name);
            }

            FileInfo[] tfFiles = d.Parent.GetFiles("*tf.bin");
            foreach (FileInfo f in tfFiles)
            {
                Stream stream = new FileStream(f.Name,
                              FileMode.Open,
                              FileAccess.Read,
                              FileShare.Read);
                TrainingFacility tf = (TrainingFacility)formatter.Deserialize(stream);
                tf.create();
                allTF.Add(tf);
                stream.Close();
            }

            FileInfo[] files = d.Parent.GetFiles("*koala.bin");
            foreach (FileInfo f in files)
            {
                Stream stream = new FileStream(f.Name,
                              FileMode.Open,
                              FileAccess.Read,
                              FileShare.Read);
                Koala ko = (Koala)formatter.Deserialize(stream);
                koalas.Add(ko);
                stream.Close();
            }
        }
        FileInfo[] fe = d.Parent.GetFiles("*.bin");
        foreach (FileInfo f in fe)
        {
            f.Delete();
        }
    }

    public static void saveGame()
    {
        PlayerPrefs.SetFloat("TimeScale", timeScale);
        int i = 0;
        IFormatter formatter = new BinaryFormatter();
        foreach (Koala k in koalas)
        {
            Stream stream = new FileStream(@Application.dataPath + i++ + "koala.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, k);
            stream.Close();
        }
        i = 0;
        foreach (Facility fac in allTB)
        {
            Stream stream = new FileStream(@Application.dataPath + i++ + "tb.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, fac);
            stream.Close();
        }
        i = 0;
        foreach (TrainingFacility fac in allTF)
        {
            Stream stream = new FileStream(@Application.dataPath + i++ + "tf.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, fac);
            stream.Close();
        }
        {
            pd.setDayCount(days);
            pd.setYearCount(years);
            pd.setMinuteCount(minutes);
            Stream stream = new FileStream(@Application.dataPath + "pd.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, pd);
            stream.Close();
        }
    }

    //Called when game is closed
    void OnApplicationQuit()
    {
        pd.setDayCount(days);
        pd.setYearCount(years);
        pd.setMinuteCount(minutes);
        Debug.Log(pd.getFoodSupply());
        Debug.Log(pd.getWaterSupply());
        PlayerPrefs.SetInt("TimeScale", 1);
        int i = 0;
        IFormatter formatter = new BinaryFormatter();
        foreach (Koala k in koalas)
        {
            Stream stream = new FileStream(@Application.dataPath + i++ + "koala.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, k);
            stream.Close();
        }
        i = 0;
        foreach (Facility fac in allTB)
        {
            Stream stream = new FileStream(@Application.dataPath + i++ + "tb.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, fac);
            stream.Close();
        }
        i = 0;
        foreach (TrainingFacility fac in allTF)
        {
            Stream stream = new FileStream(@Application.dataPath + i++ + "tf.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, fac);
            stream.Close();
        }
        {
            Stream stream = new FileStream(@Application.dataPath + "pd.bin",
                                     FileMode.Create,
                                     FileAccess.Write, FileShare.None);
            formatter.Serialize(stream, pd);
            stream.Close();
        }
    }

    //Each update is equal to 10 minutes (normal)
    void FixedUpdate()
    {
        if(currEvent != null)
        {
            if(currEvent.Equals(Events.METEOR))
            {
                if (catastrophy != null)
                {
                    if (catastrophy.transform.position.y <= 0)
                    {
                        Bounds b = new Bounds();
                        b.SetMinMax(new Vector3(catastrophy.transform.position.x - 10, 0, catastrophy.transform.position.z - 10), new Vector3(catastrophy.transform.position.x + 10, 10, catastrophy.transform.position.z + 10));
                        foreach (GameObject go in GameObject.FindGameObjectsWithTag("inGame"))
                        {
                            if (go.GetComponent<Collider>().bounds.Intersects(b))
                            {
                                if (go.name.ToLower().Contains("training"))
                                {
                                    go.GetComponent<TrainingFacility>().setHP(Random.Range(0f, go.GetComponent<TrainingFacility>().getHP()));
                                    Debug.Log("Training Facility Damaged");
                                    EventLogger.addLog("A training facility has been damaged by the meteor.");
                                }
                                else
                                {
                                    Debug.Log("Facility damaged");
                                    EventLogger.addLog("A " + go.GetComponent<Facility>().getName() + " has been damaged by the meteor.");
                                    if (go.name.ToLower().Contains("water"))
                                    {
                                        go.GetComponent<WaterFacility>().getFacility().setHP(0);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<WaterFacility>().getFacility().getName() + " has been damaged by the Earthquake.");
                                        if (go.GetComponent<WaterFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("observatory"))
                                    {
                                        go.GetComponent<ObservatoryFacility>().getFacility().setHP(0);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<ObservatoryFacility>().getFacility().getName() + " has been damaged by the Earthquake.");
                                        if (go.GetComponent<ObservatoryFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("mining"))
                                    {
                                        go.GetComponent<MiningFacility>().getFacility().setHP(0);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<MiningFacility>().getFacility().getName() + " has been damaged by the Earthquake.");
                                        if (go.GetComponent<MiningFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("storage"))
                                    {
                                        go.GetComponent<WaterFacility>().getFacility().setHP(0);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<WaterFacility>().getFacility().getName() + " has been damaged by the Earthquake.");

                                    }
                                }
                            }
                        }
                        Destroy(catastrophy);
                        catastrophy = null;
                        currEvent = null;
                    }
                    else
                    {
                        catastrophy.transform.Translate(0, -5, 0);
                    }
                }
            }
            else if(currEvent.Equals(Events.DUST_STORM))
            {
                if(catastrophy != null)
                {
                    if(hoursToStopEvent <= 0)
                    {
                        Destroy(catastrophy);
                        catastrophy = null;
                        currEvent = null;
                    }
                }
            }
        }

        minutes++;
        int killedKoalas = 0;

        if (minutes % 60 == 0 && minutes != 0)
        {
            minutes -= 60;
            hours++;
            if(hoursToStopEvent > 0)
            {
                hoursToStopEvent--;
            }
            if(hourPassed != null)
            {
                hourPassed();
            }
        }
        if (hours % 8 == 0 && hours != 0)
        {
            foreach(Koala k in koalas)
            {
                int food = k.getFood();
                int water = k.getWater();
                k.setFood(k.getFood() - 20);
                k.setWater(k.getWater() - 30);
                //if food level is less than 50, start eating. Then check food levels
                if(k.isAlive())
                {
                    if (pd.getFoodSupply() > 0)
                    {
                        if (k.getFood() <= 50 && k.isAlive())
                        {
                            int foodToUse = Mathf.CeilToInt(((100 - k.getFood()) / (DEFAULT_ADDFOOD_PER_UNIT / k.getSize())));
                            if (pd.getFoodSupply() > foodToUse)
                            {
                                k.setFood(100);
                                pd.setFoodSupply(pd.getFoodSupply() - foodToUse);
                                k.setHappy(true);
                            }
                            else
                            {
                                if (k.getFood() + (DEFAULT_ADDFOOD_PER_UNIT / k.getSize()) > 100)
                                {
                                    k.setFood(100);
                                }
                                else
                                {
                                    k.setFood(k.getFood() + (DEFAULT_ADDFOOD_PER_UNIT / k.getSize()));
                                }
                                pd.setFoodSupply(pd.getFoodSupply() - 1);
                            }
                        }
                    }
                    else
                    {
                        k.setHappy(false);
                    }
                    if (pd.getWaterSupply() > 0)
                    {
                        if (k.getWater() <= 50 && k.isAlive())
                        {
                            int waterToUse = Mathf.CeilToInt(((100 - k.getWater()) / (DEFAULT_ADDWATER_PER_UNIT / k.getSize())));
                            if (pd.getWaterSupply() > waterToUse)
                            {
                                k.setWater(100);
                                pd.setWaterSupply(pd.getWaterSupply() - waterToUse);
                                k.setHappy(true);
                            }
                            else
                            {
                                if (k.getWater() + (DEFAULT_ADDWATER_PER_UNIT / k.getSize()) > 100)
                                {
                                    k.setWater(100);
                                }
                                else
                                {
                                    k.setWater(k.getWater() + (DEFAULT_ADDWATER_PER_UNIT / k.getSize()));
                                }
                                pd.setWaterSupply(pd.getWaterSupply() - 1);
                            }
                        }
                    }
                    else
                    {
                        k.setHappy(false);
                    }
                }
                if ((k.getWater() <= 0 || k.getFood()<= 0) && k.isAlive())
                {
                    k.killKoala();
                    EventLogger.addLog(string.Format("{0} is now dead. RIP", k.getName()));
                    killedKoalas++;
                }
            }
            if (killedKoalas > 0)
            {
                List<Koala> allK = new List<Koala>();
                foreach (Koala k in koalas)
                {
                    allK.Add(k);
                    if (killedKoalas > 0 && k.isHappy())
                    {
                        k.setHappy(false);
                        killedKoalas--;
                    }
                }
                foreach(Koala k in allK)
                {
                    koalas.Remove(k);
                }
            }
        }
        if (hours % 25 == 0 && hours != 0)
        {
            REDay++;
            Debug.Log(REDay + " : " + RAND_Day);
            if(REDay == RAND_Day)
            {
                Debug.Log("Equal");
                REDay = 0;
                RAND_Day = Random.Range(3, 6);
                if(Random.Range(0, 100) > 20 && currEvent == null)
                {
                    Debug.Log("Going through event!");
                    int m = Random.Range(0, events.Count-1);
                    int i = 0;
                    foreach (Events e in events)
                    {
                        if (i == m)
                        {
                            Collider col = terrain.GetComponent<Collider>();
                            if (e.Equals(Events.METEOR))
                            {
                                catastrophy = Instantiate(Resources.Load("Meteor", typeof(GameObject)),
                                    new Vector3(Random.Range(col.bounds.min.x, col.bounds.max.x), 300, 
                                    Random.Range(col.bounds.min.z, col.bounds.max.z)), Quaternion.identity) as GameObject;
                                currEvent = Events.METEOR;
                                EventLogger.addLog("Meteor inbound!");
                                Debug.Log("Shit fam we got a meteor on our hands!");
                            }
                            else if (e.Equals(Events.DUST_STORM))
                            {
                                catastrophy = Instantiate(Resources.Load("DustStorm", typeof(GameObject)),
                                    new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                                currEvent = Events.DUST_STORM;
                                EventLogger.addLog("A dust storm makes its way towards your land.");
                                hoursToStopEvent = Random.Range(20, 36);
                            }
                            else if (e.Equals(Events.EARTHQUAKE))
                            {
                                foreach(GameObject go in GameObject.FindGameObjectsWithTag("inGame"))
                                {
                                    if (go.name.ToLower().Contains("training"))
                                    {
                                        go.GetComponent<TrainingFacilityFacility>().getTrainingFacility().setHP(go.GetComponent<TrainingFacilityFacility>().getTrainingFacility().getHP() - Random.Range(0f, .3f));
                                        Debug.Log("Training Facility Damaged");
                                        EventLogger.addLog("A training facility has been damaged by the Earthquake.");
                                        if (go.GetComponent<TrainingFacilityFacility>().getTrainingFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if(go.name.ToLower().Contains("water"))
                                    {
                                        go.GetComponent<WaterFacility>().getFacility().setHP(go.GetComponent<WaterFacility>().getFacility().getHP() - Random.Range(0f, .3f));
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<WaterFacility>().getFacility().getName() + " has been damaged by the Earthquake.");
                                        if (go.GetComponent<WaterFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("observatory"))
                                    {
                                        go.GetComponent<ObservatoryFacility>().getFacility().setHP(go.GetComponent<ObservatoryFacility>().getFacility().getHP() - Random.Range(0f, .3f));
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<ObservatoryFacility>().getFacility().getName() + " has been damaged by the Earthquake.");
                                        if (go.GetComponent<ObservatoryFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("mining"))
                                    {
                                        go.GetComponent<MiningFacility>().getFacility().setHP(go.GetComponent<MiningFacility>().getFacility().getHP() - Random.Range(0f, .3f));
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<MiningFacility>().getFacility().getName() + " has been damaged by the Earthquake.");
                                        if (go.GetComponent<MiningFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("storage"))
                                    {
                                        go.GetComponent<WaterFacility>().getFacility().setHP(go.GetComponent<WaterFacility>().getFacility().getHP() - Random.Range(0f, .3f));
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<WaterFacility>().getFacility().getName() + " has been damaged by the Earthquake.");

                                    }
                                }
                                EventLogger.addLog("Earthquake (Marsquake?) tremors begin...");
                            }
                            else if (e.Equals(Events.SOLAR_FLARE))
                            {
                                foreach (GameObject go in GameObject.FindGameObjectsWithTag("inGame"))
                                {
                                    if (go.name.ToLower().Contains("training"))
                                    {
                                        go.GetComponent<TrainingFacilityFacility>().getTrainingFacility().setHP(go.GetComponent<TrainingFacilityFacility>().getTrainingFacility().getHP() - .3f);
                                        Debug.Log("Training Facility Damaged");
                                        EventLogger.addLog("A training facility has been damaged by the solar flare.");
                                        if (go.GetComponent<TrainingFacilityFacility>().getTrainingFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("water"))
                                    {
                                        go.GetComponent<WaterFacility>().getFacility().setHP(go.GetComponent<WaterFacility>().getFacility().getHP() - .3f);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<WaterFacility>().getFacility().getName() + " has been damaged by the Solar Flare.");
                                        if (go.GetComponent<WaterFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("observatory"))
                                    {
                                        go.GetComponent<ObservatoryFacility>().getFacility().setHP(go.GetComponent<ObservatoryFacility>().getFacility().getHP() - .3f);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<ObservatoryFacility>().getFacility().getName() + " has been damaged by the Solar Flare.");
                                        if (go.GetComponent<ObservatoryFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("mining"))
                                    {
                                        go.GetComponent<MiningFacility>().getFacility().setHP(go.GetComponent<MiningFacility>().getFacility().getHP() - .3f);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<MiningFacility>().getFacility().getName() + " has been damaged by the Solar Flare.");
                                        if (go.GetComponent<MiningFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                    else if (go.name.ToLower().Contains("storage"))
                                    {
                                        go.GetComponent<WaterFacility>().getFacility().setHP(go.GetComponent<WaterFacility>().getFacility().getHP() - .3f);
                                        Debug.Log("Facility damaged");
                                        EventLogger.addLog("A " + go.GetComponent<WaterFacility>().getFacility().getName() + " has been damaged by the Solar Flare.");
                                        if (go.GetComponent<WaterFacility>().getFacility().getHP() <= 0)
                                        {
                                            Destroy(go);
                                        }
                                    }
                                }
                                EventLogger.addLog("A solar flare has emerged.");
                            }
                            break;
                        }
                        i++;
                    }
                }
            }
            hours -= 25;
            days++;
            if(dayPassed != null)
            {
                dayPassed();
            }
            int happyCount = 0;
            foreach(Koala k in koalas)
            {
                if(k.getDayBorn()%days == 0 && k.getDayBorn() != 0)
                {
                    k.incrementAge();
                    if (k.getAge() >= 6 && k.getSkill().Equals(Skill.CHILD) && k.isAlive())
                    {
                        k.setSkill(Skill.LABORER);
                        EventLogger.addLog(k.getName() + " is now a laborer!");
                    }
                    else if(k.getAge() >= 40 && k.isAlive())
                    {
                        k.killKoala();
                    }
                    if(k.getAge() >= 10 && !k.hadKid())
                    {
                        if(Random.Range(0,1) > 80)
                        {
                            System.Random rnd = new System.Random();
                            Storage.koalas.Add(new Koala(NameDictionary.firstNames[rnd.Next(NameDictionary.firstNames.Length)] + " " + NameDictionary.lastNames[rnd.Next(NameDictionary.lastNames.Length)],
                                0, rnd.Next(2) + 1, rnd.Next(2) + 1, rnd.Next(2) + 1, Storage.days, (int)Random.Range(1, 3)));
                            k.setHaveKid(true);
                        }
                    }
                    k.setHappy(true);
                }
                if(k.isHappy() && k.isAlive())
                {
                    happyCount++;
                }
            }
            pd.setHappyLevel(happyCount / ((koalas.Count > 0) ? koalas.Count : 1));
        }
        if (days % 687 == 0 && days != 0)
        {
            days -= 687;
            years++;
            if(yearPassed != null)
            {
                yearPassed();
            }
        }
    }

    public static void setTimeScale(float timeScale)
    {
        Time.timeScale = timeScale;
        Storage.timeScale = timeScale;
    }
}
