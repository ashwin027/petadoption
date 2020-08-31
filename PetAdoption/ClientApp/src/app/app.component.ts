import { Component } from '@angular/core';
import { faPaw } from '@fortawesome/free-solid-svg-icons';
import {AuthService} from './services/auth.service'

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.scss']
})
export class AppComponent {
  faPaw = faPaw;
  title = 'petadoption';

  constructor(public auth: AuthService){

  }

}
