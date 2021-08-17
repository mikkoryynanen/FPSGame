using UnityEngine;

[RequireComponent(typeof(Animator))]
public class PlayerWeaponController : MonoBehaviour 
{
    [Header("References")]
    [SerializeField] ParticleSystem muzzleParticle;
    [SerializeField] ParticleSystem shellEjectParticle;
    [SerializeField] Transform muzzleTransform;
    [SerializeField] Weapon weaponSettings;

    Animator _anim;

    public int CurrentAmmo { get; private set; }
    public int MaxAmmo { get { return weaponSettings.clipSize; } }
    public int Damage { get { return weaponSettings.damage; } }
    public float FireRate { get { return weaponSettings.fireRate; } }

    private void Awake() 
    {
        muzzleParticle.Stop();
        shellEjectParticle.Stop();

        _anim = GetComponent<Animator>();

        CurrentAmmo = MaxAmmo;
    }

    public void ShootAnimation()
    {
        _anim.SetTrigger("fire");

        muzzleParticle.Play();
        shellEjectParticle.Play();

        CurrentAmmo--;
        EventManager.Send(new OnFireWeapon { currentAmmo = CurrentAmmo, maxAmmo = MaxAmmo });
    }

    public void ShootEnd()
    {
        EventManager.Send(new OnFireWeaponEnd { });
    }

    public void ReloadAnimation()
    {
        _anim.SetTrigger("reload");
        
        CurrentAmmo = MaxAmmo;

        EventManager.Send(new OnWeaponReload { });
        EventManager.Send(new OnFireWeapon { currentAmmo = CurrentAmmo, maxAmmo = MaxAmmo });
    }

    public void SetMovementAnimation(float input)
    {
        _anim.SetFloat("speed", input);
    }

    public void SetAimingPosition(Vector3 pos)
    {
        transform.position = pos;
    }
}