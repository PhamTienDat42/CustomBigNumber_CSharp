using System;
using UnityEngine;

public enum EBigIntPower
{
    A = 1,
    B = 2,
    C = 3,
    D = 4,
    E = 5,
    F = 6,
    G = 7,
    H = 8,
    I = 9,
    J = 10,
    K = 11,
    L = 12,
    M = 13,
    N = 14,
    O = 15,
    P = 16,
    Q = 17,
    R = 18,
    S = 19,
    T = 20,
    U = 21,
    V = 22,
    W = 23,
    X = 24,
    Y = 25,
    Z = 26,
}

public class CustomBigValue
{
    public double root;
    public int idMultiple;

    private const double epsilon = 1e-10;
    private const double tenCubed = 1e3;
    private const int maxEnumValue = 26;
    private const int roundEpsilon = 4;

    public override string ToString()
    {
        return $"{root}E{idMultiple}";
    }

    public string ToVisualString(int decimalAmount = 2)
    {
        if (idMultiple == 0)
        {
            double value = Math.Round(root, decimalAmount);
            return value.ToString();
        }
        string suffix = string.Empty;
        int remainingPower = idMultiple;
        while (remainingPower >= maxEnumValue)
        {
            suffix = EBigIntPower.Z.ToString() + suffix;
            remainingPower -= maxEnumValue;
        }
        suffix = suffix + ((EBigIntPower)remainingPower).ToString();
        double roundedRoot = Math.Round(root, decimalAmount);
        string stringDecimal = string.Empty;
        for (int i = 0; i < decimalAmount; i++)
        {
            stringDecimal += '#';
        }
        return $"{roundedRoot.ToString($"0.{stringDecimal}")}{suffix}";
    }

    public void RoundValue()
    {
        if (idMultiple <= roundEpsilon)
        {
            root = Math.Round(root, idMultiple * 3);
        }
    }

    public CustomBigValue()
    {
        root = 0;
        idMultiple = 0;
    }

    public CustomBigValue(double root, int idMultiple)
    {
        this.root = root;
        this.idMultiple = idMultiple;
    }

    public CustomBigValue(CustomBigValue customBigInt)
    {
        this.root = customBigInt.root;
        this.idMultiple = customBigInt.idMultiple;
    }

    public CustomBigValue(string value)
    {
        if (value.Contains("E"))
        {
            var val = value.Split("E");
            this.root = double.Parse(val[0]);
            this.idMultiple = int.Parse(val[1]);
        }
        else if (double.TryParse(value, out double result))
        {
            this.root = result;
            Normalize();
        }
        else
            Debug.LogError($"Undefine Input {value}");
    }

    public void Normalize()
    {
        if (root != 0)
        {
            double sign = Math.Sign(root);
            root = Math.Abs(root);

            while (root >= tenCubed)
            {
                root /= tenCubed;
                idMultiple++;
            }
            while (root < 1)
            {
                root *= tenCubed;
                idMultiple--;
            }

            root *= sign;
        }
    }

    public void NormalizeTo(int _idMultiple)
    {
        if (idMultiple != _idMultiple)
        {
            var dif = idMultiple - _idMultiple;
            var step = Math.Sign(dif);

            while (dif != 0)
            {
                var temp = Math.Abs(dif) > 10 ? 10 * step : dif;

                root *= Math.Pow(tenCubed, temp);
                idMultiple -= temp;
                dif -= temp;
            }
        }
    }

