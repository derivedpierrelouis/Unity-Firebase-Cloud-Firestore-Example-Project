using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase;
using Firebase.Unity;
using Firebase.Firestore;
using System.Threading.Tasks;

public class Cloud_Firestore_Example : MonoBehaviour
{
	public List<string>Text_Example = new List<string>();
	public TMP_Text Text_Example2;
	public FirebaseFirestore db;
	
    // Start is called before the first frame update
    void Start()
    {
		db = FirebaseFirestore.DefaultInstance;

		Grab();		
		Grab2();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	//Grab all data or set query on which data you grab		
	void Grab()
    {
		
		// Enter in Your Collection's name
		Firebase.Firestore.Query query = db.Collection("YourCollectionName");
		
		//Firebase.Firestore.Query query = db.Collection("YourCollectionName").WhereEqualTo("isBlue", true);;

		ListenerRegistration listener = query.Listen(snapshot =>
		{
			foreach (DocumentSnapshot documentSnapshot in snapshot.Documents)
			{
				Debug.Log(documentSnapshot.Id);
		
				Dictionary<string, object> data = documentSnapshot.ToDictionary();
				Text_Example.Add(data["Document_Field_Name"].ToString());
			};
		});
	}
	
	//Grab data by document name 
	void Grab2()
    {
	
		// Enter in Your Collection's name
		DocumentReference docRef = db.Collection("YourCollectionName").Document("YourDocument");

		ListenerRegistration listener = docRef.Listen(snapshot =>
		{
			Debug.Log(snapshot.Id);
		
			Dictionary<string, object> data = snapshot.ToDictionary();
			Text_Example2.text = data["Document_Field_Name"].ToString();
		});
	}
	
	//Write data to cloud firestore
	//REMEMBER TO ALLOW WRITE TO CLOUD FIRESTORE FOR IT TO WORK;
	void Write()
    {
		
		CollectionReference colRef = db.Collection("YourCollectionName");
		colRef.Document("WriteData").SetAsync(new Dictionary<string, object>()
		{
			
			//Write in Data
			{ "Document_Field_Name", "Data" },
			{ "Name", "Derived" }

		});
	}
}