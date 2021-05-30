//Michelle + Trevor 
//December 23, 2016
//Touhou Game!
//Bullet hell, enemy shoots projectils at the player, and player shoots projectiles at the enemy. The goal is to defeat the enemy.
//Bullet hell games contain tons of projectiles, although it may seem overwhelming at first, it trains player skill and their game abilties.
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WMPLib;

namespace _24_Touhou
{
    public partial class Form1 : Form
    {
        //Michelle variables, constants, arrays:

        //Constant integer for the speed of the player
        const int PLAYER_SPEED = 12;
        //Boolean Integers for the player moving 
        bool playerUp = false;
        bool playerDown = false;
        bool playerRight = false;
        bool playerLeft = false;
        //Integers for the frame rate loop
        int previousTime;
        int timePassed;
        //Constant integer for the frame delay in the frame rate loop
        const int FRAME_DELAY = 60;
        //Integer to store the game state; start, running, paused
        int gameState;
        //Constant integers to represent the game states
        const int START = 0;
        const int RUNNING = 1;
        const int PAUSED = 2;
        const int END = 3;
        //Player location graphics data
        Point playerLocation;
        Size playerSize;
        Rectangle playerHitBox;
        //Player's actual hitbox data for collision detection
        Rectangle playerCollisionDetectionHitBox;
        Point playerCollisionDetectionLocation;
        Size playerCollisionDetectionSize;
        //Integer frame for player idle animations
        int playerAnimationFrame = 0;
        //Constants forenemy max health and starting player lives
        const int MAX_ENEMY_HEALTH = 2000;
        const int STARTING_PLAYER_LIVES = 3;
        //Intergers for player lives enemy health
        int playerLives = STARTING_PLAYER_LIVES;
        int enemyHealth = MAX_ENEMY_HEALTH;
        //Constant for the damage of the projectile
        const int PLAYER_PROJECTILE_DAMAGE = 1;
        //A boolean to determine wether the player or enemy won
        bool playerWin = false;
        //Constant for the number of indexes that will be resized
        const int RESIZE_NUMBER = 15;
        //Constant projectile type of nothing.
        const int NO_PROJECTILE = -1;
        //A boolean to determine wether the player is shooting projectiles or not
        bool playerShoot = false;
        //Constant Integer of angle for projectiles flying staight
        const int STRAIGHT_UP_ANGLE = 90;
        //Constant Integer of angle of projectiles flying up-right
        const int RIGHT_UPWARDS_ANGLE = 80;
        //Constant Integer of angle of projectiles flying up-left
        const int LEFT_UPWARDS_ANGLE = 100;
        //Constant integer for player projectile's speed
        const int PLAYER_PROJECTILE_SPEED = 10;
        //Health Bar Graphics Data
        Point enemyHealthBarLocation;
        Size enemyHealthBarSize;
        Rectangle enemyHealthBarBoundary;
        //Player Health Bar Graphics Data
        Size enemyCurrentBarSize;
        Rectangle enemyCurrentHealthBarBoundary;
        //Constants for health bar size
        const int HEALTH_BAR_HEIGHT = 5;
        const int HEALTH_BAR_LENGTH = 500;
        //Location to display the player's lives
        PointF playerStatsLocation;
        //Constant for spacing for displaying the player's stats
        const int STAT_HEIGHT_SPACING = 12;
        //Boolean to determine whether the player's actual hitbox is shown or not, and wether the player's speed is slowed down or not
        bool preciseMovementAndShowPlayerActualHitBox = false;
        //Random number generator variables
        Random numberGenerator = new Random();
        //To store the random number
        int powerUpChanceNumber;
        //Constants for the minimum and maximum number for power up 
        const int MIN_POWER_UP_CHANCE = 1;
        const int MAX_POWER_UP_CHANCE = 100;
        //Constant for the chance of the power up being dropped
        const int POWER_UP_DROP_CHANCE = 20;
        //Booleans for the powerups
        bool damageBoost = false;
        bool speedBoost = false;
        //Constant for a new types of projectile - power ups
        const int DAMAGE_POWER_UP_PROJECTILE = 2;
        const int SPEED_POWER_UP_PROJECTILE = 3;
        const int ADDITIONAL_LIFE_PROJECTILE = 4;
        //Constant for the angle power up projectiles will fly at
        const int POWER_UP_PROJECTILE_ANGLE = 270;
        //Constant for the power up projectile speed
        const int POWER_UP_PROJECTILE_SPEED = 3;
        //A constant for the chance of a power up being dropped
        const int CHANCE_OF_POWER_UP = 5;
        //Location for player starting location
        Point playerStartingLocation;
        //player projectile size
        Size playerProjectileSize = new Size(4, 4);
        //Powerup size
        Size powerUpSize = new Size(9, 13);

        //Integer to count frames that has passed 
        int frameCounter = 0;

        //Pattern Four Variables
        //Constant for the number of frames passed for attack pattern 4
        const int PATTERN_FOUR_DELAY = 5;
        //Boolean to keep track of which set of projectiles get fired in pattern 4
        bool patternFourPartOne = false;
        //Counter to count how many times pattern four has run
        int patternFourCounter;
        //Constant for how many times pattern four runs
        const int PATTERN_FOUR_RUN_TIME = 10;
        //Constant for the angle increments
        const int PATTERN_FOUR_ANGLE_INCREMENT = 20;

        //Pattern Eight Variables
        //Constant for the number of frames passed to find delay in pattern 8
        const int PATTERN_EIGHT_DELAY = 3;
        //Constant for the starting angles in pattern 8
        const int PATTERN_EIGHT_STARTING_ANGLE = 0;
        //Constant for the angle increment for each set (petal)
        const int PATTERN_EIGHT_PETAL_ANGLE_INCREMENT = 45;
        //Constant for angle increment per projectile
        const int PATTERN_EIGHT_PROJECTILES_ANGLE_INCRMENET = 5;
        //Integer for the increment of each ring of petal
        int patternEightAIncrement = 0;
        int patternEightBIncrement = 0;
        int patternEightCIncrement = 0;
        int patternEightDIncrement = 0;
        int patternEightEIncrement = 0;
        //Constant for starting speed in pattern 8
        const int PATTERN_EIGHT_STARTING_SPEED = 10;
        //Constant for speed increment
        const int PATTERN_EIGHT_SPEED_INCREMENT = 3;
        //Constant to check wether the increment has reached a certain point (to determine when to start next ring of projectiles)
        const int PATTERN_EIGHT_INCREMENT_CHECKPOINT = 3;
        //Counter to count how many times pattern eight has run (1 time = 1 full flower with 5 rings)
        int patternEightCounter = 0;
        //Constant for the maximum times the pattern runs
        const int PATTERN_EIGHT_MAX_RUN_TIME = 3;

        //Pattern Two Variables
        //Constant for the number of frames for delay in pattern 2
        const int PATTERN_TWO_DELAY = 1;
        //Constant for starting angle in pattern 2
        const int PATTERN_TWO_STARTING_ANGLE = 0;
        //Constant for angle increment between each line of projectile
        const int PATTERN_TWO_ANGLE_INCREMENT = 60;
        //Integer for the angle increment
        int patternTwoAngleIncrement = 0;
        //Constant for the angle increment
        const int PATTERN_TWO_PROJECTILE_ANGLE_INCREMENT = 10;
        //Constant for the maximum angle increment
        const int PATTERN_TWO_MAX_ANGLE_INCR = 10;
        //Constant for the minimum angle increment
        const int PATTERN_TWO_MIN_ANGLE_INCR = 0;
        //Boolean for increment or decrement 
        bool patternTwoIncrement = true;
        //Counter to count how many times the pattern has run
        int patternTwoCounter;
        //constant for how many times pattern two runs
        const int PATTERN_TWO_RUN_TIME = 6;

        //PatternNineVariables
        //Integer for the angle of the projectiles
        int patternNineAngle = 0;
        //Constant for the angle increment
        const int PATTERN_NINE_ANGLE_INCREMENT = 10;
        //Constant for the number of projectiles in each angle
        const int PATTERN_NINE_PROJECTILE_NUMBER = 4;
        //Integer to count how many projectiles have been fired per angle.
        int patternNineProjectiles = 0;
        //Integer for angle increment between each shot of projectiles
        int patternNineAngleIncrement = 0;
        //Counter to count how many times pattern nine has run
        int patternNineCounter;
        //Constant for how many times pattern nine runs
        const int PATTERN_NINE_RUN_TIME = 10;
        //Constant for the amount of space (angles) in between each projectile being fired
        const int PATTERN_NINE_ANGLE_SPACE = 90;

        //PatternTenVariables
        //Boolean to check if circular projectiles have been addedd
        bool patternTenCircularProjectiles = false;
        //Constant for the delay (frames) 
        const int PATTERN_TEN_DELAY = 3;
        //Counter to count how many times this pattern has run (how many projectiles were shot)
        int patternTenRunTimeCounter = 0;
        //A constant for how long the pattern runs - 50 shots from enemy
        const int PATTERN_TEN_RUN_TIME = 25;



        //Trevor Variables, Constants, Arrays:

        //Create a variable to store the current number of projectiles
        int numberOfProjectiles;

        //Create a constant to store the starting size of the projectile array
        const int PROJECTILE_ARRAY_STARTING_SIZE = 15;
        //Create a constant to store each type of projectile, and the number of projectile types
        const int STRAIGHT_PROJECTILE = 0;
        const int CIRCULAR_PROJECTILE = 1;
        //Create a constant to store whose projectile each projectile is
        const int PLAYER_PROJECTILE = 0;
        const int ENEMY_PROJECTILE = 1;

        //projectile location array, stores location of each graphic
        PointF[] projectileLocation = new PointF[PROJECTILE_ARRAY_STARTING_SIZE];
        //projectile boundary array, stores boundary of each graphic
        RectangleF[] projectileBoundary = new RectangleF[PROJECTILE_ARRAY_STARTING_SIZE];
        //projectile movement type, specifies how the projectile will move (straight motion, circular motion)
        int[] projectileMovementType = new int[PROJECTILE_ARRAY_STARTING_SIZE];
        //projectile possestion array, specifies whose projectile is store in the current index
        int[] projectilePossession = new int[PROJECTILE_ARRAY_STARTING_SIZE];
        //projectileXSpeed array, contains the projectileXSpeed (on straight projectiles)
        float[] projectileXSpeed = new float[PROJECTILE_ARRAY_STARTING_SIZE];
        //projectileYSpeed array, contains the projectileYSpeed (on straight projectiles)
        float[] projectileYSpeed = new float[PROJECTILE_ARRAY_STARTING_SIZE];
        //projectileAngle array, contains the projectile's angle in relation to the enemy (only applied to circular projectiles) (used in enemy pattern 1)
        int[] projectileAngle = new int[PROJECTILE_ARRAY_STARTING_SIZE];
        //Array for enemy projectile colors (applies to certain attack patterns)
        int[] projectileColor = new int[PROJECTILE_ARRAY_STARTING_SIZE];

        //Constants for enemy size proportions
        const int ENEMY_HEIGHT = 60;
        const int ENEMY_WIDTH = 45;
        //Enemy size variable
        Size enemySize = new Size(ENEMY_WIDTH, ENEMY_HEIGHT);
        //Enemy location variable
        Point enemyLocation;
        //Enemy boundary variable
        Rectangle enemyBoundary;

        //Very small circular projectile size variable
        Size verySmallCircProjectileSize = new Size(10, 10);
        //small circular projectile size variable
        Size smallCircProjectileSize = new Size(20, 20);
        //Constant for small circular projectile radius
        const int SMALL_CIRC_RADIUS = 10;
        //large circular projectile size variable
        Size largeCircProjectileSize = new Size(60, 60);
        //Constant for small circular projectile radius
        const int LARGE_CIRC_RADIUS = 30;
        //blue straight projectile size for the enemy
        Size enemyThinProjectileSize = new Size(5, 15);

        //Create a variable to store the amount of frames passed since the attack pattern started
        int elapsedFrames;

        //Constants for each attack pattern
        //constant for calculating the angle new projectiles are shot at
        const int ATK_PTN_1_ANGLE_INCREASE = 90;
        //Constant for the distance from the enemy to a projectile with a circular movement type
        const int CIRCULAR_PROJECTILE_HYPOTENUSE_DISTANCE = 50;
        //Constant for counting if new projectiles should be shot (after 5 frames has passed)
        const int ATTACK_PATTERN_1_DELAY = 5;

        //variable for where the enemy will travel to
        Point travelDestination;

        //Have variables for the set enemy locations.
        Point enemyLocationCentre;
        Point enemyLocationTop;
        Point enemyLocationTopLeft;
        Point enemyLocationTopRight;
        Point enemyLocationMiddleRight;
        Point enemyLocationMiddleLeft;

        //Constant for number of enemy locations
        const int NUMBER_OF_LOCATIONS = 6;

        //variables for enemy animation
        int enemyAnimationFrame;
        int enemyDirection;

        //Constants for enemy direction
        const int ENEMY_STRAIGHT = 0;
        const int ENEMY_MOVE_LEFT = 1;
        const int ENEMY_MOVE_RIGHT = 2;

        //Constants for projectile colors
        const int BLUE = 0;
        const int RED = 1;
        const int GREEN = 2;

        //variable to keep track of attack phases
        int phase = NO_PHASE;

        //Constants for the phases
        const int NO_PHASE = -1;
        const int PHASE_1 = 0;
        const int PHASE_2 = 1;
        const int PHASE_3 = 2;

        //Bool, true if enemy is currently attacking
        bool isEnemyAttacking = false;

        //Constant for enemy movement speed
        const int ENEMY_SPEED = 7;

