using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item : MonoBehaviour {
	public string itemName;
	public int itemID;
	public string itemDesc;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;
	public DropType dropType;
	public Texture2D itemIcon;

	public enum ItemType{
		Weapon, Consumable, Quest, Outfit
	}

	public enum DropType{
		ConsumeOnPick, ConsumeOnUse, Use
	}

	public Item(string name, int id, string desc, int power, int speed, ItemType type, DropType drop){
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemPower = power;
		itemSpeed = speed;
		itemType = type;
		dropType = drop;
		itemIcon = Resources.Load<Texture2D> ("Textures/"+name);
	}

	public Item(){
		itemName = null;
		itemID = -1;
	}
}
