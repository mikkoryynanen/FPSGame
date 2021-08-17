using UnityEngine;

public class AudioManager : MonoBehaviour 
{
    [SerializeField] AudioClip[] weaponSounds;
    [SerializeField] AudioClip weaponSoundTail;
    [SerializeField] AudioClip weaponReload;

    AudioSource _source;


    private void OnEnable()
    {
        EventManager.Subscribe<OnFireWeapon>(OnFiredWeapon);
        EventManager.Subscribe<OnFireWeaponEnd>(OnFiredWeaponEnd);
        EventManager.Subscribe<OnWeaponReload>(OnWeaponReaload);
    }

    private void OnDisable()
    {
        EventManager.Unsubscribe<OnFireWeapon>();
        EventManager.Unsubscribe<OnFireWeaponEnd>();
    }

    private void Awake() 
    {
        _source = GetComponent<AudioSource>();
    }

    private void OnFiredWeapon(OnFireWeapon obj)
    {
        _source.PlayOneShot(weaponSounds[Random.Range(0, weaponSounds.Length - 1)]);
        _source.pitch = Random.Range(.95f, 1.05f);
    }

    private void OnFiredWeaponEnd(OnFireWeaponEnd obj)
    {
        _source.PlayOneShot(weaponSoundTail);
        _source.pitch = 1f;
    }

    private void OnWeaponReaload(OnWeaponReload obj)
    {
        _source.PlayOneShot(weaponReload);
        _source.pitch = 1f;
    }
}