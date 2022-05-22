using Microsoft.EntityFrameworkCore;
using System.Linq;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs.Sizes;

namespace TrenerPersonalny.Extensions
{
    public static class SizesExtensions
    {
        public static IQueryable<SizesDTO> MapSizesToDto(this IQueryable<Sizes> query)
        {
            return query.Select(sizes => new SizesDTO
            {
                Id = sizes.Id,
                UpdateDate = sizes.UpdateDate,
                PersonId = sizes.PersonId,
                SizeDetails = sizes.SizeDetails.Select(details => new SizeDetailsDTO
                {
                    Id = details.Id,
                    SizeCm = details.SizeCm,
                    ExcerciseTypeId = details.ExcerciseTypeId,
                    ExcerciseType = details.ExcerciseType
                }).ToList()
            }).AsNoTracking();
        }

        public static IQueryable<Sizes> RetrieveSizesWithDetails(this IQueryable<Sizes> query, int id)
        {
            return query.Include(i => i.SizeDetails).Where(b => b.PersonId == id);
        }
    }
}

