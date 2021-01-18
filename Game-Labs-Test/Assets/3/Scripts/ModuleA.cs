using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ThirdTask
{
	public class ModuleA : Module
	{
		public override string Name { get => "A"; }

		public override int AdditionalShield => 50;
	}
}