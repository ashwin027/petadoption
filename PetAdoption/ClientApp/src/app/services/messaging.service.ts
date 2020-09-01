import { Injectable } from '@angular/core';
import * as signalR from "@microsoft/signalr";
import { Subject, throwError } from 'rxjs';
import { AuthService } from './auth.service';
import { mergeMap, catchError } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class MessagingService {
  notificationSubject = new Subject<Notification>();
  hubName = 'adoptionHub';
  connection: signalR.HubConnection;
  constructor(private auth: AuthService) { }

  initializeConnection() {
    if (!this.connection) {
      this.auth.getTokenSilently$().subscribe((token) =>{this.startConnection(token)});
    }
  }

  private startConnection(token: string) {
    this.connection = new signalR.HubConnectionBuilder()
      .configureLogging(signalR.LogLevel.Error)
      .withAutomaticReconnect()
      .withUrl(`/${this.hubName}`, {
        accessTokenFactory: () => token
      })
      .build();

    this.connection.start().catch(err => document.write(err));

    this.connection.on("adoptionRequestReceived", (notification: Notification) => {
      this.notificationSubject.next(notification);
      console.log(notification);
    });
  }
}
