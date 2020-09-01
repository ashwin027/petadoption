import { Component, OnInit, OnDestroy } from '@angular/core';
import { faPaw } from '@fortawesome/free-solid-svg-icons';
import {AuthService} from './services/auth.service'
import {MessagingService} from './services/messaging.service'
import { Subscription } from 'rxjs';
import {AppNotification} from './models/appNotification';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit, OnDestroy {
  faPaw = faPaw;
  notifications = new Array<AppNotification>();
  notificationSubscription: Subscription;
  notificationCounter = 0;

  constructor(public auth: AuthService, private messagingService: MessagingService){
  }

  ngOnInit(): void {
    this.notificationSubscription = this.messagingService.notificationSubject.subscribe((notification) =>{
      this.notificationCounter +=1;
      this.notifications.push(notification);
    });
  }

  ngOnDestroy(): void {
    if (this.notificationSubscription){
      this.notificationSubscription.unsubscribe();
    }
  }

  openNotifications(): void{
    this.notificationCounter = 0;
  }
}
