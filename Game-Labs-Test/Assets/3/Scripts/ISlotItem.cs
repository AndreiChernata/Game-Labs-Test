namespace ThirdTask
{
	public interface ISlotItem
	{
		void AddToShip(SpaceShip spaceShip);
		void RemoveFromShip(SpaceShip spaceShip);

		string Name { get; set; }
	}
}
