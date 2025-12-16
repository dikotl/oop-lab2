using Fractionals;

class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine($"""
            # Normalize Test
                Input       {"Output",-11} Expected
                4/6         {new Fractional(4L, 6L).Normalize().ToStringImproper(),-11} 2/3
                6/5         {new Fractional(6L, 4L).Normalize().ToStringImproper(),-11} 3/2
                6/-4        {new Fractional(6L, -4L).Normalize().ToStringImproper(),-11} -3/2
                -6/4        {new Fractional(-6L, 4L).Normalize().ToStringImproper(),-11} -3/2
                -6/-4       {new Fractional(-6L, -4L).Normalize().ToStringImproper(),-11} 3/2
            """);

        Console.WriteLine($"""
            # ToStringProper Test (not normalized)
                Input       {"Output",-11} Expected
                3/1         {new Fractional(3L, 1L).ToStringProper(),-11} 3
                3/-1        {new Fractional(3L, -1L).ToStringProper(),-11} -3
                -3/1        {new Fractional(-3L, 1L).ToStringProper(),-11} -3
                -3/-1       {new Fractional(-3L, -1L).ToStringProper(),-11} 3
                3/2         {new Fractional(3L, 2L).ToStringProper(),-11} 1 + 1/2
                3/-2        {new Fractional(3L, -2L).ToStringProper(),-11} -1 + 1/-2
                -3/2        {new Fractional(-3L, 2L).ToStringProper(),-11} -1 - 1/2
                -3/-2       {new Fractional(-3L, -2L).ToStringProper(),-11} 1 - 1/-2
            """);

        Console.WriteLine("# CalcExpr1 Test");

        for (int i = 0; i < 6; i++)
        {
            Fractional x = CalcExpr1(i).Normalize();
            Fractional y = new Fractional(i, i + 1L).Normalize(); // n/(n+1)

            string myImpl = $"CalcExpr1 => {x.ToStringImproper()} ({x.ToDouble()})";
            string check = $"n/(n+1) => {y.ToStringImproper()} ({y.ToDouble()})";
            Console.WriteLine($"    n={i}: {myImpl,-44} {check}");
        }

        Console.WriteLine("# CalcExpr2 Test");

        for (int i = 0; i < 6; i++)
        {
            Fractional x = CalcExpr2(i).Normalize();
            Fractional y = new Fractional(i + 1L, 2L * i).Normalize(); // (n+1)/(2n)

            string myImpl = $"CalcExpr2 => {x.ToStringImproper()} ({x.ToDouble()})";
            string check = $"(n+1)/(2n) => {y.ToStringImproper()} ({y.ToDouble()})";
            Console.WriteLine($"    n={i}: {myImpl,-44} {check}");
        }
    }

    static Fractional CalcExpr1(int n)
    {
        if (n < 1)
            return new(0, 1);

        var result = new Fractional(1, 2);

        for (int i = 2; i <= n; ++i)
        {
            var next = new Fractional(1, i * (i + 1));
            result += next;
        }

        return result;
    }

    static Fractional CalcExpr2(int n)
    {
        if (n < 1)
            return new(1, 0);

        var result = new Fractional(1, 1);

        for (int i = 2; i <= n; ++i)
        {
            // (1 - 1/k^2) = (k^2 - 1) / k^2
            long square = (long)i * i;
            var next = new Fractional(square - 1, square);
            result *= next;
        }

        return result;
    }
}
