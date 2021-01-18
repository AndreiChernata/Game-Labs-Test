using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace ThirdTask
{
	public class ModuleC : Module
	{
		public override string Name { get => "C"; }

		public override int IncreaseReload => -20;
	}
}