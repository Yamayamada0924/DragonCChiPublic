


namespace App.Scripts.GameCommon
{
	public static class CommonParameters
	{
		public interface IViewParamReadOnly
		{
			public int Money { get; }
		}
		
		public struct ViewParam : IViewParamReadOnly
		{
			public int Money { get; set; }
		}
	}
}
