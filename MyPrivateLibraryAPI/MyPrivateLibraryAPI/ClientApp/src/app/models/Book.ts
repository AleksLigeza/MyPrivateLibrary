export class Book {
    Id: string;
    Title: string;
    PublicationYear: number;
    ReadingStart: Date;
    ReadingEnd: Date;

    constructor() {
        this.Id = null;
        this.Title = null;
        this.PublicationYear = null;
        this.ReadingStart = null;
        this.ReadingEnd = null;
    }

    static createArray(res): Book[] {

        let result: Book[];
        result = res;

        result.forEach((value, index) => {
            Book.normalize(value, res[index].id);
        });
        return result;
    }

    static normalize(book: Book, id: number) {
        book.Id = id.toString();
    }
}
