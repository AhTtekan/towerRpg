using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class Utility
{
    public static void DestroyAllChildren(this Transform transform)
    {
        foreach (Transform child in transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}

public static class GUIUtility
    {

        public static void DimGUI(Transform t, float alpha = 0.7f)
        {
            foreach (Transform child in t)
                DimGUI(child, alpha);
            Color co;
            Image im = t.GetComponent<Image>();
            Text tx = t.GetComponent<Text>();
            //outline alpha mimics text alpha
            if (im != null)
            {
                co = im.color;
                co.a = alpha;
                im.color = co;
            }
            if (tx != null)
            {
                co = tx.color;
                co.a = alpha;
                tx.color = co;
            }
        }
    }

public static class MathUtility
{
    public static System.Random RR = new System.Random();
    public static int Truncate(float d)
    {
        int output = 0;
        string s = d.ToString();
        if (s.IndexOf('.') > 0)
        {
            output = int.Parse(s.Substring(0, s.IndexOf('.')));
        }
        else
        {
            output = int.Parse(s);
        }
        return output;
    }

    public static int Random(int lowerBound, int upperBound)
    {
        int r = RR.Next(lowerBound, upperBound);
        return r;
    }

    internal static int Clamp(int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}