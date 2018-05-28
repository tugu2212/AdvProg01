using UnityEngine;
using System.Collections;

[System.Serializable]
public class Item{

	public string itemName;
	public int itemID;
	public string itemDesc;
	public Texture2D itemIcon;
	public int itemPower;
	public int itemSpeed;
	public ItemType itemType;
	public DropType dropType;

	public enum ItemType
	{
		Weapon,
		Consumable,
		Quest,
		Outfit
	}

	public enum DropType
	{
		ConsumeOnPick,
		ConsumeOnUse,
		None
	}

	public Item(string name, int id, string desc, int power, int speed, ItemType type, DropType dtype)
	{
		itemName = name;
		itemID = id;
		itemDesc = desc;
		itemIcon = Resources.Load<Texture2D>("Textures/"+name);
		itemPower = power;
		itemSpeed = speed;
		itemType = type;
		dropType = dtype;
	}
	public Item()
	{
		itemName = null;
		itemID = -1;
	}
}
