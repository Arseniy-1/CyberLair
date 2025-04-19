using UnityEngine;

[CreateAssetMenu(fileName = "MapConifg", menuName = "Map/MapConifig", order = 51)]
public class MapData : ScriptableObject
{
    [field: SerializeField] public string MapName { get; private set; }
    [field: SerializeField] public Sprite MapImage { get; private set; }
    [field: SerializeField] public string EasyMap { get; private set; }
    [field: SerializeField] public string HardMap { get; private set; }
}