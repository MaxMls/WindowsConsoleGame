using System;

public class Material {

    public ConsoleColor consoleColor;
    byte r;
    byte g;
    byte b;
    byte a;


    public Material(ConsoleColor consoleColor = ConsoleColor.Black) {
        this.consoleColor = consoleColor;
    }

    public void Color(byte r, byte g, byte b, byte a = 0xff) {
        this.r = r;
        this.g = g;
        this.b = b;
        this.a = a;
        consoleColor = ClosestConsoleColor(r, g, b);

    }

    public void Color(uint hex) {
        a = (byte)((hex & -16777216) >> 0x18);
        r = (byte)((hex & 0xff0000) >> 0x10);
        g = (byte)((hex & 0xff00) >> 8);
        b = (byte)(hex & 0xff);

        Color(r, g, b, a);
    }



    static ConsoleColor ClosestConsoleColor(byte r, byte g, byte b) {
        ConsoleColor ret = 0;
        double rr = r, gg = g, bb = b, delta = double.MaxValue;

        foreach (ConsoleColor cc in Enum.GetValues(typeof(ConsoleColor))) {
            var n = Enum.GetName(typeof(ConsoleColor), cc);

            var c = System.Drawing.Color.FromName(n == "DarkYellow" ? "Orange" : n); // bug fix
            var t = Math.Pow(c.R - rr, 2.0) + Math.Pow(c.G - gg, 2.0) + Math.Pow(c.B - bb, 2.0);
            if (t == 0.0)
                return cc;
            if (t < delta) {
                delta = t;
                ret = cc;
            }
        }
        return ret;
    }

}

