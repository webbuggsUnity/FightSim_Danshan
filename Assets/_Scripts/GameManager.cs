using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    public GameObject[] enemyPrefabs;
    public int totalEnemiesToInstantiate, totalAliveCount;
    public List<Transform> enemiesTransforms;
    public List<GameObject> instantiatedEnemies;
    public Material[] allEnemiesMat;

    [Header("UI")]
    public GameObject fightScreen; 
    public GameObject winScreen;
    public TextMeshProUGUI totalAliveText;
    public TextMeshProUGUI winnerNameText;


    private void Awake()
    {
        instance = this;
    }
    int posNo;
    private void Start()
    {
        ShuffleList(enemiesTransforms);
        posNo = 0;
        fightScreen.SetActive(true);
        Invoke(nameof(DisappearFight), 3f);

        InstantiateEnemies(DataContainer.Instance.randomEntries, allEnemiesMat[Random.Range(1, allEnemiesMat.Length)],-1);
        InstantiateEnemies(DataContainer.Instance.divineEntries, allEnemiesMat[0],0);
        InstantiateEnemies(DataContainer.Instance.rootEntries, allEnemiesMat[1],1);
        InstantiateEnemies(DataContainer.Instance.paragonEntries, allEnemiesMat[2], 2);
        InstantiateEnemies(DataContainer.Instance.ordinamEntries, allEnemiesMat[3], 3);

        totalEnemiesToInstantiate = instantiatedEnemies.Count;
        totalAliveCount = instantiatedEnemies.Count;
        totalAliveText.text = "Total Alive Count: " + totalAliveCount + " / " + totalEnemiesToInstantiate;
    }

    void ShuffleList(List<Transform> list)
    {
        System.Random rng = new System.Random();
        int n = list.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Transform value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }

    private void InstantiateEnemies(List<string> entries, Material material,int characterNo)
    {
        int count = entries.Count;
        int selectedCharacter=0;
        for (int i = 0; i < count; i++)
        {
            if (characterNo == -1)
            {
                selectedCharacter=Random.Range(0,enemyPrefabs.Length);
            }
            else
            {
                selectedCharacter = characterNo;
            }
            GameObject _enemy = Instantiate(enemyPrefabs[selectedCharacter]);
            _enemy.GetComponent<EnemyCustomizations>().enemyName = entries[i];

            _enemy.transform.position = enemiesTransforms[posNo].transform.position;
            _enemy.transform.rotation = enemiesTransforms[posNo].transform.rotation;

            if(characterNo!=0)
            _enemy.GetComponentInChildren<SkinnedMeshRenderer>().material = material;

            instantiatedEnemies.Add(_enemy);
            _enemy.SetActive(true);
            posNo++;
        }
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
        totalAliveCount = instantiatedEnemies.Count;
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
