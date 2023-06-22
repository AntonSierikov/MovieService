namespace MovieService.Settings;

public class MovieAppOptions
{
    public ConnectionStrings ConnectionStrings { get; set; }
}

public class ConnectionStrings
{
    public string PostgresSqlConnectionString { get; set; }
}