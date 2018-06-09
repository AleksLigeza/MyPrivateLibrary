import { Component, OnInit } from '@angular/core';
import { BookFilters } from '../models/BookFilers';
import { Book } from '../models/Book';
import { AlertService } from '../services/alert.service';
import { BooksService } from '../services/books-service.service';

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.css']
})
export class DashboardComponent implements OnInit {

  filters: BookFilters;
  filtersPos: number;
  books: Book[];
  newBook: Book;

  filtersVisible: boolean;
  filtersApplied: boolean;

  count: number;
  records: number;
  shouldResetBooks: boolean;

  constructor(
    private booksService: BooksService,
    private alert: AlertService
  ) { }

  ngOnInit() {
    this.newBook = new Book();
    this.resetBooks();
    this.clearFilters();
    this.getBooks();
  }

  changeType(pos: number) {
    this.filtersPos = pos;
    if (pos === 2) {
      this.filters.Read = false;
      this.filters.CurrentlyReading = true;
    } else if (pos === 1) {
      this.filters.Read = true;
      this.filters.CurrentlyReading = false;
    } else {
      this.filters.Read = false;
      this.filters.CurrentlyReading = false;
    }
  }

  getBooks() {
    this.booksService.GetBooks().subscribe(
      res => {
        this.addBooksToArray(Book.createArray(res));
      },
      err => {
        this.alert.error('Loading error');
      }
    );
  }

  getBooksWithFilters() {
    this.filtersApplied = true;
    let tempFilters: BookFilters;
    tempFilters = Object.create(this.filters);
    tempFilters.removeNulls();

    this.booksService.GetBooksWithFilters(tempFilters).subscribe(
      res => {
        this.addBooksToArray(Book.createArray(res));
      },
      err => {
        this.alert.error('Loading error');
      }
    );
  }

  addBooksToArray(res: Book[]) {
    if (this.shouldResetBooks) {
      this.resetBooks();
    }

    this.books = this.books.concat(res);
    this.records = this.books.length;

    if (this.count !== -1) {
      if (this.records !== this.count) {
        this.alert.info('New books loaded!');
      } else {
        this.alert.warn('There is no more books!');
      }
    }
  }

  showHideFilters() {
    this.filtersVisible = !this.filtersVisible;
  }

  clearFilters() {
    this.filters = new BookFilters();
    this.filters.nullAllParameters();
    this.filtersApplied = false;
    this.changeType(0);
  }

  resetBooks() {
    this.books = [];
    this.records = 0;
    this.count = -1;
    this.shouldResetBooks = false;
  }

  allowResetBooks() {
    this.shouldResetBooks = true;
    this.records = 0;
    this.count = -1;
  }

  AddBook() {
    this.booksService.AddBook(Object.create(this.newBook)).subscribe(
      res => {
        this.newBook = new Book();
        this.resetBooks();
        this.clearFilters();
        this.getBooks();
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  AddStartTime(book: Book) {
    this.booksService.AddStartTime(book).subscribe(
      res => {
        this.newBook = new Book();
        this.resetBooks();
        this.clearFilters();
        this.getBooks();
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  AddEndTime(book: Book) {
    this.booksService.AddEndTime(book).subscribe(
      res => {
        this.newBook = new Book();
        this.resetBooks();
        this.clearFilters();
        this.getBooks();
      },
      err => {
        this.alert.error('Invalid data');
      });
  }
}
