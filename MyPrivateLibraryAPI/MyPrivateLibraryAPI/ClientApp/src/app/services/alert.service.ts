import { Injectable } from '@angular/core';
import { Router, NavigationStart } from '@angular/router';
import { Observable } from 'Rxjs';
import { Subject } from 'rxjs/Subject';

import { Alert, AlertType } from '../models/alert';

@Injectable()
export class AlertService {
  private subject = new Subject<Alert>();
  private keepAlert = false;

  constructor(private router: Router) {
    router.events.subscribe(event => {
      if (event instanceof NavigationStart) {
        if (this.keepAlert) {
          this.keepAlert = false;
        } else {
          this.clear();
        }
      }
    });
  }

  getAlert(): Observable<any> {
    return this.subject.asObservable();
  }

  success(message: string, keepAlert = false) {
    this.alert(AlertType.Success, message, keepAlert);
  }

  error(message: string, keepAlert = false) {
    this.alert(AlertType.Error, message, keepAlert);
  }

  info(message: string, keepAlert = false) {
    this.alert(AlertType.Info, message, keepAlert);
  }

  warn(message: string, keepAlert = false) {
    this.alert(AlertType.Warning, message, keepAlert);
  }

  alert(type: AlertType, message: string, keepAlert = false) {
    this.keepAlert = keepAlert;
    this.subject.next(<Alert>{ type: type, message: message, keep: keepAlert});
  }

  clear() {
    this.subject.next();
  }
}
