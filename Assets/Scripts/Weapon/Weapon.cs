using Carmone.Item;
using EnumTypes;
using System.Collections;
using UnityEngine;


namespace Carmone.Weapons
{
    public class Weapon : MonoBehaviour
    {
        public WeaponType weaponType;
        public float damage;
        public int penetration;
        public float bulletSpeed;
        public float coolTime;
        public int projectileCount = 3;
        public int knockback;
        private PoolType bulletType;

        float timer;
        Player player;
        bool isShown;
        float bulletScale = 1f;
        WeaponStat weaponStat;

        private GameObject shield;

        private void Awake()
        {
            player = GameManager.instance.player;
        }
        private StatEffect[] effects;
        private int enchantmentLevel;
        public int EnchantmentLevel { get => enchantmentLevel; }

        void Update()
        {
            if (!GameManager.instance.isLive)
                return;

            HandleWeaponType();
        }

        private void HandleWeaponType()
        {
            switch (weaponType)
            {
                case WeaponType.ELEMENTAL:
                    RotateElementalWeapon();
                    break;
                case WeaponType.SWORD:
                    if (!isShown)
                    {
                        StartCoroutine(ShowAndHideWeapon());
                    }
                    if (shield == null) break;
                    var shieldTransForm = shield.GetComponentInChildren<SpriteRenderer>().transform;
                    shieldTransForm.localScale = new Vector3(1, 1, 1) * bulletScale;
                    var playerDirection = player.CurrentDirection;
                    if (playerDirection == Vector2.zero)
                    {
                        playerDirection = Player.DEFAULT_DIRECTION;
                    }
                    shieldTransForm.localRotation = Quaternion.Euler(0, 0, GetAngleFromDirection(playerDirection.x, playerDirection.y));
                    break;
                default:
                    HandleRegularWeapon();
                    break;
            }
        }

        private void RotateElementalWeapon()
        {
            transform.Rotate(Vector3.back * bulletSpeed * Time.deltaTime);
        }

        private void HandleRegularWeapon()
        {
            timer += Time.deltaTime;

            if (timer > coolTime)
            {
                timer = 0f;
                Fire();
            }
        }

        public void Setting()
        {
            if (weaponType == WeaponType.ELEMENTAL)
            {
                Batch();
            }
        }

        public void LevelUp(WeaponStat nextWeaponStat)
        {
            ApplyStat(nextWeaponStat);
            if (weaponType == WeaponType.ELEMENTAL)
            {
                Batch();
            }
        }

        public void Init(ItemData data)
        {
            // Basic Set
            name = "Weapon " + data.itemType;
            transform.parent = player.transform;
            transform.localPosition = Vector3.zero;

            // Property Set
            weaponType = data.weaponType;
            bulletType = (PoolType)data.weaponType;
            ApplyStat(data.initStat);
            switch (weaponType)
            {
                case WeaponType.SWORD:
                    EquipShield();
                    break;
                case WeaponType.ELEMENTAL:
                    Batch();
                    break;
                case WeaponType.AREA:
                    Transform bullet = GetBullet(bulletType).transform;
                    bullet.parent = transform;
                    bullet.localPosition = Vector3.zero;
                    bullet.parent.transform.localPosition = new Vector3(0, -0.5f, 0);
                    bullet.GetComponent<Bullet>().Init(
                        damage: damage,
                        per: -100,
                        knockback: knockback,
                        dir: Vector3.zero
                    );
                    break;
                default:
                    break;
            }

            //Hand Set
            Hand hand = player.hands[(int)data.itemType];
            hand.spriter.sprite = data.hand;
            hand.gameObject.SetActive(true);
        }

        public void ApplyStat()
        {
            ApplyStat(weaponStat);
        }

