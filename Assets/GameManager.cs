using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject enemyPrefab;
    public int totalEnemiesToInstantiate, totalAliveCount;
    public List<Transform> enemiesTransforms;
    public List<GameObject> instantiatedEnemies;

    [Header("UI")]
    public GameObject fightScreen; 
    public GameObject winScreen;
    public TextMeshProUGUI totalAliveText;
    public TextMeshProUGUI winnerNameText;


    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        fightScreen.SetActive(true);
        Invoke(nameof(DisappearFight),3f);
        totalEnemiesToInstantiate=DataContainer.Instance.entriesData.Count;
        for(int i = 0;i< totalEnemiesToInstantiate; i++)
        {
            GameObject _enemy=Instantiate(enemyPrefab);
            _enemy.GetComponent<EnemyCustomizations>().enemyName = DataContainer.Instance.entriesData[i];

            _enemy.transform.position = enemiesTransforms[i].transform.position;
            _enemy.transform.rotation = enemiesTransforms[i].transform.rotation;
            instantiatedEnemies.Add(_enemy);
            _enemy.SetActive(true);
        }

        totalAliveCount = totalEnemiesToInstantiate;
        totalAliveText.text = "Total Alive Count: " + totalAliveCount + " / " + totalEnemiesToInstantiate;
    }

    void DisappearFight()
    {
        fightScreen.SetActive(false);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    //GameObject foundObj;
    public void CheckWinner()
    {
        totalAliveCount--;
        totalAliveText.text = "Total Alive Count: " + totalAliveCount + " / " + totalEnemiesToInstantiate;

        if (totalAliveCount == 1)
        {
            //winnerNameText.text=""
            winScreen.SetActive(true);

            //foreach(GameObject obj in instantiatedEnemies)
            //{
            //    if (obj.activeInHierarchy)
            //    {
            //        foundObj = obj;
            //        break;
            //    }
            //}
            winnerNameText.text = instantiatedEnemies[0].GetComponent<EnemyCustomizations>().enemyName + " is the Winner";
        }
    }
}
