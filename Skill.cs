using System.Collections.Generic;

[System.Serializable]
public class Skill {

    //687 days in a year
    //string name, int timeToLearnInDays
    public static readonly Skill CHILD = new Skill("Child", 0);
    public static readonly Skill ENGINEER = new Skill("Engineer", 28);
    public static readonly Skill SCIENTIST = new Skill("Scientist", 28);
    public static readonly Skill FARMER = new Skill("Farmer", 7);
    public static readonly Skill LABORER = new Skill("Farmer", 0);

    public static IEnumerable<Skill> Values
    {
        get
        {
            yield return CHILD;
            yield return ENGINEER;
            yield return SCIENTIST;
        }
    }

    private readonly string sName;
    private readonly int timeToLearn;

    Skill(string sName, int timeToLearn)
    {
        this.sName = sName;
        this.timeToLearn = timeToLearn;
    }

    public string Name { get { return this.sName; } }
    public int TimeToLearn { get { return this.timeToLearn; } }

}
