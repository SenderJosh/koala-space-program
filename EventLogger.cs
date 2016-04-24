using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Linq;

public class EventLogger : MonoBehaviour {

    private static Stack<String> logs = new Stack<String>();
    private static GameObject g;

    void Start()
    {
        g = GameObject.FindGameObjectWithTag("eventlogger_text");
    }

    public static String getLogs()
    {
        string str = "";
        foreach(string s in logs)
        {
            str += s + Environment.NewLine;
        }
        return str;
    }

    public static String getLast10()
    {
        String str = "";
        string[] s = new string[10];
        for (int i = 0; i<10; i++)
        {
            if(logs.Count > 0)
            {
                string it = logs.Pop();
                str += it + Environment.NewLine;
                s[i] = it;
            }
            else
            {
                break;
            }
        }
        foreach(string blah in s)
        {
            if(!string.IsNullOrEmpty(blah))
            {
                logs.Push(blah);
            }
            Debug.Log("--------------------------------------Worked" + blah);
        }
        return str;
    }

    public static void clearLogs()
    {
        logs.Clear();
    }

    public static void addLog(String s)
    {
        logs.Push(s);
        if(g != null)
        {
            if (g.activeInHierarchy)
            {
                g.GetComponent<Text>().text = getLogs();
            }
        }
    }


}
