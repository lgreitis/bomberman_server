using Models.Game;

public class PlayerBuilder
{
	private Player _player;

	public PlayerBuilder()
	{
		this._player = new Player();
	}

	public void UserId(int userId)
	{
		this._player.UserId = userId;
	}

	public void Username(string username)
	{
		this._player.Username = username;
	}

	public void Token(string token)
	{
		this._player.Token = token;
	}

	public void Health(int health)
	{
		this._player.Health = health;
	}

	public void LocationX(int x)
	{
		this._player.LocationX = x;
	}

    public void LocationY(int y)
    {
        this._player.LocationY = y;
    }

	public void IsConnected()
	{
		this._player.IsConnected = true;
	}

	public Player Build()
	{
		Player built = this._player;

		this._player = new Player();

		return built;
	}
}
