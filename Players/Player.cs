using UnityEngine;
using System.Collections;
using Com.LuisPedroFonseca.ProCamera2D;

public class Player : MonoBehaviour 
{
    public enum InputType
    {
        MOUSE, JOYSTICK
    }

    public enum State
    {
        ALIVE, DEAD, COMPLETE
    }

    public int playerNum;
    public int shipNum;
    public string shipName;
    public InputType inputType;
    public State state = State.ALIVE;
    public ParticleSystem explosion; 

    protected Rigidbody rB;
    protected float speed;

    protected Material flashMat;
    protected Material defaultMat;

    protected float flashTime = 0.1f;
    protected float flashTimer;
    protected bool isFlashed = false;

    protected float speedMod = 1;
    protected float damageMod = 1;
    protected float knockBackMod = 1;

    public Vector3 velocity;
    protected Vector3 rotation;

    protected float maxHealth = 120;
    protected float health;
    protected float energy;

    public Transform core;
    public float coreDistance;


    protected bool trigger1Down = false;
    protected bool trigger2Down = false;

    //public float[] solarMilestone = { 500, 1000, 1500, 2000 };

    protected float maxSolarDistance = 2200;

    // Face the mouse
    protected Vector3 mousePos;
    protected Vector3 mainPos;
    protected float aimAngle;
    protected float angleDelay = -45;

    // Shooting
    protected float shootTimer;
    protected float[] shootPower = { 0, 0, 0 };
    protected float[] shootDelay = { 1, 1, 1 };

    protected int weaponLevel = 0;
    //public float weaponExp;
    //public float expCap = 100;

    // Skill Variables
    protected float[] skillEnergy = new float[2];
    protected float[] skillCost = new float[2];
    protected float[] skillMax = new float[2];

    DebugText debugText;

    // Other
    //public bool isAlive = true;

    float expPullRadius = 500;

    public float GetHealth() { return health; }
    public virtual float GetMaxHealth() { return maxHealth; }

    public float GetEnergy() { return energy; }

    public float GetRot() { return aimAngle; }
    public float GetSolarDistance() { return Vector3.Magnitude(transform.position - core.position); }

    public int GetWeaponLevel() { return weaponLevel; }
    public int GetCharges(int _num) { return (int)(skillEnergy[_num] / skillCost[_num]); }
    public int GetMaxCharges(int _num) { return (int)(skillMax[_num] / skillCost[_num]); }
    public float GetCooldown(int _num) { return (skillEnergy[_num] % skillCost[_num]) / skillCost[_num]; }

    protected GameObject[] expCubes;


    const float COIN_HP_VALUE = 10;
    const float COIN_ENERGY_VALUE = 5;

    protected int floorMask;

    protected SolarRings solarRings;

    bool godMode;
    bool hardMode;

    protected virtual void Start()
    {
        health = maxHealth;
        energy = 0;

        for (int i = 0; i < 2; i++)
        {
            skillEnergy[i] = skillMax[i];
        }

        defaultMat = GetComponent<Renderer>().material;
        flashMat = (Material)Resources.Load("Flash", typeof(Material));

        floorMask = LayerMask.GetMask("Click");

        rB = GetComponent<Rigidbody>();
        rB.mass = 2;
        rB.drag = 10;
        speed = 12000;

        core = GameObject.FindWithTag("Core").transform;

        solarRings = Object.FindObjectOfType<SolarRings>();
        debugText = Object.FindObjectOfType<DebugText>();

        godMode = GodMode.active;
        hardMode = HardMode.active;

        //Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(camPoint.transform, 1, 1, 0);
        Camera.main.GetComponent<ProCamera2D>().AddCameraTarget(transform, 1, 1, 0);

        BaseLevel.OnBeat += BeatEvent;
        BaseLevel.OnLevelEnd += LevelEnd;
    }

    protected virtual void Update()  
    {
        if (state == State.ALIVE && !GUIManager.isPaused)
        {

            ButtonInput();
            
            // Check if alive
            if (health <= 0)
            {
                health = 0;
                state = State.DEAD;
                //Explode();

                PlayerManager.Instance.PlayerAmount(-1);
                Invoke("Explode", 0.1f);

                
            }


            CooldownUpdate(solarRings.GetSolarValue(transform.position));

            //Hieght back up
            //if (transform.position.y != 0)
            //    transform.position = new Vector3(transform.position.x, 0, transform.position.z);

            // Flash when takeing damage


            //transform.rotation = Quaternion.Euler(rotation);   
        }

        if (isFlashed)
        {
            flashTimer += Time.deltaTime;

            if (flashTimer >= flashTime)
            {
                Flash(false);
                flashTimer = 0;
            }
        }
    }

    protected virtual void FixedUpdate()
    {
        if (state == State.ALIVE && !GUIManager.isPaused)
        {
            Movement();
            ExpMagnet();
            AimInput();

            rB.MoveRotation(Quaternion.Euler(rotation));
        }
    }

