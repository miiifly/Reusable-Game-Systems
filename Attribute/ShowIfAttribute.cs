using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ShowIfAttribute : PropertyAttribute
{
    public string ConditionFieldName { get; private set; }
    public bool ShowIfTrue { get; private set; }

    public ShowIfAttribute(string conditionFieldName, bool showIfTrue = true)
    {
        ConditionFieldName = conditionFieldName;
        ShowIfTrue = showIfTrue;
    }
}

