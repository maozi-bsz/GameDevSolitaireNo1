using RG.Basic;
using RG.Zeluda;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.SceneManagement;

public class SceneLoadManager : ManagerBase
{
	public Scene curScene;
	public GameObject pfb_obj;
	public MapCA mapCA;
	public void Load(string scene)
	{
		if (curScene.name != null)
		{
			if (curScene.name == scene) { return; }
			SceneManager.UnloadScene(curScene);
		}
		LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Additive);
		curScene = SceneManager.LoadScene(scene, parameters);
	}
	public void Load(MapCA ca)
	{
		mapCA = ca;

		string sceneName = ca.scene;

		if (curScene.name != null)
		{

			if (curScene.name == sceneName) { return; }
			if (pfb_obj != null)
			{
				GameObject.Destroy(pfb_obj);
			}
			SceneManager.UnloadScene(curScene);
		}
		LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Additive);
		curScene = SceneManager.LoadScene(sceneName, parameters);
		LoadPrefab(); // ���óɹ��ص�
	}
	public void Load(int id)
	{
		MapFactory mf = CBus.Instance.GetFactory(FactoryName.MapFactory) as MapFactory;
		mapCA = mf.GetCA(id) as MapCA;
		if (curScene.name != null)
		{
			if (curScene.name == mapCA.scene) { return; }
			if (pfb_obj != null)
			{
				GameObject.Destroy(pfb_obj);
			}
			SceneManager.UnloadScene(curScene);
		}
		LoadSceneParameters parameters = new LoadSceneParameters(LoadSceneMode.Additive);
		curScene = SceneManager.LoadScene(mapCA.scene, parameters);
		LoadPrefab(); // ���óɹ��ص�
	}

	public void LoadPrefab()
	{
		if (pfb_obj != null)
		{
			GameObject.Destroy(pfb_obj);
			pfb_obj = null;

		}
		GameManager gm = CBus.Instance.GetManager(ManagerName.GameManager) as GameManager;
		int day = Mathf.Clamp(gm.day - 1, 0, mapCA.prefab.Length - 1);
        //�����������ӣ���ͼ����ز�ͬ��Ԥ����?
        UIManager um = CBus.Instance.GetManager(ManagerName.UIManager) as UIManager;
		um.OpenPanel(mapCA.prefab[day]);
		//GameObject obj = Resources.Load<GameObject>(mapCA.prefab[day]);
		//pfb_obj = GameObject.Instantiate(obj);
		//pfb_obj.transform.position = Vector3.zero;
		//Transform startPoint = pfb_obj.transform.Find(mapCA.ptrans);

		//if (startPoint == null)
		//{
		//	PlayerCharacterController.Inst.transform.position = Vector3.zero;
		//	PlayerCharacterController.Inst.transform.eulerAngles = Vector3.zero;
		//	PlayerCharacterController.Inst.agent.destination = Vector3.zero;
		//}
		//else {
		//	PlayerCharacterController.Inst.transform.position = startPoint.position;
		//	PlayerCharacterController.Inst.transform.eulerAngles = startPoint.eulerAngles;
		//	PlayerCharacterController.Inst.agent.destination = startPoint.position;
		//}

		//PlayerCharacterController.Inst.ani.Play(mapCA.pani);
		//PlayerCharacterController.Inst.moveLock = mapCA.ctrl == 0;

	}
	public void LoadSimplePrefab(string prefab)
	{
		if (pfb_obj != null)
		{
			GameObject.Destroy(pfb_obj);
			pfb_obj = null;

		}
		GameObject obj = Resources.Load<GameObject>(prefab);
		pfb_obj = GameObject.Instantiate(obj);
		pfb_obj.transform.position = Vector3.zero;
	}
}
