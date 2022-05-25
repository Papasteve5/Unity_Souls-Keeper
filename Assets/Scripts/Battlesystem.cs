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

    public GameObject firePrefab;

    public Transform playerPos;
    [SerializeField] GameObject playerSprite;
    public Transform enemyPos;

    attribute playerAttribute;
    attribute enemyAttribute;

    public Text battleText;

    public PlayerHUD playerHUD;
    public EnemyHUD enemyHUD;

    [SerializeField] GameObject enemyInfo;

    public GameObject box;

    Vector3 originalPos;

    // Always updates HP and HUD of the Player and Enemy
    void Update()
    {
        playerHUD.setHP(playerAttribute.currentHP);
        playerHUD.setHUD(playerAttribute);

        enemyHUD.setHP(enemyAttribute.currentHP);
        enemyHUD.setHUD(enemyAttribute);
    }

    // Gets called when starting Scene
    void Start()
    {
        playerPrefab.GetComponent<Movement>().speed = 0;
        state = BattleState.START;
        StartCoroutine(setBattle());
        originalPos = new Vector3(playerAttribute.transform.position.x, playerAttribute.transform.position.y);
    }


    IEnumerator setBattle() {

        // Makes Player "invisible" for the start of battle
        playerPos = playerSprite.transform;
        playerSprite.SetActive(false);

        // Spawns Player and gets Information from script attribute
        GameObject playerSpawn = Instantiate(playerPrefab, playerPos);
        playerAttribute = playerSpawn.GetComponent<attribute>();

        // Spawns Enemy and gets Information from script attribute
        GameObject enemySpawn = Instantiate(enemyPrefab, enemyPos);
        enemyAttribute = enemySpawn.GetComponent<attribute>();

        battleText.text = "A wild " + enemyAttribute.Name + " approached";

        yield return new WaitForSeconds(2f);

        // Show Interfaces of Player
        playerHUD.setHUD(playerAttribute);

        // Change State
        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    // Gets called when Enemy Name is clicked after Attack Button
    IEnumerator PlayerAttack() {

        bool isDead = enemyAttribute.TakeDamage(playerAttribute.damage);

        enemyInfo.SetActive(false);

        battleText.text = enemyAttribute.Name + " has been hit";

        yield return new WaitForSeconds(2f);

        // Checks if Enemy is alive
        if(isDead) {

            state = BattleState.WON;
            EndBattle();

        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator PlayerFire() {

        // Creates Fire "Animation"
        GameObject fire = Instantiate(firePrefab, enemyPos);
        Destroy(fire, 2);

        // Makes Fire Damage
        bool isDead = enemyAttribute.TakeDamage(playerAttribute.damage * playerAttribute.fire_multiplier);

        enemyInfo.SetActive(false);

        battleText.text = "It was super effective";

        yield return new WaitForSeconds(2f);

        // Checks if Enemy is alive
        if(isDead) {

            state = BattleState.WON;
            EndBattle();

        } else {

            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    IEnumerator EnemyTurn() {

        playerPos = playerSprite.transform;
        playerSprite.SetActive(true);

        // Disables EnemyHUD during Enemys turn
        enemyInfo.SetActive(false);

        // Sets the scale of the GameObject "box"
        box.transform.localScale = new Vector3(8,8,0);

        playerAttribute.transform.position = originalPos;

        // Activates the movement script to doge Enemy hits
        playerAttribute.GetComponent<Movement>().speed = 5;

        battleText.text = enemyAttribute.Name + " is going to attack!";

        yield return new WaitForSeconds(2f);

        enemyAttribute.GetComponent<Attack>().allAttack();

        yield return new WaitForSeconds(3f);

        // Checks if Player is alive
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

        // Change size of box
        box.transform.localScale = new Vector3(32,8,0);

        // Makes Player "invisible" for this turn
        playerPos = playerSprite.transform;
        playerSprite.SetActive(false);

        battleText.text = "Choose your option";

        // Disable Movement for PlayerTurn
        playerAttribute.GetComponent<Movement>().speed = 0;
    }

    public void OnAttackButton() {

        if (state != BattleState.PLAYERTURN) {
            return;
        }
        else {
            enemyInfo.SetActive(true);
        }
    }

    public void OnAttackEnemyButton() {

        if (state != BattleState.PLAYERTURN) {
            return;
        }

        StartCoroutine(PlayerAttack());
    }

    public void OnFireButton() {

        if (state != BattleState.PLAYERTURN) {
            return;
        }

        StartCoroutine(PlayerFire());
    }

    public void OnActButton() {

        enemyInfo.SetActive(false);

        if (state != BattleState.PLAYERTURN) {
            return;
        }

        Debug.Log("Act");
    }
}
