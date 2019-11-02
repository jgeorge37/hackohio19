using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Sprint_4.Factories;
using Sprint_4.States;
using Sprint_4.States.Action_States;
using Sprint_4.States.PowerupStates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Sprint_4.Models.BlockModels;
using Sprint_4.Models;
using System.Diagnostics;
using Sprint_4.Collision;

namespace HackGame.Sprites
{
    class MarioModel : ISprite, ICollidable
    {
        public bool leftFacing;
        public bool canMoveDown;
        public bool support;
        public bool gravityAllowed;
        public int horizontalSpeed;
        public int jumpSpeed;
        public int heightJumped;
        public Texture2D texture;
        public Vector2 position;
        public IActionState actionState = IdlingState.Instance;
        public IPowerupState powerupState;
        private MarioSpriteBoxFactory spriteFactory;
        //private IActionState prevState;
        public Vector2 Velocity;
        public Vector2 Acceleration;
        public MarioSprite Sprite;
        public double MarioHeightFactor;
        private int invincibleFrames = 0;
        public bool FeatureOn = false;
        public Vector2 Scale = new Vector2(1.5f, 1.5f);
        public MarioModel(MarioSpriteBoxFactory spriteFactory, Vector2 coordinates, Vector2 velocity, IPowerupState powerupState)
        {
            leftFacing = false;
            canMoveDown = false;
            support = false;
            gravityAllowed = false;
            horizontalSpeed = 70;
            jumpSpeed = 50;
            heightJumped = 0;
            this.position = coordinates;
            this.powerupState = powerupState;
            this.spriteFactory = spriteFactory;
            Sprite = new MarioSprite(this);
            Velocity = velocity;
            Acceleration = new Vector2(0, 0);
            MarioHeightFactor = 1;
        }

        public void Draw(SpriteBatch s)
        {
            Sprite.Draw(s);

        }
        public void Update(GameTime gameTime)
        {
            Velocity += Acceleration * (float)gameTime.ElapsedGameTime.TotalSeconds;
            position += Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //handle illegal state where mario is standard and crouching -- can arise when mario takes damage while crouching
            if (powerupState is StandardState && actionState is CrouchingState)
            {
                this.actionState = IdlingState.Instance;
            }

            this.texture = spriteFactory.GetSprite(this.actionState, this.powerupState, gameTime);

            //changes bounding box if mario is crouching, since the sprite size doesn't change
            MarioHeightFactor = 1;
            if (actionState is CrouchingState)
            {
                MarioHeightFactor = 0.75;
            }
            else if (powerupState is StandardState || powerupState is DeadState)
            {
                MarioHeightFactor = 0.5;
            }
            //Update and send Max Velocity
            UpdateMaxVelocity(80f);
            //check for max height
            if (this.Velocity.Y == -jumpSpeed)
            {
                //Debug.WriteLine("the Update code is being run at all");
                UpdateJump(false);
            }
            //Debug.WriteLine("Mario has support");
            //support is updated when: mario moves left or right, mario stops jumping, mario collides horrizontally 
            if (IsMoving())
            {
                Gravity(support);
            }
        }
        public void UpdateMaxVelocity(float maxVelocity)
        {
            //Set Maximum velocity
            if (Velocity.X >= maxVelocity)
            {
                Velocity.X = maxVelocity;
            }
            else if (Velocity.X <= -maxVelocity)
            {
                Velocity.X = -maxVelocity;
            }

            if (Velocity.Y >= maxVelocity)
            {
                Velocity.Y = maxVelocity;
            }
            else if (Velocity.Y <= -maxVelocity)
            {
                Velocity.Y = -maxVelocity;
            }
        }
        public Rectangle BoundingBox => new Rectangle((int)this.position.X, (int)this.position.Y, (int)(texture.Width * Scale.X), (int)((texture.Height * Scale.Y) * MarioHeightFactor));

