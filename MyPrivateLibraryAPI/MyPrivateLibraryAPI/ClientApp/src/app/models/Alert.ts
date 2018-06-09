export class Alert {
    type: AlertType;
    message: string;
    keep: boolean;
}

export enum AlertType {
    Success,
    Error,
    Info,
    Warning
}
