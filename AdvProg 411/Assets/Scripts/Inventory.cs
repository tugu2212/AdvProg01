using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {
	public int slotsX;
	public int slotsY;
	public GUISkin skin;
	public List<Item> slots = new List<Item> ();
	public List<Item> playerInventory = new List<Item> ();
	public ItemDatabase itemDB;
	private bool showInventory;
	private bool showToolTip;
	private string toolTip;
	private bool draggingItem;
	private Item draggedItem;
	private int prevIndex;
	private Item tempItem;
	private int slotsWitdh = 60;
	private int slotsHeight = 60;
	private Rect inventoryArea = new Rect(0, 0, 300, 300);
	private GameObject playerChar;
	// Use this for initialization
	void Start () {
		slotsX = 4;
		slotsY = 3;
		showInventory = false;
		playerChar = GameObject.FindGameObjectWithTag ("Player");
		for (int i = 0; i < slotsX * slotsY; i++) {
			slots.Add (new Item ());
			playerInventory.Add (new Item ());
		}
		AddItem(0);
		AddItem(1);
		AddItem(2);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButtonDown("Inventory")){
			showInventory = !showInventory;
		}
	}

	void OnGUI(){
		if (GUI.Button (new Rect (40, 500, 100, 40), "Save")) {
			SaveInventory ();
		}
		if(GUI.Button(new Rect(40, 550, 100, 40), "Load")){
			LoadInventory ();
		}
	}

	void DrawInventory(){
		Event e = Event.current;
		int i = 0;
		for (int y = 0; y < slotsY; y++) {
			for(int x = 0; x < slotsX; x++){
				Rect slotRect = new Rect (x * slotsWitdh, y * slotsY, slotsWitdh, slotsHeight);
				GUI.Box (slotRect, (y * slotsY + x).ToString (), skin.GetStyle ("Slot"));
				if(playerInventory[i].itemName != null){
					GUI.DrawTexture (slotRect, playerInventory [i].itemIcon);
				}
				i++;
			}
		}
		//drag
		if(inventoryArea.Contains(e.mousePosition)){
			int mx, my;
			mx = Mathf.Clamp ((int)e.mousePosition.x / slotsWitdh, 0, slotsX - 1);
			my = Mathf.Clamp((int) e.mousePosition.y / slotsHeight, 0, slotsY -1);
			i = my * slotsY + mx;
			//tooltip
			toolTip = CreateToolTip (playerInventory[i]);
			//mouse0 down, release
			if (e.button == 0 && e.type == EventType.mouseDown && !draggingItem) {
				draggingItem = true;
				prevIndex = i;
				draggedItem = playerInventory [prevIndex];
				playerInventory [prevIndex] = new Item ();
			} else if (e.type == EventType.MouseUp && e.button == 0) {
				if (playerInventory [i].itemName == null) {
					//move to empty
					if (prevIndex == i) {
						playerInventory [i] = draggedItem;
					} else {
						playerInventory [i] = draggedItem;
						playerInventory [prevIndex] = new Item ();
					}
					draggingItem = false;
					draggedItem = null;
				} else if (playerInventory [i].itemName != null) {
					//swap items
					playerInventory[prevIndex] = playerInventory[i];
					playerInventory [i] = draggedItem;
					draggingItem = false;
					draggedItem = null;
				}
			} else if(e.type == EventType.MouseDown && e.button == 1 && playerInventory[i].itemName != null){
				if (playerInventory [i].itemType == Item.ItemType.Consumable) {
					//if clicked item is consumable, then consume
					UseConsumable (playerInventory [i], i, true);
				}
				//TODO: Equip if equipable
				// else if(playerInventory[i].itemType == Item.ItemType.Weapon){}//etc
			}
		}
	}

	string CreateToolTip(Item item){
		if (item.itemName != null) {
			toolTip = "<color = #ffffff>" + item.itemName + "\n\n" + item.itemDesc + "</color>\n\n";
			if(item.itemPower != 0){
				toolTip += "<color = #ff5588>" + item.itemSpeed + "</color>\n\n";
			}
			toolTip += "<color = #2f2f2f>" + "Type: " + item.itemType + "</color>";
			showToolTip = true;
		} else {
			toolTip = "";
			showToolTip = false;
		}

		return toolTip;
	}

	void RemoveItem(int id){
		for (int i = 0; i < playerInventory.Count; i++) {
			if (playerInventory [i].itemID == id) {
				slots [i] = new Item ();
				playerInventory [i] = new Item ();
				break;
			}
		}
	}

	void AddItem(int id){
		for (int i = 0; i < playerInventory.Count; i++) {
			if (playerInventory [i].itemName == null) {
				for (int j = 0; j < itemDB.itemList.Count; j++) {
					if (itemDB.itemList [j].itemID == id) {
						playerInventory [i] = itemDB.itemList [j];
					}
				}
				break;
			}
		}
	}

	bool InventoryContaints(int id){
		for(int i = 0; i < playerInventory.Count; i++){
			if (playerInventory [i].itemID == id) {
				return true;
			}
		}
		return false;
	}

	private void UseConsumable(Item item, int slot, bool deleteItem){
		switch (item.itemID) {
		case 20:{
				if (playerChar.GetComponent<PlayerControl> ().health >= 100) {
					deleteItem = false;
				} else {
					Debug.Log ("tastes like .... herb.... yuck!");
				}
				break;
			}
		default:
			break;
		}
		if (deleteItem) {
			playerInventory [slot] = new Item ();
		}
	}

	void SaveInventory(){
		for(int i = 0; i < playerInventory.Count; i++){
			PlayerPrefs.SetInt ("Inventory " + i, playerInventory [i].itemID);
		}
	}

	void LoadInventory(){
		for (int i = 0; i < playerInventory.Count; i++) {
			playerInventory[i] = PlayerPrefs.GetInt("Inventory " + i, -1) >= 0 ? 
				itemDB.itemList[PlayerPrefs.GetInt("Inventory " + i)] : new Item();
		}
	}
}
