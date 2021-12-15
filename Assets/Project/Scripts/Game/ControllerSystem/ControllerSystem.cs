using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IControllerSystem
{
    IEnumerator Init();

}

public static class ControllerSystem
{
    private static Dictionary<string,float> AxisList = new Dictionary<string, float>();


    public static void CreateAxis(string AxisName)
    {
        if(AxisList.ContainsKey(AxisName))
        {
            Debug.Log("Axis Already Exist!");
        }
        else
        {
            AxisList.Add(AxisName,0);
        }

    }

    public static void SetAxisValue(string AxisName, float AxisValue)
    {
        if(AxisList.ContainsKey(AxisName))
        {
            AxisList[AxisName] = AxisValue;
        }
        else
        {
            Debug.Log("!Axis Not Exist!");
        }

    }


    public static float GetAxis(string AxisName)
    {
        float axisValue = 0;
        if(AxisList.ContainsKey(AxisName))
        {
            axisValue = AxisList[AxisName];
        }
        else
        {
            Debug.Log("!Axis Not Exist!");
        }

        return axisValue;
    }
}
