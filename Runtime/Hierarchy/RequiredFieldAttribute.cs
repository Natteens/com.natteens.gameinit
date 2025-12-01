using System;
using UnityEngine;

namespace GameInit.Hierarchy
{
    [AttributeUsage(AttributeTargets.Field)]
    public class RequiredFieldAttribute : PropertyAttribute { }
}