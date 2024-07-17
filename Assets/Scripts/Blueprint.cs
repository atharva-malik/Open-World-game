using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blueprint: MonoBehaviour{
    public string itemName;
    public string Req1;
    public string Req2;
    public int Req1Amt;
    public int Req2Amt;
    public int numberOfRequirements;
    public int numberOfItemsToProduce;

    public Blueprint(int producedItems, string itemName, string Req1, string Req2, int Req1Amt, int Req2Amt, int numberOfRequirements){
        this.itemName = itemName;
        this.Req1 = Req1;
        this.Req2 = Req2;
        this.Req1Amt = Req1Amt;
        this.Req2Amt = Req2Amt;
        this.numberOfRequirements = numberOfRequirements;
        this.numberOfItemsToProduce = producedItems;
    }
}