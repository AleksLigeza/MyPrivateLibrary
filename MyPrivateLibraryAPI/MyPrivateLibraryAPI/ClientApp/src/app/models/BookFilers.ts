export class BookFilters {
    PublicationYearSince: number;
    PublicationYearTo: number;
    Title: string;
    Read: boolean;
    CurrentlyReading: boolean;

    constructor() {
        this.PublicationYearSince = 0;
        this.PublicationYearTo = 2100;
        this.Title = '';
        this.Read = false;
        this.CurrentlyReading = false;
    }

    nullAllParameters() {
        this.PublicationYearSince = null;
        this.PublicationYearTo = null;
        this.Title = null;
        this.Read = null;
        this.CurrentlyReading = null;
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
