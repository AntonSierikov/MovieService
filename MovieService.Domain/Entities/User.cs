namespace MovieService.Domain.Entities;

public class User {

	public Guid Id { get; private set; }

	public string Email { get; private set; }

	public string Login { get; private set; }

	public int Gender { get; private set; }

	public string Country { get; private set; }

	public string City { get; private set; }

	public string PasswordHash { get; private set; }

	public string ProfilePhoto { get; private set; }

	public DateTimeOffset RegisteredAt { get; private set; }

	private User(
		string email,
		string login,
		int gender,
		string country,
		string city,
		string profilePhoto,
		string passwordHash)
	{
		Email = email;
		Login = login;
		Gender = gender;
		Country = country;
		City = city;
		ProfilePhoto = profilePhoto;
		PasswordHash = passwordHash;
	}

	public static User Register(string email, string login, int gender, string country, string city, string passwordHash, string profilePhotoUrl)
	{
		return new User(
			email,
			login,
			gender,
			country,
			city,
			profilePhotoUrl,
			passwordHash);
	}
	public void UpdateEmail(string email)
	{
		Email = email;
	}
	public void UpdatePassword(string password)
	{
		PasswordHash = PasswordHash;
	}

	public void UpdateCity(string country, string city)
	{
		City = city;
	}
}