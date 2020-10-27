using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticValue
{
    static public string phoneNumber = "";
    internal static GameObject CountryCode;

    public static string getPhoneNumber()
    {
        return phoneNumber;
    }

}
