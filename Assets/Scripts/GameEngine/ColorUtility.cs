using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorUtility
{


    public static Color ChangeColorLuminosity(Color rgb, float percentage, bool increase = false)
    {
        float val1 = (rgb.r / 100) * percentage;

        float val2 = (rgb.g / 100) * percentage;

        float val3 = (rgb.b / 100) * percentage;

        if (!increase)
        {

            rgb.r -= val1;
            rgb.g -= val2;
            rgb.b -= val3;
        }
        else
        {
            rgb.r += val1;
            rgb.g += val2;
            rgb.b += val3;
        }
        return rgb;
    }


}
