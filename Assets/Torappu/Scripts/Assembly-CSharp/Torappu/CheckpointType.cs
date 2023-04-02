namespace Torappu
{
	public enum CheckpointType
	{
		MOVE = 0,
		WAIT_FOR_SECONDS = 1,
		WAIT_FOR_PLAY_TIME = 2,
		WAIT_CURRENT_FRAGMENT_TIME = 3,
		WAIT_CURRENT_WAVE_TIME = 4,
		DISAPPEAR = 5,
		APPEAR_AT_POS = 6,
		ALERT = 7,
		PATROL_MOVE = 8
	}
}
