using System.Collections;  
using System.Collections.Generic;  
using UnityEngine;  
using PlayDarts.Com;  

public class GameModel : MonoBehaviour {  
	public GameObject PlaneItem, LauncherItem, ExplosionItem;  
	public Material greenMat, redMat, blueMat;  

	private GameObject plane, launcher, explosion;  
	private MainSceneController scene;  

	private const float LAUNCH_GAP = 0.1f;  

	void Start () {  
		scene = MainSceneController.getInstance();  
		scene.setGameModel(this);  

		plane = Instantiate(PlaneItem);  
		launcher = Instantiate(LauncherItem);  
		explosion = Instantiate(ExplosionItem);  
	}  

	void Update () {  
		DartFactory.getInstance().detectReuseDarts();  

	}  

	
	public void launchDarts() {  
		int roundNum = scene.getRoundNum();  
		if (!DartFactory.getInstance().isLaunching())  
			StartCoroutine(launchDartsWithGapTime(roundNum));  
	}  
	  
	IEnumerator launchDartsWithGapTime(int roundNum) {  
		for (int i = 0; i < roundNum; i++) {  
			GameObject dart = DartFactory.getInstance().getDart();  
			dart.transform.position = launcher.transform.position;  
			dart.GetComponent<MeshRenderer>().material = getMaterial(roundNum);  

			Vector3 force = getRandomForce();  
			dart.GetComponent<Rigidbody>().AddForce(force, ForceMode.Impulse);  

			yield return new WaitForSeconds(LAUNCH_GAP);  
		}  
	}  
	Vector3 getRandomForce() {  
		int x = Random.Range(-30, 31);  
		int y = Random.Range(30, 41);  
		int z = Random.Range(20, 31);  
		return new Vector3(x, y, z);  
	}  


	public void strikeTheDart(Vector3 mousePos) {  
		Ray ray = Camera.main.ScreenPointToRay(mousePos);  

		RaycastHit hit;  
		if (Physics.Raycast(ray, out hit)) {  
			if (hit.collider.gameObject.tag.Equals("Dart")) {  
				createExplosion(hit.collider.gameObject.transform.position);  
				scene.addScore();  
				DartFactory.getInstance().ReuseWhenDartBeingStruck(hit.collider.gameObject);  
			}  
		}  
	}  
	void createExplosion(Vector3 position) {  
		explosion.transform.position = position;  
		explosion.GetComponent<ParticleSystem>().GetComponent<Renderer>().material =  
			getMaterial(scene.getRoundNum());  
		explosion.GetComponent<ParticleSystem>().Play();  
	}  

	Material getMaterial(int roundNum) {  
		switch (roundNum % 3) {  
		case 0:  
			return redMat;  
		case 1:  
			return greenMat;  
		case 2:  
			return blueMat;  
		default:  
			return redMat;  
		}  
	}  
}  
