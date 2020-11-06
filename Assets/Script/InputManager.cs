using UnityEngine;


public class InputManager : MonoBehaviour
{
    
    public static InputManager Instance { get; private set; }


    // dokunmatik ekranda parmağı kaç pixel sürükleyince
    // o finger swipe'ın bir input olarak varsayılacağını
    // depolayan değişken
    public float mobilTouchSensitivity = 30f;
    private int parmakId = -1;
    private Vector2 parmakIlkPosition;
    private float sensorOutput = 0f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (this != Instance)
        {
            Destroy(this);
            return;
        }


        mobilTouchSensitivity *= mobilTouchSensitivity;

    }

    private void Update()
    {

        for (int i = 0; i < Input.touchCount; i++)
        {
            Touch parmak = Input.GetTouch(i);

            if (parmakId == -1 && parmak.phase == TouchPhase.Began)
            {
                parmakId = parmak.fingerId;
                parmakIlkPosition = parmak.position;
            }
            else if (parmak.fingerId == parmakId)
            {
                // Eğer ki üzerinde bulunduğumuz parmak, hareketini takip etmekte olduğumuz parmak ise:
                if (parmak.phase != TouchPhase.Ended && parmak.phase != TouchPhase.Canceled)
                {
                    // Eğer parmağın ekranla bağlantısı kesilmemişse (parmak ekrandan kaldırılmamışsa):
                    // parmağın son konumuyla ilk konumu arasındaki Vector2 farkını bul
                    Vector2 deltaPosition = parmak.position - parmakIlkPosition;
                    if (deltaPosition.sqrMagnitude >= mobilTouchSensitivity)
                    {
                        // Eğer deltaPosition'ın büyüklüğünün karesi (Vector'lerin büyüklüğünün karesini bulmak,
                        // kendisini bulmaktan daha kolay ve hızlıdır) sensitivity'den büyükse (bu yüzden Start
                        // fonksiyonunda sensitivity'nin karesini aldık; iki uzunluğun karelerini kıyaslıyoruz):
                        float x = deltaPosition.x;
                        float y = deltaPosition.y;

                        if (y > Mathf.Abs(x))
                        {
                            // eğer parmak y ekseninde (yukarı yönde) daha çok hareket etmişse player'ı zıplat
                            PlayerControl.Instance.Zipla();
                        }
                        else if (x > 0f)
                        {
                            // eğer parmak sağ yönde daha çok hareket etmişse player'ı sağ yol ayrımına saptır
                            PlayerControl.Instance.SagaDon();
                        }
                        else
                        {
                            // eğer parmak sol yönde daha çok hareket etmişse player'ı sol yol ayrımına saptır
                            PlayerControl.Instance.SolaDon();
                        }

                        // bu parmakla işimiz bitti, artık herhangi bir parmağın hareketini takip etmediğimizi
                        // sisteme bildir
                        parmakId = -1;
                       
                    }
                    
                }
                else
                {
                    // Eğer takip ettiğimiz parmak ekrandan kaldırılmışsa parmakId'yi -1 yaparak
                    // artık herhangi bir parmağı takip etmediğimizi belirle
                    parmakId = -1;
                   
                }
            }
        }

    }
}