        void ApplyStat(WeaponStat weaponStat)
        {
            this.weaponStat = weaponStat;
            // 무기데미지 * (캐릭터 기본 무기데미지 + 캐릭터 무기데미지 상승량)
            this.damage = weaponStat.damage * (player.playerStatus.defaultDamageMultiply + player.playerStatus.gearDamageMultiplyIncrease);
            // 무기 크기 * ( 캐릭터 기본 무기 크기 + 캐릭터 무기 크기 상승량)
            this.bulletScale = weaponStat.scale * (player.playerStatus.defaultSizeMultiply + player.playerStatus.gearSizeMultiplyIncrease);
            // 쿨타임 * (캐릭터 기본 쿨타임 - 캐릭터 쿨타임 감소량)
            this.coolTime = weaponStat.attackSpeed * (player.playerStatus.defaultCoolTimeMultiply - player.playerStatus.gearCoolTimeMultiplyDecrease);
            // 투사체 속도 * (캐릭터 기본 투사체 속도 + 캐릭터 투사체 속도 증가량)
            this.bulletSpeed = weaponStat.projectileSpeed * 150 * (player.playerStatus.defaultProjectileSpeedMultiply + player.playerStatus.gearProjectileSpeedMultiplyIncrease);
            // 관통 + 캐릭터 기본 관통 + 캐릭터 관통 증가량
            this.penetration = weaponStat.penetration + player.playerStatus.defaultPenetration + player.playerStatus.gearPenetrationIncrease;
            // 캐릭터 기본 넉백 + 캐릭터 넉백 증가량
            this.knockback = player.playerStatus.defaultKnockback + player.playerStatus.gearKnockbackIncrease;
            // 투사체 개수 + 캐릭터 기본 투사체 개수 + 캐릭터 투사체 개수 증가량
            this.projectileCount = weaponStat.projectileCount + player.playerStatus.defaultProjectileCountIncrease + player.playerStatus.gearProjectileCountIncrease;

            if (effects != null)
            {
                foreach (var effect in effects)
                {
                    ApplyEffect(effect);
                }
            }
        }

        #region Element
        private Transform InitBullet(int index, bool update)
        {
            Transform bullet;
            if (update)
            {
                bullet = GetBullet(bulletType).transform;
                var obj = transform.GetChild(index);
                if (obj != null)
                {
                    obj.gameObject.SetActive(false);
                }
            }
            else if (index < transform.childCount)
            {
                bullet = transform.GetChild(index);
            }
            else
            {
                bullet = GetBullet(bulletType).transform;
            }
            bullet.parent = transform;
            return bullet;
        }

        void Batch(bool update = false)
        {
            for (int index = 0; index < projectileCount; index++)
            {
                Transform bullet = InitBullet(index, update);

                bullet.localPosition = Vector3.zero;
                bullet.localRotation = Quaternion.identity;

                Vector3 rotVec = Vector3.forward * 360 * index / projectileCount;
                bullet.Rotate(rotVec);
                bullet.Translate(bullet.up * 1.5f, Space.World);

                bullet.GetComponent<Bullet>().Init(
                    damage: damage,
                    per: -100,
                    knockback: knockback,
                    dir: Vector3.zero
                 );
            }
        }
        #endregion

        #region Fire
        void Fire()
        {
            switch (weaponType)
            {
                case WeaponType.FIREBALL:
                    FireFireball();
                    break;
                case WeaponType.ARROW:
                    FireBow();
                    break;
            }
        }
        private void FireFireball()
        {
            var nearestTarget = player.scanner.NearestTarget;
            if (nearestTarget == null) return;
            Vector3 targetPso = nearestTarget.position;
            Vector3 dir = (targetPso - transform.position).normalized;
            dir = dir.normalized;
            Debug.Log(dir);

            Transform bullet = GetBullet(bulletType).transform;
            bullet.position = transform.position;
            bullet.rotation = Quaternion.FromToRotation(Vector3.up, dir);
            bullet.localScale = new Vector3(1, 1, 1) * bulletScale;
            bullet.GetComponent<Bullet>().Init(
                damage: damage,
                per: penetration,
                knockback: knockback,
                dir: dir
            );

            AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
        }

