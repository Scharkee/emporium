using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ListItemPrice : MonoBehaviour
{


    public Dictionary<string, float> PriceCache = new Dictionary<string, float>();  // laikomos vieno listItem kainos (juice ir produce)

    public Dictionary<string, float> WeightCache = new Dictionary<string, float>();  // laikomos visu listitem bendras svoris;


    private void Start()
    {

        //preinitialization visiems dictionaries;
        WeightCache.Add("kriauses", 0);
        WeightCache.Add("kriauses_sultys", 0);
        WeightCache.Add("apelsinai", 0);
        WeightCache.Add("apelsinai_sultys", 0);
        WeightCache.Add("arbuzai", 0);
        WeightCache.Add("arbuzai_sultys", 0);
        WeightCache.Add("bananai", 0);
        WeightCache.Add("bananai_sultys", 0);
        WeightCache.Add("obuoliai", 0);
        WeightCache.Add("obuoliai_sultys", 0);
        WeightCache.Add("slyvos", 0);
        WeightCache.Add("slyvos_sultys", 0);
        WeightCache.Add("vysnios", 0);
        WeightCache.Add("vysnios_sultys", 0);
        WeightCache.Add("persikai", 0);
        WeightCache.Add("persikai_sultys", 0);
        WeightCache.Add("kiviai", 0);
        WeightCache.Add("kiviai_sultys", 0);
        WeightCache.Add("nektarinai", 0);
        WeightCache.Add("nektarinai_sultys", 0);

        PriceCache.Add("juice", 0);
        PriceCache.Add("produce", 0);


    }


    public void UpdatePrice(Text text)
    {

        text.text = "+" + (PriceCache["produce"] + PriceCache["juice"]).ToString() + "$";

    }

    public void UpdateTotalWeight(Text text)
    {
        float total = 0;

        foreach (KeyValuePair<string, float> entry in WeightCache)
        {
            total += entry.Value;

        }

        text.text = total.ToString();

    }



}
