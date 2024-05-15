using System;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public Matrix2x2 RotationMatrix { get; private set; }
    public Matrix2x2 InverseMatrix => RotationMatrix.Transpose();       // Inverse of a rotation matrix is its transposition
    
    public Vector2 Up => RotationMatrix.Column(1);
    public Vector2 Right => RotationMatrix.Column(0);
    public Vector2 Down => -Up;
    public Vector2 Left  => -Right;
    
    public Quaternion RotationQuaternion { get; private set; }

    public event Action OnGravityChanged;

    public void Awake()
    {
        RotationMatrix = CanonicalRotation;
        CalculateQuaternion();
    }

    public readonly Matrix2x2 CanonicalRotation = Matrix2x2.Identity;
    
    public void RotateTo(GravityDirectionSo direction)
    {
        RotationMatrix = new Matrix2x2(direction.up, direction.right);
        CalculateQuaternion();
        OnGravityChanged?.Invoke();
    }
    
    public void RotateBy(GravityDirectionSo direction)
    {
        RotationMatrix = new Matrix2x2(direction.up, direction.right) * RotationMatrix;
        CalculateQuaternion();
        OnGravityChanged?.Invoke();
    }

    public Vector2 ApplyMatrix(Vector2 vector)
    {
        return RotationMatrix * vector;
    }

    public Vector2 ApplyInverse(Vector2 vector)
    {
        return InverseMatrix * vector;
    }

    private void CalculateQuaternion()
    {
        RotationQuaternion = Quaternion.FromToRotation(Vector2.up, Up);
    }
    
}
