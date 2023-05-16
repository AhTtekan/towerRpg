using System.Linq;
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
    public static Random RR = new Random();

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

    public static IWeighted GetRandomWeightedObject(IWeighted[] options)
    {
        int totalWeight = options.Sum(x => x.Weight), random = Random.Range(0, totalWeight), cumulativeWeight = 0;
        foreach (var option in options)
        {
            cumulativeWeight += option.Weight;
            Debug.Log("Random value is " + random + ", cumulative value is " + cumulativeWeight);
            if (random < cumulativeWeight)
            {
                return option;
            }
        }

        throw new System.Exception("Unable to find random weighted object.");
    }

    internal static int Clamp(this int value, int min, int max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }

    internal static float Clamp(this float value, float min, float max)
    {
        return (value < min) ? min : (value > max) ? max : value;
    }
}