using UnityEngine;

public class GravityController : MonoBehaviour
{
    
    public Matrix2x2 RotationMatrix { get; private set; }
    public Vector2 Up => RotationMatrix.Column(1);
    public Vector2 Right => RotationMatrix.Column(0);
    public Vector2 Down => -Up;
    public Vector2 Left  => -Right;

    public void Awake()
    {
        RotationMatrix = CanonicalRotation;
    }

    public readonly Matrix2x2 CanonicalRotation = Matrix2x2.Identity;
    
    public void RotateTo(GravityDirectionSo direction)
    {
        RotationMatrix = new Matrix2x2(direction.up, direction.right);
    }
    
    public void RotateBy(GravityDirectionSo direction)
    {
        RotationMatrix = new Matrix2x2(direction.up, direction.right) * RotationMatrix;
    }
}
