using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class Battlesystem : MonoBehaviour
{
    public BattleState state;
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform playerPos;
    public Transform enemyPos;

    attribute playerAttribute;
    attribute enemyAttribute;

    public Text enemyName;

    public BattleHUD playerHUD;

    Vector3 originalPos;

    void Update()
    {
        playerHUD.setHP(playerAttribute.currentHP);
        playerHUD.setHUD(playerAttribute);
    }

    void Start()
    {
        state = BattleState.START;
        setBattle();
        originalPos = new Vector3(playerAttribute.transform.position.x, playerAttribute.transform.position.y);
    }

    void setBattle() {

        // Spawn Player and get it's Information
        GameObject playerSpawn = Instantiate(playerPrefab, playerPos);
        playerAttribute = playerSpawn.GetComponent<attribute>();

        // Disable Movement for the start of Battle (for PlayerTurn)
        playerPrefab.GetComponent<Movement>().enabled = false;

        // Spawn Enemy and get it's Information
        GameObject enemySpawn = Instantiate(enemyPrefab, enemyPos);
        enemyAttribute = enemySpawn.GetComponent<attribute>();

        // Show Interfaces of Player
        playerHUD.setHUD(playerAttribute);

        // Change State
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    IEnumerator PlayerAttack() {

        bool isDead = enemyAttribute.TakeDamage(playerAttribute.damage);

        Debug.Log("Enemy Hit");

        yield return new WaitForSeconds(2f);

        if(isDead) {

            state = BattleState.WON;
            EndBattle();

        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn() {

        playerAttribute.transform.position = originalPos;


        // Activates the movement script to doge Enemy hits
        playerAttribute.GetComponent<Movement>().enabled = true;

        yield return new WaitForSeconds(2f);

        enemyAttribute.GetComponent<Attack>().allAttack();

        yield return new WaitForSeconds(3f);

        if(playerAttribute.currentHP <= 0) {

            state = BattleState.LOST;
            EndBattle();

        } else {

            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle() {

        if (state == BattleState.WON) {

            SceneManager.LoadScene(sceneName:"WinScreen");

        } else if (state == BattleState.LOST) {

            SceneManager.LoadScene(sceneName:"Deathscreen");
        }
    }

    void PlayerTurn() {

        // Disable Movement for PlayerTurn
        playerAttribute.GetComponent<Movement>().enabled = false;

    }

    public void OnAttackButton() {

        if (state != BattleState.PLAYERTURN) {
            return;
        }

        StartCoroutine(PlayerAttack());
    }
}
