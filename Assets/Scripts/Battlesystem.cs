using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST }

public class Battlesystem : MonoBehaviour
{
    public BattleState state;

    // GameObject of characters
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    // GameObject of others
    public GameObject firePrefab;
    public GameObject box;
    [SerializeField] GameObject playerSprite;

    // Spawn Position
    Vector3 originalPos;
    public Transform playerPos;
    public Transform enemyPos;

    // Clones of Prefabs, in Scene these are the actual GameObjects
    attribute playerAttribute;
    attribute enemyAttribute;

    // UI of Player & Enemy
    public PlayerHUD playerHUD;
    public EnemyHUD enemyHUD;

    // Instantiated Object from script "Items"
    public Items item_functions;

    // Displayed Text during battle
    public Text battleText;

    //Button Objects (FIGHT / ACT / ITEM / MERCY)
    [SerializeField] GameObject enemyInfo;
    [SerializeField] GameObject acts;
    [SerializeField] GameObject items;
    [SerializeField] GameObject mercyDecision;

    // Boolean to check if a move was made during Player Turn
    public bool attacked;
    public bool acted;

    // Updates HP & HUD of the Player and the Enemy to the newest version
    void Update()
    {
        // Functions "setHP" & "setHUD" used from the PLayerHUD and EnemyHUD script
        playerHUD.setHP(playerAttribute.currentHP);
        playerHUD.setHUD(playerAttribute);

        enemyHUD.setHP(enemyAttribute.currentHP);
        enemyHUD.setHUD(enemyAttribute);
    }

    // Start Function -> Calls "setBattle"
    void Start()
    {
        playerPrefab.GetComponent<Movement>().speed = 0;

        state = BattleState.START;
        StartCoroutine(setBattle());

        // Gets starting Position of Player for Position resets
        originalPos = new Vector3(playerAttribute.transform.position.x, playerAttribute.transform.position.y);
    }

