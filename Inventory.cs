using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Inventory : MonoBehaviour {
	public int slotsX, slotsY;
	public GUISkin skin;
	public List<Item> slots = new List<Item> ();
	public List<Item> playerInventory = new List<Item>();
	private ItemDatabase itemDB;
	private bool showInventory;
	private bool showTooltip;
	private string tooltip;
	private bool draggingItem;
	private Item draggedItem;
	public int prevIndex;
	private Item tempItem;
	private int slotWidth = 60;
	private int slotHeight = 60;
	private Rect inventoryArea = new Rect (0, 0, 300, 300);
	private GameObject playerChar;
	// Use this for initialization
	void Start () {
		playerChar = GameObject.Find ("Player");
		for (int i = 0; i < slotsX * slotsY; i++) {
			slots.Add (new Item ());
			playerInventory.Add (new Item ());
		}
		itemDB = GameObject.FindGameObjectWithTag ("ItemDatabase").GetComponent<ItemDatabase> ();
	//	playerInventory.Add (itemDB.itemList[0]);
	//	playerInventory.Add (itemDB.itemList[1]);
	//	playerInventory.Add (itemDB.itemList[2]);
	//	playerInventory[0] = itemDB.itemList[0];
	//	playerInventory[1] = itemDB.itemList[1];
	//	playerInventory[2] = itemDB.itemList[2];
		AddItem (0);
		AddItem (1);
		AddItem (2);
		AddItem (3);
	//	Debug.Log (InventoryContains (0));
	//	RemoveItem(1);
	//	Debug.Log (InventoryContains (3));
	}

	void Update()
	{
		if(Input.GetButtonDown("Inventory"))
		{
				showInventory = !showInventory;
		}
	}
	void OnGUI()
	{
		if (GUI.Button (new Rect (40, 500, 100, 40), "Save")) {
			SaveInventory ();
		}
		if (GUI.Button (new Rect (40, 550, 100, 40), "Load")) {
			LoadInventory ();
		}

		tooltip = "";
		GUI.skin = skin;
		if (showInventory) {
			DrawInventory ();
			Time.timeScale = 0f;;//oause scene
			if (showTooltip) {
				GUI.Box (new Rect (Event.current.mousePosition, new Vector2 (200, 200)), tooltip, skin.GetStyle ("Tooltip"));
			}
		} else {
			//TODO: on pause bullets freeze and any other scripts working while timeScale = 0
			Time.timeScale = 1f;//resume scene
		}
	//	for(int i = 0; i < playerInventory.Count; i++)
	//	{
	//		GUI.Label (new Rect (10, i * 20, 200, 200), playerInventory[i].itemName);
	//	}
		if (draggingItem && draggedItem.itemName!=null) {
			GUI.DrawTexture(new Rect(Event.current.mousePosition, new Vector2(slotHeight, slotHeight)), draggedItem.itemIcon);
		}
	}
		
	void DrawInventory()
	{
		Event e = Event.current;
		int i = 0;
		//put offset, padding
		for (int y = 0; y < slotsY; y++){
			for (int x = 0; x < slotsX; x++) {
				Rect slotRect = new Rect (x * slotWidth, y * slotHeight, slotWidth, slotHeight);			//slots size
				GUI.Box (slotRect, (y * slotsY + x).ToString(), skin.GetStyle("Slot"));						//draw slot texture
				if (playerInventory[i].itemName != null) {													//if player inv slot is not empty
					GUI.DrawTexture (slotRect, playerInventory [i].itemIcon);								//	draw item icon texture in the slot
				}
				i++;
			}
		}
		//drag
		if (inventoryArea.Contains(e.mousePosition)) {
			int mx, my;
			mx = Mathf.Clamp((int)e.mousePosition.x / slotWidth, 0, slotsX - 1);
			my = Mathf.Clamp((int)e.mousePosition.y / slotHeight, 0, slotsY - 1);
			i = my * slotsY + mx;
			//tooltip
			tooltip = CreateTooltip (playerInventory [i]);										//item tooltip string edit

			//mouse0 down
			if (e.button == 0 && e.type == EventType.MouseDown && !draggingItem) {
				//temp = a,
				draggingItem = true;
				prevIndex = i;
				draggedItem = playerInventory [prevIndex];
				playerInventory [prevIndex] = new Item ();
			}
			//mouse0 release
			else if (e.type == EventType.mouseUp && e.button == 0) {
				if (playerInventory [i].itemName == null) {
					//move items
					if (prevIndex == i) {
						playerInventory [i] = draggedItem;
					} else {
						playerInventory [i] = draggedItem;
						playerInventory [prevIndex] = new Item ();
					}
					draggingItem = false;
					draggedItem = null;
				} else if (playerInventory [i].itemName != null) {
					//swap item
					//  a = b, b = temp
					playerInventory [prevIndex] = playerInventory[i];
					playerInventory[i] = draggedItem;

					draggingItem = false;
					draggedItem = null;
				}
			}
			//mouse 2 to consume/equip
			else if (e.type == EventType.mouseDown && e.button == 1 && playerInventory [i].itemName != null) {
				//if consumable -> consume
				if (playerInventory [i].itemType == Item.ItemType.Consumable) {
					//	Debug.Log ("Use Consumable" + i);
					UseConsumable (playerInventory [i], i, true);
					//	RemoveItem (slots[i].itemID);
				}
				//TODO : Equip
				/* else if (playerInventory [i].itemType == Item.ItemType.Outfit) {
				} else if (playerInventory [i].itemType == Item.ItemType.Weapon) {
				} else if (playerInventory [i].itemType == Item.ItemType.Quest) {
				}*/
			}
		}

	}
	string CreateTooltip(Item item)
	{
	//	tooltip = "";
		if (item.itemName != null) {
			tooltip = "<color=#ffffff>" + item.itemName + "\n\n" + item.itemDesc + "</color>\n\n";
			if (item.itemPower != 0) {
				tooltip += "<color=#ff5588>" + "Power: " + item.itemPower + "\nSpeed: " + item.itemSpeed + "</color>\n\n";
			}
			tooltip += "<color=#2f2f2f>" + "Type: " + item.itemType + "</color>";
			showTooltip = true;
		} else {
			tooltip = "";
			showTooltip = false;
		}

		return tooltip;
	}

	void RemoveItem(int id)
	{
		for (int i = 0; i < playerInventory.Count; i++) {
			if (playerInventory [i].itemID == id) {
				//replaced with empty item
				slots[i] = new Item();
				playerInventory[i] = new Item();
				break;
			}
		}
	}

	void AddItem(int id)
	{
		for (int i = 0; i < playerInventory.Count; i++) {
			if (playerInventory [i].itemName == null) {
			//	playerInventory [i] = itemDB.itemList [id];
				for(int j = 0; j < itemDB.itemList.Count; j++){
					if (itemDB.itemList [j].itemID == id) {
						playerInventory[i] = itemDB.itemList[j];
					}
				}
				break;
			}
		}
	}

	bool InventoryContains(int id)
	{
		for (int i = 0; i < playerInventory.Count; i++) {
			if (playerInventory [i].itemID == id) {
				return true;
			}
		}
		return false;
	}

	private void UseConsumable(Item item, int slot, bool deleteItem)
	{
		//switch (item.itemID) {
		//case 3:
		//	{
		//		Debug.Log ("Used item:" + item.itemName);
		//		//increase player hp (stat.ID, duration, amount)
		//
		//		break;
		//	}
		//default:
		//	{
		//		break;
		//	}
		//}
		switch (item.itemID) {
		case 3:
			{
				if (playerChar.GetComponent<PlayerControl> ().health == 100) {
				//	Debug.Log ("Player Full Health"); //dialogue box - idea
					deleteItem = false;
				} else {
					Debug.Log ("ahhh... feels good, man");
					playerChar.GetComponent<PlayerControl> ().Heal(true, item.itemPower);
				}
				break;
			}
		default:
			{
				break;
			}
		}
		if (deleteItem) {
			playerInventory[slot] = new Item ();
		}
	}

	void SaveInventory ()
	{
		for (int i = 0; i < playerInventory.Count; i++) {
			PlayerPrefs.SetInt ("Inventory " + i, playerInventory[i].itemID);

		}
	}
	void LoadInventory()
	{
		for (int i = 0; i < playerInventory.Count; i++) {
			playerInventory[i] =  PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? itemDB.itemList[PlayerPrefs.GetInt("Inventory " + i)]  : new Item();
		}
	}
}
