using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityController : MonoBehaviour
{
    public enum Direction { Up, Down, Left, Right }
    
    public Matrix2x2 RotationMatrix { get; private set; }
    public Vector2 Up => RotationMatrix.Column(1);
    public Vector2 Right => RotationMatrix.Column(0);
    public Vector2 Gravity => -Up;
    public Vector2 Left  => -Right;

    public void Awake()
    {
        RotationMatrix = CanonicalRotation;
    }

    public readonly Matrix2x2 CanonicalRotation = new(new float[,]
    {
        { 1, 0 },
        { 0, 1 }
    });
    
    public readonly Matrix2x2 LeftRotation = new(new float[,]
    {
        { 0, 1 },
        { -1, 0 }
    });
    
    public readonly Matrix2x2 RightRotation = new(new float[,]
    {
        { 0, -1 },
        { 1, 0 }
    });
    
    public void RotateTo(Direction direction)
    {
        switch (direction)
        {
            case Direction.Up:
                RotationMatrix = CanonicalRotation;
                break;
            case Direction.Down:
                RotationMatrix = LeftRotation * LeftRotation;
                break;
            case Direction.Left:
                RotationMatrix = LeftRotation;
                break;
            case Direction.Right:
                RotationMatrix = RightRotation;
                break;
        }
    }
}
