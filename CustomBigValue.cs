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

public static class CustomBigValueHelper
{
    public static CustomBigValue Pow(this CustomBigValue bigInt, int power)
    {
        CustomBigValue returnValue = new CustomBigValue(bigInt);
        while (power > 0)
        {
            returnValue *= bigInt;
            power--;
        }
        return returnValue;
    }

    public static int ToInt32(this CustomBigValue bigInt)
    {
        if (bigInt < int.MinValue || bigInt > int.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        var temp = new CustomBigValue(bigInt);
        temp.NormalizeTo(0);
        return (int)temp.Root;
    }

    public static long ToInt64(this CustomBigValue bigInt)
    {
        if (bigInt < long.MinValue || bigInt > long.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        var temp = new CustomBigValue(bigInt);
        temp.NormalizeTo(0);
        return (long)temp.Root;
    }

    public static float ToFloat(this CustomBigValue bigInt)
    {
        if (bigInt < float.MinValue || bigInt > float.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        var temp = new CustomBigValue(bigInt);
        temp.NormalizeTo(0);
        return (float)temp.Root;
    }
}

public class CustomBigValue
{
    public double root;
    public int idMultiple;

    private const double epsilon = 1e-10;
    private const double tenCubed = 1e3;
    private const int maxEnumValue = 26;
    private const int roundEpsilon = 4;

    public double Root { get => root; set => root = value; }
    public int IdMultiple { get => idMultiple; set => idMultiple = value; }

    public override string ToString()
    {
        return $"{root}E{idMultiple}";
    }

    public string ToVisualString()
    {
        if (idMultiple == 0)
        {
            double value = Math.Round(root, 2);
            return value.ToString();
        }

        string suffix = "";
        int remainingPower = idMultiple;
        while (remainingPower >= maxEnumValue)
        {
            suffix = EBigIntPower.Z.ToString() + suffix;
            remainingPower -= maxEnumValue;
        }
        suffix = suffix + ((EBigIntPower)remainingPower).ToString();
        double roundedRoot = Math.Round(root, 2);

        return $"{roundedRoot.ToString("0.##")}{suffix}";
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
            this.idMultiple = int.Parse(val[2]);
        }
        Debug.LogError($"Undefine Input {value}");
    }

    public void Normalize()
    {
        double sign = Math.Sign(root);
        root = Math.Abs(root);

        while (root >= tenCubed)
        {
            root /= tenCubed;
            idMultiple++;
        }
        while (root < 1 && idMultiple > 0)
        {
            root *= tenCubed;
            idMultiple--;
        }

        root *= sign;
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

    public static implicit operator CustomBigValue(int numb)
    {
        return CustomBigValue.Parse(numb);
    }

    public static implicit operator CustomBigValue(long numb)
    {
        return CustomBigValue.Parse(numb);
    }

    public static implicit operator CustomBigValue(float numb)
    {
        return CustomBigValue.Parse(numb);
    }

    public static implicit operator int(CustomBigValue customBigInt)
    {
        if (customBigInt < int.MinValue || customBigInt > int.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        var temp = new CustomBigValue(customBigInt);
        temp.NormalizeTo(0);
        return (int)(temp.root);
    }

    public static implicit operator long(CustomBigValue customBigInt)
    {
        if (customBigInt < long.MinValue || customBigInt > long.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        var temp = new CustomBigValue(customBigInt);
        temp.NormalizeTo(0);
        return (long)(temp.root);
    }

    public static implicit operator float(CustomBigValue customBigInt)
    {
        if (customBigInt < float.MinValue || customBigInt > float.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        var temp = new CustomBigValue(customBigInt);
        temp.NormalizeTo(0);
        return (float)(temp.root);
    }

    public static implicit operator double(CustomBigValue customBigInt)
    {
        if (customBigInt < double.MinValue || customBigInt > double.MaxValue)
        {
            Debug.LogError("Over Flow");
            return 0;
        }
        var temp = new CustomBigValue(customBigInt);
        temp.NormalizeTo(0);
        return temp.root;
    }

    public static CustomBigValue Create(double root, int idMultiple = 0)
    {
        CustomBigValue cBi = new(root, idMultiple);
        cBi.Normalize();
        return cBi;
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

    #region CustomBigValue vs CustomBigValue
    public static bool operator !=(CustomBigValue a, CustomBigValue b)
    {
        return a.idMultiple != b.idMultiple || Math.Abs(a.root - b.root) > epsilon;
    }

    public static bool operator ==(CustomBigValue a, CustomBigValue b)
    {
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

    public static bool operator <(CustomBigValue a, CustomBigValue b)
    {
        return !(a >= b);
    }

    public static bool operator >=(CustomBigValue a, CustomBigValue b)
    {
        return a > b || a == b;
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
        if (b.root <= 0)
            throw new DivideByZeroException("Can't devide by zero");
        double newRoot = a.root / b.root;
        int newIdMultiple = a.idMultiple - b.idMultiple;
        return CustomBigValue.Create(newRoot, newIdMultiple);
    }

    public static CustomBigValue operator *(CustomBigValue a, CustomBigValue b)
    {
        return CustomBigValue.Create(a.root * b.root, a.idMultiple + b.idMultiple);
    }

    public static CustomBigValue operator -(CustomBigValue a, CustomBigValue b)
    {
        CustomBigValue _b = new CustomBigValue(b);
        _b.NormalizeTo(a.idMultiple);
        return Create(a.root - _b.root, a.idMultiple);
    }

    public static CustomBigValue operator +(CustomBigValue a, CustomBigValue b)
    {
        CustomBigValue _b = new CustomBigValue(b);
        _b.NormalizeTo(a.idMultiple);
        return Create(a.root + _b.root, a.idMultiple);
    }

    public static CustomBigValue operator -(CustomBigValue a)
    {
        return CustomBigValue.Create(-a.root, a.idMultiple);
    }
    #endregion
    #region CustomBigInt vs Double
    public static CustomBigValue Parse(double numb)
    {
        return CustomBigValue.Create(numb, 0);
    }

    public static CustomBigValue operator *(CustomBigValue a, double b)
    {
        return CustomBigValue.Create(a.root * Parse(b), a.idMultiple);
    }

    public static CustomBigValue operator /(CustomBigValue a, double b)
    {
        return CustomBigValue.Create(a.root / Parse(b), a.idMultiple);
    }

    public static bool operator ==(CustomBigValue a, double b)
    {
        return a == CustomBigValue.Parse(b);
    }

    public static bool operator !=(CustomBigValue a, double b)
    {
        return a != CustomBigValue.Parse(b);
    }

    public static bool operator >(CustomBigValue a, double b)
    {
        return a > CustomBigValue.Parse(b);
    }

    public static bool operator <(CustomBigValue a, double b)
    {
        return a < CustomBigValue.Parse(b);
    }

    public static bool operator >=(CustomBigValue a, double b)
    {
        return a >= CustomBigValue.Parse(b);
    }

    public static bool operator <=(CustomBigValue a, double b)
    {
        return a <= CustomBigValue.Parse(b);
    }

    public static CustomBigValue operator +(CustomBigValue a, double b)
    {
        return a + CustomBigValue.Parse(b);
    }

    public static CustomBigValue operator -(CustomBigValue a, double b)
    {
        return a - CustomBigValue.Parse(b);
    }
    #endregion
}