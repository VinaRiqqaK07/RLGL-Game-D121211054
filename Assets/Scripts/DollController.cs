using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DollController : MonoBehaviour
{
    public float minTimer, maxTimer;
    public bool isGreenLight = true;

    public Animator animator;
    public readonly string greenLightAnim = "GreenLight";

    public Transform shootPoint;
    public GameObject bulletPrefab;
    public bool hasShot;

    private AudioSource audioSource;
    public AudioClip redLightSfx, greenLightSfx, shootSfx;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(ChangeLightCoroutine());
    }

    IEnumerator ChangeLightCoroutine()
    {
        yield return new WaitForSeconds(Random.Range(minTimer, maxTimer));

        if (isGreenLight)
        {
            animator.SetBool(greenLightAnim, false);
            audioSource.PlayOneShot(redLightSfx);
            print("Lampu merah");
            yield return new WaitForSeconds(0.8f);
            isGreenLight = false;
        } else
        {
            isGreenLight = true;
            audioSource.PlayOneShot(greenLightSfx);
            animator.SetBool(greenLightAnim, true);
            print("Lampu hijau");
        }

        StartCoroutine(ChangeLightCoroutine());
    }

    public void ShootPlayer(Transform playerTarget)
    {
        if (hasShot) return;
        audioSource.PlayOneShot(shootSfx);
        GameObject bulletGO = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        bulletGO.GetComponent<BulletMovement>().playerTarget = playerTarget;
        hasShot = true;
    }
}
