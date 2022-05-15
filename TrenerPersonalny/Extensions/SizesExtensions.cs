using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;
using TrenerPersonalny.Models.DTOs.Sizes;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

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
                    SizeCm = details.SizeCm,
                    ExcerciseTypeId = details.ExcerciseTypeId
                }).ToList()
            }).AsNoTracking();
        }

        public static IQueryable<Sizes> RetrieveSizesWithDetails(this IQueryable<Sizes> query, int id)
        {
            return query.Include(i => i.SizeDetails).Where(b => b.PersonId == id);
        }
    }
}

