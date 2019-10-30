[System.Serializable]
public class APCore
{
    public int APBuildRateInSeconds { get; set; }

    public float AP_Current
    {
        get
        {
            return ap_Current.Clamp(0, AP_Max);
        }
        set
        {
            ap_Current = value;
        }
    }
    private float ap_Current;

    public float AP_Max { get; set; }
}
