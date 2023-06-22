using System;
using System.Collections.Generic;
using System.Text;
using TaskService.Domain.DTO.CinemaEntities;
using System.Threading.Tasks;

namespace TaskService.Domain.ServiceInterface
{
    public interface ILoadDataService
    {

        Task<MovieDto> LoadMovie(string movieId);
        Task<PeopleDto> LoadPeople(string peopleId);
        Task<List<GenreDto>> LoadGenres();
        Task<ProductionCompanyDto> LoadCompany(string companyId);
        Task<List<int>> LoadTopMovieIdsByPage(int page);
        Task<CreditsDto> LoadCreditsByMovieId(int movieId);
        Task<List<DepartmentDto>> LoadDepartments();
    }
}
