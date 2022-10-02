using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Weapon Data", menuName = "DataSO/Weapon Data")]
public class WeaponDataSO : ScriptableObject
{
    public List<ItemPooling.WeaponTypeData> WeaponDatas;
    public List<ItemPooling.WeaponSkinData> WeaponSkinDatas;
}
