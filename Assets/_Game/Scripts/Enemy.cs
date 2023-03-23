using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : CharacterBase, IPoolCharacter, IHit
{
    public AIAgent agent;
    public bool IsMovable { get; private set; }
    public GameObject BotGameObject;

    public void OnSpawn()
    {
        agent.stateMachine.ChangeState(AIStateId.IdleState);

        IsAlive = true;
        CharacterCollider.enabled = true;

        SetUpBotStat();

        AttackTarget = null;
        AttackTargetTrans = null;

        SetRandomEnumData();
        SetUpHandWeapon();
        SetUpPantSkin();
        SetUpHat();
        SetUpShield();
        SetRandomBodySkin();
        SetRandomName();

        if (GameManager.Instance.CurrentGameState == GameState.Playing)
        {
            DisplayCharacterUI();
            IsMovable = true;
        }
        else
        {
            RemoveCharacterUI();
        }
    }
    public void OnDespawn()
    {
        RemoveCharacterUI();
    }
    protected override void GameManagerOnGameStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Playing:
                if (BotGameObject.activeInHierarchy)
                {
                    DisplayCharacterUI();
                }
                IsMovable = true;
                break;
            case GameState.Pause:
                IsMovable = false;
                break;
            case GameState.ReviveOption:
                IsMovable = false;
                break;
            case GameState.LoadLevel:
                BotPooling.Instance.PushBotToPool(BotGameObject);
                break;
            case GameState.ResultPhase:
                IsMovable = false;
                break;
            default:
                break;
        }
    }
    private void SetRandomEnumData()
    {
        WeaponTag = (WeaponType)Random.Range((int)WeaponType.Axe, (int)WeaponType.Knife + 1);
        switch (WeaponTag)
        {
            case WeaponType.Axe:
                WeaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Axe_0, (int)WeaponSkinType.Axe_1_2 + 1);
                break;
            case WeaponType.Knife:
                WeaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Knife_1, (int)WeaponSkinType.Knife_2 + 1);
                break;
            case WeaponType.Candy:
                WeaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Candy_1, (int)WeaponSkinType.Candy_2 + 1);
                break;
            case WeaponType.Hammer:
                WeaponSkinTag = (WeaponSkinType)Random.Range((int)WeaponSkinType.Hammer_1, (int)WeaponSkinType.Hammer_2 + 1);
                break;
            default:
                break;
        }

        PantSkinTag = (PantSkinType)Random.Range((int)PantSkinType.Batman, (int)PantSkinType.Vantim + 1);
        HatTag = (HatType)Random.Range((int)HatType.Arrow, (int)HatType.Beard + 1);

        //NOTE: Bot can have a chance to get shield only if player have shield
        if (Player.PlayerGlobalReference.ShieldTag != ShieldType.None)
        {
            float ran = Random.Range(0f, 100f);
            if (ran > ConstValues.VALUE_PERCENTAGE_OF_BOT_HAVE_SHIELD)
            {
                ShieldTag = (ShieldType)Random.Range((int)ShieldType.Star, (int)ShieldType.Knight + 1);
            }
        }
    }
    public void SetRandomBodySkin()
    {
        CharacterRenderer.material = ItemStorage.Instance.GetRandomBotMaterial();
    }
    public void SetRandomName()
    {
        CharacterName = ItemStorage.Instance.GetRandomBotName();
    }
    public void SetUpBotStat()
    {
        CharacterLevel = LevelManager.Instance.GetCurrentBotLevel();
        SizeUpCharacter(CharacterLevel, false);
        
        int floorScore = Mathf.CeilToInt(Mathf.Pow(CharacterLevel/scoreSlope, 2));
        int CeilScore = Mathf.FloorToInt(Mathf.Pow(CharacterLevel + 1/scoreSlope, 2));

        Score = Random.Range(floorScore, CeilScore);
        KillScore = Mathf.FloorToInt(CharacterLevel * killScoreMultipler);
    }

    public void OnHit(CharacterBase bulletOwner, Weapon weapon)
    {
        agent.stateMachine.ChangeState(AIStateId.DeathState);
        IsAlive = false;
        CharacterCollider.enabled = false;

        ItemStorage.Instance.PushWeaponToPool(weapon);

        ParticlePooling.Instance.PopParticleFromPool(ParticleType.HitCharacter,
                                                    CharaterTrans.position,
                                                    Quaternion.Euler(0, 180f, 0) * Quaternion.LookRotation(weapon.WeaponTrans.forward),
                                                    this);

        RemoveCharacterUI();

        bulletOwner?.OnKillEnemy(KillScore);
    }
}
