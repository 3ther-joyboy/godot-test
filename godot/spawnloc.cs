using Godot;
using System;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Resolvers;

public partial class spawnloc : Node2D
{
		public static Node snowflake = GD.Load<Node>("res://snow.tscn");
	private static float timer = 0;
	public override void _Process(double delta)
	{
		timer += (float)delta;
		if(timer > 1){
			timer = 0;
			this.AddChild(snowflake);
		}
	}
}
