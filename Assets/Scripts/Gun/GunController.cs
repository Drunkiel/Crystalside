using UnityEngine;

public class GunController : MonoBehaviour
{
    public bool isGunShowed;
    public int ammunitionInMagazine;
    private int maxAmmunitionInMagazine = 30;
    public int remainingMagazines;
    public Transform point;
    public Transform player;

    public LayerMask layerMask;

    // Update is called once per frame
    void Update()
    {
        if (GameController.isGamePlaying && !GameController.isGamePaused)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1)) ShowHideGun();

            if (isGunShowed && Input.GetKeyDown(KeyCode.Mouse0)) Shot();
            if (isGunShowed && ammunitionInMagazine < maxAmmunitionInMagazine && Input.GetKeyDown(KeyCode.R)) Reload(); 
        }
    }

    private void ShowHideGun()
    {
        isGunShowed = !isGunShowed;
        transform.GetChild(0).gameObject.SetActive(isGunShowed);
    }

    private void Shot()
    {
        if (ammunitionInMagazine > 0)
        {
            ammunitionInMagazine -= 1;

            Quaternion playerRotation = player.rotation;
            Quaternion childRotation = transform.GetChild(0).rotation;

            // Kombinacja rotacji gracza, obiektu i jego dziecka
            Quaternion totalRotation = playerRotation * childRotation;

            // Obliczanie kierunku raycasta na podstawie rotacji
            Vector3 rayDirection = totalRotation * Vector3.forward;

            RaycastHit hit;
            if (Physics.Raycast(point.position, point.forward, out hit, 50, layerMask))
            {
                print(hit.collider.transform.name);
            }

            // Wizualizacja raycasta
            Debug.DrawRay(point.position, point.forward * 50, Color.red, 1.0f);
        }
    }

    private void Reload()
    {
        if (remainingMagazines == 0) return; 

        ammunitionInMagazine = maxAmmunitionInMagazine;
        remainingMagazines -= 1;
    }
}
