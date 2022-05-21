using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrenerPersonalny.Models;

namespace TrenerPersonalny.Extensions
{
    public static class TrainerExtensions
    {
        public static IQueryable<Trainers> Sort(this IQueryable<Trainers> query, string orderBy)
        {
            if (string.IsNullOrWhiteSpace(orderBy)) return query.OrderBy(p => p.Person.LastName);

            query = orderBy switch
            {
                "price" => query.OrderBy(p => p.Price),
                "priceDesc" => query.OrderByDescending(p => p.Price),
                _ => query.OrderBy(p => p.Person.LastName)
            };

            return query;
        }

        public static IQueryable<Trainers> Search(this IQueryable<Trainers> query, string searchTerm)
        {
            if (string.IsNullOrEmpty(searchTerm)) return query;

            var lowerCaseSearchTerm = searchTerm.Trim().ToLower();
                      

            return query.Where(p => p.Person.LastName.ToLower().Contains(lowerCaseSearchTerm)
                            || p.Person.FirstName.ToLower().Contains(lowerCaseSearchTerm));
        }

        public static IQueryable<Trainers> Filter(this IQueryable<Trainers> query, int price, int rating)
        {
            query = query.Where(p => p.Price <= price);

            return query;
        }
    }
}
