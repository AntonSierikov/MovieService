namespace MovieService.Domain.Entities;

public class Movie
{
	public Guid Id { get; }
	public string Title { get; private set; }
	public DateTimeOffset ReleasedAt { get; private set; }
	public decimal BudgetAmount { get; private set; }
	public string BudgetCurrency { get; private set; }
	public string Description { get; private set; }
	public string ProductionCompany { get; private set; }
	public string Country { get; private set; }
	public decimal BoxOfficeAmount { get; private set; }
	public string BoxOfficeCurrency { get; private set; }
	public decimal AverageMark { get; private set; }

	public string PosterUrl { get; private set; }

	public int VoteCount { get; private set; }
	public string MovieExtraId { get; }

	public Actor Director { get; }

	public decimal Popularity { get;  }

	public int RunTime { get; set; }

	public IReadOnlyCollection<Cast> Casts { get; private set; }

	public IReadOnlyCollection<Genre> Genres { get; private set; }

	private Movie(
		Guid id,
		string title,
		DateTimeOffset releasedAt,
		decimal budgetAmount,
		string budgetCurrency,
		string description,
		string country,
		string productionCompany,
		decimal boxOfficeAmount,
		string boxOfficeCurrency,
		decimal averageMark,
		string posterUrl,
		int voteCount,
		string movieExtraId,
		Actor director,
		decimal popularity,
		int runTime,
		IReadOnlyCollection<Genre> genres,
		IReadOnlyCollection<Cast> casts)
	{
		Id = id;
		Title = title;
		ReleasedAt = releasedAt;
		BudgetAmount = budgetAmount;
		BudgetCurrency = budgetCurrency;
		Description = description;
		ProductionCompany = productionCompany;
		Country = country;
		BoxOfficeAmount = boxOfficeAmount;
		BoxOfficeCurrency = boxOfficeCurrency;
		AverageMark = averageMark;
		PosterUrl = posterUrl;
		Casts = casts;
		VoteCount = voteCount;
		MovieExtraId = movieExtraId;
		Genres = genres;
		Director = director;
		Popularity = popularity;
		RunTime = runTime;
	}

	public static Movie Create(
		string title,
		DateTimeOffset releaseAt,
		decimal budgetAmount,
		string budgetCurrency,
		string description,
		string country,
		string productionCompany,
		decimal boxOfficeAmount,
		string boxOfficeCurrency,
		decimal averageMark,
		string posterUrl,
		int voteCount,
		string movieExtraId,
		Actor director,
		decimal popularity,
		int runTime,
		IReadOnlyCollection<Genre> genres,
		IReadOnlyCollection<Cast> casts) => new Movie(Guid.NewGuid(), title, releaseAt, budgetAmount, budgetCurrency, description, country, productionCompany, boxOfficeAmount, boxOfficeCurrency, averageMark, posterUrl, voteCount, movieExtraId, director, popularity, runTime, genres, casts);

	public static Movie Read(
		Guid id,
		string title,
		DateTimeOffset releaseAt,
		decimal budgetAmount,
		string budgetCurrency,
		string description,
		string country,
		string productionCompany,
		decimal boxOfficeAmount,
		string boxOfficeCurrency,
		decimal averageMark,
		string posterUrl,
		int voteCount,
		string movieExtraId,
		Actor director,
		decimal popularity,
		int runTime,
		IReadOnlyCollection<Genre> genres,
		IReadOnlyCollection<Cast> casts) => new Movie(id, title, releaseAt, budgetAmount, budgetCurrency, description, country, productionCompany, boxOfficeAmount, boxOfficeCurrency, averageMark, posterUrl, voteCount, movieExtraId, director, popularity, runTime, genres, casts);

	public void Update(
		string title,
		DateTimeOffset releaseAt,
		decimal budgetAmount,
		string budgetCurrency,
		string description,
		string country,
		string productionCompany,
		decimal boxOfficeAmount,
		string boxOfficeCurrency)
	{
		Title = title;
		Title = title;
		ReleasedAt = releaseAt;
		BudgetAmount = budgetAmount;
		BudgetCurrency = budgetCurrency;
		Description = description;
		ProductionCompany = productionCompany;
		Country = country;
		BoxOfficeAmount = boxOfficeAmount;
		BoxOfficeCurrency = boxOfficeCurrency;
	}

	public void UpdateAverageMark(int averageMark)
	{
		AverageMark = averageMark;
	}
}