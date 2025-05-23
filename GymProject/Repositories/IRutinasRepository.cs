﻿using GymProject.Models.Domain;
using GymProject.Models.DTO;

namespace GymProject.Repositories
{
    public interface IRutinasRepository
    {
        Task<IEnumerable<Rutinas>>GetAllAsync(string? searchQuery, DateTime? searchDate, int pageNumber = 1, int pageSize = 9);
        Task<IEnumerable<Rutinas>> GetRutinas();

        Task<RutinaDto> GetAsync(int id);
        Task<Rutinas> AddAsync(Rutinas rutina);
        Task<Rutinas> UpdateAsync(Rutinas rutina);
        Task<Rutinas> DeleteAsync(int id);
        Task<int>CountAsync();

    }
}