    // Gets called when Scene is started
    IEnumerator setBattle()
    {
        // Deactivates buttons and Player Sprite
        enemyInfo.SetActive(false);
        acts.SetActive(false);
        items.SetActive(false);
        mercyDecision.SetActive(false);
        playerSprite.SetActive(false);

        // Sets Player to the right position
        playerPos = playerSprite.transform;

        // Spawns Player and gets Information from script attribute
        GameObject playerSpawn = Instantiate(playerPrefab, playerPos);
        playerAttribute = playerSpawn.GetComponent<attribute>();
        playerAttribute.setCharacter();

        // Spawns Enemy and gets Information from script attribute
        GameObject enemySpawn = Instantiate(enemyPrefab, enemyPos);
        enemyAttribute = enemySpawn.GetComponent<attribute>();
        enemyAttribute.setCharacter();

        // Text shown at the start of the battle
        battleText.text = "\"My- \"";
        yield return new WaitForSeconds(1f);
        battleText.text = "\"No\"";
        yield return new WaitForSeconds(1f);
        battleText.text = "\"OUR NAME IS " + enemyAttribute.Name + "\"";
        yield return new WaitForSeconds(2f);

        // Show Interfaces of Player
        playerHUD.setHUD(playerAttribute);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    // Gets called at the end of PlayerTurn
    void showHUD()
    {
        // Activates / makes HUD visible
        playerHUD.playerName.enabled = true;
        playerHUD.playerLVL.enabled = true;
        playerHUD.buttons.SetActive(true);

        // Sets Healthbar to the standard position
        playerHUD.health_POS.anchoredPosition = new Vector2(0, 47);
        playerHUD.playerHP_POS.anchoredPosition = new Vector2(264, 47);
    }

    // Gets called during EnemyTurn
    void hideHUD()
    {
        // Deactivates / makes HUD invisible
        playerHUD.playerName.enabled = false;
        playerHUD.playerLVL.enabled = false;
        playerHUD.buttons.SetActive(false);

        // Sets Healthbar to the bottom left of the screen
        playerHUD.health_POS.anchoredPosition = new Vector2(-715, 10);
        playerHUD.playerHP_POS.anchoredPosition = new Vector2(-470, 10);
    }


    // Function resets battlescene for the players turn
    void PlayerTurn()
    {
        // Change box and player so he can make a turn
        box.transform.localScale = new Vector3(32, 8, 0);
        playerAttribute.GetComponent<Movement>().speed = 0;
        playerPos = playerSprite.transform;
        playerSprite.SetActive(false);

        acted = false;
        attacked = false;

        battleText.enabled = true;
        battleText.text = "* Choose your move";

        showHUD();
    }

    // Function checks status, sets battle scene different, attacks and checks if battle is over
    IEnumerator EnemyTurn()
    {
        // Informs the Player, when "MERCY" (sparing the enemy) is possible
        if (enemyAttribute.friendliness >= enemyAttribute.maxfriendliness)
        {
            battleText.text = "* The Enemy has taken a Liking to you now";
            yield return new WaitForSeconds(2f);
        }
        // Informs the Player, when he attacks the enemy
        else if (enemyAttribute.currentHP < enemyAttribute.maxHP)
        {
            battleText.text = "* " + enemyAttribute.Name + " has been hit";
            yield return new WaitForSeconds(1f);
        }

        // Informs the Player, that the enemy is attacking
        battleText.text = "* " + enemyAttribute.Name + " is going to attack!";
        yield return new WaitForSeconds(2f);

        // Disables HUD during Enemys turn
        enemyInfo.SetActive(false);
        hideHUD();

        // Changes Box and Player for an enemy Attack
        box.transform.localScale = new Vector3(8, 8, 0);
        playerPos = playerSprite.transform;
        playerSprite.SetActive(true);
        playerAttribute.transform.position = originalPos;
        playerAttribute.GetComponent<Movement>().speed = 5;

        battleText.enabled = false;
        yield return new WaitForSeconds(2f);

        // Function allAttack from the Attack script inside the enemy
        enemyAttribute.GetComponent<Attack>().allAttack();
        yield return new WaitForSeconds(6f);

        // Checks if Player is alive
        if (playerAttribute.currentHP <= 0)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }
    }


