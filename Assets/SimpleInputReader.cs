using UnityEngine;

public class SimpleInputReader : InputReader
{
    Vector3 LastFrameDir;

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
            SendOnCancel();
        if (Input.GetButtonDown("Submit"))
            SendOnSubmit();
        Vector3 input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        if (!(input == Vector3.zero && LastFrameDir == Vector3.zero))
            SendOnMovement(input);
        LastFrameDir = input;
    }
}