import { Component, OnInit } from '@angular/core';
import { BookFilters, OrderByFiled } from '../models/BookFilers';
import { Book } from '../models/Book';
import { AlertService } from '../services/alert.service';
import { BooksService } from '../services/books-service.service';
import { Router } from '@angular/router';

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

  count: number;
  records: number;
  shouldResetBooks: boolean;

  constructor(
    private booksService: BooksService,
    private alert: AlertService,
    private router: Router
  ) { }

  ngOnInit() {
    this.reloadPage();
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

  changeOrder(pos: number) {
    this.filters.Order = pos;
    this.getBooksWithFilters();
  }

  getBooks() {
    this.booksService.GetBooks().subscribe(
      res => {
        this.books = Book.createArray(res);
      },
      err => {
        this.alert.error('Loading error');
      }
    );
  }

  getBooksWithFilters() {
    let tempFilters: BookFilters;
    tempFilters = Object.create(this.filters);
    tempFilters.removeNulls();

    this.booksService.GetBooksWithFilters(tempFilters).subscribe(
      res => {
        this.books = Book.createArray(res);
      },
      err => {
        this.alert.error('Loading error');
      }
    );
  }

  clearFilters() {
    this.filters = new BookFilters();
    this.filters.nullAllParameters();
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

  reloadPage() {
    this.newBook = new Book();
    this.resetBooks();
    this.clearFilters();
    this.getBooks();
  }

  AddBook() {
    this.booksService.AddBook(Object.create(this.newBook)).subscribe(
      res => {
        this.reloadPage();
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  AddStartTime(book: Book) {
    this.booksService.AddStartTime(book).subscribe(
      res => {
        this.reloadPage();
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  AddEndTime(book: Book) {
    this.booksService.AddEndTime(book).subscribe(
      res => {
        this.reloadPage();
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  RemoveBook(id: number) {
    this.booksService.RemoveBook(id).subscribe(
      res => {
        this.reloadPage();
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  BookDetails(id: number) {
    window.scrollTo(0, 0);
    this.router.navigate(['/details/' + id]);
  }
}
