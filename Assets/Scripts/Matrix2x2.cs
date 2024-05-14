using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Matrix2x2
{
    const int ROWS = 2;
    const int COLUMNS = 2;
    
    private float[,] _vals;

    public static Matrix2x2 Identity => new(Vector2.up, Vector2.right);
    
    public Matrix2x2(float intialValues = 0)
    {
        _vals = new float[ROWS, COLUMNS];
        Fill(intialValues);
    }
    
    public Matrix2x2(float[,] values)
    {
        _vals = values;
    }

    public Matrix2x2(Vector2 up, Vector2 right)
    {
        _vals = new float[ROWS, COLUMNS];
        _vals[0, 0] = right.x;
        _vals[0, 1] = right.y;
        _vals[1, 0] = up.x;
        _vals[1, 1] = up.y;
    }
    
    public static Matrix2x2 operator +(Matrix2x2 m1, Matrix2x2 m2)
    {
        var result = new Matrix2x2();
        
        for (int row = 0; row < ROWS; row++)
        {
            for (int col = 0; col < COLUMNS; col++)
            {
                result[row, col] = m1[row, col] + m2[row, col];
            }
        }
        
        return result;
    }

    public static Matrix2x2 operator *(Matrix2x2 m1, Matrix2x2 m2)
    {
        var result = new Matrix2x2();
        
        for (int row = 0; row < ROWS; row++)
        {
            for (int col = 0; col < COLUMNS; col++)
            {
                result[row, col] = Vector2.Dot(m1.Row(row), m2.Column(col));
            }
        }
        
        return result;
    }
    
    public static Vector2 operator *(Matrix2x2 m, Vector2 v)
    {
        float x = Vector2.Dot(m.Row(0), v);
        float y = Vector2.Dot(m.Row(1), v);
        return new Vector2(x, y);
    }
    
    public float this[int row, int col]
    {
        get => _vals[row, col];
        set => _vals[row, col] = value;
    }

    public Matrix2x2 Fill(float value)
    {
        for (int i = 0; i < ROWS; i++)
        {
            for (int j = 0; j < COLUMNS; j++)
            {
                _vals[i, j] = value;
            }
        }

        return this;
    }
    
    public Vector2 Column(int index)
    {
        return new Vector2(_vals[0, index], _vals[1, index]);
    }
    
    public Vector2 Row(int index)
    {
        return new Vector2(_vals[index, 0], _vals[index, 1]);
    }

    public IEnumerable<Vector2> Columns()
    {
        for (int i = 0; i < COLUMNS; i++)
        {
            yield return Column(i);
        }
    }
    
    public IEnumerable<Vector2> Rows()
    {
        for (int i = 0; i < ROWS; i++)
        {
            yield return Row(i);
        }
    }
}
