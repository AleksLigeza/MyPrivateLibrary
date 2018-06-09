export class BookFilters {
    PublicationYearSince: number;
    PublicationYearTo: number;
    Title: string;
    Read: boolean;
    CurrentlyReading: boolean;
    Order: number;

    constructor() {
        this.PublicationYearSince = 0;
        this.PublicationYearTo = 2100;
        this.Title = '';
        this.Read = false;
        this.CurrentlyReading = false;
        this.Order = OrderByFiled.OrderByTitle;
    }

    nullAllParameters() {
        this.PublicationYearSince = null;
        this.PublicationYearTo = null;
        this.Title = null;
        this.Read = null;
        this.CurrentlyReading = null;
        this.Order = 0;
    }

    removeNulls() {
        const cleanTemplate = new BookFilters();
        if (this.PublicationYearSince === null) {
            this.PublicationYearSince = cleanTemplate.PublicationYearSince;
        }
        if (this.PublicationYearTo === null) {
            this.PublicationYearTo = cleanTemplate.PublicationYearTo;
        }
        if (this.Title === null) {
            this.Title = cleanTemplate.Title;
        }
        if (this.Read === null) {
            this.Read = cleanTemplate.Read;
        }
        if (this.CurrentlyReading === null) {
            this.CurrentlyReading = cleanTemplate.CurrentlyReading;
        }
    }
}

export enum OrderByFiled {
    OrderByTitle,
    OrderByTitleDesc,
    OrderByYear,
    OrderByYearDesc
  }
