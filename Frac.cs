namespace Fractionals;

public record Fractional(long Nom, long Denom)
{
    public double ToDouble()
    {
        if (Denom == 0)
            return double.PositiveInfinity;

        return Nom / (double)Denom;
    }

    public string ToStringImproper()
    {
        return $"{Nom}/{Denom}";
    }

    public string ToStringProper()
    {
        long integral = Nom / Denom;
        long nom = Nom;
        nom %= Denom;

        if (integral == 0)
        {
            return ToStringImproper();
        }

        if (nom == 0)
        {
            return $"{integral}";
        }

        char sign = nom >= 0 ? '+' : '-';

        return $"{integral} {sign} {Math.Abs(nom)}/{Denom}";
    }

    public Fractional Normalize()
    {
        // Greatest Common Divisor
        static long GCD(long a, long b)
        {
            a = Math.Abs(a);
            b = Math.Abs(b);
            while (b != 0)
            {
                (a, b) = (b, a % b);
            }
            return a;
        }

        if (Denom == 0)
        {
            // 1/0 is Inf, 0/0 is NaN.
            return new(1, 0);
        }

        long gcd = GCD(Nom, Denom);

        return new(Nom / gcd * (Denom < 0 ? -1 : 1), Math.Abs(Denom / gcd));
    }

    public static Fractional operator +(Fractional a, Fractional b)
    {
        a = a.Normalize();
        b = b.Normalize();
        return new Fractional(a.Nom * b.Denom + a.Denom * b.Nom, a.Denom * b.Denom).Normalize();
    }

    public static Fractional operator -(Fractional a, Fractional b)
    {
        a = a.Normalize();
        b = b.Normalize();
        return new Fractional(a.Nom * b.Denom - a.Denom * b.Nom, a.Denom * b.Denom).Normalize();
    }

    public static Fractional operator *(Fractional a, Fractional b)
    {
        a = a.Normalize();
        b = b.Normalize();
        return new Fractional(a.Nom * b.Nom, a.Denom * b.Denom).Normalize();
    }

    public static Fractional operator /(Fractional a, Fractional b)
    {
        return a * new Fractional(b.Denom, b.Nom);
    }
}