    // Checks if Player has WON or LOST
    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            StartCoroutine(EnemyDeath());
        }
        // Loads a new scene, where you can retry or exit game
        else if (state == BattleState.LOST)
        {
            SceneManager.LoadScene(sceneName: "DeathScene");
        }
    }

    // Gets called in Function "EndBattle" -> Shows Text, deletes enemy and loads a new Scene
    IEnumerator EnemyDeath()
    {
        battleText.text = "Argh, you've defeated me...";
        yield return new WaitForSeconds(2f);

        Destroy(enemyAttribute.gameObject);

        battleText.text = "You've won!";
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName: "WinScene");
    }


    // FIGHT Button, function called when clicked
    public void OnAttackButton()
    {
        if (state != BattleState.PLAYERTURN || acted == true)
        {
            return;
        }
        // Shows UI of Options to fight
        else if (attacked == false)
        {
            battleText.enabled = false;
            items.SetActive(false);
            acts.SetActive(false);
            enemyInfo.SetActive(true);
        }
    }

    // Function called when Enemy's name is clicked
    public void OnAttackEnemyButton()
    {
        PlayerAttack();
        attacked = true;
    }

    // Function called after Function "OnAttackEnemyButton" was clicked -> hurts enemy and checks if he's won battle
    void PlayerAttack()
    {
        // Function "TakeDamage" gets called from the attribute script
        bool isDead = enemyAttribute.TakeDamage(playerAttribute.damage);

        enemyInfo.SetActive(false);
        battleText.enabled = true;

        // Checks if Enemy is alive
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }

    // Function called when Fire Button is clicked
    public void OnFireButton()
    {
        if (attacked == false)
        {
            attacked = true;
            StartCoroutine(PlayerFire());
        }
        else
        {
            return;
        }
    }

    // Function called after Function "OnFireButton" was clicked -> does twice as much damage as normal Attack
    IEnumerator PlayerFire()
    {
        // Creates Fire "Animation"
        GameObject fire = Instantiate(firePrefab, enemyPos);
        Destroy(fire, 2);

        // Makes Fire Damage (Twice as much damage as a normal attack)
        bool isDead = enemyAttribute.TakeDamage(playerAttribute.damage * playerAttribute.fire_multiplier);

        enemyInfo.SetActive(false);
        battleText.enabled = true;
        battleText.text = "* It was super effective";
        yield return new WaitForSeconds(2f);

        // Checks if Enemy is alive
        if (isDead)
        {
            state = BattleState.WON;
            EndBattle();
        }
        else
        {
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }
    }


    // ACT Button, function called when clicked
    public void OnActButton()
    {
        if (state != BattleState.PLAYERTURN || acted == true || attacked == true)
        {
            return;
        }
        else
        {
            battleText.enabled = false;
            enemyInfo.SetActive(false);
            items.SetActive(false);
            acts.SetActive(true);
        }
    }

    // Joke Button, called when clicked "OnActButton" -> Neutral Dialogue
    public void OnActButtonJoke()
    {
        acted = true;
        StartCoroutine(PlayerActJoke());
    }

    // Function called when "OnActButtonJoke" is clicked -> get FriendshiPoints only once
    IEnumerator PlayerActJoke()
    {
        acts.SetActive(false);
        battleText.enabled = true;

        bool toldJoke = false;

        string[] joke = { "* It found the joke hilarious", "* You have already told that joke" };
        string[] reactions = { "\"HAHAHAHAAHAHA\" \n\"I hate to admit it but that was funny!\"", "\"Dude you have already told me the joke\"" };

        battleText.text = "* You told the enemy a joke";
        yield return new WaitForSeconds(2f);

        // First time telling a joke -> gains FriendshipPoints
        if (toldJoke == false)
        {
            battleText.text = joke[0];
            yield return new WaitForSeconds(2f);
            battleText.text = reactions[0];
            yield return new WaitForSeconds(3f);
            enemyAttribute.friendliness += 25;

            // If Player has threated an enemy, this will reset the damage to it's original value
            if (playerAttribute.takenDamage > 1)
            {
                playerAttribute.takenDamage = 1;
                battleText.text = "* Damage has been reduced";
                yield return new WaitForSeconds(2f);
            }
            toldJoke = true;
        }
        // Second Time telling a joke (the same) -> doesn't gain FriendshipPoints
        else
        {
            battleText.text = joke[1];
            yield return new WaitForSeconds(2f);
            battleText.text = reactions[1];
            yield return new WaitForSeconds(2f);
        }
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    // Compliment Button, called when clicked "OnActButton" -> Positive Dialogue
    public void OnActButtonCompliment()
    {
        acted = true;
        StartCoroutine(PlayerActCompliment());
    }

    // Function called when "OnActButtonCompliment" is clicked -> always get FriendshipPoints
    IEnumerator PlayerActCompliment()
    {
        acts.SetActive(false);

        battleText.enabled = true;
        yield return new WaitForSeconds(2.0f);
        battleText.text = "* You gave the enemy a compliment";

        yield return new WaitForSeconds(2.0f);
        battleText.text = "* It liked it a lot";

        yield return new WaitForSeconds(2.0f);
        battleText.text = "\"Awww, thank you!\"";

        enemyAttribute.friendliness += 20;
        acted = true;

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    // Threat Button, called when clicked "OnActButton" -> Negative Dialogue
    public void OnActButtonThreat()
    {
        acted = true;
        StartCoroutine(PlayerActThreat());
    }

    // Function called when "OnActButtonThreat" is clicked -> increase Enemy Damage and get some FriendshipPoints
    IEnumerator PlayerActThreat()
    {
        acts.SetActive(false);

        battleText.enabled = true;

        battleText.text = "* You threatened the enemy";
        enemyAttribute.friendliness += 10;
        yield return new WaitForSeconds(2f);

        battleText.text = "\"Tch, go home kid\"";
        yield return new WaitForSeconds(2f);

        // "Increases Enemy Damage"
        playerAttribute.takenDamage += 1;
        battleText.text = "* The enemys Damage has increased";
        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }


    // ITEM Button, function called when clicked
    public void OnItemButton()
    {
        if (state != BattleState.PLAYERTURN || acted == true || attacked == true)
        {
            return;
        }
        else
        {
            battleText.enabled = false;
            enemyInfo.SetActive(false);
            acts.SetActive(false);
            items.SetActive(true);
        }
    }

    // HealthPotion Button, function called when clicked
    public void OnHealthPotionButton()
    {
        if (playerAttribute.currentHP != playerAttribute.maxHP && item_functions.used == false)
        {
            item_functions.HealthPotion(playerAttribute);
            items.SetActive(false);
            state = BattleState.ENEMYTURN;
            StartCoroutine(EnemyTurn());
        }

        // Calls Function errorHeal from the "Items" script
        else
        {
            StartCoroutine(item_functions.errorHeal(battleText));
        }
    }


    // MERCY Button, function called when clicked
    public void OnMercyButton()
    {
        if (state != BattleState.PLAYERTURN || acted == true || attacked == true)
        {
            return;
        }
        else if (enemyAttribute.friendliness >= enemyAttribute.maxfriendliness)
        {
            battleText.enabled = true;
            enemyInfo.SetActive(false);
            acts.SetActive(false);
            items.SetActive(false);
            StartCoroutine(PlayerMercy());
        }
    }

    // Gets called when requirements are fullfilled and OnMercyButton is clicked
    IEnumerator PlayerMercy()
    {
        state = BattleState.WON;

        battleText.text = "\"You know what?\"";
        yield return new WaitForSeconds(2f);
        battleText.text = "\"Screw this fight\"";
        yield return new WaitForSeconds(2f);
        battleText.text = "\"I kinda like you\"";
        yield return new WaitForSeconds(2f);
        battleText.text = "* It seems the enemy want's to be friends with you";
        yield return new WaitForSeconds(2f);

        // Displays Message on Top Center of the screen
        battleText.alignment = TextAnchor.UpperCenter;
        battleText.text = "* Do you want to recruit " + enemyAttribute.Name + "?";

        // Displays OnMercyYESButton & OnMercyNOButton
        mercyDecision.SetActive(true);
    }

    public void OnMercyYESButton()
    {
        StartCoroutine(MercyYES());
    }

    // Function called when "OnMercyYESButton" is clicked -> Loads new Scene to recruit enemy
    IEnumerator MercyYES()
    {
        battleText.alignment = TextAnchor.MiddleCenter;
        mercyDecision.SetActive(false);

        battleText.text = "\"I'm glad to be working with you\"";
        yield return new WaitForSeconds(3f);

        SceneManager.LoadScene(sceneName: "NewMemberScene");
    }

    public void OnMercyNOButton()
    {
        StartCoroutine(MercyNO());
    }

    // Function called when "OnMercyNOButton" is clicked -> Player wins, rejects enemy
    IEnumerator MercyNO()
    {
        battleText.alignment = TextAnchor.MiddleCenter;
        mercyDecision.SetActive(false);

        battleText.text = "\"That's ok\"";
        yield return new WaitForSeconds(2f);

        battleText.text = "\"I'll see you later\"";
        yield return new WaitForSeconds(2f);

        battleText.text = "* You have rejected " + enemyAttribute.Name + " from your party";
        yield return new WaitForSeconds(2f);

        // Creates the visual of the enemy fading out
        enemyAttribute.GetComponentInChildren<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0.25f);
        enemyAttribute.GetComponentInChildren<Animator>().enabled = false;

        battleText.text = "You've won!";
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName: "WinScene");
    }
}