        public void CollisionResponse(ICollidable collidable)
        {
            Rectangle collidingBox = collidable.BoundingBox;
            Rectangle spriteBox = this.BoundingBox;
            if (collidable is BlockModel || collidable is InvisibleWall)
            {
                CollideWithBlock(collidable, collidingBox, spriteBox);
            }
            if (collidable is ItemModel)
            {
                CollideWithItem(collidable);
            }
            //if(collidable is InvisibleWall)
            if (collidable is GoombaEnemyModel || collidable is TurtleEnemyModel)
            {
                EnemyModel enemy = (EnemyModel)collidable;
                //Vertical collision with mario above enemy
                if ((spriteBox.X + spriteBox.Width) >= collidingBox.X && (spriteBox.Y + spriteBox.Height) >= (collidingBox.Y) && (spriteBox.X + spriteBox.Width - collidingBox.X) > (spriteBox.Y + spriteBox.Height - collidingBox.Y) && (collidingBox.X + collidingBox.Width - spriteBox.X) > (spriteBox.Y + spriteBox.Height - collidingBox.Y))
                {
                    //enemy either turns red to take damage or dies depending on mario's state
                    enemy.CurrentState.InjuredTransition();
                    this.Velocity.Y = 0;
                }
                else
                {
                    this.TakeDamage();
                }
            }
        }
        public void CollisionResponseTop(ICollidable collidable)
        {

        }
        public void CollisionResponseBottom(ICollidable collidable)
        {

        }
        public void CollisionResponseLeft(ICollidable collidable)
        {
            //Debug.WriteLine("H");
        }
        public void CollisionResponseRight(ICollidable collidable)
        {
            //Debug.WriteLine("H");
        }
        public void CollideWithBlock(ICollidable collidable, Rectangle collidingBox, Rectangle spriteBox)
        {
            if (!(collidable.State is BrickBlockExplodedState) || FeatureOn)
            {
                VerticalBlockCollide(collidingBox, spriteBox);

                HorizontalBlockCollide(collidingBox, spriteBox);

                Vector2 spritePosition = new Vector2(spriteBox.X, spriteBox.Y);
                //if mario is below the box
                if (((collidingBox.Y + collidingBox.Height) - spritePosition.Y) < ((spritePosition.Y + spriteBox.Height) - collidingBox.Y))
                {
                    if (((spritePosition.X + spriteBox.Width) - collidingBox.X) < ((collidingBox.Y + collidingBox.Height) - spritePosition.Y))
                    {
                        this.position.X = (int)collidingBox.X - (int)spriteBox.Width;
                    }
                    else if (((collidingBox.X + collidingBox.Width) - spritePosition.X) < ((collidingBox.Y + collidingBox.Height) - spritePosition.Y))
                    {
                        this.position.X = (int)collidingBox.X + (int)collidingBox.Width;
                    }
                    else
                    {
                        this.position.Y = (int)collidingBox.Y + (int)collidingBox.Height;
                    }
                }
                else
                {
                    if (((spritePosition.X + spriteBox.Width) - collidingBox.X) < ((spritePosition.Y + spriteBox.Height) - collidingBox.Y))
                    {
                        this.position.X = (int)collidingBox.X - (int)spriteBox.Width;
                    }
                    else if (((collidingBox.X + collidingBox.Width) - spritePosition.X) < ((spritePosition.Y + spriteBox.Height) - collidingBox.Y))
                    {
                        this.position.X = (int)collidingBox.X + (int)collidingBox.Width;
                    }
                    else
                    {
                        //Debug.WriteLine("Second correction is occurring");
                        //This is where the problem is!!!
                        Debug.WriteLine("Y spot mario bottom before correction " + (int)(this.position.Y + spriteBox.Height) + " and " + (this.position.Y + spriteBox.Height));
                        Debug.WriteLine("Y spot box top before correction " + (int)collidingBox.Y + " and " + collidingBox.Y);
                        this.position.Y = (int)collidingBox.Y - (int)spriteBox.Height + 1;
                        Debug.WriteLine("Y spot mario bottom after correction " + (int)(this.position.Y + spriteBox.Height) + " and " + (this.position.Y + spriteBox.Height));
                        Debug.WriteLine("Y spot box top after correction " + (int)collidingBox.Y + " and " + collidingBox.Y);

                    }
                }
            }

            /*if (((spritePosition.X + spriteBox.Width) - collidingBox.X) < (spritePosition.X - (collidingBox.X + collidingBox.Width)))
            {
                this.position.X = (int)collidingBox.X - (int)spriteBox.Width;
            }
            else 
            {
                this.position.X = (int)collidingBox.X + (int)collidingBox.Width;
            }*/
        }

