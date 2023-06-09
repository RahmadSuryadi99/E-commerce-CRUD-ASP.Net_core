﻿using Basilisk.Repository;
using Basilisk.ViewModel;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Basilisk.Providerr
{
    public abstract class BaseProvider
    {
        protected const int _totalDataPerPage = 5;
        protected static int TotalHalaman(int totalData)
        {
            int totalHalaman = (int)Math.Ceiling(totalData / (decimal)_totalDataPerPage);
            return totalHalaman;
        }
        protected static int GetSkip(int page)
        {
            int skip = (page - 1) * _totalDataPerPage;
            return skip;
        }
        public static void MappingModel<TDest,TSource>(TDest destination,TSource source)
        where TDest : class
        where TSource : class
        {
            var destinationProperties = destination.GetType().GetProperties();
            var sourceProperties = source.GetType().GetProperties(); 
            foreach (var sourceProperty in sourceProperties)
            {
                var property = destinationProperties.FirstOrDefault(a => a.Name == sourceProperty.Name);
                if (property != null)
                {
                    property.SetValue(destination, sourceProperty.GetValue(source));
                }
            }
        }
        public List<DropDownViewModel> GetDataAuthor()
        {
            var categories = CategoryRepository.GetRepository().GetAll();
            var result = categories.Select(a => new DropDownViewModel
            {
                LongValue = a.Id,
                Text = a.Name
            }).ToList();
            return result;

        }

    }
}
