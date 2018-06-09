import { Component, OnInit } from '@angular/core';
import { BooksService } from '../services/books-service.service';
import { Router, ActivatedRoute } from '@angular/router';
import { AlertService } from '../services/alert.service';
import { Book } from '../models/Book';

@Component({
  selector: 'app-details',
  templateUrl: './details.component.html',
  styleUrls: ['./details.component.css']
})
export class DetailsComponent implements OnInit {
  myBook: Book;

  constructor(private booksService: BooksService,
    private router: Router,
    private alert: AlertService,
    private route: ActivatedRoute
  ) {
    this.myBook = new Book();
  }

  ngOnInit() {
    const id: string = this.route.snapshot.paramMap.get('id');

    this.booksService.GetOne(parseInt(id, 10)).subscribe(
      res => {
        this.myBook = Book.FromResult(res);
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  GoBack() {
    this.router.navigate(['/dashboard']);
  }

  Update(book: Book) {
    this.booksService.UpdateBook(book).subscribe(
      res => {
        this.router.navigate(['/dashboard']);
      },
      err => {
        this.alert.error('Invalid data');
      });
  }

  Remove(id: number) {
    this.booksService.RemoveBook(id).subscribe(
      res => {
        this.router.navigate(['/dashboard']);
      },
      err => {
        this.alert.error('Delete error');
      });
  }

}
