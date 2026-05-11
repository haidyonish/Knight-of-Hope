using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Localization/Table")]
public class LocalizationTable : ScriptableObject
{
    public List<LocalizationEntry> entries = new();
}