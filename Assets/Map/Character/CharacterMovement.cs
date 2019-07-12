using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
#pragma warning disable 0649
    [SerializeField]
    Animator _animator;
    [SerializeField]
    Rigidbody2D _rigidbody2D;
    [SerializeField]
    InputReader _reader;
#pragma warning restore 0649

    // Update is called once per frame
    void Awake()
    {
        _reader.OnMovement += Move;
    }

    private void OnDestroy()
    {
        _reader.OnMovement -= Move;
    }

    private void Move(Vector3 movement)
    {
        _rigidbody2D.velocity = new Vector2(movement.x, movement.y);

        SetAnimationVariables(movement);
    }

    private void SetAnimationVariables(Vector3 movement)
    {
        _animator.SetFloat("Horizontal", movement.x);
        _animator.SetFloat("Vertical", movement.y);
        _animator.SetFloat("Magnitude", movement.magnitude);
    }
}
