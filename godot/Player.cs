using Godot;
using System;

public partial class Player : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float MaxSpeed = 400.0f;
	
	public const float Friction = 0.6f;
	public const float AirControl = 0.5f;
	
	
	public const float JumpVelocity = -500.0f;
	
	public const float DashStrengh = 2f;	
	public const byte MaxDash = 1;
	public byte Dash = MaxDash;
	public const float DashTimerMax =  0.3f;
	public float DashTimer = -1;
	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	public override void _Process(double delta)
	{
		Vector2 velocity = Velocity;
		
		if (!IsOnFloor()) velocity.Y += gravity * (float)delta; // gravity

		if (Input.IsActionPressed("jump") && IsOnFloor()) velocity.Y = JumpVelocity;
		if (!Input.IsActionPressed("jump") && !IsOnFloor() && velocity.Y < 0 && Dash == MaxDash) velocity.Y/=3;
		
		float dir = Input.GetAxis("left", "right");
		if(velocity.X * dir < MaxSpeed ){
			if (IsOnFloor()){
				velocity.X += dir * Speed * (float)delta * 10;
			}else{
			velocity.X += dir * Speed * (float)delta * AirControl * 10;
		}}
		if(IsOnFloor()){
		if(dir == 0)velocity.X *= Friction ;
		Dash = MaxDash;
		}
			if (DashTimer < DashTimerMax && DashTimer >= 0){
				DashTimer += (float)delta;
			}else{
				if(DashTimer >= DashTimerMax){
					DashTimer = -1;
					velocity/=DashStrengh;
				}
		}
		
		if (Input.IsActionJustPressed("dash") && Dash > 0 && DashTimer < 0){
			Dash--;
			DashTimer = 0;
			Vector2 dirDash = Input.GetVector("left", "right", "up", "down");
			if (dirDash != Vector2.Zero) velocity = dirDash * MaxSpeed * DashStrengh;
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
