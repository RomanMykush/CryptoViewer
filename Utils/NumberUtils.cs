using System;
using System.Globalization;
using System.Numerics;

namespace CryptoViewer.Utils;

public static class NumberUtils
{
    public static string ToKmbFormat<T>(this T num) where T : INumber<T>
    {
        decimal dec = Convert.ToDecimal(num);

        if (Math.Abs(dec) >= 1000000000)
            return num.ToString("0,,,.###B", CultureInfo.InvariantCulture);

        if (Math.Abs(dec) >= 1000000)
            return num.ToString("0,,.##M", CultureInfo.InvariantCulture);

        if (Math.Abs(dec) >= 1000)
            return num.ToString("0,.#K", CultureInfo.InvariantCulture);

        return dec.ToString(CultureInfo.InvariantCulture);
    }
}
