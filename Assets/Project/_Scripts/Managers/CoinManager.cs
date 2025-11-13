using DG.Tweening;
using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public Transform Target;
    public GameObject Coin;
    public static CoinManager Instance;
    public int GoldCount;

    private void Awake()
    {
        Instance = this;
    }

    public void SpawnCoin(Transform spawnpoint, Transform targetpoint)
    {
        float chance = Random.Range(0f, 100f);
       // if (chance > 50f) return;
        
        GameObject currCoin = Instantiate(Coin, spawnpoint.position, spawnpoint.rotation);
        CoinMove(targetpoint, currCoin);

    }
    public void CoinMove(Transform target, GameObject coin)
    {
        coin.transform.DOScale(3f, 0.5f);
        coin.transform.DOJump(target.position + transform.up, 2f, 1, 0.5f)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
            {
                coin.transform.DOMove(target.position, 0.2f).OnComplete(() =>
                {
                    Destroy(coin);
                    GoldCount++;
                });
            });




    }





}