        //variable for attack pattern 6 projectile angles
        int attackPattern6Angle;
        //Variable for the direction of attack in attack pattern six
        int attackPattern6Direction;
        //Variable for how many cycles the enemy has attacked in certain attack patterns (3 and 6)
        int cycles;

        //constant for directions of attack
        const int ATTACK_LEFT = 0;
        const int ATTACK_RIGHT = 1;

        //Constant for the amount of frames that need to be passed in attack pattern 7 before new projectiles are shot
        const int ATTACK_PATTERN_7_DELAY = 7;

        //Variable for counting how many frames has passed since the player died
        int respawnFrameCounter;
        //Variable for distinguishing if the player is dead
        bool isPlayerDead = false;
        //Variable for distinguishing if the player should be drawn (dont draw if they are dead for a bit)
        bool playerVisible = true;
        //Variable for keeping track of how many frames has passed since player visibility was toggled (used in the PlayerDied subprogram)
        int lastVisibilityToggleFrame;

        //Variable for selecting which attack pattern to call
        int currentAttackPattern;

        //Constants for each attack pattern
        const int ATK_PTN_1 = 0;
        const int ATK_PTN_2 = 1;
        const int ATK_PTN_3 = 2;
        const int ATK_PTN_4 = 3;
        const int ATK_PTN_5 = 4;
        const int ATK_PTN_6 = 5;
        const int ATK_PTN_7 = 6;
        const int ATK_PTN_8 = 7;
        const int ATK_PTN_9 = 8;
        const int ATK_PTN_10 = 9;
        const int NO_ATK_PTN = -1;

        //Constant for the number of attacks
        const int NUMBER_OF_ATTACK_PATTERNS = 10;

        //Create constants for differnt projectile speeds
        const int SLOW_PROJECTILE = 5;
        const int MODERATE_PROJECTILE = 10;
        const int FAST_PROJECTILE = 15;
        const int VERY_FAST_PROJECTILE = 20;

        //Objects for background music sound
        WindowsMediaPlayer backgroundMusic = new WindowsMediaPlayer();

        //Constants for differnt sounds
        const int BATTLE_BACKGROUND_MUSIC = 0;
        const int START_AND_END_BACKGROUND_MUSIC = 1;

        //Location for the background
        Point backgroundLocation;
        //Size for background
        Size backgroundSize;
        //Boundary for background
        Rectangle backgroundBoundary;

        //Variable to let the program know if the initial delay is over before the enemy starts attacking
        bool enemyInitialDelayOver = false;

        public Form1()
        {
            //Inintialize the Component
            InitializeComponent();
            //Run Trevor Form 1 (Trevor's Set up code)
            TrevorForm1();
            //Run michelle Form 1 (Michelle's Set up code)
            MichelleForm1();
        }

        //Michelle Kee
        //Form 1 code - set up code for game/start screen 
        void MichelleForm1()
        {
            //Print onpaint picture
            Refresh();
            //Sets the game state to start
            gameState = START;
            //Start Screen:
            //Set text for lbl motivation
            lblMotivationPrompt.Text = "The evil overlord, Little Hsiung-Hsiung, has given everybody 0's on their final assignments! \r\nYou must deafeat his body guard, Sakuya and return peace to the grade 11 compuer science class!";
            //Set text for lbl Instructions
            lblInstructions.Text = "Objective:\r\n Dodge the Sakuya's bullets and defeat Sakuya with your own bullets.\r\nInstructions:\r\n Use Up, Right, Down, Left Arrow Keys to move the player.\r\n Hold down spacebar to shoot.\r\n Hold down shift to slow down and view your hit box.";
            //Text for prompt
            lblPrompt1.Text = "Press Enter to Begin";
            //Text for the power ups
            lblPromptBoost1.Text = "Damage Power Up - Increases player's Damage by 5, Cannot be Stacked";
            lblPromptBoost2.Text = "Speed Power Up - Increases player's Speed slightly, Cannot be stacked";
            lblPromptBoost3.Text = "Life Power Up - Gives the player one extra life, can be collected many times";


            //Subprogram that sets up the player location size and hitbox, and the size of the player's actual hitbox
            SetUpPlayer();
            //Subprogram that sets up the health bar graphics data
            SetUpHealthBars();
        }

        //Trevor Kollins
        //Trevor's set up code, set's location of point's the enemy will travel to, as well as background and enemy startup code
        void TrevorForm1()
        {
            //Run SetupEnemy subprogram, runs enemy setup code.
            SetupEnemy();

            //Setup a background location at 0, 0
            backgroundLocation = new Point(0, 0);
            //Setup the background size as the clientsize width and height
            backgroundSize = new Size(ClientSize.Width, ClientSize.Height);
            //Set the background boundary to the location and height
            backgroundBoundary = new Rectangle(backgroundLocation, backgroundSize);

            //Hide the end game text
            lblEndGameText.Visible = false;

            //Play the start background music
            Music(START_AND_END_BACKGROUND_MUSIC);
        }

        //Trevor Kollins
        //Enemy setup which runs on program startup
        void SetupEnemy()
        {
            //Set the preset enemy locations (enemy will travel here during game):
            //Centre location (half client width and height)
            enemyLocationCentre = new Point(ClientSize.Width / 2, ClientSize.Height / 2);
            //Top location (half client width, 4th client height)
            enemyLocationTop = new Point(ClientSize.Width / 2, ClientSize.Height / 4);
            //Top left location (8th client width, 8th client height)
            enemyLocationTopLeft = new Point(ClientSize.Width / 8, ClientSize.Height / 8);
            //Top right location (8th client width times 7, 8th client height)
            enemyLocationTopRight = new Point((ClientSize.Width / 8) * 7, ClientSize.Height / 8);
            //Middle right location (8th client width times 5, 4th client height)
            enemyLocationMiddleRight = new Point((ClientSize.Width / 8) * 5, ClientSize.Height / 4);
            //Middle left location (8th client width times 3, 4th client height)
            enemyLocationMiddleLeft = new Point((ClientSize.Width / 8) * 3, ClientSize.Height / 4);

            //Set enemy location to the centre and enemy travel destination to the top to start;
            enemyLocation = enemyLocationCentre;
            travelDestination = enemyLocationTop;

            //Set the enemyBoundary to the enemy location and size.
            enemyBoundary = new Rectangle(enemyLocation, enemySize);
        }

        //Michelle Kee
        //Subprogram that sets up the player location size and hitbox, and the size of the player's actual hitbox
        void SetUpPlayer()
        {
            //Sets the size of the player 
            playerSize = new Size(32, 58);

            //Sets up the player starting location (for respawning)
            playerStartingLocation = new Point(ClientSize.Width / 2 - playerSize.Width / 2, (ClientSize.Height / 3) * 2 - playerSize.Height / 2);

            //Sets the player location at center, bottom section of screen
            playerLocation = playerStartingLocation;

            //Sets the hitbox of the player to the player location and player size
            playerHitBox = new Rectangle(playerLocation, playerSize);

            //Sets up the size of the player's actual hitbox
            playerCollisionDetectionSize = new Size(8, 8);

            //Sets the location of where to display lives
            playerStatsLocation = new Point(0, ClientSize.Height - 60);
        }

        //Michelle Kee
        //Subprogram that sets up the rectangles of the outlines of the health bars of the player and enemy
        void SetUpHealthBars()
        {
            enemyHealthBarSize = new Size(HEALTH_BAR_LENGTH, HEALTH_BAR_HEIGHT);
            enemyHealthBarLocation = new Point(ClientSize.Width / 2 - enemyHealthBarSize.Width / 2, ClientSize.Height / 6 - enemyHealthBarSize.Height / 2);
            enemyHealthBarBoundary = new Rectangle(enemyHealthBarLocation, enemyHealthBarSize);

            //Enemy's current health (filled part) health bar
            enemyCurrentBarSize = new Size(HEALTH_BAR_LENGTH, HEALTH_BAR_HEIGHT);
            enemyCurrentHealthBarBoundary = new Rectangle(enemyHealthBarLocation, enemyCurrentBarSize);


            // (playerLocation.X - healthBarSize.Width/2, playerLocation.Y + playerSize.Height - healthBarSize.Height/2);
        }

        //Michelle Kee
        //Subprogram for the loop that will run every 60 miliseconds
        void FrameRateLoop()
        {
            //Saves the time passed to the previous time
            previousTime = Environment.TickCount;
            //While the game is running 
            while (gameState == RUNNING)
            {
                //Calculates the amount of time passed
                timePassed = Environment.TickCount - previousTime;
                //If 60 or more miliseconds have passed
                if (timePassed >= FRAME_DELAY)
                {
                    //Updates the previous time (to calculate the time past again and count to 60 again)
                    previousTime = Environment.TickCount;
                    //Call Michelle's frame rate loop
                    MichelleFrameRateLoop();
                    //Call Trevor's frame rate loop
                    TrevorFrameRateLoop();
                    //Refresh graphics
                    Refresh();
                }
                Application.DoEvents();
            }
        }

        //Michelle Kee
        //Frame Rate Loop - this subprogram is run every 60 milliseconds
        void MichelleFrameRateLoop()
        {
            //If player shoot is true (player is holding down spacebar) and player is not dead
            if (playerShoot == true && isPlayerDead == false)
            {
                //Create the player's projectiles, if the user is holding down spacebar and the player is not dead
                CreatePlayerProjectiles();
            }
            //Updates the player's location depending on the inputs
            MovePlayer();
            //Updates the enemy health bar
            UpdateEnemyHealthBar();
            //Checks if the projectile is alive
            IsProjectileAlive();
        }

        //Trevor Kollins
        //Frame rate loop, runs every 60th of a second
        void TrevorFrameRateLoop()
        {
            //Run MoveStraightProjectile (moves straight projectiles)
            MoveStraightProjectile();

            //Run UpdateEnemyLocationAndDirection (Moves the enemy)
            UpdateEnemyLocationAndDirection();

            //Run SelectEnemyAttack (selects which enemyAttackSubprogram to run)
            SelectEnemyAttack();


            //If player is dead
            if (isPlayerDead == true)
            {
                //Run the PlayerDied subprogram (gives the player invincibility frames and respawns them at the centre location after a certain time)
                PlayerDied();
            }
        }

        //Michelle Kee
        //Key Down - check if a button is pressed and updates variables
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            //If the up arrow is pressed 
            if (e.KeyCode == Keys.Up)
            {
                //Player up boolean becomes true
                playerUp = true;
            }
            //If the down arrow is pressed
            else if (e.KeyCode == Keys.Down)
            {
                //Player down boolean becomes true
                playerDown = true;
            }
            //If the right arrow is pressed
            else if (e.KeyCode == Keys.Right)
            {
                //Player right boolean becomes true
                playerRight = true;
            }
            //If the left arrow is pressed
            else if (e.KeyCode == Keys.Left)
            {
                //Player left boolean beocmes true
                playerLeft = true;
            }

            //If enter is pressed
            else if (e.KeyCode == Keys.Enter)
            {
                //If the game has not already ended and the game is not already running
                if (gameState != END && gameState != RUNNING)
                {
                    //Play battle background music
                    Music(BATTLE_BACKGROUND_MUSIC);
                    //The game state will become running; game will start
                    gameState = RUNNING;
                    //Hide the start screen labels
                    lblTitle.Hide();
                    lblInstructions.Hide();
                    lblPrompt1.Hide();
                    lblMotivationPrompt.Hide();
                    lblPromptBoost1.Hide();
                    lblPromptBoost2.Hide();
                    lblPromptBoost3.Hide();
                    //Hide start screen picture boxes
                    picAddLife.Hide();
                    picDmgBoost.Hide();
                    picSpeedBoost.Hide();
                }
                //The frame rate loop will start 
                FrameRateLoop();
            }

            //If space bar is pressed
            else if (e.KeyCode == Keys.Space)
            {
                //Player shooting boolean becomes true
                playerShoot = true;
            }

            //If shift is pressed
            else if (e.KeyCode == Keys.ShiftKey)
            {
                //Boolean for showing the player's actual hitbox and slowing down the player will be true
                preciseMovementAndShowPlayerActualHitBox = true;

            }

            //If r is pressed
            else if (e.KeyCode == Keys.R)
            {
                //If the game has ended
                if (gameState == END)
                {
                    //Then restart the game
                    Application.Restart();
                }
            }

            //If esc is pressed
            else if (e.KeyCode == Keys.Escape)
            {
                //If the game has ended
                if (gameState == END)
                {
                    //Then close the game
                    Application.Exit();
                }
            }
        }

