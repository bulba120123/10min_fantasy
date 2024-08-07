using EnumTypes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Carmone
{
    public class TownManager : MonoBehaviour
    {
        [SerializeField]
        Dictionary<GameObject, (int id, bool active)> towns = new Dictionary<GameObject, (int, bool)>();

        [SerializeField]
        List<GameObject> townArrows = new List<GameObject>();

        public void OpenAllTown()
        {
            float[] radiuses = { 10f, 20f, 30f };

            for(int i=0;i<radiuses.Length;i++)
            {
                GameObject townObj = GameManager.instance.pool.Get(PoolType.Town);
                towns.Add(townObj, (i, true));
                Vector3 townPosition = GetRandomPoint(GameManager.instance.playerPos, radiuses[i]);
                townObj.transform.position = townPosition;

                GameObject targetTownArrowObj = GameManager.instance.pool.Get(PoolType.TargetTownArrow);
                targetTownArrowObj.GetComponent<TargetTownArrow>().SetArrow(townPosition);
                targetTownArrowObj.transform.SetParent(GameManager.instance.player.transform.Find("TownArrows"));
                townArrows.Add(targetTownArrowObj);
            }

        }
        public Vector3 GetRandomPoint(Vector3 rootPos, float radius)
        {
            float angle = Random.Range(0f, Mathf.PI * 2);

            float x = rootPos.x + radius * Mathf.Cos(angle);
            float y = rootPos.y + radius * Mathf.Sin(angle);

            return new Vector3(x, y, rootPos.z);
        }

        public void DisableTown(GameObject townObj)
        {
            int townId = towns[townObj].id;
            towns[townObj] = (towns[townObj].id, false);
        }

        public void CloseTownAuto()
        {
            var findDisableTowns = towns.Where(town => town.Value.active == false);

            foreach (var disableTown in findDisableTowns)
            {
                GameManager.instance.pool.Release(disableTown.Key);
                int disableTownId = disableTown.Value.id;

                GameManager.instance.pool.Release(townArrows[disableTownId]);
            }
            
            GameManager.instance.NextStage();
        }
    }
}
