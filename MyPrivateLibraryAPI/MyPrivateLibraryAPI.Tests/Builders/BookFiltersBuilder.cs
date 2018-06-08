using System;
using System.Collections.Generic;
using System.Text;
using MyPrivateLibraryAPI.Models;

namespace MyPrivateLibraryAPI.Tests.Builders
{
    public class BookFiltersBuilder
    {
        private BookFilters _bookFilters;

        public BookFiltersBuilder()
        {
            _bookFilters = new BookFilters();
        }

        public BookFiltersBuilder WithPublicationYearSince(int year)
        {
            _bookFilters.PublicationYearSince = year;
            return this;
        }

        public BookFiltersBuilder WithPublicationYearTo(int year)
        {
            _bookFilters.PublicationYearTo = year;
            return this;
        }

        public BookFiltersBuilder WithTitle(string title)
        {
            _bookFilters.Title = title;
            return this;
        }

        public BookFiltersBuilder WithCurrentlyReadingStatus(bool status)
        {
            _bookFilters.CurrentlyReading = status;
            return this;
        }

        public BookFiltersBuilder WithReadStatus(bool status)
        {
            _bookFilters.Read = status;
            return this;
        }

        public BookFilters Build()
        {
            return _bookFilters;
        }
    }
}
