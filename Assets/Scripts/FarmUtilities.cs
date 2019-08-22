using UnityEngine;
using UnityEngine.Tilemaps;

class FarmUtilites 
{
	// Get the centt of a cell in world position
	public static Vector3 GetCenterOfCell (Tilemap _tilemap , Vector3 _worldPos)
	{
		if(_tilemap == null)
		{
			_tilemap = TilemapGroup.normal;
		}

		return _tilemap.GetCellCenterWorld(_tilemap.WorldToCell(_worldPos));
	}
}