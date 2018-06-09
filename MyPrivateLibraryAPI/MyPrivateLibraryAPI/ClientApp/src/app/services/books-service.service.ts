import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { AlertService } from './alert.service';
import { BookFilters } from '../models/BookFilers';
import { Book } from '../models/Book';

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

  GetOne(id: number) {
    return this.http.get(this.baseUrl + 'Get/' + id);
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

  AddBook(book: Book) {
    const toSend = {
      Id: 0,
      Title: book.Title,
      PublicationYear: book.PublicationYear,
      ReadingStart: null,
      ReadingEnd: null
    };

    return this.http.post(this.baseUrl + 'AddBook', toSend);
  }

  UpdateBook(book: Book) {
    const toSend = {
      Id: book.Id,
      Title: book.Title,
      PublicationYear: book.PublicationYear,
      ReadingStart: book.ReadingStart,
      ReadingEnd: book.ReadingEnd,
    };

    return this.http.put(this.baseUrl + 'UpdateBook', book);
  }

  RemoveBook(id: number) {
    return this.http.delete(this.baseUrl + 'RemoveBook/' + id);
  }

  AddStartTime(book: Book) {
    const toSend = {
      Id: book.Id,
      Title: book.Title,
      PublicationYear: book.PublicationYear,
      ReadingStart: book.ReadingStart,
      ReadingEnd: book.ReadingEnd,
    };

    return this.http.put(this.baseUrl + 'SetStartReading', book);
  }

  AddEndTime(book: Book) {
    const toSend = {
      Id: book.Id,
      Title: book.Title,
      PublicationYear: book.PublicationYear,
      ReadingStart: book.ReadingStart,
      ReadingEnd: book.ReadingEnd,
    };

    return this.http.put(this.baseUrl + 'SetEndReading', book);
  }
}
