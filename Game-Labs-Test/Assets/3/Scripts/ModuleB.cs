using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ThirdTask
{
	public class ModuleB : Module
	{
		public override string Name { get => "B"; }

		public override int AdditionalLife => 50;
	}
}
