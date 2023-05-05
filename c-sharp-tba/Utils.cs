using System;

namespace MyUtilities
{
	public static class RandomGen
	{
		public static int RandomInt(int _minValue, int _maxValue)
		{
			//value's range is  ( min -> max - 1)
			var rnd = new Random();
			return (int)rnd.Next(_minValue,_maxValue);
		}

		public static bool RandomBool()
        {
			var rnd = new Random();
			int i = (int)rnd.Next(0, 2);
			return Convert.ToBoolean(i);
		}
	}

}

