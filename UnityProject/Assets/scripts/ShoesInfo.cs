using System;
using System.IO;

public class ShoesInfo{

    public double Speed { get; set; }
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }


    public ShoesInfo()
    {
    }

    public static ShoesInfo Load()
    {
        using(StreamReader reader = new StreamReader(@"d:\data.txt")){
            string msg = reader.ReadLine();
            return Parse(msg);
        }
    }

    public static ShoesInfo Parse(string data)
    {
        string message = data.Replace("\r", "").Replace("\n", ""); ;
        string[] cols = message.Split(new string[] { "\t" }, System.StringSplitOptions.None);

        if (cols.Length < 3)
        {
            return null ;
        }

        ShoesInfo info = new ShoesInfo();

        info.Speed = Double.Parse(cols[0]);
        info.Y = Double.Parse(cols[1]);
        info.X = Double.Parse(cols[2]);

        return info;
    }

    public override string ToString()
    {
        return String.Format("x:{0}, y:{1}, z:{2}, speed:{3}",
                                X, Y, Z, Speed);

    }

}
