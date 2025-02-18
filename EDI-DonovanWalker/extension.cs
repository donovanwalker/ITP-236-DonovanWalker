using System;
using System.Globalization;

public static class StringExtensions
{
    public static string ProperName(this string name)
    {
        if (string.IsNullOrEmpty(name)) return name;
        
        // Convert the name to title case (each word's first letter capitalized)
        TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
        return textInfo.ToTitleCase(name.ToLower());
    }
}

class Program
{
    static void Main()
    {
        string name1 = "donovan walker";
        string name2 = "dOnOvAn WaLkEr";

        Console.WriteLine(name1.ProperName()); // Output: Donovan Walker
        Console.WriteLine(name2.ProperName()); // Output: Donovan Walker
    }
}
