using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<GameObject> contestants;
    public GameObject player;
    private Player playerScript;
    private int playerRank;
    private int finishedCount = 0, count;
    [Space]
    public Text rankTxt;
    public Canvas mainCanvas;
    private void Start()
    {
        playerScript = player.GetComponent<Player>();
        mainCanvas.enabled = true;
        contestants.Add(player);
        count = contestants.Count;
    }
    private void Update()
    {
        if(playerScript.isFinished == false)
        {
            RankSystem();
        }
    }
    public void RankSystem()
    {
        SortPlayers();
        playerRank = contestants.FindIndex(x => x == player);
        rankTxt.text = (finishedCount + playerRank + 1).ToString() + "/" + count.ToString();
    }

    private void SortPlayers()//the bigger is first
    {
        GameObject temp;
        for (int i = 0; i < contestants.Count - 1; i++)
        {
            for(int j = i + 1; j < contestants.Count; j++)
            {
                if(contestants[i].TryGetComponent(out AI ai))
                {
                    if (ai.isFinished == true)
                    {
                        finishedCount++;
                        contestants.RemoveAt(i);
                        break;
                    }
                }
                if(contestants[i].transform.position.z < contestants[j].transform.position.z)
                {
                    temp = contestants[i];
                    contestants[i] = contestants[j];
                    contestants[j] = temp;
                }
            }
        }
    }

    public void StartTheGame()
    {
        mainCanvas.enabled = false;
        playerScript.Begin();

        for (int i = 0; i < contestants.Count; i++)
            if (contestants[i].TryGetComponent(out AI ai))
                ai.Begin();
    }

    public void NextLevel()
    {
        int loadSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if(loadSceneIndex >= SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(0);
        }
        else
        {
            SceneManager.LoadScene(loadSceneIndex);
        }
    }
}