        public void VerticalBlockCollide(Rectangle collidingBox, Rectangle spriteBox)
        {
            //Vertical Collisions
            //top of mario
            if (spriteBox.X <= (collidingBox.X + collidingBox.Width) && (spriteBox.X + spriteBox.Width) >= (collidingBox.X + collidingBox.Width) && spriteBox.Y <= (collidingBox.Y + collidingBox.Height) && collidingBox.Y <= spriteBox.Y)
            {
                this.CollideVertical(false);
            }
            else if ((spriteBox.X + spriteBox.Width) >= collidingBox.X && spriteBox.Y <= (collidingBox.Y + collidingBox.Height) && collidingBox.Y <= spriteBox.Y)
            {
                this.CollideVertical(false);
            }
            //bottom of mario
            //theoretically this is it
            else if (spriteBox.X <= (collidingBox.X + collidingBox.Width) && (spriteBox.Y + spriteBox.Height) >= collidingBox.Y)
            {
                //this.support = true;
                this.CollideVertical(true);
            }
            else if ((spriteBox.X + spriteBox.Width) >= collidingBox.X && (spriteBox.Y + spriteBox.Height) >= collidingBox.Y)
            {
                //this.support = true;
                this.CollideVertical(true);
            }
            Gravity(support);
        }

        public void HorizontalBlockCollide(Rectangle collidingBox, Rectangle spriteBox)
        {

            //Horizontal Collisions
            if (collidingBox.X <= (spriteBox.X + spriteBox.Width) && (collidingBox.Y + collidingBox.Height) <= spriteBox.Y)
            {
                //Console.WriteLine("first case");
                this.CollideHorizontal();
                //Debug.WriteLine("H");
            }
            else if ((collidingBox.X + collidingBox.Width) >= spriteBox.X && (collidingBox.Y + collidingBox.Height) <= spriteBox.Y)
            {
                //Console.WriteLine("second case");
                this.CollideHorizontal();
                //Debug.WriteLine("H");
            }
            else if (collidingBox.X <= (spriteBox.X + spriteBox.Width) && collidingBox.Y >= (spriteBox.Y + spriteBox.Height))
            {
                //Console.WriteLine("third case");
                this.CollideHorizontal();
                //Debug.WriteLine("H");
            }
            else if ((collidingBox.X + collidingBox.Width) >= spriteBox.X && collidingBox.Y >= (spriteBox.Y + spriteBox.Height))
            {
                // Console.WriteLine("fourth case");
                this.CollideHorizontal();
                //Debug.WriteLine("H");
            }
            //Fix clipping issues
            support = false;
            Gravity(support);
        }

        public void CollideWithItem(ICollidable collidable)
        {
            //ItemModel item = (ItemModel)collidable;
            //item.CollideVertical(this);
            if (collidable is FireFlowerModel)
            {
                if (this.powerupState == StandardState.Instance)
                {
                    this.powerupState = SuperState.Instance;
                }
                else if (this.powerupState == SuperState.Instance)
                {
                    this.powerupState = FireState.Instance;
                }

            }
            else
            if (collidable is RedMushroomModel)
            {
                if (this.powerupState == StandardState.Instance)
                {
                    this.powerupState = SuperState.Instance;
                }
            }
            else
            if (collidable is GreenMushroomModel)
            {
                if (this.powerupState == StandardState.Instance)
                {
                    this.powerupState = SuperState.Instance;
                }
            }
            else
            if (collidable is SuperStarModel)
            {
                if (this.powerupState == StandardState.Instance)
                {
                    this.powerupState = SuperState.Instance;
                }
            }
            else
             if (collidable is CoinModel)
            {
                //Give Coins to mario.
            }

        }
        public IStates State => powerupState;

