import { Component, OnInit } from '@angular/core';
import { AuthService } from '../services/auth.service';
import { UserProfile } from '../models/userProfile';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {
  profile: UserProfile;
  constructor(public auth: AuthService) { }

  ngOnInit(): void {
     this.auth.userProfile$.subscribe((prfl) =>{
      this.profile = prfl;
     });
  }

}