    protected virtual void Movement()
    {
        Vector3 movement = new Vector3(Input.GetAxis("Vertical P" + playerNum), 0,
            Input.GetAxis("Horizontal P" + playerNum)).normalized * speed * speedMod;

        //debugText.SetText(movement.x + "\n" + movement.z);

        rB.AddForce(movement);
    }

    protected virtual void ButtonInput() { }

    protected virtual void AimInput()
    {
        float x = Input.GetAxis("Horizontal Aim P" + playerNum);
        float y = Input.GetAxis("Vertical Aim P" + playerNum);

        shootTimer -= Time.deltaTime;

        if ((x + y) != 0)
        {
            aimAngle = Mathf.Atan2(y, -x);
            aimAngle = (aimAngle * Mathf.Rad2Deg * -1);

            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = shootDelay[weaponLevel];
            }

            ToggleInput(false);
        }

        // Mouse Aim Controls
        if(inputType == InputType.MOUSE && playerNum == 1)
        {

            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            if (Physics.Raycast(camRay, out floorHit, 3000f, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                aimAngle = Mathf.Atan2(playerToMouse.x, playerToMouse.z * -1) * Mathf.Rad2Deg * -1;
            }
        }
        
        // Defaults on an angle
        rotation.y = aimAngle - angleDelay;

        // Shoot
        if (Input.GetMouseButton(0) && playerNum == 1)
        {
            if (shootTimer <= 0)
            {
                Shoot();
                shootTimer = shootDelay[weaponLevel];
            }

            ToggleInput(true);
        }
    }

    protected virtual void ToggleInput(bool mouse)
    {
        if(mouse)
        {
            if(inputType != InputType.MOUSE)
            {
                inputType = InputType.MOUSE;
                Camera.main.GetComponent<ProCamera2DPointerInfluence>().ToggleInput(true);
                Cursor.visible = true;
            }
            
        }
        else
        {
            if (inputType != InputType.JOYSTICK)
            {
                inputType = InputType.JOYSTICK;
                Camera.main.GetComponent<ProCamera2DPointerInfluence>().ToggleInput(false);
                Cursor.visible = false;
            }
        }
    }

    protected virtual void BeatEvent()
    {
        if(solarRings.GetSolarValue(transform.position) == 0)
        {
            TakeDamage(15);

        }

        if (health <= maxHealth * 0.25f)
        {
            //Camera.main.transform.DOMoveY(Camera.main.transform.position.y + 20, 0.1f).From();

            //ProCamera2DShake.Instance.Shake(
            //    0.1f, //duration
            //    new Vector3(25, 25), //strength
            //    1, //vibrato
            //    1f, //randomness
            //    -1, //initialAngle (-1 = random)
            //    new Vector3(0, 0, 0), //rotation
            //    0.3f); //smoothness
        }

    }

    public virtual void CooldownUpdate(int x)
    {
        for (int i = 0; i < 2; i++)
        {
            skillEnergy[i] += ((x * 30f) + 30)* Time.deltaTime;

            if (skillEnergy[i] > skillMax[i])
                skillEnergy[i] = skillMax[i];
        }

        //DebugText.Instance.SetText("" + x);

        //if(x > 1)
        //  health += (x - 1) * Time.deltaTime;

        if (health >= maxHealth)
            health = maxHealth;
    }

    protected virtual void ExpMagnet()
    {
        expCubes = GameObject.FindGameObjectsWithTag("Exp");

        foreach (GameObject exp in expCubes)
        {
            float distance = (transform.position - exp.transform.position).magnitude;
            //Vector3 expVel = (transform.position - exp.transform.position).normalized * MAGNET_PULL;

            if (distance <= expPullRadius)
            {
                exp.GetComponent<Rigidbody>().MovePosition(exp.transform.position +
                    (transform.position - exp.transform.position).normalized 
                    * ((expPullRadius / distance) * 0.8f + 10));       
                
                if(distance <= 15)
                {
                    exp.SetActive(false);
                    UpdateEnergy(1);
                }   
            }
        }
    }

    public void UpdateEnergy(float increase)
    {
        energy += increase;

        if (energy > 100)
            energy = 100;

        if (energy < 0)
            energy = 0;

        switch (weaponLevel)
        {
            default:
            case 0:
                if (energy >= 50)
                {
                    TextManager.Instance.WeaponText("Weapon++", transform.GetComponent<Collider>().bounds, true);
                    weaponLevel = 1;
                }
                break;

            case 1:
                if (energy < 45)
                {
                    TextManager.Instance.WeaponText("Weapon--", transform.GetComponent<Collider>().bounds, false);
                    weaponLevel = 0;
                }
                else if (energy >= 95)
                {
                    TextManager.Instance.WeaponText("Weapon Max", transform.GetComponent<Collider>().bounds, true);
                    weaponLevel = 2;
                }
                break;

            case 2:
                if (energy < 90)
                {
                    TextManager.Instance.WeaponText("Weapon--", transform.GetComponent<Collider>().bounds, false);
                    weaponLevel = 1;
                }
                break;
        }
    }

    protected virtual void Explode()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
 
