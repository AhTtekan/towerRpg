using UnityEngine;

public abstract class InputReader : MonoBehaviour
{
    public delegate void OnMovementEventHandler(Vector3 dir);
    public delegate void OnCancelEventHandler();
    public delegate void OnSubmitEventHandler();

    public event OnMovementEventHandler OnMovement;
    public event OnCancelEventHandler OnCancel;
    public event OnSubmitEventHandler OnSubmit;
    
    protected void SendOnMovement(Vector3 dir)
    {
        OnMovement?.Invoke(dir);
    }
    protected void SendOnCancel()
    {
        OnCancel?.Invoke();
    }
    protected void SendOnSubmit()
    {
        OnSubmit?.Invoke();
    }
}
