using UnityEngine;

//TODO: Refactor to use GravityData struct from PlayerData

[RequireComponent(typeof(Rigidbody2D))]
public class GravitySwitch : MonoBehaviour
{
    Rigidbody2D playerRb;
    [SerializeField]
    [Tooltip("The current gravity scale applied to the player.")]
    float _gravityScale = 3f;
    public float GravityScale => _gravityScale;

    [SerializeField]
    [Tooltip("The default gravity scale to reset to when the player is not affected by any gravity-altering power-ups.")]
    float defaultGravityScale = 3f;
    public float DefaultGravityScale => defaultGravityScale;
    private void Awake()
    {
        playerRb = GetComponent<Rigidbody2D>();
    }
    public void SetGravity(float gravityScale)
    {
        playerRb.gravityScale = gravityScale;
        _gravityScale = gravityScale; //can be used for UI or other systems to know the current gravity scale without accessing the 
        //Rigidbody2D component directly.
    }
   
    public void ResetGravity()
    {
        _gravityScale = defaultGravityScale;
        playerRb.gravityScale = _gravityScale;
    }

    public void ModifyGravity(float amount) //different from SetGravity as it adds to the current gravity scale instead
                                              //of setting it to a specific value, allowing for more dynamic adjustments.
    {
        _gravityScale += amount;
        playerRb.gravityScale = _gravityScale;
    }

#if UNITY_EDITOR
    private void Update()
    {
        if(SetGravityBool && playerRb.gravityScale != _gravityScale)
        {
            SetGravity(_gravityScale);
        }
    }
    public bool SetGravityBool;
#endif
}