    protected virtual void Flash(bool flash)
    {   // Flash when take damgage, true = change texture, false = change back
        if (flash) 
            ChangMat(flashMat);
        else
            ChangMat(defaultMat);

        isFlashed = flash;
    }

    protected virtual void ChangMat(Material newMat)
    {   // Change the material of all models
        GetComponent<Renderer>().material = newMat;
    }
 
    public virtual void TakeDamage(float damage)
    {   
        if(state == State.ALIVE && !isFlashed)
        {

            if (hardMode)
                damage *= 2;  

            if (!godMode)
            {
                health -= damage * 0.8f;
                GetComponent<AudioSource>().Play();
            }


            ScoreManager.Instance.damageTaken += (int)(damage * 0.5f);

            Flash(true);
            UpdateEnergy(-damage);
           


            //Shake(float duration, Vector2 strength, int vibrato, float randomness, float initialAngle, Vector3 rotation, float smoothness)
            //ProCamera2DShake.Instance.Shake(0.3f, new Vector3(100, 100, 100), 2, 1, 0, new Vector3(0, 0, 0), 0.1f);
            //ProCamera2DShake.Instance.Shake("PlayerHit");

            ProCamera2DShake.Instance.Shake(
                0.3f, //duration
                new Vector3(100, 100, 100), //strength
                2, //vibrato
                1, //randomness
                -1, //initialAngle (-1 = random)
                new Vector3(0, 0, 0), //rotation
                0.1f); //smoothness

            ScoreManager.Instance.EndKillStreak();
        }
    }

    protected virtual void LevelEnd()
    {
        if(!BaseLevel.Instance.gameOver)
        {
            state = State.COMPLETE;
            rB.isKinematic = true;
        }

    }

    protected virtual void OnDisable()
    {
        BaseLevel.OnBeat -= BeatEvent;
        BaseLevel.OnLevelEnd -= LevelEnd;
    }

    protected virtual void Knockback(Vector3 col, float magnitude)
    {
        Vector3 direction = Vector3.Normalize(col - transform.position);
        direction.y = 0;
        //velocity += direction * magnitude * knockBackMod * -1;

        rB.AddForce(direction * magnitude * knockBackMod * -100);
  
    }

    protected virtual void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            float damage = 0;

            damage = col.gameObject.GetComponent<BaseEnemy>().GetImpact();

            TakeDamage(damage);
            Knockback(col.gameObject.transform.position, damage * 10);
        }
    }

    protected virtual void OnTriggerEnter(Collider col)
    {
        if (state == State.ALIVE)
        {
            if (col.gameObject.tag == "Enemy")
            {
                float damage = 0;

                if (col.GetComponent<BaseAI>())
                    damage = col.GetComponent<BaseAI>().GetImpact();
                else if (col.GetComponent<BaseEnemy>())
                    damage = col.GetComponent<BaseEnemy>().GetImpact();

                TakeDamage(damage);
                Knockback(col.gameObject.transform.position, damage * 10);
            }

            if (col.gameObject.tag == "Enemy Laser"
                || col.gameObject.tag == "Enemy AoE")
            {
                Debug.Log(col.gameObject.name);
                TakeDamage(col.GetComponent<BaseAttack>().damage);
            }
        }

        if (col.gameObject.tag == "Coin")
        {
            col.gameObject.GetComponent<Coin>().Explode();


            ScoreManager.Instance.AddCoin(col.transform.GetComponent<Collider>().bounds);


            health += COIN_HP_VALUE;
            UpdateEnergy(COIN_ENERGY_VALUE);
        }
    }

    protected virtual void OnTriggerStay(Collider col)
    {   // Called every frame
        if (state == State.ALIVE)
        {
            if (col.gameObject.tag == "Enemy")
            {
                float damage = 0;

                if (col.GetComponent<BaseAI>())
                    damage = col.GetComponent<BaseAI>().GetImpact();
                else if (col.GetComponent<BaseEnemy>())
                    damage = col.GetComponent<BaseEnemy>().GetImpact();

                TakeDamage(damage);
                Knockback(col.gameObject.transform.position, damage * 10);
            }

            //if (col.gameObject.tag == "Enemy Laser"
            //    || col.gameObject.tag == "Enemy AoE")
            //{
            //    TakeDamage(col.GetComponent<BaseAttack>().damage);
            //}

            if (col.gameObject.tag == "Enemy Laser Beam")   
            {
                //TakeDamage(100 * Time.deltaTime); 
                TakeDamage(20);
            }
        }
    }

    // Skills
    protected virtual void Shoot() { }

    protected virtual void UseSkill(int _Num)
    {
        if (skillEnergy[_Num] >= skillCost[_Num]) //&& energy >= skillCost[_Num])
        {
            //shootTimer = shootDelay[weaponLevel];
            //cdTimer[_Num] = 0;

            skillEnergy[_Num] -= skillCost[_Num];
            //energy -= skillCost[_Num];

            switch (_Num)
            {
                case 0:
                    Skill_0();
                    break;

                case 1:
                    Skill_1();
                    break;

                default:
                    break;
            }
        }
    }

    protected virtual void Skill_0() { }

    protected virtual void Skill_1() { }
}
