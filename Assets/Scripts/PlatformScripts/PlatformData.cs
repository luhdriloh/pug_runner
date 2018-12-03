using System.Collections.Generic;
using UnityEngine;

public class PlatformData : MonoBehaviour
{
    public List<GameObject> _platformPrototypes;
    public List<int> _spawnPriorityRates;
    public List<float> _spawnRatePercentages;

    private void Awake()
    {
        _spawnRatePercentages = new List<float>();
        float totalPriorityRates = 0f;
        _spawnPriorityRates.ForEach(value => totalPriorityRates += value);

        for (int i = 0; i < _spawnPriorityRates.Count; i++)
        {
            _spawnRatePercentages.Add(_spawnPriorityRates[i] / totalPriorityRates + (i > 0f ? _spawnRatePercentages[i-1] : 0f));
        }
    }
}
