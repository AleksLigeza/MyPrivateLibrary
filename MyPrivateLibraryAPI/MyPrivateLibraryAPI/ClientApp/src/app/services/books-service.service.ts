import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AlertService } from './alert.service';
import { BookFilters } from '../models/BookFilers';

@Injectable()
export class BooksService {

  constructor(private http: HttpClient,
    @Inject('BASE_URL') private baseUrl: string,
    public router: Router,
    private alertService: AlertService
  ) {
    this.baseUrl += 'api/book/';
  }

  GetBooks() {
    return this.http.get(this.baseUrl + 'GetAll');
  }

  GetBooksWithFilters(filters: BookFilters) {

    let filtersUrl = 'Filter/';
    filtersUrl += filters.PublicationYearSince + '/' +
      filters.PublicationYearTo  + '/' +
      filters.Read + '/' +
      filters.CurrentlyReading + '/' +
      filters.Title;

    return this.http.get(this.baseUrl + filtersUrl);
  }
}
