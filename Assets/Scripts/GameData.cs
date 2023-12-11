using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


[CreateAssetMenu(fileName = "GameData", menuName = "ConnectingMinds/GameData")]
[Serializable]
public class GameData : Indexable
{
    public List<PathData> Paths;
    public List<ItemData> Items;
    public List<PositionData> Positions;
    public override void AddEventData(SendEvent sendEvent)
    {
        throw new System.NotImplementedException();
    }

    public override T GetData<T>()
    {
        return new GameDataObject(this) as T;
    }
}
public class GameDataObject : ScriptableObjectData
{
    public List<PathObject> Paths;
    public List<ItemObject> Items;
    public List<PositionObject> Positions;
    public GameDataObject(GameData gameData)
    {
        Paths = MapAndGetDataList(gameData.Paths, x => x.GetData<PathObject>());
        Items = MapAndGetDataList(gameData.Items, x => x.GetData<ItemObject>());
        Positions = MapAndGetDataList(gameData.Positions, x => x.GetData<PositionObject>());
    }
    private List<TOutput> MapAndGetDataList<TInput, TOutput>(List<TInput> inputList, Func<TInput, TOutput> mapFunction)
    {
        return inputList.Select(mapFunction).ToList();
    }

}