    public static implicit operator CustomBigValue(double numb)
    {
        return new CustomBigValue(numb, 0);
    }
    public static implicit operator int(CustomBigValue customBigInt)
    {
        return (int)((double)customBigInt);
    }
    public static implicit operator long(CustomBigValue customBigInt)
    {
        return (long)((double)customBigInt);
    }
    public static implicit operator float(CustomBigValue customBigInt)
    {
        return (float)((double)customBigInt);
    }
    public static implicit operator double(CustomBigValue customBigInt)
    {
        if (customBigInt < double.MinValue || customBigInt > double.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        return customBigInt.root * Math.Pow(tenCubed, customBigInt.idMultiple);
    }
    public override bool Equals(object obj)
    {
        if (obj is CustomBigValue)
        {
            return (obj as CustomBigValue) == this;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    #region CustomBigInt vs CustomBigInt
    public static bool operator !=(CustomBigValue a, CustomBigValue b)
    {
        if (ReferenceEquals(a, null))
        {
            return !ReferenceEquals(b, null);
        }
        if (ReferenceEquals(b, null))
        {
            return true;
        }
        return a.idMultiple != b.idMultiple || Math.Abs(a.root - b.root) > epsilon;
    }

    public static bool operator ==(CustomBigValue a, CustomBigValue b)
    {
        if (ReferenceEquals(a, null))
        {
            return ReferenceEquals(b, null);
        }
        if (ReferenceEquals(b, null))
        {
            return false;
        }
        return a.idMultiple == b.idMultiple && Math.Abs(a.root - b.root) < epsilon;
    }

    public static bool operator >(CustomBigValue a, CustomBigValue b)
    {
        if (a.root >= 0 && b.root < 0)
        {
            return true;
        }
        else if (a.root < 0 && b.root >= 0)
        {
            return false;
        }

        if (a.root >= 0 && b.root >= 0)
        {
            if (a.idMultiple > b.idMultiple)
                return true;
            else if (a.idMultiple < b.idMultiple)
                return false;
            else if (a.root > b.root)
                return true;
            return false;
        }
        else
        {
            if (a.idMultiple > b.idMultiple)
                return false;
            else if (a.idMultiple < b.idMultiple)
                return true;
            else if (a.root > b.root)
                return true;
            return false;
        }
    }

    public static bool operator >=(CustomBigValue a, CustomBigValue b)
    {
        return a > b || a == b;
    }

    public static bool operator <(CustomBigValue a, CustomBigValue b)
    {
        return !(a >= b);
    }

    public static bool operator <=(CustomBigValue a, CustomBigValue b)
    {
        return !(a > b);
    }

    public static CustomBigValue operator /(CustomBigValue a, CustomBigValue b)
    {
        // ignore case div by bigger number
        // ignore case div by number < 1
        // ignore case div by 0
        if (b == 0)
            throw new DivideByZeroException("Can't devide by zero");
        CustomBigValue _a = new CustomBigValue(a);
        _a.root /= b.root;
        _a.idMultiple -= b.idMultiple;
        _a.Normalize();
        return _a;
    }

    public static CustomBigValue operator *(CustomBigValue a, CustomBigValue b)
    {
        CustomBigValue _a = new CustomBigValue(a);
        _a.root *= b.root;
        _a.idMultiple += b.idMultiple;
        _a.Normalize();
        return _a;
    }

    public static CustomBigValue operator +(CustomBigValue a, CustomBigValue b)
    {
        CustomBigValue _a = new CustomBigValue(a);
        _a.NormalizeTo(b.idMultiple);
        _a.root += b.root;
        _a.Normalize();
        return _a;
    }

    public static CustomBigValue operator -(CustomBigValue a, CustomBigValue b)
    {
        CustomBigValue _a = new CustomBigValue(a);
        _a.NormalizeTo(b.idMultiple);
        _a.root -= b.root;
        _a.Normalize();
        return _a;
    }

    public static CustomBigValue operator -(CustomBigValue a)
    {
        return new CustomBigValue(-a.root, a.idMultiple);
    }

    public static CustomBigValue operator ++(CustomBigValue a)
    {
        return a + 1;
    }

    public static CustomBigValue operator --(CustomBigValue a)
    {
        return a - 1;
    }

    public static CustomBigValue operator %(CustomBigValue a, CustomBigValue b)
    {
        return a - (a / b) * b;
    }

    public static CustomBigValue operator ^(CustomBigValue a, int power)
    {
        CustomBigValue _a = new CustomBigValue(a);
        for (int i = 1; i < power; i++)
        {
            _a *= a;
        }
        return _a;
    }
    #endregion
}