        public void MoveLeft()
        {
            support = false;
            if (leftFacing)
            {
                this.Velocity.X = -horizontalSpeed;
            }
            this.actionState = this.actionState.MoveLeft(ref leftFacing);
        }
        public void StopLeft()
        {
            if (this.Velocity.X == -horizontalSpeed)
            {
                this.Velocity.X = 0;
                if (this.Velocity.Y != -jumpSpeed)
                {
                    this.actionState = this.actionState.MoveRight(ref leftFacing);
                }
            }
        }

        public void MoveRight()
        {
            support = false;
            if (!leftFacing)
            {
                this.Velocity.X = horizontalSpeed;
            }
            this.actionState = this.actionState.MoveRight(ref leftFacing);
        }

        public void StopRight()
        {
            if (this.Velocity.X == horizontalSpeed)
            {
                this.Velocity.X = 0;
                if (this.Velocity.Y != -jumpSpeed)
                {
                    this.actionState = this.actionState.MoveLeft(ref leftFacing);
                }
            }
        }

        public void Jump()
        {
            if (!(this.actionState is CrouchingState))
            {
                this.Velocity.Y = -this.jumpSpeed;
            }
            this.actionState = this.actionState.Jump();
        }
        public void UpdateJump(bool stoping)
        {
            int maxJumpHeight = 5000;
            if (heightJumped > maxJumpHeight || stoping)
            {
                //Debug.WriteLine(this.heightJumped + " height jumped after stop");
                this.Velocity.Y = 0;
                heightJumped = 0;
                //this needs to change to falling once I get that implemented correctly
                if (this.actionState is JumpingState)
                {
                    this.actionState = this.actionState.Crouch(this.powerupState);
                }
                support = false;
                Gravity(support);
            }
            else
            {
                this.heightJumped = this.heightJumped + jumpSpeed;
                //Debug.WriteLine( this.heightJumped + " heightJumped and " + maxJumpHeight + " maxJumpHeight.");
            }
        }

        public void Crouch()
        {
            if (canMoveDown)
            {
                if (this.Velocity.Y == jumpSpeed)
                {
                    this.Velocity.Y = 0;
                }
                else
                {
                    this.Velocity.Y = jumpSpeed;
                }
            }
            this.actionState = this.actionState.Crouch(this.powerupState);
            //this stop method is just the Jump method

        }
        public void StopCrouch()
        {
            if (!(this.powerupState is StandardState))
            {
                this.actionState = this.actionState.Jump();
            }
        }

        public void TakeDamage()
        {
            Debug.WriteLine("invincible frames: " + invincibleFrames);
            if (this.invincibleFrames == 0)
            {
                this.powerupState = this.powerupState.TakeDamage();
                this.invincibleFrames = 100;
            }
        }

        public void Standard()
        {
            this.powerupState = StandardState.Instance;
        }
        public void Super()
        {
            this.powerupState = SuperState.Instance;
        }
        public void Fire()
        {
            this.powerupState = FireState.Instance;
        }

        public void CollideHorizontal()
        {
            //this.prevState = this.actionState;
            //Debug.WriteLine("Horrizontal collision is occurring");
            this.actionState = IdlingState.Instance;
            this.Velocity.X = 0;
            Gravity(support);
        }

        public void CollideVertical(bool standing)
        {
            //this.prevState = this.actionState;
            if (this.Velocity.Y != 0)
            {
                //Debug.WriteLine("there is Y velocity");
                //this.actionState = IdlingState.Instance;
                this.Velocity.Y = 0;
            }
            if (standing)
            {
                support = true;
                //Debug.WriteLine("colliding support is being checked");
            }
            //Gravity(support);
        }

        public bool IsMoving()
        {
            return this.Velocity.Length() != 0;
        }

        public void ToggleCrouchCommand()
        {
            canMoveDown = !canMoveDown;
        }
        public void Gravity(bool onSomething)
        {
            //this will do something 
            if (!onSomething && this.Velocity.Y != -jumpSpeed && gravityAllowed)
            {
                //this.actionState = this.actionState.Fall();
                this.Velocity.Y = jumpSpeed;
            }
            else
            {
                //Debug.WriteLine("there is support");
            }

        }
        public void ToggleGravity()
        {
            gravityAllowed = !gravityAllowed;
        }
    }
}
