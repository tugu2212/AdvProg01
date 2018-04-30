using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class FileInputOutput : MonoBehaviour {

	string readPath;
	string writePath;
	PlayerControl player;
	public List<string> stringList = new List<string> ();
	public List<string> writeList= new List<string> ();
	// Use this for initialization
	void Start () {

		readPath = Application.dataPath + "/TestTxt.txt";
		writePath = Application.dataPath + "/TestFile.txt";

		ReadFile(readPath);
		WriteFile(writePath);
		AppendFile(writePath);
		player = GameObject.FindGameObjectWithTag ("Player").GetComponent<PlayerControl>();
	}
	
	// Update is called once per frame
	void Update () 
	{
		
	}

	void ReadFile(string filePath)
	{
		StreamReader sReader = new StreamReader (filePath);

		while (!sReader.EndOfStream) 
		{

			string line = sReader.ReadLine ();
			stringList.Add (line);
		}

		sReader.Close ();
	}

	void WriteFile(string filePath)
	{	
		StreamWriter sWriter;
		if (!File.Exists (filePath))		
		{
			sWriter = File.CreateText (Application.dataPath + "/TestFile.txt");
		} 
		else
		{
			sWriter = new StreamWriter (filePath);
		}

		for (int k = 0; k < writeList.Count; k++)
		{

			//write file name
			sWriter.WriteLine(writeList[k]);
		}
		{
			//playerInfo
			sWriter.WriteLine(player.transform.position.ToString() + " : " + player.health.ToString());
		}		
		sWriter.Close();
	}

	void AppendFile(string filePath)
	{
		StreamWriter sWriter;
		
	if (!File.Exists(filePath))
	{
		sWriter = File.CreateText(Application.dataPath + "/TestFile.txt");
	}
	else
	{
		sWriter = new StreamWriter(filePath, append: true);
	}

	for(int k=0; k<writeList.Count; k++)
	{
		sWriter.WriteLine(writeList[k]);
	}	

	sWriter.Close();
	}

//PlayerControl loadPlayer()
//{
//	PlayerControl newPlayer;
//	newPlayer.speed = 10;
//	newPlayer.health = 100;// = //Files unshaad instantiae;
//	return newPlayer;
//}
}