        //Michelle Kee
        //Key Up - updates variables when a key is released
        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            //if up arrow is released
            if (e.KeyCode == Keys.Up)
            {
                //player up boolean becomes false
                playerUp = false;
            }
            //if the down arrow is released
            else if (e.KeyCode == Keys.Down)
            {
                //player down boolean beomces false
                playerDown = false;
            }
            //if the right arrow is released
            else if (e.KeyCode == Keys.Right)
            {
                //player right boolean becomes false
                playerRight = false;
            }
            //if the left arrow is released
            else if (e.KeyCode == Keys.Left)
            {
                //player left boolean becomes false
                playerLeft = false;
            }
            //If space bar is released
            else if (e.KeyCode == Keys.Space)
            {
                //Player shooting boolean becomes false
                playerShoot = false;
            }
            //If shift is released
            else if (e.KeyCode == Keys.ShiftKey)
            {
                //Boolean for showing the player's real hitbox and slowing down the player becomes false
                preciseMovementAndShowPlayerActualHitBox = false;
            }
        }

        //Michelle Kee
        //Subprogram for player movement 
        void MovePlayer()
        {
            //Variable to add to speed, changes depending on whether there's a speed powerup or not.
            int speedIncrease = 0;
            //Variable to divide the player's speed. It changes depending if the player holds shift or not
            int speedDivide = 1;

            //If the speed boost is active, the player speed will increase.
            if (speedBoost == true)
            {
                //The player's speed will increase, this variable will be added to all player speeds
                speedIncrease = 2;
            }
            //If player is holding shift and preciseMovementAndShowPlayerActualHitBox is true
            if (preciseMovementAndShowPlayerActualHitBox == true)
            {
                //The variable used to divide the player's speed will be 2, this will cause the speed to be divided by 2
                speedDivide = 2;
            }

            //If playerup is true
            if (playerUp == true)
            {
                //The player will move up - Player's Y location is subtracted by player speed ( and also the speed increment amount ) divided by the speedDivide variable ( 1 or 2 depending if shift key is pressed)
                playerLocation.Y = playerLocation.Y - (PLAYER_SPEED - speedIncrease) / speedDivide;
                //Sets top client boundary
                //If the player's y is less than 0 (above the top)
                if (playerLocation.Y < 0)
                {
                    //Then the player's y will be 0
                    playerLocation.Y = 0;
                }
            }
            //If playerdown is true
            else if (playerDown == true)
            {
                //The player will move down - Player's Y location is added by player speed ( and also the speed increment amount ) divided by the speedDivide variable ( 1 or 2 depending if shift key is pressed)
                playerLocation.Y = playerLocation.Y + (PLAYER_SPEED + speedIncrease) / speedDivide;
                //Sets bottom client boundary
                //If the player is lower than the bottom of the client
                if (playerLocation.Y + playerSize.Height > ClientSize.Height)
                {
                    //The bottom of the player will be at the bottom of the client
                    playerLocation.Y = ClientSize.Height - playerSize.Height;
                }
            }
            //If playerright is true
            if (playerRight == true)
            {
                //The player will move right - Player's X location is added by player speed ( and also the speed increment amount ) divided by the speedDivide variable ( 1 or 2 depending if shift key is pressed)
                playerLocation.X = playerLocation.X + (PLAYER_SPEED + speedIncrease) / speedDivide;
                //If the player exceeds the right boundary
                if (playerLocation.X + playerSize.Width > ClientSize.Width)
                {
                    //The player will be at the very right of the screen
                    playerLocation.X = ClientSize.Width - playerSize.Width;
                }
            }
            //If playerleft is true
            else if (playerLeft == true)
            {
                //The player will move left - Player's X location is subtracted by player speed ( and also the speed increment amount ) divided by the speedDivide variable ( 1 or 2 depending if shift key is pressed)
                playerLocation.X = playerLocation.X - (PLAYER_SPEED - speedIncrease) / speedDivide;
                //If the player exceeds the left boundary
                if (playerLocation.X < 0)
                {
                    //The player will be at the very left of the client
                    playerLocation.X = 0;
                }
            }
            //Updates the player location to the hitbox 
            playerHitBox.Location = playerLocation;
            //Updates the location of the player's actual hitbox (for dodging projectiles)
            playerCollisionDetectionLocation = new Point(((playerLocation.X + (playerSize.Width / 2)) - (playerCollisionDetectionSize.Width / 2)), ((playerLocation.Y + (playerSize.Height / 2)) - (playerCollisionDetectionSize.Height / 2)));
            //Updates the player's actual hitbox (for dodging projectiles)
            playerCollisionDetectionHitBox = new Rectangle(playerCollisionDetectionLocation, playerCollisionDetectionSize);
        }

        //Onpaint, calls trevor and michelle's onpaints, which draw graphics
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            //Run Trevor's onpaint graphics
            TrevorOnPaint(e);
            //Run Michelle's Onpaint graphics
            MichelleOnPaint(e);
        }

        //Michelle Kee
        //Draws grahpics
        void MichelleOnPaint(PaintEventArgs e)
        {
            //If the game just started
            if (gameState == START)
            {
                //draw the start background
                e.Graphics.DrawImage(Properties.Resources.SkyBackground, 0, 0, ClientSize.Width, ClientSize.Height);
            }
            //If the game is running
            else if (gameState == RUNNING)
            {
                //If the player is not invisible
                if (playerVisible == true)
                {

                    //If the player is moving right
                    if (playerRight == true)
                    {
                        //Draws a picture of the player facing right
                        e.Graphics.DrawImage(Properties.Resources.CirnoFlyRight, playerHitBox);
                    }
                    //If the player is moving left
                    else if (playerLeft == true)
                    {
                        //Draws a picture of the player facing left
                        e.Graphics.DrawImage(Properties.Resources.CirnoFlyLeft, playerHitBox);
                    }
                    //If the player is moving down
                    else if (playerDown == true)
                    {
                        //Draws a picture of the player facing back
                        e.Graphics.DrawImage(Properties.Resources.CirnoFlyBack, playerHitBox);
                    }
                    //If the player is moving up
                    else if (playerUp == true)
                    {
                        //Draws a picture of the player facing foward
                        e.Graphics.DrawImage(Properties.Resources.CirnoFlyFoward, playerHitBox);
                    }
                    //If the player is idling - not moving
                    else
                    {
                        //If the player animation frame count is between  0 - 1
                        if (playerAnimationFrame < 2)
                        {
                            //Show the first player idle grahpic
                            e.Graphics.DrawImage(Properties.Resources.CirnoIdle1, playerHitBox);
                        }
                        //if the player animation frame count is between 2 - 3
                        else if (playerAnimationFrame >= 2 && playerAnimationFrame < 4)
                        {
                            //Show the second player idle grahpic
                            e.Graphics.DrawImage(Properties.Resources.CirnoIdle2, playerHitBox);
                        }
                        //if the player animation frame count is between 4 - 5
                        else if (playerAnimationFrame >= 4 && playerAnimationFrame < 6)
                        {
                            //Show the third player idle grahpic
                            e.Graphics.DrawImage(Properties.Resources.CirnoIdle3, playerHitBox);
                        }
                        //if the player animatino frame count is more than 6
                        else
                        {
                            //Show the fourth player idle grahpic
                            e.Graphics.DrawImage(Properties.Resources.CirnoIdle4, playerHitBox);
                        }
                        //Subprogram that updates the frame number for the player animation
                        UpdatePlayerAnimationFrame();
                    }

                    //If the show player's real hit box variable is true
                    if (preciseMovementAndShowPlayerActualHitBox == true)
                    {
                        //Draws the player's actual hit box for collision detection
                        e.Graphics.FillRectangle(Brushes.PaleVioletRed, playerCollisionDetectionHitBox);
                    }

                }
                //Loops through all projectiles
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    //If the projectile is a damage powerup
                    if (projectilePossession[i] == DAMAGE_POWER_UP_PROJECTILE)
                    {
                        //Draw the projectile using the damage boost image
                        e.Graphics.DrawImage(Properties.Resources.DamageBoostPowerUp, projectileBoundary[i]);
                    }
                    //If the projectile is a speed power up
                    else if (projectilePossession[i] == SPEED_POWER_UP_PROJECTILE)
                    {
                        //Draw the projectile using the speed boost image
                        e.Graphics.DrawImage(Properties.Resources.SpeedBoostPowerUp, projectileBoundary[i]);
                    }
                    //If the projectile is a life item
                    else if (projectilePossession[i] == ADDITIONAL_LIFE_PROJECTILE)
                    {
                        //Draw the projectile using the life power up image
                        e.Graphics.DrawImage(Properties.Resources.LifePowerUp, projectileBoundary[i]);
                    }
                    //If the projectile is a player projectile
                    else if (projectilePossession[i] == PLAYER_PROJECTILE)
                    {
                        //Draw the projetile using the player projectile image
                        e.Graphics.DrawImage(Properties.Resources.PlayerProjectile, projectileBoundary[i]);
                    }

                }

                //Draws how many lives there are left for the player
                e.Graphics.DrawString("Number of Lives: " + playerLives, DefaultFont, Brushes.DeepSkyBlue, playerStatsLocation);

                //Tells the player whether the player's actual hitbox is shown or not. It is displayed under the number of lives left.
                e.Graphics.DrawString("Show Player Hitbox: " + preciseMovementAndShowPlayerActualHitBox, DefaultFont, Brushes.DeepSkyBlue, playerStatsLocation.X, playerStatsLocation.Y + STAT_HEIGHT_SPACING);

                //Tells the player if power ups are present
                e.Graphics.DrawString("Damage Boost: " + damageBoost, DefaultFont, Brushes.DeepSkyBlue, playerStatsLocation.X, playerStatsLocation.Y + STAT_HEIGHT_SPACING * 2);
                e.Graphics.DrawString("Speed Boost: " + speedBoost, DefaultFont, Brushes.DeepSkyBlue, playerStatsLocation.X, playerStatsLocation.Y + STAT_HEIGHT_SPACING * 3);

                //Fills the player's health bars
                e.Graphics.FillRectangle(Brushes.Red, enemyCurrentHealthBarBoundary);

                //Draws the player's health bar outline
                e.Graphics.DrawRectangle(Pens.Black, enemyHealthBarBoundary);
            }
        }

        //Trevor Kollins
        //Draws graphics Trevor makes
        void TrevorOnPaint(PaintEventArgs e)
        {
            //if the gamestate is running
            if (gameState == RUNNING)
            {
                //Draw the background battle image using backgroundBoundary
                e.Graphics.DrawImage(Properties.Resources.BattleBackground, backgroundBoundary);

                //Loop for all projectiles
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    //If the projectile belongs to the enemy
                    if (projectilePossession[i] == ENEMY_PROJECTILE)
                    {
                        //if the projectile is blue
                        if (projectileColor[i] == BLUE)
                        {
                            //Draw a blue projectile with the boundary set
                            e.Graphics.DrawImage(Properties.Resources.Blue_Projectile, projectileBoundary[i]);
                        }

                        //else if the projectile is red
                        else if (projectileColor[i] == RED)
                        {
                            //Draw a red projectile with the boundary set
                            e.Graphics.DrawImage(Properties.Resources.Red_Projectile, projectileBoundary[i]);
                        }

                        //else the projectile is green
                        else
                        {
                            //Draw a red projectile with the boundary set
                            e.Graphics.DrawImage(Properties.Resources.Green_Projectile, projectileBoundary[i]);
                        }
                    }
                }

                //Update enemy animation frame (controls what sprite is shown)
                enemyAnimationFrame++;

                //If enemy is moving left
                if (enemyDirection == ENEMY_MOVE_LEFT)
                {
                    //If enemy animation frame is less or equal to 1
                    if (enemyAnimationFrame <= 1)
                    {
                        //Show the first enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_F_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 2 and less than or equal to 4
                    else if (enemyAnimationFrame > 2 && enemyAnimationFrame <= 4)
                    {
                        //Show the second enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_L_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 4 and less than or equal to 6
                    else if (enemyAnimationFrame > 4 && enemyAnimationFrame <= 6)
                    {
                        //Show the third enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_L_1, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 6 and less than or equal to 8
                    else if (enemyAnimationFrame > 6 && enemyAnimationFrame <= 8)
                    {
                        //Show the fourth enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_L_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 8 and less than or equal to 10
                    else if (enemyAnimationFrame > 8 && enemyAnimationFrame <= 10)
                    {
                        //Show the fifth enemy leftsprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_F_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 10 and less than or equal to 12
                    else if (enemyAnimationFrame > 10 && enemyAnimationFrame <= 12)
                    {
                        //Show the sixth enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_R_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 12 and less than or equal to 14
                    else if (enemyAnimationFrame > 12 && enemyAnimationFrame <= 14)
                    {
                        //Show the seventh enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_R_1, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 14 and less than or equal to 16
                    else if (enemyAnimationFrame > 14 && enemyAnimationFrame <= 16)
                    {
                        //Show the eighth enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_R_0, enemyBoundary);
                    }

                    //Else enemy animation frame is greater than 16
                    else
                    {
                        //Show the ninth enemy left sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_L_F_0, enemyBoundary);

                        //If enemyAnimationFrame is 17
                        if (enemyAnimationFrame == 17)
                        {
                            //Set enemyAnimationFrame back to 0
                            enemyAnimationFrame = 0;
                        }
                    }
                }

                //else If enemy is moving right
                else if (enemyDirection == ENEMY_MOVE_RIGHT)
                {
                    //If enemy animation frame is less or equal to 1
                    if (enemyAnimationFrame <= 1)
                    {
                        //Show the first enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_F_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 2 and less than or equal to 4
                    else if (enemyAnimationFrame > 2 && enemyAnimationFrame <= 4)
                    {
                        //Show the second enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_L_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 4 and less than or equal to 6
                    else if (enemyAnimationFrame > 4 && enemyAnimationFrame <= 6)
                    {
                        //Show the third enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_L_1, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 6 and less than or equal to 8
                    else if (enemyAnimationFrame > 6 && enemyAnimationFrame <= 8)
                    {
                        //Show the fourth enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_L_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 8 and less than or equal to 10
                    else if (enemyAnimationFrame > 8 && enemyAnimationFrame <= 10)
                    {
                        //Show the fifth enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_F_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 10 and less than or equal to 12
                    else if (enemyAnimationFrame > 10 && enemyAnimationFrame <= 12)
                    {
                        //Show the sixth enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_R_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 12 and less than or equal to 14
                    else if (enemyAnimationFrame > 12 && enemyAnimationFrame <= 14)
                    {
                        //Show the seventh enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_R_1, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 14 and less than or equal to 16
                    else if (enemyAnimationFrame > 14 && enemyAnimationFrame <= 16)
                    {
                        //Show the eighth enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_R_0, enemyBoundary);
                    }

                    //Else enemy animation frame is greater than 16
                    else
                    {
                        //Show the ninth enemy right sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_R_F_0, enemyBoundary);

                        //If enemyAnimationFrame is 17 
                        if (enemyAnimationFrame == 17)
                        {
                            //Set enemyAnimationFrame back to 0
                            enemyAnimationFrame = 0;
                        }
                    }
                }

                //Else the enemy is facing forward
                else
                {
                    //If enemy animation frame is less or equal to 1
                    if (enemyAnimationFrame <= 1)
                    {
                        //Show the first enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_FR_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 2 and less than or equal to 4
                    else if (enemyAnimationFrame > 2 && enemyAnimationFrame <= 4)
                    {
                        //Show the second enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_L_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 4 and less than or equal to 6
                    else if (enemyAnimationFrame > 4 && enemyAnimationFrame <= 6)
                    {
                        //Show the third enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_L_1, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 6 and less than or equal to 8
                    else if (enemyAnimationFrame > 6 && enemyAnimationFrame <= 8)
                    {
                        //Show the fourth enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_L_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 8 and less than or equal to 10
                    else if (enemyAnimationFrame > 8 && enemyAnimationFrame <= 10)
                    {
                        //Show the fifth enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_FR_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 10 and less than or equal to 12
                    else if (enemyAnimationFrame > 10 && enemyAnimationFrame <= 12)
                    {
                        //Show the sixth enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_R_0, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 12 and less than or equal to 14
                    else if (enemyAnimationFrame > 12 && enemyAnimationFrame <= 14)
                    {
                        //Show the seventh enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_R_1, enemyBoundary);
                    }

                    //Else if enemy animation frame is greater than 14 and less than or equal to 16
                    else if (enemyAnimationFrame > 14 && enemyAnimationFrame <= 16)
                    {
                        //Show the eighth enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_R_0, enemyBoundary);
                    }

                    //Else enemy animation frame is greater than 16
                    else
                    {
                        //Show the ninth enemy forward sprite
                        e.Graphics.DrawImage(Properties.Resources.ENEMY_FOR_FR_0, enemyBoundary);

                        //If enemyAnimationFrame is 17
                        if (enemyAnimationFrame == 17)
                        {
                            //Set enemyAnimationFrame back to 0
                            enemyAnimationFrame = 0;
                        }
                    }
                }
            }

            //Else if the game has ended
            else if (gameState == END)
            {
                //Draw the background end image using backgroundBoundary
                e.Graphics.DrawImage(Properties.Resources.EndBackground, backgroundBoundary);
            }
        }

        //Michelle Kee
        //Subprogram that updates the frame number for the player animation
        void UpdatePlayerAnimationFrame()
        {
            //Increases the frame number
            playerAnimationFrame++;
            //If the frame number exceeds 7 
            if (playerAnimationFrame > 7)
            {
                //The frame number will start from 0 again
                playerAnimationFrame = 0;
            }
        }

        //Trevor Kollins
        //Generates a random location for the enemy to travel to
        void RandomEnemyLocation()
        {
            //Generate a random number to determine the location the enemy will go to
            int location = numberGenerator.Next(0, NUMBER_OF_LOCATIONS);

            //If the number is 0
            if (location == 0)
            {
                //Travel to the centre
                travelDestination = enemyLocationCentre;
            }

            //else if the number is 1
            else if (location == 1)
            {
                //Travel to the top
                travelDestination = enemyLocationTop;
            }

            //else if the number is 2
            else if (location == 2)
            {
                //Travel to the top left corner
                travelDestination = enemyLocationTopLeft;
            }

            //else if the number is 3
            else if (location == 3)
            {
                //Travel to the top right corner
                travelDestination = enemyLocationTopRight;
            }

            //else if the number is 4
            else if (location == 4)
            {
                //Travel to the middle left location
                travelDestination = enemyLocationMiddleLeft;
            }

            //Else the number is 5
            else
            {
                //Travel to the middle right location
                travelDestination = enemyLocationMiddleRight;
            }
        }

        //Add graphic subprogram, adds graphics for enemy and player projectiles (Trevor subprogram)
        //Uses the projectileLocationVariable parameter, which passes in the location of the projectile
        //Uses the whoseProjectile pararmeter, which specifies if its a player or enemy projectile
        //Uses the angle parameter, which specifies the angle of the projectile
        //Uses the speed parameter, which controls the projectile's speed.
        //Uses the projectileSize parameter, which controls the projectile's size
        //Uses color parameter, which specifies the projectile's color
        void AddStraightProjectile(PointF projectileLocationVariable, int whoseProjectile, double angle, int speed, Size projectileSize, int color)
        {
            //Check if the length of the arrays equals the number of projectiles (such that there is a value in the last index of the arrays)
            if (numberOfProjectiles == projectileLocation.Length)
            {
                //If the array needs to be resized to fit new projectile, run ResizeBigger subprogram
                ResizeBigger();
            }

            //Store who the projectile belongs to in the index equaling the number of projectiles
            projectilePossession[numberOfProjectiles] = whoseProjectile;

            //Store the color of the projectile in the index eqaling the number of projectile
            projectileColor[numberOfProjectiles] = color;

            //Set the location of the projectile in the index equaling the number of projectiles (using the parameter passed in)
            projectileLocation[numberOfProjectiles] = projectileLocationVariable;

            //Set the projectile type to straight (in the index equaling the number of projectiles)
            projectileMovementType[numberOfProjectiles] = STRAIGHT_PROJECTILE;
            //Set the boundary of the projectile (in the index equaling the number of projectiles) to the location of the projectile and the size of a straight projectile
            projectileBoundary[numberOfProjectiles] = new RectangleF(projectileLocation[numberOfProjectiles], projectileSize);
            //Set the projectile x speed to cos(angle) times the speed of the projectile, rounded to a float.
            projectileXSpeed[numberOfProjectiles] = (float)(Math.Cos(angle * Math.PI / 180) * speed);
            //Set the projectile y speed to sin(angle) times the speed of the projectile, rounded a float.
            projectileYSpeed[numberOfProjectiles] = (float)(Math.Sin(angle * Math.PI / 180) * speed);

            //Increase the number of projectiles by 1
            numberOfProjectiles++;
        }

        //Trevor Kollins
        //Subprogram to add a circular projectile
        //Uses the parameters angle, which is the angle in relation to the enemy the projectile is at, the size of the projectile, and the color of the projectile
        void AddCircularProjectile(int angle, Size projectileSize, int color)
        {
            //Check if the length of the arrays equals the number of projectiles (such that there is a value in the last index of the arrays)
            if (numberOfProjectiles == projectileLocation.Length)
            {
                //If the array needs to be resized to fit new projectile, run ResizeBigger subprogram
                ResizeBigger();
            }

            //Give the projectile an angle on the cardinal plane in relation to the enemy
            //Set the angle that was passed in as the angle of the projectile
            projectileAngle[numberOfProjectiles] = angle;

            //Set the location of the projectile in the index equaling the number of projectiles (using the parameter angle passed in as a reference)
            //X location is the enemy x location plus the cosine of the angle on the cartinal plane passed in in relation to the enemy, multiplied by the distance between the the enemy location and the projectile constant
            projectileLocation[numberOfProjectiles].X = enemyLocation.X + ((int)Math.Round(Math.Cos(projectileAngle[numberOfProjectiles] * Math.PI / 180) * CIRCULAR_PROJECTILE_HYPOTENUSE_DISTANCE));
            //Y location is the enemy y location minus the sine of the angle on the cartinal plane passed in in relation to the enemy, multiplied by the distance between the the enemy location and the projectile constant
            projectileLocation[numberOfProjectiles].Y = enemyLocation.Y - ((int)Math.Round(Math.Sin(projectileAngle[numberOfProjectiles] * Math.PI / 180) * CIRCULAR_PROJECTILE_HYPOTENUSE_DISTANCE));

            //Set the color in the index equaling the number of projectiles to the color passed in
            projectileColor[numberOfProjectiles] = color;
            //Store it as an enemy projectile in the index equaling the number of projectiles
            projectilePossession[numberOfProjectiles] = ENEMY_PROJECTILE;
            //Set the projectile type to circular (in the index equaling the number of projectiles)
            projectileMovementType[numberOfProjectiles] = CIRCULAR_PROJECTILE;
            //Set the boundary of the projectile (in the index equaling the number of projectiles) to the location of the projectile and the size of a circular projectile
            projectileBoundary[numberOfProjectiles] = new RectangleF(projectileLocation[numberOfProjectiles], projectileSize);

            //Increase the number of projectiles by 1
            numberOfProjectiles++;
        }

        //Michelle Kee
        //A subprogram that removes a projectile and shifts the other projectile indexes towards the top of the projectile list (so there are no empty spaces in the array
        void RemoveProjectile(int projectileIndex)
        {
            //Only run if the index to be removed is the last index
            if (projectileIndex == numberOfProjectiles - 1)
            {
                //Sets the movement type to -1 (Nothing)
                projectileMovementType[projectileIndex] = NO_PROJECTILE;
                //Other projectile arrays are not updated since they will be updated with a new projectile eventually or removed when array is resized.
                //The number of projectiles will decrease
                numberOfProjectiles--;
                //No need to shift anything in this case
            }
            //If the index is anywhere in the middle of the filled array (the parts of the array with active projectiles)
            else
            {
                //First, shift everything over and overlap the index (that's intended to be removed), then remove the last index.

                //Runs through all the indexes after removed index and copies data to one index after
                for (int i = projectileIndex + 1; i < numberOfProjectiles; i++)
                {
                    //Shifts all the data in the indexes to the one piror, replacing the removed index.
                    projectileLocation[i - 1] = projectileLocation[i];
                    projectileBoundary[i - 1] = projectileBoundary[i];
                    projectileMovementType[i - 1] = projectileMovementType[i];
                    projectilePossession[i - 1] = projectilePossession[i];
                    projectileXSpeed[i - 1] = projectileXSpeed[i];
                    projectileYSpeed[i - 1] = projectileYSpeed[i];
                    projectileAngle[i - 1] = projectileAngle[i];
                    projectileColor[i - 1] = projectileColor[i];
                }
                //Removes the last index (with active projectile)
                //Sets the movement type to -1 (Nothing)
                projectileMovementType[numberOfProjectiles - 1] = NO_PROJECTILE;
                //Other projectile arrays are not updated since they will be updated with a new projectile eventually or removed when array is resized.
                //The number of projectiles will decrease
                numberOfProjectiles--;
            }

            //Check if the number of projectiles are 15 less than the size of the array
            if (numberOfProjectiles + 15 == projectilePossession.Length)
            {
                //Then the projectile arrays will be resized smaller
                ResizeSmaller();
            }
        }


        //Trevor Kollins
        //Function which calculates x location of a circular projectile
        //Uses an angle parameter which is the angle of the projectile in relation to the enemy on the cartinal plane
        //Uses the projectileRadius parameter, which is the radius of the projectile graphic
        int circularXLocation(int angle, int projectileRadius)
        {
            //X projectile location is enemy projectile location + the cosine of the angle on the cartesian plane in with the enemy in the centre, multiplied by the hypotnuse distance between the enemy and the projectile, subtracted by the radius of the projecitle size.
            return enemyLocation.X + ((int)Math.Round(Math.Cos(angle * Math.PI / 180) * CIRCULAR_PROJECTILE_HYPOTENUSE_DISTANCE)) - projectileRadius;
        }

        //Trevor Kollins
        //Function which calculates y location of a circular projectile
        //Uses an angle parameter which is the angle of the projectile in relation to the enemy on the cartinal plane
        //Uses the projectileRadius parameter, which is the radius of the projectile graphic
        int circularYLocation(int angle, int projectileRadius)
        {
            //Y projectile location is enemy projectile location - the sine of the angle on the cartesian plane in with the enemy in the centre, multiplied by the hypotnuse distance between the enemy and the projectile, subtracted by the radius of the projecitle size.
            return enemyLocation.Y - ((int)Math.Round(Math.Sin(angle * Math.PI / 180) * CIRCULAR_PROJECTILE_HYPOTENUSE_DISTANCE)) - projectileRadius;
        }

        //Trevor Kollins
        //Move projectile subprogram, moves straight projectiles
        void MoveStraightProjectile()
        {
            //Loop once for the number of projectiles
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                //If the projecitle is straight
                if (projectileMovementType[i] == STRAIGHT_PROJECTILE)
                {
                    //Add the x speed and decrease the projecitle y speed to the location
                    projectileLocation[i].X = projectileLocation[i].X + projectileXSpeed[i];
                    projectileLocation[i].Y = projectileLocation[i].Y - projectileYSpeed[i];
                    //Update the hitbox location
                    projectileBoundary[i].Location = projectileLocation[i];
                }
            }
        }

        //Trevor Kollins
        //Resize bigger subprogram, increases the projectile location, boundary, movement type, possesion, x speed, y speed angle and color arrays. (Trevor subprogram)
        void ResizeBigger()
        {
            //Create a local array with a length 15 indexs larger then the given arrays
            PointF[] resizeArrayLoc = new PointF[projectileLocation.Length + RESIZE_NUMBER];
            RectangleF[] resizeArrayBou = new RectangleF[projectileBoundary.Length + RESIZE_NUMBER];
            int[] resizeArrayMovTyp = new int[projectileMovementType.Length + RESIZE_NUMBER];
            int[] resizeArrayPos = new int[projectilePossession.Length + RESIZE_NUMBER];
            float[] resizeArrayXSpd = new float[projectileXSpeed.Length + RESIZE_NUMBER];
            float[] resizeArrayYSpd = new float[projectileYSpeed.Length + RESIZE_NUMBER];
            int[] resizeArrayAng = new int[projectileAngle.Length + RESIZE_NUMBER];
            int[] resizeArrayCol = new int[projectileColor.Length + RESIZE_NUMBER];

            //Loop once for each index in which there is a value stored (the size of the array)
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                //Copy the value currently stored in index i of each array to the resize array
                resizeArrayLoc[i] = projectileLocation[i];
                resizeArrayBou[i] = projectileBoundary[i];
                resizeArrayMovTyp[i] = projectileMovementType[i];
                resizeArrayPos[i] = projectilePossession[i];
                resizeArrayXSpd[i] = projectileXSpeed[i];
                resizeArrayYSpd[i] = projectileYSpeed[i];
                resizeArrayAng[i] = projectileAngle[i];
                resizeArrayCol[i] = projectileColor[i];
            }

            //Set the old arrays to the resize array (such that it is now 15 index's larger)
            projectileLocation = resizeArrayLoc;
            projectileBoundary = resizeArrayBou;
            projectileMovementType = resizeArrayMovTyp;
            projectilePossession = resizeArrayPos;
            projectileXSpeed = resizeArrayXSpd;
            projectileYSpeed = resizeArrayYSpd;
            projectileAngle = resizeArrayAng;
            projectileColor = resizeArrayCol;
        }

        //Michelle Kee
        //A subprogram that resizes the projectile arrays (location, boundary, movement type, possesion, xspeed, yspeed, angle, color)
        void ResizeSmaller()
        {
            //Arrays that will be used to resize the old projectile arrays
            PointF[] resizeProjectileLocation = new PointF[projectileLocation.Length - RESIZE_NUMBER];
            RectangleF[] resizeProjectileBoundary = new RectangleF[projectileBoundary.Length - RESIZE_NUMBER];
            int[] resizeProjectileMovementType = new int[projectileMovementType.Length - RESIZE_NUMBER];
            int[] resizeProjectilePossession = new int[projectilePossession.Length - RESIZE_NUMBER];
            float[] resizeProjectileXSpeed = new float[projectileXSpeed.Length - RESIZE_NUMBER];
            float[] resizeProjectileYSpeed = new float[projectileYSpeed.Length - RESIZE_NUMBER];
            int[] resizeProjectileAngle = new int[projectileAngle.Length - RESIZE_NUMBER];
            int[] resizeProjectileColor = new int[projectileColor.Length - RESIZE_NUMBER];

            //Runs through all the indexes with an active projectile
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                //Copies the information in the original projectile arrays to the resize arrays
                resizeProjectileLocation[i] = projectileLocation[i];
                resizeProjectileBoundary[i] = projectileBoundary[i];
                resizeProjectileMovementType[i] = projectileMovementType[i];
                resizeProjectilePossession[i] = projectilePossession[i];
                resizeProjectileXSpeed[i] = projectileXSpeed[i];
                resizeProjectileYSpeed[i] = projectileYSpeed[i];
                resizeProjectileAngle[i] = projectileAngle[i];
                resizeProjectileColor[i] = projectileColor[i];
            }
            //Resizes the original arrays
            projectileLocation = resizeProjectileLocation;
            projectileBoundary = resizeProjectileBoundary;
            projectileMovementType = resizeProjectileMovementType;
            projectilePossession = resizeProjectilePossession;
            projectileXSpeed = resizeProjectileXSpeed;
            projectileYSpeed = resizeProjectileYSpeed;
            projectileAngle = resizeProjectileAngle;
            projectileColor = resizeProjectileColor;
        }

        //Michelle Kee
        //A subprogram that checks for projectile collision
        void IsProjectileAlive()
        {
            //A loop that runs through all the projectiles in the array
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                //If the projectile's location exits the boundary of the client
                if (projectileLocation[i].X < 0 || projectileLocation[i].X > ClientSize.Width || projectileLocation[i].Y < 0 || projectileLocation[i].Y > ClientSize.Height)
                {
                    //The projectile will be destroyed
                    //Also - The other projectiles will be shifted so there are no spaces in the array after a projectile is removed
                    RemoveProjectile(i);
                }
                //If the enemy projectile hits the player     (and the projectile is not circling aruond the enemy)       (only runs if player is not dead)
                else if (projectileBoundary[i].IntersectsWith(playerCollisionDetectionHitBox) && projectilePossession[i] == ENEMY_PROJECTILE && projectileMovementType[i] != CIRCULAR_PROJECTILE && isPlayerDead == false)
                {

                    //The projectile will be destroyed
                    //Also - The other projectiles will be shifted so there are no spaces in the array after a projectile is removed
                    RemoveProjectile(i);

                    //The player will lose one life
                    playerLives--;
                    //Player will be dead (For invincibility frames/respawning player)
                    isPlayerDead = true;
                    //The player will lose all power ups
                    ResetPowerUps();
                    //If the player's lives is zero, the player will lose.
                    if (playerLives == 0)
                    {
                        //A boolean that will indicate that the player has lost
                        playerWin = false;
                        //The gamestate will be updated, the game will stop (framerate timer will stop)
                        gameState = END;
                        //Shows the end game text
                        SetUpEndGame();

                    }

                }
                //If the player projectile hits the enemy        
                else if (projectilePossession[i] == PLAYER_PROJECTILE && projectileBoundary[i].IntersectsWith(enemyBoundary))
                {
                    //Variable for the additional damage from the damage boost
                    int damageBoostDamage = 0;
                    //If the damage boost is on
                    if (damageBoost == true)
                    {
                        damageBoostDamage = 5;
                    }

                    //The projectile will be destroyed
                    RemoveProjectile(i);

                    //The enemy will lose health
                    enemyHealth = enemyHealth - PLAYER_PROJECTILE_DAMAGE - damageBoostDamage;

                    //Generates a random number and determines if a powerup is dropped. (When enemy is hit, there will be a chance that a powerup is dropped)
                    PowerUpChance();

                    //If the enemy's health is less than or equal to zero, the enemy will die and the player will win
                    if (enemyHealth <= 0)
                    {
                        //A bool that indicates the player has won
                        playerWin = true;
                        //The gamestate will be updated, the game will stop (framerate timer will stop)
                        gameState = END;
                        //Shows the end game text
                        SetUpEndGame();

                    }

                }
                //If the player gets the speed power up
                else if (projectilePossession[i] == SPEED_POWER_UP_PROJECTILE && projectileBoundary[i].IntersectsWith(playerCollisionDetectionHitBox))
                {
                    //The speed boost variable will become true, enabling the boost.
                    speedBoost = true;
                    //The power up projectile will be destroyed
                    RemoveProjectile(i);
                }
                //If the player gets the damage power up
                else if (projectilePossession[i] == DAMAGE_POWER_UP_PROJECTILE && projectileBoundary[i].IntersectsWith(playerCollisionDetectionHitBox))
                {
                    //The damage boost variable will become true, enabling the boost
                    damageBoost = true;
                    //The power up projectile will be destroyed
                    RemoveProjectile(i);
                }
                //If the player gets the extra life power up
                else if (projectilePossession[i] == ADDITIONAL_LIFE_PROJECTILE && projectileBoundary[i].IntersectsWith(playerCollisionDetectionHitBox))
                {
                    //The player will gain an additional life
                    playerLives++;
                    //The power up projectile will be destroyed
                    RemoveProjectile(i);
                }
            }
        }

        //Michelle Kee
        //Creates the players projectiles if the player is holding down spacebar
        void CreatePlayerProjectiles()
        {
            //Adds a new projectile that will fire from the player, upwards.
            AddStraightProjectile(new Point(playerLocation.X + (playerSize.Width / 2), playerLocation.Y), PLAYER_PROJECTILE, STRAIGHT_UP_ANGLE, PLAYER_PROJECTILE_SPEED, playerProjectileSize, BLUE);
            //Adds a new projectile that will fire from the player, diagonally, upwards, angled to the right
            AddStraightProjectile(new Point(playerLocation.X + (playerSize.Width / 2), playerLocation.Y), PLAYER_PROJECTILE, RIGHT_UPWARDS_ANGLE, PLAYER_PROJECTILE_SPEED, playerProjectileSize, BLUE);
            //Adds a new projectile that will fire from the player, diagonally, upwards, angled to the left
            AddStraightProjectile(new Point(playerLocation.X + (playerSize.Width / 2), playerLocation.Y), PLAYER_PROJECTILE, LEFT_UPWARDS_ANGLE, PLAYER_PROJECTILE_SPEED, playerProjectileSize, BLUE);
        }

        //Michelle Kee
        //Subprogram that updates the enemy's health bar
        void UpdateEnemyHealthBar()
        {
            //Updates the length of the health bar based on enemy's health  - finds the percentage of the enemy's health by dividing its current health by it's maximum health, then multiplies it by the length of the health bar.
            enemyCurrentBarSize.Width = (int)((double)enemyHealth / MAX_ENEMY_HEALTH * enemyHealthBarSize.Width);
            //Updates the boundary to the location and new size
            enemyCurrentHealthBarBoundary = new Rectangle(enemyHealthBarLocation, enemyCurrentBarSize);
        }

        //Michelle Kee
        //A subprogram that generates a random number, and determines if a power up is dropped.
        void PowerUpChance()
        {
            //Generates a random number (for the chance of power up dropping) and saves it into powerUpChanceNumber
            powerUpChanceNumber = numberGenerator.Next(MIN_POWER_UP_CHANCE, MAX_POWER_UP_CHANCE + 1);
            //If the generated number is between 1 - 5 (the percentage chance of a powerup being dropped), a new powerup will drop.
            if (powerUpChanceNumber >= 1 && powerUpChanceNumber <= CHANCE_OF_POWER_UP)
            {
                //A new powerup will be generated
                GenerateNewPowerUp();
            }
        }

        //Michelle Kee
        //A subprogram that generates a new power up
        void GenerateNewPowerUp()
        {
            //A new chance number will be generated to determine the type of power up dropped.
            powerUpChanceNumber = numberGenerator.Next(MIN_POWER_UP_CHANCE, MAX_POWER_UP_CHANCE + 1);
            //POWER UPS - INFORMATION AND DROP RATES
            //Damage Boost - 60%    Increases the damage done by player - cannot be stacked
            //Speed Boost - 35%     Boosts the speed of the player - cannot be stacked
            //Additional Life - 5%  Gives an additional life to the player
            const int DAMAGE_BOOST_DROP_RATE = 60;
            const int SPEED_BOOST_DROP_RATE = 35;
            //Generates a random x location for the powerup to spawn at
            int randomPowerUpXLocation = numberGenerator.Next(0, ClientSize.Width + 1);
            //Generates a random y location for the powerup to spawn at (has to be at the top half of the screen)
            int randomPowerUpYLocation = numberGenerator.Next(0, ClientSize.Height / 2 + 1);
            //Stores the location in a point 
            Point powerUpLocation = new Point(randomPowerUpXLocation, randomPowerUpYLocation);

            //if the number is between 1 - 60 
            if (powerUpChanceNumber >= 1 && powerUpChanceNumber <= DAMAGE_BOOST_DROP_RATE)
            {
                //Damage boost power up will be created
                AddStraightProjectile(powerUpLocation, DAMAGE_POWER_UP_PROJECTILE, POWER_UP_PROJECTILE_ANGLE, POWER_UP_PROJECTILE_SPEED, powerUpSize, BLUE);
            }
            //If the number is between 61- 95
            else if (powerUpChanceNumber > DAMAGE_BOOST_DROP_RATE && powerUpChanceNumber <= DAMAGE_BOOST_DROP_RATE + SPEED_BOOST_DROP_RATE)
            {
                //Speed boost power up will be created
                AddStraightProjectile(powerUpLocation, SPEED_POWER_UP_PROJECTILE, POWER_UP_PROJECTILE_ANGLE, POWER_UP_PROJECTILE_SPEED, powerUpSize, BLUE);
            }
            //If the number is between 96 - 100
            else
            {
                //Additional life power up will be created
                AddStraightProjectile(powerUpLocation, ADDITIONAL_LIFE_PROJECTILE, POWER_UP_PROJECTILE_ANGLE, POWER_UP_PROJECTILE_SPEED, powerUpSize, BLUE);
            }

        }

        //Michelle Kee
        //A subprogram that resets the power up booleans to false - this is used when the player loses a life - they will also lose all their powerups
        void ResetPowerUps()
        {
            //Sets all power up booleans to false - player is able to collect power ups again
            damageBoost = false;
            speedBoost = false;

        }

        //Subprogram to update the enemy location
        void UpdateEnemyLocationAndDirection()
        {
            //If the travel destination's y value is less then the enemy location's y value
            if (travelDestination.Y < enemyLocation.Y)
            {
                //Travel up (decrease y location by ememy speed)
                enemyLocation.Y = enemyLocation.Y - ENEMY_SPEED;

                //If the enemy is less than 7 units away (enemy speed) in the y direction 
                if (enemyLocation.Y - travelDestination.Y < ENEMY_SPEED)
                {
                    //set the y location the destination y location
                    enemyLocation.Y = travelDestination.Y;
                }

                //Update enemy direction to straight
                enemyDirection = ENEMY_STRAIGHT;
            }

            //Else if the travel destination's y value is greater then the enemy location's y value
            else if (travelDestination.Y > enemyLocation.Y)
            {
                //travel down (increase y location by enemy speed)
                enemyLocation.Y = enemyLocation.Y + ENEMY_SPEED;

                //If the enemy is less than 7 units away (enemy speed) in the y direction
                if (travelDestination.Y - enemyLocation.Y < ENEMY_SPEED)
                {
                    //set the y location the destination y location
                    enemyLocation.Y = travelDestination.Y;
                }

                //Update enemy direction to straight
                enemyDirection = ENEMY_STRAIGHT;
            }

            //If the travel destination's x value is less then the enemy location's x value
            if (travelDestination.X < enemyLocation.X)
            {
                //Travel left (decrease x location by enemy speed)
                enemyLocation.X = enemyLocation.X - ENEMY_SPEED;

                //If the enemy is less than 7 units away (enemy speed) in the x direction
                if (enemyLocation.X - travelDestination.X < ENEMY_SPEED)
                {
                    //set the y location the destination y location
                    enemyLocation.X = travelDestination.X;
                }

                //Update enemy direction to left
                enemyDirection = ENEMY_MOVE_LEFT;
            }

            //Else if the travel destination's x value is greater then the enemy location's x value
            else if (travelDestination.X > enemyLocation.X)
            {
                //travel right (increase x location by enemy speed)
                enemyLocation.X = enemyLocation.X + ENEMY_SPEED;

                //If the enemy is less than 7 units away (enemy speed) in the x direction
                if (travelDestination.X - enemyLocation.X < ENEMY_SPEED)
                {
                    //set the y location the destination y location
                    enemyLocation.X = travelDestination.X;
                }

                //Update enemy direction to right
                enemyDirection = ENEMY_MOVE_RIGHT;
            }

            //Else enemy is not moving left or right
            else
            {
                //Set enemy direction to straight
                enemyDirection = ENEMY_STRAIGHT;
            }

            //Since enemy location is at the centre of the enemy, set the boudary location to be the location, subtracted by half of the size of the sprite in the x and y direction
            //Get the location minus half of the sprite size in the x and y direction
            int enemyBoundaryX = enemyLocation.X - (ENEMY_WIDTH / 2);
            int enemyBoundaryY = enemyLocation.Y - (ENEMY_HEIGHT / 2);
            //Set the location the boundary will be set at
            Point enemyBoundaryLocation = new Point(enemyBoundaryX, enemyBoundaryY);

            //Update boundary with the location.
            enemyBoundary.Location = enemyBoundaryLocation;
        }

        //Trevor Kollins
        //Subprogram that is run when the player's life is taken away, removes the player, and respawns them back in with invincibility frames for a short period
        void PlayerDied()
        {
            //If the player just died (respawn frame timer is 0)
            if (respawnFrameCounter == 0)
            {
                //Set is player dead to true
                isPlayerDead = true;
                //set player visible to false so they're no longer drawn
                playerVisible = false;
            }

            //Playervisible will blink on and off to let the user know they have invincibility frames:

            //Else if 15 frames has passed
            else if (respawnFrameCounter == 15)
            {
                //set the player's location to the starting location
                playerLocation = playerStartingLocation;
                //Update the player's boundary
                playerHitBox.Location = playerLocation;
                //set playerVisible to true
                playerVisible = true;
            }

            //Else if more than 17 frames has passed and less than 30
            else if (respawnFrameCounter >= 17 && respawnFrameCounter < 30)
            {
                //If 3 or more frames has passed since the last visibility toggle
                if (respawnFrameCounter - lastVisibilityToggleFrame >= 2)
                {
                    //Set the last visibility toggle frame to the current respawnFrameCounter value
                    lastVisibilityToggleFrame = respawnFrameCounter;

                    //if player visible is true
                    if (playerVisible == true)
                    {
                        //Set player visible to false
                        playerVisible = false;
                    }

                    //else player visible is false
                    else
                    {
                        //set player visible to true
                        playerVisible = true;
                    }
                }
            }

            //Else if 30 or more frames has passed (player starts blinking faster)
            else if (respawnFrameCounter >= 30)
            {
                //If 38 frames has passed
                if (respawnFrameCounter == 38)
                {
                    //Set player visible to true
                    playerVisible = true;
                    //Set isPlayerDead to false
                    isPlayerDead = false;
                    //Set respawnFrameCounter to 0
                    respawnFrameCounter = 0;
                    //Set lastVisibilityToggleFrame to 0
                    lastVisibilityToggleFrame = 0;
                }

                //else if player visible is true
                else if (playerVisible == true)
                {
                    //Set player visible to false
                    playerVisible = false;
                }

                //else player visible is false
                else
                {
                    //set player visible to true
                    playerVisible = true;
                }
            }

            //If player is still dead
            if (isPlayerDead == true)
            {
                //Increase the respawn frame counter by 1
                respawnFrameCounter++;
            }
        }

        //Trevor Kollins
        //Selects which enemy attack subprogram to call
        void SelectEnemyAttack()
        {
            //If the initial delay is not over (such that the player has a chance of moving before the enemy starts attacking
            if (enemyInitialDelayOver == false)
            {
                //if the enemy location equals travel destination
                if (enemyLocation == travelDestination)
                {
                    //Set initial delay to over so the enemy can start attakcing
                    enemyInitialDelayOver = true;
                }
            }

            //Else the initial delay is over
            else
            {
                //if the enemy is not attacking
                if (isEnemyAttacking == false)
                {
                    //Run the new attack function, which sets up and selectsa  new attack
                    currentAttackPattern = NewAttack();
                }

                //If the current attack pattern is attack pattern 1
                if (currentAttackPattern == ATK_PTN_1)
                {
                    //Call attack pattern 1
                    EnemyAttackPatternOne();
                }

                //Else if the current attack pattern is attack pattern 2
                else if (currentAttackPattern == ATK_PTN_2)
                {
                    //Call attack pattern 2
                    EnemyAttackPatternTwo();
                }

                //Else if the current attack pattern is attack pattern 3
                else if (currentAttackPattern == ATK_PTN_3)
                {
                    //Call attack pattern 3
                    EnemyAttackPatternThree();
                }

                //Else if the current attack pattern is attack pattern 4
                else if (currentAttackPattern == ATK_PTN_4)
                {
                    //Call attack pattern 5
                    EnemyAttackPatternFive();
                }

                //Else if the current attack pattern is attack pattern 6
                else if (currentAttackPattern == ATK_PTN_6)
                {
                    //Call attack pattern 6
                    EnemyAttackPatternSix();
                }
                //Else if the current attack pattern is attack pattern 7
                else if (currentAttackPattern == ATK_PTN_7)
                {
                    //Call attack pattern 7
                    EnemyAttackPatternSeven();
                }
                //Else if the current attack pattern is attack pattern 8
                else if (currentAttackPattern == ATK_PTN_8)
                {
                    //Call attack pattern 8
                    EnemyAttackPatternEight();
                }

                //Else if the current attack pattern is attack pattern 9
                else if (currentAttackPattern == ATK_PTN_9)
                {
                    //Call attack pattern 8
                    EnemyAttackPatternNine();
                }
                //Else the current attack pattern is attack pattern 10
                else
                {
                    //Call attack pattern 10
                    EnemyAttackPatternTen();
                }
            }
        }

        //Trevor Kollins
        //Function which runs when a new attack starts, returns the value which determines the next attack
        int NewAttack()
        {
            //resets frameCoutner to 0
            frameCounter = 0;
            //Set isEnemyAttacking to true
            isEnemyAttacking = true;
            //returns a random number between the value for attack pattern 1, and the number of attack patterns, this will select the new attack
            return numberGenerator.Next(ATK_PTN_1, NUMBER_OF_ATTACK_PATTERNS);
        }

        //Trevor Kollins
        //Plays Music and sound
        void Music(int sound)
        {
            //If the sound being played is the background music for the battle
            if (sound == BATTLE_BACKGROUND_MUSIC)
            {
                //play the background music for the battle
                backgroundMusic.URL = "Resources\\Sakuya's Theme - Flowering Night.mp3";
            }

            //Else the music is cirno's theme (start and end theme)
            else
            {
                //Play the start and end them
                backgroundMusic.URL = "Resources\\Cirnos Theme - Beloved Tomboyish Daughter.mp3";
            }
        }

        //Trevor Kollins
        //End game subprogram, sets up end game settings
        void SetUpEndGame()
        {
            //Show the title label
            lblTitle.Visible = true;

            //Show the end game label
            lblEndGameText.Visible = true;

            //Play the end background music
            Music(START_AND_END_BACKGROUND_MUSIC);

            //If the player won
            if (playerWin == true)
            {
                //If the player won, write the winning version of the end game text
                lblEndGameText.Text = "Congratulations! You Have Defeated Sakuya\r\n And Got Little Hsiung-Hsiung To Give\r\nEverybody 100's!\r\n\r\nPress R to Play Again! Press Esc to Close.";
            }

            //Else the player lost
            else
            {
                //If the player lost, write the winning version of the end game text
                lblEndGameText.Text = "Dang It! You Lost! Little Hsiung-Hsiung Got\r\n Away With His Horrible Plans!\r\n\r\nPress R to Play Again! Press Esc to Close.";
            }
        }

        //When form is closing
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Set the gamestate to end
            gameState = END;
        }

        //Michelle Kee
        //Function subprogram to check if enemy should attack 
        Boolean ShouldEnemyAttack(int delay)
        {
            //If the frame counter reaches the delay frame time
            if (frameCounter == delay)
            {
                //The frame counter will be reset to zero
                frameCounter = 0;
                //The variable will become true, enabling the enemy to attack
                return true;
            }
            //Otherwise
            else
            {
                //The variable will be false, and the enemy cannot attack
                return false;
            }
        }

        //Trevor Kollins
        //Enemy Pattern 1
        //adds 8 projectile which rotate around the enemy and shoot straight projectiles
        void EnemyAttackPatternOne()
        {
            //Set the enemy travel destination to the centre
            travelDestination = enemyLocationCentre;

            //If the enemy is at the travel destination
            if (enemyLocation == travelDestination)
            {
                //If the elapsed frames are 0 (the attack pattern just started)
                if (elapsedFrames == 0)
                {
                    //Create 8 small blue circular projectiles, one in each direction from the enemy (right, upright, up, upleft, left, downleft, down, downright)
                    AddCircularProjectile(0, smallCircProjectileSize, BLUE);
                    AddCircularProjectile(45, smallCircProjectileSize, BLUE);
                    AddCircularProjectile(90, smallCircProjectileSize, BLUE);
                    AddCircularProjectile(135, smallCircProjectileSize, BLUE);
                    AddCircularProjectile(180, smallCircProjectileSize, BLUE);
                    AddCircularProjectile(225, smallCircProjectileSize, BLUE);
                    AddCircularProjectile(270, smallCircProjectileSize, BLUE);
                    AddCircularProjectile(315, smallCircProjectileSize, BLUE);
                }

                bool enemyAttack = ShouldEnemyAttack(ATTACK_PATTERN_1_DELAY);

                //Loop once for each projectile
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    //If the projectile is has a circular motion type
                    if (projectileMovementType[i] == CIRCULAR_PROJECTILE)
                    {
                        //Increase the angle of the projectile by 1
                        projectileAngle[i]++;
                        //Recaculate the new position based on the angle
                        //Calculate the x location of the projectile using the circularXLocation subprogram
                        projectileLocation[i].X = circularXLocation(projectileAngle[i], SMALL_CIRC_RADIUS);
                        //Calculate the y location of the projectile using the circularYLocation subprogram
                        projectileLocation[i].Y = circularYLocation(projectileAngle[i], SMALL_CIRC_RADIUS);
                        //update the new location in the boundary
                        projectileBoundary[i].Location = projectileLocation[i];

                        //If 5 frames has passed since the last set of projectile shots
                        if (enemyAttack == true)
                        {
                            //Shoot a moderate speed. blue projectile, 90 degrees more than the circular projectile's current position in relation to the enemy on the cartinal plane.
                            AddStraightProjectile(projectileLocation[i], ENEMY_PROJECTILE, projectileAngle[i] + ATK_PTN_1_ANGLE_INCREASE, MODERATE_PROJECTILE, enemyThinProjectileSize, BLUE);
                        }
                    }
                }

                //Increase frameCounter by 1
                frameCounter++;

                //Increase the amount of elapsed frames since the attack pattern started
                elapsedFrames++;

                //If the elapsed frames is over 100
                if (elapsedFrames >= 100)
                {
                    //loop once for each projectile
                    for (int i = numberOfProjectiles - 1; i >= 0; i--)
                    {
                        //If the projectile stored in index i is circular
                        if (projectileMovementType[i] == CIRCULAR_PROJECTILE)
                        {
                            //Remove it
                            RemoveProjectile(i);
                        }
                    }

                    //Set elapsed frames back to 0
                    elapsedFrames = 0;

                    //Set is enemy attacking to false
                    isEnemyAttacking = false;
                }
            }
        }

        //Michelle Kee
        //A subprogram for another enemy attack pattern
        //The enemy shoots in 6 directions and spins back and forth
        void EnemyAttackPatternTwo()
        {
            //If the enemy is done travelling its designed destination, it will travel to another point, after the pattern runs half the time (this keeps the enemy moving)
            if (enemyLocation == travelDestination && patternTwoCounter > PATTERN_TWO_RUN_TIME / 2)
            {
                //Travel to another random point
                RandomEnemyLocation();
            }
            //If the frames counter has reached the desired delay time
            if (ShouldEnemyAttack(PATTERN_TWO_DELAY) == true)
            {
                //A loop that runs 6 times
                for (int times = 0; times < 6; times++)
                {
                    //Adds a new straight projectile at the enemy location, owned by the enemy
                    //Angle is the current increment (patternTwoAngleIncrement) mutiplied by the constant angle incement (PATTERN_TWO_PROJECTILE_ANGLE_INCREMENT) plus the # of times the loop has run multiplied by the spaces between each projectile being fired at once(PATTERN_TWO_ANGLE_INCREMENT)
                    //Speed is fast, projectile is thin, color is blue.
                    AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, (patternTwoAngleIncrement * PATTERN_TWO_PROJECTILE_ANGLE_INCREMENT) + (times * PATTERN_TWO_ANGLE_INCREMENT), FAST_PROJECTILE, enemyThinProjectileSize, BLUE);
                }
                //If pattern two increment is true
                if (patternTwoIncrement == true)
                {
                    //Increases the increment variable
                    patternTwoAngleIncrement++;
                    //If the number is maximum number, the boolean will turn false and the incremnet will decrease (decrement)
                    if (patternTwoAngleIncrement == PATTERN_TWO_MAX_ANGLE_INCR)
                    {
                        //bool becomes false
                        patternTwoIncrement = false;
                    }
                }
                //Otherwise, if pattern two increment is false
                else
                {
                    //Decreases the increment variable
                    patternTwoAngleIncrement--;
                    //If the number reaches the smallest number , the boolean will turn false and the increment will decrease causing a decrement
                    if (patternTwoAngleIncrement == PATTERN_TWO_MIN_ANGLE_INCR)
                    {
                        //bool beocomes true
                        patternTwoIncrement = true;

                        //Counter to check how many times the pattern has run (through part 1 and 2)
                        patternTwoCounter++;

                        //The enemy will travel have a new random point to travel to when the pattern runs for half of the time its supposed to
                        if (patternTwoCounter == PATTERN_TWO_RUN_TIME / 2)
                        {
                            RandomEnemyLocation();
                        }
                        //If this pattern has run 10 times,
                        else if (patternTwoCounter == PATTERN_TWO_RUN_TIME)
                        {
                            //Set is enemy attacking to false
                            isEnemyAttacking = false;
                            //Resets the pattern two counter
                            patternTwoCounter = 0;
                            //Resets increment variable
                            patternTwoAngleIncrement = 0;
                        }
                    }
                }

            }
            //Adds to the frame counter
            frameCounter++;
        }

        //Trevor Kollins
        //enemy attack pattern 3
        //Shoot fast projectiles at 3 angles, and slow projectiles at randomly selected angles
        void EnemyAttackPatternThree()
        {
            //Increase frameCounter by 1
            frameCounter++;

            //If there is not currently a phase in progress
            if (phase == NO_PHASE)
            {
                //Set the attack to phase 1
                phase = PHASE_1;
                //Set the travel to a random enemy location
                RandomEnemyLocation();
                //Set cycles to 0
                cycles = 0;
            }

            //Else if the phase is currently phase 1
            else if (phase == PHASE_1)
            {
                //If the enemy is at the travel destination
                if (enemyLocation == travelDestination)
                {
                    //If the frame counter is greater than or equal to 4 frames
                    if (frameCounter >= 4)
                    {
                        //Set the frame counter back to 0
                        frameCounter = 0;

                        //Create a double for the angle the projectile will be shot at
                        double angle = 180;
                        //While the angle is less than or equal to 360
                        while (angle <= 360)
                        {
                            //Add a straight red, thin, enemy projectile at angle, originating from the enemy
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, angle, FAST_PROJECTILE, enemyThinProjectileSize, RED);
                            //Increase angle by 10
                            angle += 10;
                        }

                        //loop for 5 times
                        for (int i = 0; i < 5; i++)
                        {
                            //Set angle to 247.5 + a random number between -20 and 20
                            angle = 247.5 + numberGenerator.Next(-20, 20 + 1);

                            //Shoot a slow, straight, blue enemy projectile at that angle
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, angle, MODERATE_PROJECTILE, verySmallCircProjectileSize, BLUE);

                            //Set angle to 292.5 + a random number between -20 and 20
                            angle = 292.5 + numberGenerator.Next(-20, 20 + 1);
                            //Shoot a slow, straight, blue enemy projectile at that angle
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, angle, MODERATE_PROJECTILE, verySmallCircProjectileSize, BLUE);
                        }
                        //Increase cycles by 1
                        cycles++;

                        //If enemy has attacked 8, 16 or 24 times
                        if (cycles == 8 || cycles == 16 || cycles == 24)
                        {
                            //Set the enemy to a random location
                            RandomEnemyLocation();
                        }

                        //else if the enemy has attacked 32 times
                        else if (cycles == 32)
                        {
                            //Set cycles back to 0
                            cycles = 0;
                            //Set phase back to no pase
                            phase = NO_PHASE;
                            //Set isEnemyAttacking to false
                            isEnemyAttacking = false;
                        }
                    }
                }
            }
        }

        //Michelle Kee
        //Subprogram for an enemy pattern, that shoots at alternating angles.
        void EnemyAttackPatternFour()
        {
            //If the enemy should attack (the delay time is reached)
            if (ShouldEnemyAttack(PATTERN_FOUR_DELAY) == true)
            {
                //If the first part of this pattern has not been fired yet
                if (patternFourPartOne == false)
                {
                    //A loop that runs through every 20th angle starting from 0
                    for (int angle = 0; angle <= 360; angle += PATTERN_FOUR_ANGLE_INCREMENT)
                    {
                        //Adds straight projectile running at those angles
                        //Projectile starts at enemy location, is owned by enemy, the speed is moderate, the projectile is thin, color is green
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, angle, MODERATE_PROJECTILE, enemyThinProjectileSize, GREEN);

                    }
                    //Sets the part one variable to true so part two will run
                    patternFourPartOne = true;
                }
                //If the first part of the pattern has been completed, the second part will be executed
                else
                {
                    //A loop that runs through every 20th angle starting from 10
                    for (int a = 10; a <= 360; a += PATTERN_FOUR_ANGLE_INCREMENT)
                    {
                        //Adds straight projectile running at those angles
                        //Projectile starts at enemy location, is owned by enemy, the speed is moderate, the projectile is thin, color is green
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, a, MODERATE_PROJECTILE, enemyThinProjectileSize, GREEN);
                    }
                    //Sets the part one variable to false so part one will run again the next time.
                    patternFourPartOne = false;
                    //Adds to the counter of how many times pattern four has run (counts every time 2 sets of projectiles have been fired, alternating) 
                    patternFourCounter++;
                    //If pattern four ran 10 times
                    if (patternFourCounter == PATTERN_FOUR_RUN_TIME)
                    {
                        //pattern will end
                        //Set is enemy attacking to false
                        isEnemyAttacking = false;
                    }
                }
            }

            //Adds to the frames counter variable
            frameCounter++;
        }

        //Trevor Kollins
        //Enemy Attack pattern 5
        //Travel to the top left, top and right and shoot a barage of projectiles
        void EnemyAttackPatternFive()
        {
            //If there is not currently a phase in progress
            if (phase == NO_PHASE)
            {
                //Set the attack to phase 1
                phase = PHASE_1;
                //Set the travel destination to the top left corner
                travelDestination = enemyLocationTopLeft;
            }

            //else if the attack is in phase 1
            else if (phase == PHASE_1)
            {
                //If the enemy is at the travel destination
                if (enemyLocation == travelDestination)
                {
                    //If frame counter is less than or equal to 6
                    if (frameCounter <= 6)
                    {
                        //Shoot straight projectiles every 5 degrees at angles 270-360 (use a for loop to loop straight projectiles)
                        for (int angle = 270; angle <= 360; angle += 5)
                        {
                            //Add a straight blue very fast thin enemy projectile at the current angle
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, angle, VERY_FAST_PROJECTILE, enemyThinProjectileSize, BLUE);
                        }

                        //Increase framecounter
                        frameCounter++;
                    }

                    //else frame counter is 7 or higher
                    else
                    {
                        //Set frame counter to 0
                        frameCounter = 0;
                        //Set to phase 2
                        phase = PHASE_2;
                        //set travel location to top 
                        travelDestination = enemyLocationTop;
                    }
                }
            }

            //else if the attack is in phase 2
            else if (phase == PHASE_2)
            {
                //If the enemy is at the travel destination
                if (enemyLocation == travelDestination)
                {
                    //If frame counter is less than or equal to 6
                    if (frameCounter <= 6)
                    {
                        //Shoot straight projectiles every 5 degrees at angles 225-315 (use a for loop to loop straight projectiles)
                        for (int angle = 225; angle <= 315; angle += 5)
                        {
                            //Add a straight blue very fast thin enemy projectile at the current angle
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, angle, VERY_FAST_PROJECTILE, enemyThinProjectileSize, BLUE);
                        }

                        //Increase framecounter
                        frameCounter++;
                    }

                    //else frame counter is 7 or higher
                    else
                    {
                        //Set frame counter to 0
                        frameCounter = 0;
                        //Set to phase 3
                        phase = PHASE_3;
                        //set travel location to top right
                        travelDestination = enemyLocationTopRight;
                    }
                }
            }

            //else attack is in phase 3
            else
            {
                //If the enemy is at the travel destination
                if (enemyLocation == travelDestination)
                {
                    //If frame counter is less than or equal to 6
                    if (frameCounter <= 6)
                    {
                        //Shoot straight projectiles every 5 degrees at angles 180-270 (use a for loop to loop straight projectiles)
                        for (int angle = 180; angle <= 270; angle += 5)
                        {
                            //Add a straight blue very fast thin enemy projectile at the current angle
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, angle, VERY_FAST_PROJECTILE, enemyThinProjectileSize, BLUE);
                        }

                        //Increase framecounter
                        frameCounter++;
                    }

                    //else frame counter is 7 or higher
                    else
                    {
                        //Set to no phase
                        phase = NO_PHASE;
                        //set travel location to centre
                        travelDestination = enemyLocationCentre;
                        //Set is enemy attacking to false
                        isEnemyAttacking = false;
                    }
                }
            }
        }

        //Trevor Kollins
        //Enemy Attack Pattern 6
        //Travel to random location and then alternate shooting projectiles from the left and right
        void EnemyAttackPatternSix()
        {
            //If the attack is not currently in any phase
            if (phase == NO_PHASE)
            {
                //Set to phase 1
                phase = PHASE_1;
                //Generate a random location to travel to
                RandomEnemyLocation();
                //Set the original angle of attack to 230
                attackPattern6Angle = 230;
                //Set the attack to the left side
                attackPattern6Direction = ATTACK_LEFT;
                //Set cycles (number of cycles enemy has attacked to 0)
                cycles = 0;
            }

            //else if the attack is in phase 1
            else if (phase == PHASE_1)
            {
                //If the enemy's location equals the travel destination
                if (enemyLocation == travelDestination)
                {
                    //If enemy has attacked 6 times (cycles is 6)
                    if (cycles == 6)
                    {
                        //Generate a random location
                        RandomEnemyLocation();
                        //increase cycles by 1
                        cycles++;
                    }

                    //else if cycles is 13
                    else if (cycles == 13)
                    {
                        //Generate a random location
                        RandomEnemyLocation();
                        //increase cycles by 1
                        cycles++;
                    }

                    //else if cycles is 20
                    else if (cycles == 20)
                    {
                        //Set is enemy attacking to false
                        isEnemyAttacking = false;
                        //Set phase to no phase
                        phase = NO_PHASE;
                    }

                    //Else cycles is a number that is not 6, 13 or 20
                    else
                    {
                        //If the direction of attack is left
                        if (attackPattern6Direction == ATTACK_LEFT)
                        {
                            //Add a straight red fast thin enemy projectile at the angle given
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, attackPattern6Angle, FAST_PROJECTILE, smallCircProjectileSize, RED);
                            //Increase the angle by 10
                            attackPattern6Angle += 10;

                            //if the angle is above or equals 350
                            if (attackPattern6Angle >= 350)
                            {
                                //Set the direction of attack to right
                                attackPattern6Direction = ATTACK_RIGHT;
                                //Increase cycles by 1
                                cycles++;
                                //Set the angle of attack to 310
                                attackPattern6Angle = 310;
                            }
                        }

                        //else the direction of attack is right
                        else
                        {
                            //Add a straight fast thin green enemy projectile at the angle given
                            AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, attackPattern6Angle, MODERATE_PROJECTILE, smallCircProjectileSize, GREEN);
                            //Dncrease the angle by 10
                            attackPattern6Angle -= 10;

                            //if the angle is less than or equals 190
                            if (attackPattern6Angle <= 190)
                            {
                                //Set the direction of attack to right
                                attackPattern6Direction = ATTACK_LEFT;
                                //Increase cycles by 1
                                cycles++;
                                //Set the angle of attack to 230
                                attackPattern6Angle = 230;
                            }
                        }
                    }
                }
            }
        }

        //Trevor Kollins
        //Enemy attack pattern 7
        //have a large circular projectile shoot many straight projectiles in all directions
        void EnemyAttackPatternSeven()
        {
            //Set the enemy travel destination to the top
            travelDestination = enemyLocationTop;

            //If the enemy is at the travel destination
            if (travelDestination == enemyLocation)
            {
                //If the elapsed frames are 0 (the attack pattern just started)
                if (elapsedFrames == 0)
                {
                    //Add a large circular projectile 270 degrees in relation to the enemy
                    AddCircularProjectile(270, largeCircProjectileSize, BLUE);
                }

                //Loop once for each projectile
                for (int i = 0; i < numberOfProjectiles; i++)
                {
                    //If the projectile is hsa a circular motion
                    if (projectileMovementType[i] == CIRCULAR_PROJECTILE)
                    {
                        //Increase the angle of the projectile by 3
                        projectileAngle[i] += 10;
                        //Recaculate the new position based on the angle
                        //Calculate the x location of the projectile using the circularXLocation subprogram
                        projectileLocation[i].X = circularXLocation(projectileAngle[i], LARGE_CIRC_RADIUS);
                        //Calculate the y location of the projectile using the circularYLocation subprogram
                        projectileLocation[i].Y = circularYLocation(projectileAngle[i], LARGE_CIRC_RADIUS);
                        //update the new location in the boundary
                        projectileBoundary[i].Location = projectileLocation[i];

                        //If 3 frames has passed since the last set of projectile shots
                        if (ShouldEnemyAttack(ATTACK_PATTERN_7_DELAY) == true)
                        {
                            //Create a variable to store the angle the projectile will be shot at
                            int angle = 0;

                            //While the angle is less than 360
                            while (angle < 360)
                            {
                                //Add a straight red fast projectile at angle, originating from the circular projectile (centre, so the location = the radius in each direction)
                                AddStraightProjectile(new PointF(projectileLocation[i].X + LARGE_CIRC_RADIUS, projectileLocation[i].Y + LARGE_CIRC_RADIUS), ENEMY_PROJECTILE, angle, FAST_PROJECTILE, enemyThinProjectileSize, RED);
                                //Increase angle by 5
                                angle += 5;
                            }
                        }
                    }
                }

                //Increase frame counter by 1
                frameCounter++;

                //Increase the amount of elapsed frames since the attack pattern started
                elapsedFrames++;

                //If the elapsed frames is over 100
                if (elapsedFrames >= 100)
                {
                    //loop once for each projectile
                    for (int i = numberOfProjectiles - 1; i >= 0; i--)
                    {
                        //If the projectile stored in index i is circular
                        if (projectileMovementType[i] == CIRCULAR_PROJECTILE)
                        {
                            //Remove it
                            RemoveProjectile(i);
                        }
                    }

                    //Set elapsed frames back to 0
                    elapsedFrames = 0;
                    //Set is enemy attacking to false
                    isEnemyAttacking = false;
                }
            }
        }

        //Michelle Kee
        //A subprogram for another enemy attack pattern
        //The enemy shoots projectiles in a flower pattern
        void EnemyAttackPatternEight()
        {
            //If the enemy should attack (the delay time is reached) and the enemy is done travelling to where it wants to go
            if (ShouldEnemyAttack(PATTERN_EIGHT_DELAY) == true && enemyLocation == travelDestination)
            {
                //If the increment of the first ring (ring A) of petal is less than five.
                if (patternEightAIncrement < 5)
                {
                    //A loop that runs 8 times (For each petal)
                    for (int times = 0; times < 8; times++)
                    {
                        //Adds a straight projectile firing from the enemy location, projectile is owned by enemy, 
                        //angle is the current increment (patternEightAIncrement) multiplied by the constant angle increment (PATTERN_EIGHT_PROJECTILES_ANGLE_INCRMENET) plus the times the loop has run multiplied by the spaces between the 8 projectiles being fired (PATTERN_EIGHT_PETAL_ANGLE_INCREMENT)
                        //speed is the current increment (patternEightAIncrement) multiplied by the constant speed increment (PATTERN_EIGHT_SPEED_INCREMENT) plus the starting speed (PATTERN_EIGHT_STARTING_SPEED)
                        //size is thin, color is red
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, (patternEightAIncrement * PATTERN_EIGHT_PROJECTILES_ANGLE_INCRMENET) + (times * PATTERN_EIGHT_PETAL_ANGLE_INCREMENT), (patternEightAIncrement * PATTERN_EIGHT_SPEED_INCREMENT) + PATTERN_EIGHT_STARTING_SPEED, enemyThinProjectileSize, RED);
                    }
                    //Increases the increment counter for projectiles set A
                    patternEightAIncrement++;
                }

                //If the first ring is at least little more than halfway done, and ring B is not complete, then ring B will start
                if (patternEightAIncrement >= PATTERN_EIGHT_INCREMENT_CHECKPOINT && patternEightBIncrement < 5)
                {
                    //A loop that runs 8 times (For each petal)
                    for (int times = 0; times < 8; times++)
                    {
                        //Adds a straight projectile firing from the enemy location, projectile is owned by enemy,
                        //angle and speed are calculated similarly but uses patternEightBIncrement instead
                        //size is thin, color is blue
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, (patternEightBIncrement * PATTERN_EIGHT_PROJECTILES_ANGLE_INCRMENET) + (times * PATTERN_EIGHT_PETAL_ANGLE_INCREMENT), (patternEightBIncrement * PATTERN_EIGHT_SPEED_INCREMENT) + PATTERN_EIGHT_STARTING_SPEED, enemyThinProjectileSize, BLUE);
                    }
                    //Increases the increment counter for projectiles set B 
                    patternEightBIncrement++;
                }
                //If the second ring (ring B) is at least little more than halfway done, and ring c is not complete, then ring C will start
                if (patternEightBIncrement >= PATTERN_EIGHT_INCREMENT_CHECKPOINT && patternEightCIncrement < 5)
                {
                    //A loop that runs 8 times (For each petal)
                    for (int times = 0; times < 8; times++)
                    {
                        //Adds a straight projectile firing from the enemy location, projectile is owned by enemy,
                        //angle and speed are calculated similarly but uses patternEightCIncrement instead
                        //size is thin, color is green
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, (patternEightCIncrement * PATTERN_EIGHT_PROJECTILES_ANGLE_INCRMENET) + (times * PATTERN_EIGHT_PETAL_ANGLE_INCREMENT), (patternEightCIncrement * PATTERN_EIGHT_SPEED_INCREMENT) + PATTERN_EIGHT_STARTING_SPEED, enemyThinProjectileSize, GREEN);
                    }
                    //Increases the increment counter for projectiles set C
                    patternEightCIncrement++;
                }
                //If the third ring is about done, and fourth ring is incomplete
                if (patternEightCIncrement >= PATTERN_EIGHT_INCREMENT_CHECKPOINT && patternEightDIncrement < 5)
                {
                    //A loop that runs 8 times (For each petal)
                    for (int times = 0; times < 8; times++)
                    {
                        //Adds a straight projectile firing from the enemy location, projectile is owned by enemy,
                        //angle and speed are calculated similarly but uses patternEightDIncrement instead
                        //size is thin, color is blue
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, (patternEightDIncrement * PATTERN_EIGHT_PROJECTILES_ANGLE_INCRMENET) + (times * PATTERN_EIGHT_PETAL_ANGLE_INCREMENT), (patternEightDIncrement * PATTERN_EIGHT_SPEED_INCREMENT) + PATTERN_EIGHT_STARTING_SPEED, enemyThinProjectileSize, BLUE);
                    }
                    //Increases the increment counter for projectiles set D
                    patternEightDIncrement++;
                }
                //If the fourth ring is about done, and fith ring is incomplete
                if (patternEightDIncrement >= PATTERN_EIGHT_INCREMENT_CHECKPOINT && patternEightEIncrement < 5)
                {
                    //A loop that runs 8 times (For each petal)
                    for (int times = 0; times < 8; times++)
                    {
                        //Adds a straight projectile firing from the enemy location, projectile is owned by enemy,
                        //angle and speed are calculated similarly but uses patternEightEIncrement instead
                        //size is thin, color is red
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, (patternEightEIncrement * PATTERN_EIGHT_PROJECTILES_ANGLE_INCRMENET) + (times * PATTERN_EIGHT_PETAL_ANGLE_INCREMENT), (patternEightEIncrement * PATTERN_EIGHT_SPEED_INCREMENT) + PATTERN_EIGHT_STARTING_SPEED, enemyThinProjectileSize, RED);
                    }
                    //Increases the increment counter for projectiles set D
                    patternEightEIncrement++;
                }
                //If the pattern is finished, 
                if (patternEightEIncrement == 5)
                {
                    //Adds to the counter of how many times the pattern has run
                    patternEightCounter++;
                    //Reset variables in this pattern
                    patternEightAIncrement = 0;
                    patternEightBIncrement = 0;
                    patternEightCIncrement = 0;
                    patternEightDIncrement = 0;
                    patternEightEIncrement = 0;
                    //Make the enemy go to a new location
                    RandomEnemyLocation();
                    //If the pattern ran 3 times, this pattern will stop
                    if (patternEightCounter == PATTERN_EIGHT_MAX_RUN_TIME)
                    {
                        //Set is enemy attacking to false
                        isEnemyAttacking = false;
                        //Reset pattern eight counter
                        patternEightCounter = 0;
                    }
                }
            }
            //Adds to the frame counter
            frameCounter++;
        }

        //Michelle Kee
        //Subprogram for another enemy attack pattern
        //Enemy shoots lines of projectiles in a X pattern that spins
        void EnemyAttackPatternNine()
        {
            //If the enemy has reached it's destination
            if (enemyLocation == travelDestination)
            {
                //Adds to the counter every time a projectile will be fired
                patternNineProjectiles++;
                //If the counter does not exceed 4 projectiles
                if (patternNineProjectiles <= PATTERN_NINE_PROJECTILE_NUMBER)
                {
                    //Creates 4 projectile, starting at that angle, adding 90 degrees each time
                    //Runs four times
                    for (int i = 0; i <= 4; i++)
                    {
                        //Creates a straight projectile at the enemy location, owned my the enemy
                        //Angle is the current angle plus the angle increment
                        //Speed is fast, projectile is thin, color is blue
                        AddStraightProjectile(enemyLocation, ENEMY_PROJECTILE, patternNineAngle + patternNineAngleIncrement, FAST_PROJECTILE, enemyThinProjectileSize, BLUE);
                        //Increases the angle for the next projectile (90 degrees)
                        patternNineAngleIncrement += PATTERN_NINE_ANGLE_SPACE;
                    }
                }
                //Otherwise - (if 4 projectiles are already fired in that angle)
                else
                {
                    //The angle will increase for the next set of projectiles
                    patternNineAngle += PATTERN_NINE_ANGLE_INCREMENT;
                    //The counter will reset to count the next set of projectiles 
                    patternNineProjectiles = 0;
                    //Increases counter to keep track of how much it has run ( counts every time a set (4) of projectiles have been fired)
                    patternNineCounter++;
                }
                //If 10 sets have been fired, the pattern will stop
                if (patternNineCounter == PATTERN_NINE_RUN_TIME)
                {
                    //Set is enemy attacking to false
                    isEnemyAttacking = false;
                    //Reset pattern variables
                    patternNineAngle = 0;
                }
            }
        }

        //Michelle Kee
        //Subprogram for enemy attack
        //The enemy moves around with 2 circular projectiles firing downwards
        void EnemyAttackPatternTen()
        {
            //If the enemy arrived its travel destination, generate another random location for the enemy to travel 
            if (enemyLocation == travelDestination)
            {
                //generate another random location for the enemy to travel to
                RandomEnemyLocation();
            }
            //If circular projectiles have not been added (patternTenCircularProjectiles is false)
            if (patternTenCircularProjectiles == false)
            {
                //A for loop that runs 4 times -  from 0 - 360, every 90 degrees
                for (int a = 0; a < 360; a += 90)
                {
                    //Adds a projectile around the enemy, at the angle from the for loop
                    AddCircularProjectile(a, smallCircProjectileSize, GREEN);
                }
                //the variable for adding circular projectiles will become true
                patternTenCircularProjectiles = true;
            }
            //Determines if the enemy should attack using delay (3 frames)
            bool attack = ShouldEnemyAttack(PATTERN_TEN_DELAY);


            //A for loop that loops through all the projectile indexes
            for (int i = 0; i < numberOfProjectiles; i++)
            {
                //If the projectile is a circular projectile
                if (projectileMovementType[i] == CIRCULAR_PROJECTILE)
                {
                    //Increase the angle of the projectile
                    projectileAngle[i]++;
                    //Update the X and Y location of the projectile
                    projectileLocation[i].X = circularXLocation(projectileAngle[i], SMALL_CIRC_RADIUS);
                    projectileLocation[i].Y = circularYLocation(projectileAngle[i], SMALL_CIRC_RADIUS);
                    //Updates the boundary of the projectile with the new x and y location
                    projectileBoundary[i].Location = new PointF(projectileLocation[i].X, projectileLocation[i].Y);

                    //If the enemy should attack (3 frames delay is reached)
                    if (attack == true)
                    {
                        //A for loop that starts at angle 210, adding 10 each time until 330 is reached
                        for (int a = 220; a <= 310; a += 40)
                        {
                            //Adds straight projectile at the angles, firing from the circular projectiles 
                            AddStraightProjectile(projectileLocation[i], ENEMY_PROJECTILE, a, VERY_FAST_PROJECTILE, enemyThinProjectileSize, RED);
                        }
                    }
                }
            }
            //If the enemy attacked
            if (attack == true)
            {
                //Adds to the run time counter
                patternTenRunTimeCounter++;
            }
            //Increases the frame counter
            frameCounter++;

            //If the enemy shot 25 times
            if (patternTenRunTimeCounter == PATTERN_TEN_RUN_TIME)
            {
                //Set patternTenRunTimeCounter to 0
                patternTenRunTimeCounter = 0;
                //patternTenCircularProjectiles will be set to false 
                patternTenCircularProjectiles = false;
                //Is enemy attack will be swiched to false
                isEnemyAttacking = false;

                //A for loop that loops through all the projectile indexes
                for (int i = numberOfProjectiles - 1; i >= 0; i--)
                {
                    //If the projectile is a circular projectile
                    if (projectileMovementType[i] == CIRCULAR_PROJECTILE)
                    {
                        //Remove the projectile
                        RemoveProjectile(i);
                    }
                }
            }
        }
    }
}