        private void FireBow()
        {
            int bulletCount = projectileCount;
            float startAngle = -20;
            float endAngle = 20;
            float stepAngle = (endAngle - startAngle) / Mathf.Max(bulletCount - 1, 1);
            for (int i = 0; i < bulletCount; i++)
            {
                float angle = startAngle + stepAngle * i;
                Transform bullet = GetBullet(bulletType).transform;
                bullet.position = transform.position;

                // 발사 방향
                var playerDirection = player.CurrentDirection;
                if (playerDirection == Vector2.zero)
                {
                    playerDirection = Player.DEFAULT_DIRECTION;
                }
                Vector3 dir = Quaternion.AngleAxis(angle, Vector3.forward) * playerDirection;

                // 화살 스프라이트 방향
                float spriteRotationAngle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 90;
                bullet.localRotation = Quaternion.Euler(0, 0, spriteRotationAngle);
                bullet.localScale = new Vector3(1, 1, 1) * bulletScale;

                Bullet bulletComponent = bullet.GetComponent<Bullet>();
                bulletComponent.Init(
                    damage: damage,
                    per: penetration,
                    knockback: projectileCount,
                    dir: dir
                );
            }
            AudioManager.instance.PlaySfx(AudioManager.Sfx.Range);
        }
        #endregion

        #region Sword
        IEnumerator ShowAndHideWeapon()
        {
            isShown = true;
            Transform bullet = GetBullet(bulletType).transform;
            bullet.parent = player.transform;
            var spriteRenderer = bullet.GetComponent<SpriteRenderer>();
            if (player.CurrentDirection.x <= 0)
            {
                bullet.localPosition = Vector3.left;
                spriteRenderer.flipY = false;
            }
            else
            {
                bullet.localPosition = Vector3.right;
                spriteRenderer.flipY = true;
            }
            bullet.localScale = new Vector3(1, 1, 1) * bulletScale;
            bullet.localRotation = Quaternion.Euler(0, 0, 90);
            bullet.GetComponent<Bullet>().Init(
                damage: damage,
                per: -1,
                knockback: knockback,
                dir: Vector2.zero
            );
            yield return new WaitForSeconds(coolTime);
            bullet.gameObject.SetActive(false);
            isShown = false;
        }

        private float GetAngleFromDirection(float x, float y)
        {
            float angle = Mathf.Atan2(y, x) * Mathf.Rad2Deg;
            if (angle < 0)
            {
                angle += 360;
            }
            return angle;
        }

        private void EquipShield()
        {
            shield = GetBullet(PoolType.Shield);
            shield.transform.parent = player.transform;
            shield.transform.localPosition = Vector3.zero;
            shield.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        private void ChangeShield()
        {
            shield.SetActive(false);
        }
        #endregion

        public void EnchantWeapon(PoolType? bulletType, StatEffect[] effects)
        {
            enchantmentLevel++;
            if (bulletType != PoolType.None)
            {
                this.bulletType = (PoolType)bulletType;
            }
            this.effects = effects;
            foreach (var effect in effects)
            {
                ApplyEffect(effect);
            }
            if (weaponType == WeaponType.SWORD && bulletType == PoolType.Sword_2)
            {
                ChangeShield();
            }
            if (weaponType == WeaponType.ELEMENTAL)
            {
                Batch(true);
            }
        }

        private void ApplyEffect(StatEffect effect)
        {
            float effectValue = effect.value * enchantmentLevel;

            switch (effect.effect)
            {
                case StatEffectType.DamageIncreasePercent:
                    damage += damage * effectValue;
                    break;

                case StatEffectType.SizeIncreasePercent:
                    bulletScale *= effectValue;
                    break;

                case StatEffectType.ProjectileSpeedMultiplier:
                    bulletSpeed += effectValue;
                    break;

                case StatEffectType.PenetrationIncrease:
                    penetration += (int)effectValue;
                    break;
            }
        }

        private GameObject GetBullet(PoolType poolType)
        {
            WeaponType weaponType = (WeaponType)poolType;
            return GameManager.instance.pool.Get(weaponType.ToString());
        }
    }
}