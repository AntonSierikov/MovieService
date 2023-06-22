namespace MovieService.Dto;

public class MovieListDto
{
    public MovieDto[] Items { get; set; }

    public int Total { get; set; }
}